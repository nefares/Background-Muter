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



using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.InteropServices;




namespace WinBGMuter
{
    internal class ForegroundProcessManager
    {
        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern UInt32 GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern IntPtr SetWinEventHook(UInt32 eventMin,
            UInt32 eventMax,
            IntPtr hmodWinEventProc,
            WinEventProcDelegate pfnWinEventProc,
            UInt32 idProcess,
            UInt32 idThread,
            UInt32 dwFlags);

        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool UnhookWinEvent(IntPtr hWinEventHook);

        private delegate void WinEventProcDelegate(
          IntPtr hWinEventHook,
          UInt32 ev,
          IntPtr hwnd,
          Int32 idObject,
          Int32 idChild,
          UInt32 dwEventThread,
          UInt32 dwmsEventTime
          );

        private const UInt32 EVENT_SYSTEM_FOREGROUND = 0x0003;
        private const Int32 OBJID_WINDOW = 0x00000000;
        private const Int32 CHILDID_SELF = 0;

        private IntPtr m_hWinEventHook;
        private WinEventProcDelegate m_winEventProc;
        private static ConcurrentStack<int> m_JobStack = new ConcurrentStack<int>();

        public (bool, int) GetJobThreadSafe()
        {
            int pid;
            bool success;

            if (m_JobStack.TryPop(out pid))
            {
                m_JobStack.Clear();
                success = true;
            }
            else
            {
                pid = -1;
                success = false;
            }
            return (success, pid);

        }

        public void Init()
        {
            m_winEventProc = new WinEventProcDelegate(WinEventProc);

            m_hWinEventHook = SetWinEventHook(
                0x0003, 0x0003,
                IntPtr.Zero, m_winEventProc, 0, 0,
                0x0000);
        }

        public void CleanUp()
        {
            UnhookWinEvent(m_hWinEventHook);

        }

        private void WinEventProc(
          IntPtr hWinEventHook,
          UInt32 ev,
          IntPtr hwnd,
          Int32 idObject,
          Int32 idChild,
          UInt32 dwEventThread,
          UInt32 dwmsEventTime

          )
        {
            if (ev == EVENT_SYSTEM_FOREGROUND &&
                idObject == OBJID_WINDOW &&
                idChild == CHILDID_SELF)
            {
                uint pid = 0;
                GetWindowThreadProcessId(hwnd, out pid);

                int fpid = (int)pid;
                Process foreground = Process.GetProcessById(fpid);
                var pname = foreground.ProcessName;
                LoggingEngine.LogLine($"> Foreground Window Changed at {hwnd} {fpid} {pname}", Color.Blue);

                if ((fpid != 0) && (pname != String.Empty))
                {
                    m_JobStack.Push(fpid);
                }
            }
        }
    }
}
