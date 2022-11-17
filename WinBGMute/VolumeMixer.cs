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
    public class VolumeMixer
    {

        public VolumeMixer()
        {
            ReloadAudio(true);
        }

        ~VolumeMixer()
        {
            UnloadAudio(true);
        }

        public float? GetApplicationVolume(int pid)
        {
            ISimpleAudioVolume volume = GetVolumeObject(pid);
            if (volume == null)
                return null;

            float level;
            volume.GetMasterVolume(out level);
            Marshal.ReleaseComObject(volume);
            return level * 100;
        }

        public bool? GetApplicationMute(int pid)
        {
            ISimpleAudioVolume volume = GetVolumeObject(pid);
            if (volume == null)
                return null;

            bool mute;
            volume.GetMute(out mute);
            Marshal.ReleaseComObject(volume);
            return mute;
        }

        public void SetApplicationVolume(int pid, float level)
        {
            ISimpleAudioVolume volume = GetVolumeObject(pid);
            if (volume == null)
                return;

            Guid guid = Guid.Empty;
            volume.SetMasterVolume(level / 100, ref guid);
            Marshal.ReleaseComObject(volume);
        }

        public void SetApplicationMute(int pid, bool mute)
        {
            ISimpleAudioVolume volume = GetVolumeObject(pid);
            if (volume == null)
                return;

            Guid guid = Guid.Empty;
            volume.SetMute(mute, ref guid);
            Marshal.ReleaseComObject(volume);
        }

         struct _AudioDevice
        {
            public bool RanOnce;        
            // get the speakers (1st render + multimedia) device
            public IMMDeviceEnumerator deviceEnumerator;
            public IMMDevice speakers;
            public Guid IID_IAudioSessionManager2;
            public object o;
            // enumerate sessions for on this device
            public IAudioSessionEnumerator sessionEnumerator;
            public int count;
            public IAudioSessionManager2 mgr;
            public Dictionary<int, ISimpleAudioVolume?> volumeSessionList;
        };

        private _AudioDevice AudioDevice;


        public void UnloadAudio(bool unloadDevice = false)
        {
            if (unloadDevice)
            {
                Marshal.ReleaseComObject(AudioDevice.sessionEnumerator);
                Marshal.ReleaseComObject(AudioDevice.mgr);
                Marshal.ReleaseComObject(AudioDevice.speakers);
                Marshal.ReleaseComObject(AudioDevice.deviceEnumerator);
            }


            foreach (var vs in AudioDevice.volumeSessionList)
            {
                IAudioSessionControl2 ctl = vs.Value as IAudioSessionControl2;
                Marshal.ReleaseComObject(ctl);

            }
        }

      /*  public bool DiscardPID(int pid)
        {
            LoggingEngine.LogLine($"[>] discarding pid {pid}");
            return AudioDevice.volumeSessionList.Remove(pid);
        }
      */
        public int[] GetPIDs()
        {
            ReloadAudio(false);
            return AudioDevice.volumeSessionList.Keys.ToArray<int>();
        }
        public void ReloadAudio(bool reloadDevice = false)
        {
            try
            {
                if (reloadDevice)
                {
                    LoggingEngine.LogLine("[!] Reloading audio...", Color.Orange);

                    AudioDevice.volumeSessionList = new Dictionary<int, ISimpleAudioVolume?>();

                    AudioDevice.deviceEnumerator = (IMMDeviceEnumerator)(new MMDeviceEnumerator());
                    //not efficient
                    AudioDevice.deviceEnumerator.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia, out AudioDevice.speakers);


                    // activate the session manager. we need the enumerator
                    AudioDevice.IID_IAudioSessionManager2 = typeof(IAudioSessionManager2).GUID;

                    AudioDevice.speakers.Activate(ref AudioDevice.IID_IAudioSessionManager2, 0, IntPtr.Zero, out AudioDevice.o);
                    AudioDevice.mgr = (IAudioSessionManager2)AudioDevice.o;
                }

                //code takes too long
                AudioDevice.mgr.GetSessionEnumerator(out AudioDevice.sessionEnumerator);
                AudioDevice.sessionEnumerator.GetCount(out AudioDevice.count);



                AudioDevice.volumeSessionList.Clear();
                ISimpleAudioVolume volumeControl = null;
                for (int i = 0; i < AudioDevice.count; i++)
                {
                    IAudioSessionControl2 ctl;
                    AudioDevice.sessionEnumerator.GetSession(i, out ctl);
                    int cpid;
                    ctl.GetProcessId(out cpid);

                    volumeControl = ctl as ISimpleAudioVolume;

                    if (AudioDevice.volumeSessionList.ContainsKey(cpid))
                    {
                        AudioDevice.volumeSessionList[cpid] = volumeControl;

                        /*
                        LoggingEngine.LogLine($"[!] PID {cpid} Exists ");
                        AudioDevice.volumeSessionList.Remove(cpid);
                        */
                    }
                    else
                    {
                        AudioDevice.volumeSessionList.Add(cpid, volumeControl);

                    }
                }
            } catch (Exception ex)
            {
                if (MessageBox.Show($"Audio Initiatlization failed: {ex.Message}","Error",MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                {
                    ReloadAudio(reloadDevice);
                }
                else
                {
                    System.Windows.Forms.Application.Exit();
                }
            }

        }
        private ISimpleAudioVolume GetVolumeObject(int pid)
        {
            ReloadAudio();
            if (!AudioDevice.volumeSessionList.ContainsKey(pid))
            {
                return null;
            }
            return AudioDevice.volumeSessionList[pid];
        }
    }

    [ComImport]
    [Guid("BCDE0395-E52F-467C-8E3D-C4579291692E")]
    internal class MMDeviceEnumerator
    {
    }

    internal enum EDataFlow
    {
        eRender,
        eCapture,
        eAll,
        EDataFlow_enum_count
    }

    internal enum ERole
    {
        eConsole,
        eMultimedia,
        eCommunications,
        ERole_enum_count
    }

    [Guid("A95664D2-9614-4F35-A746-DE8DB63617E6"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMMDeviceEnumerator
    {
        int NotImpl1();

        [PreserveSig]
        int GetDefaultAudioEndpoint(EDataFlow dataFlow, ERole role, out IMMDevice ppDevice);

        // the rest is not implemented
    }

    [Guid("D666063F-1587-4E43-81F1-B948E807363F"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMMDevice
    {
        [PreserveSig]
        int Activate(ref Guid iid, int dwClsCtx, IntPtr pActivationParams, [MarshalAs(UnmanagedType.IUnknown)] out object ppInterface);

        // the rest is not implemented
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
        int NotImpl0();

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
