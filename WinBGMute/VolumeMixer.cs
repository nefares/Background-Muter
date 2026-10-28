/* Modified from original code by Simon Mourier and Anders Carstensen
 https://stackoverflow.com/questions/20938934/controlling-applications-volume-by-process-id */

/*
 *  Background Muter - Automatically mute background applications
 *  Copyright (C) 2022  Nefares (nefares@protonmail.com) github.com/nefares
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using System.Runtime.InteropServices;

namespace WinBGMuter
{
    /// <summary>
    /// Controls per-application audio sessions.
    ///
    /// Sessions are tracked on EVERY active render device, not only the
    /// default one. When Bluetooth headphones connect/disconnect (or the default
    /// device changes for any other reason), applications move their audio to a
    /// new session on a different device. Tracking a single device made the
    /// muter lose control of those apps until "Reload Audio" was pressed.
    ///
    /// Windows device-change notifications mark the device list dirty so it is
    /// rebuilt automatically on the next call, and <see cref="ConsumeDeviceChange"/>
    /// lets the UI re-apply muting after a switch.
    /// </summary>
    public class VolumeMixer
    {
        private const int DEVICE_STATE_ACTIVE = 0x1;
        private const int AUDIO_SESSION_STATE_EXPIRED = 2;

        private class AudioDeviceEntry
        {
            public string Id = "";
            public IMMDevice? Device;
            public IAudioSessionManager2? Manager;
        }

        private IMMDeviceEnumerator? m_deviceEnumerator;
        private DeviceNotificationClient? m_notificationClient;
        private readonly List<AudioDeviceEntry> m_devices = new List<AudioDeviceEntry>();

        // A process can own several sessions (several streams, or one per device).
        private readonly Dictionary<int, List<ISimpleAudioVolume>> m_sessions = new Dictionary<int, List<ISimpleAudioVolume>>();

        // Set from the Windows notification thread; read on the caller's thread.
        private volatile bool m_devicesDirty = true;
        private volatile bool m_deviceChangePending = false;

        public VolumeMixer()
        {
            ReloadAudio(true);
        }

        ~VolumeMixer()
        {
            UnloadAudio(true);
        }

        /// <summary>
        /// Returns true once after Windows reports an audio device change
        /// (device added/removed/enabled/disabled or default device switched).
        /// </summary>
        public bool ConsumeDeviceChange()
        {
            if (!m_deviceChangePending)
                return false;
            m_deviceChangePending = false;
            return true;
        }

        internal void NotifyDeviceChange()
        {
            m_devicesDirty = true;
            m_deviceChangePending = true;
        }

        public float? GetApplicationVolume(int pid)
        {
            var volumes = GetVolumeObjects(pid);
            if (volumes == null)
                return null;

            foreach (var volume in volumes)
            {
                if (volume.GetMasterVolume(out float level) >= 0)
                    return level * 100;
            }
            return null;
        }

        public bool? GetApplicationMute(int pid)
        {
            var volumes = GetVolumeObjects(pid);
            if (volumes == null)
                return null;

            bool? result = null;
            foreach (var volume in volumes)
            {
                if (volume.GetMute(out bool mute) >= 0)
                {
                    // report muted only if every session is muted
                    result = (result ?? true) && mute;
                }
            }
            return result;
        }

        public void SetApplicationVolume(int pid, float level)
        {
            var volumes = GetVolumeObjects(pid);
            if (volumes == null)
                return;

            foreach (var volume in volumes)
            {
                Guid guid = Guid.Empty;
                volume.SetMasterVolume(level / 100, ref guid);
            }
        }

        public void SetApplicationMute(int pid, bool mute)
        {
            var volumes = GetVolumeObjects(pid);
            if (volumes == null)
                return;

            foreach (var volume in volumes)
            {
                Guid guid = Guid.Empty;
                volume.SetMute(mute, ref guid);
            }
            //LoggingEngine.Log((mute ? "M+" : "M-") + pid.ToString());
        }

        public int[] GetPIDs()
        {
            ReloadAudio(false);
            return m_sessions.Keys.ToArray<int>();
        }

        public void UnloadAudio(bool unloadDevice = false)
        {
            ReleaseSessions();

            if (unloadDevice)
            {
                ReleaseDevices();

                if (m_deviceEnumerator is not null)
                {
                    if (m_notificationClient is not null)
                    {
                        try { m_deviceEnumerator.UnregisterEndpointNotificationCallback(m_notificationClient); } catch { }
                        m_notificationClient = null;
                    }
                    try { Marshal.ReleaseComObject(m_deviceEnumerator); } catch { }
                    m_deviceEnumerator = null;
                }
                m_devicesDirty = true;
            }
        }

        public void ReloadAudio(bool reloadDevice = false)
        {
            try
            {
                if (reloadDevice)
                {
                    UnloadAudio(true);
                    LoggingEngine.LogLine("[!] Reloading audio...", Color.Orange);
                }

                if (m_deviceEnumerator is null)
                {
                    m_deviceEnumerator = (IMMDeviceEnumerator)(new MMDeviceEnumerator());
                    m_notificationClient = new DeviceNotificationClient(this);
                    int hr = m_deviceEnumerator.RegisterEndpointNotificationCallback(m_notificationClient);
                    if (hr < 0)
                    {
                        // not fatal: we still rebuild devices on errors / manual reload
                        m_notificationClient = null;
                    }
                    m_devicesDirty = true;
                }

                if (m_devicesDirty)
                {
                    m_devicesDirty = false;
                    RebuildDevices();
                }

                ReleaseSessions();

                bool deviceFailed = false;
                foreach (var dev in m_devices)
                {
                    if (!EnumerateSessions(dev))
                        deviceFailed = true;
                }

                // A device that failed to enumerate was most likely just removed
                // (e.g. Bluetooth disconnect). Rebuild the list on the next call.
                if (deviceFailed)
                    m_devicesDirty = true;
            }
            catch (Exception ex)
            {
                var res = MessageBox.Show($"Audio Initiatlization failed: {ex.Message}", "Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
                if (res == DialogResult.Retry)
                {
                    Application.Restart();
                    Environment.Exit(0);
                }
                else if (res == DialogResult.Ignore)
                {
                    UnloadAudio(true);
                }
                else
                {
                    System.Windows.Forms.Application.Exit();
                }
            }
        }

        private void RebuildDevices()
        {
            ReleaseSessions();
            ReleaseDevices();

            if (m_deviceEnumerator is null)
                return;

            int hr = m_deviceEnumerator.EnumAudioEndpoints(EDataFlow.eRender, DEVICE_STATE_ACTIVE, out IMMDeviceCollection collection);
            if (hr < 0 || collection is null)
                throw new Exception($"Could not enumerate audio devices (0x{hr:X8})");

            try
            {
                collection.GetCount(out int count);
                Guid iid = typeof(IAudioSessionManager2).GUID;

                for (int i = 0; i < count; i++)
                {
                    if (collection.Item(i, out IMMDevice device) < 0 || device is null)
                        continue;

                    string id = "";
                    try { device.GetId(out id); } catch { }

                    hr = device.Activate(ref iid, 0x17 /* CLSCTX_ALL */, IntPtr.Zero, out object o);
                    if (hr < 0 || o is not IAudioSessionManager2 mgr)
                    {
                        try { Marshal.ReleaseComObject(device); } catch { }
                        continue;
                    }

                    m_devices.Add(new AudioDeviceEntry { Id = id, Device = device, Manager = mgr });
                }
            }
            finally
            {
                try { Marshal.ReleaseComObject(collection); } catch { }
            }

            LoggingEngine.LogLine($"[!] Tracking audio sessions on {m_devices.Count} output device(s)", Color.Orange);
        }

        private bool EnumerateSessions(AudioDeviceEntry dev)
        {
            if (dev.Manager is null)
                return false;

            IAudioSessionEnumerator? sessionEnumerator = null;
            try
            {
                if (dev.Manager.GetSessionEnumerator(out sessionEnumerator) < 0 || sessionEnumerator is null)
                    return false;
                if (sessionEnumerator.GetCount(out int count) < 0)
                    return false;

                for (int i = 0; i < count; i++)
                {
                    if (sessionEnumerator.GetSession(i, out IAudioSessionControl2 ctl) < 0 || ctl is null)
                        continue;

                    // Skip expired sessions: they belong to streams/processes that have
                    // already ended and can't be muted, but Windows may keep listing them.
                    bool expired = ctl.GetState(out int state) >= 0 && state == AUDIO_SESSION_STATE_EXPIRED;

                    if (expired || ctl.GetProcessId(out int cpid) < 0 || ctl is not ISimpleAudioVolume volume)
                    {
                        try { Marshal.ReleaseComObject(ctl); } catch { }
                        continue;
                    }

                    if (!m_sessions.TryGetValue(cpid, out var list))
                    {
                        list = new List<ISimpleAudioVolume>();
                        m_sessions.Add(cpid, list);
                    }
                    list.Add(volume);
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (sessionEnumerator is not null)
                {
                    try { Marshal.ReleaseComObject(sessionEnumerator); } catch { }
                }
            }
        }

        private void ReleaseSessions()
        {
            foreach (var list in m_sessions.Values)
            {
                foreach (var volume in list)
                {
                    try { Marshal.ReleaseComObject(volume); } catch { }
                }
            }
            m_sessions.Clear();
        }

        private void ReleaseDevices()
        {
            foreach (var dev in m_devices)
            {
                if (dev.Manager is not null)
                {
                    try { Marshal.ReleaseComObject(dev.Manager); } catch { }
                }
                if (dev.Device is not null)
                {
                    try { Marshal.ReleaseComObject(dev.Device); } catch { }
                }
            }
            m_devices.Clear();
        }

        private List<ISimpleAudioVolume>? GetVolumeObjects(int pid)
        {
            ReloadAudio();
            return m_sessions.TryGetValue(pid, out var list) && list.Count > 0 ? list : null;
        }
    }

    /// <summary>
    /// Receives audio device notifications from Windows (on a Windows-owned thread).
    /// Only sets flags; all real work happens on the muter's own thread.
    /// </summary>
    [ComVisible(true)]
    public sealed class DeviceNotificationClient : IMMNotificationClient
    {
        private readonly WeakReference<VolumeMixer> m_owner;

        internal DeviceNotificationClient(VolumeMixer owner)
        {
            m_owner = new WeakReference<VolumeMixer>(owner);
        }

        private void Signal()
        {
            if (m_owner.TryGetTarget(out var owner))
                owner.NotifyDeviceChange();
        }

        public int OnDeviceStateChanged(string deviceId, int newState) { Signal(); return 0; }
        public int OnDeviceAdded(string deviceId) { Signal(); return 0; }
        public int OnDeviceRemoved(string deviceId) { Signal(); return 0; }
        public int OnDefaultDeviceChanged(EDataFlow flow, ERole role, string defaultDeviceId)
        {
            if (flow == EDataFlow.eRender) Signal();
            return 0;
        }
        public int OnPropertyValueChanged(string deviceId, PROPERTYKEY key) { return 0; }
    }

    [ComImport]
    [Guid("BCDE0395-E52F-467C-8E3D-C4579291692E")]
    internal class MMDeviceEnumerator
    {
    }

    public enum EDataFlow
    {
        eRender,
        eCapture,
        eAll,
        EDataFlow_enum_count
    }

    public enum ERole
    {
        eConsole,
        eMultimedia,
        eCommunications,
        ERole_enum_count
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PROPERTYKEY
    {
        public Guid fmtid;
        public int pid;
    }

    [Guid("A95664D2-9614-4F35-A746-DE8DB63617E6"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMMDeviceEnumerator
    {
        [PreserveSig]
        int EnumAudioEndpoints(EDataFlow dataFlow, int dwStateMask, out IMMDeviceCollection ppDevices);

        [PreserveSig]
        int GetDefaultAudioEndpoint(EDataFlow dataFlow, ERole role, out IMMDevice ppDevice);

        [PreserveSig]
        int GetDevice([MarshalAs(UnmanagedType.LPWStr)] string pwstrId, out IMMDevice ppDevice);

        [PreserveSig]
        int RegisterEndpointNotificationCallback(IMMNotificationClient pClient);

        [PreserveSig]
        int UnregisterEndpointNotificationCallback(IMMNotificationClient pClient);
    }

    [Guid("0BD7A1BE-7A1A-44DB-8397-CC5392387B5E"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMMDeviceCollection
    {
        [PreserveSig]
        int GetCount(out int pcDevices);

        [PreserveSig]
        int Item(int nDevice, out IMMDevice ppDevice);
    }

    [Guid("D666063F-1587-4E43-81F1-B948E807363F"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMMDevice
    {
        [PreserveSig]
        int Activate(ref Guid iid, int dwClsCtx, IntPtr pActivationParams, [MarshalAs(UnmanagedType.IUnknown)] out object ppInterface);

        [PreserveSig]
        int OpenPropertyStore(int stgmAccess, IntPtr ppProperties); // not used

        [PreserveSig]
        int GetId([MarshalAs(UnmanagedType.LPWStr)] out string ppstrId);

        [PreserveSig]
        int GetState(out int pdwState);
    }

    [ComImport, ComVisible(true), Guid("7991EEC9-7E89-4D85-8390-6C703CEC60C0"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMMNotificationClient
    {
        [PreserveSig]
        int OnDeviceStateChanged([MarshalAs(UnmanagedType.LPWStr)] string pwstrDeviceId, int dwNewState);

        [PreserveSig]
        int OnDeviceAdded([MarshalAs(UnmanagedType.LPWStr)] string pwstrDeviceId);

        [PreserveSig]
        int OnDeviceRemoved([MarshalAs(UnmanagedType.LPWStr)] string pwstrDeviceId);

        [PreserveSig]
        int OnDefaultDeviceChanged(EDataFlow flow, ERole role, [MarshalAs(UnmanagedType.LPWStr)] string pwstrDefaultDeviceId);

        [PreserveSig]
        int OnPropertyValueChanged([MarshalAs(UnmanagedType.LPWStr)] string pwstrDeviceId, PROPERTYKEY key);
    }

    [Guid("77AA99A0-1BD6-484F-8BC7-2C654C9A9B6F"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IAudioSessionManager2
    {
        int NotImpl1();
        int NotImpl2();

        [PreserveSig]
        int GetSessionEnumerator(out IAudioSessionEnumerator SessionEnum);

        // the rest is not implemented
    }

    [Guid("E2F5BB11-0570-40CA-ACDD-3AA01277DEE8"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IAudioSessionEnumerator
    {
        [PreserveSig]
        int GetCount(out int SessionCount);

        [PreserveSig]
        int GetSession(int SessionCount, out IAudioSessionControl2 Session);
    }

    [Guid("87CE5498-68D6-44E5-9215-6DA47EF883D8"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface ISimpleAudioVolume
    {
        [PreserveSig]
        int SetMasterVolume(float fLevel, ref Guid EventContext);

        [PreserveSig]
        int GetMasterVolume(out float pfLevel);

        [PreserveSig]
        int SetMute(bool bMute, ref Guid EventContext);

        [PreserveSig]
        int GetMute(out bool pbMute);
    }

    [Guid("bfb7ff88-7239-4fc9-8fa2-07c950be9c6d"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IAudioSessionControl2
    {
        // IAudioSessionControl
        [PreserveSig]
        int GetState(out int pRetVal);

        [PreserveSig]
        int GetDisplayName([MarshalAs(UnmanagedType.LPWStr)] out string pRetVal);

        [PreserveSig]
        int SetDisplayName([MarshalAs(UnmanagedType.LPWStr)] string Value, [MarshalAs(UnmanagedType.LPStruct)] Guid EventContext);

        [PreserveSig]
        int GetIconPath([MarshalAs(UnmanagedType.LPWStr)] out string pRetVal);

        [PreserveSig]
        int SetIconPath([MarshalAs(UnmanagedType.LPWStr)] string Value, [MarshalAs(UnmanagedType.LPStruct)] Guid EventContext);

        [PreserveSig]
        int GetGroupingParam(out Guid pRetVal);

        [PreserveSig]
        int SetGroupingParam([MarshalAs(UnmanagedType.LPStruct)] Guid Override, [MarshalAs(UnmanagedType.LPStruct)] Guid EventContext);

        [PreserveSig]
        int NotImpl1();

        [PreserveSig]
        int NotImpl2();

        // IAudioSessionControl2
        [PreserveSig]
        int GetSessionIdentifier([MarshalAs(UnmanagedType.LPWStr)] out string pRetVal);

        [PreserveSig]
        int GetSessionInstanceIdentifier([MarshalAs(UnmanagedType.LPWStr)] out string pRetVal);

        [PreserveSig]
        int GetProcessId(out int pRetVal);

        [PreserveSig]
        int IsSystemSoundsSession();

        [PreserveSig]
        int SetDuckingPreference(bool optOut);
    }
}
