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
        //WinAPI to translate PID from hWND
        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern UInt32 GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        //WinAPI to set a hook for when a window size changes
        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern IntPtr SetWinEventHook(UInt32 eventMin,
            UInt32 eventMax,
            IntPtr hmodWinEventProc,
            WinEventProcDelegate pfnWinEventProc,
            UInt32 idProcess,
            UInt32 idThread,
            UInt32 dwFlags);

        //WinAPI to unhook win event
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

        private int m_lastForegroundId = -1;

        public (bool, int) GetJobThreadSafe()
        {
            int pid;
            bool success;

            /*
             * piece of code to output the contents of the stack
             * 
                string output = "";
                foreach (int i_pid in m_JobStack)
                {
                    output += $"{i_pid} - ";

                }
                if (!m_JobStack.IsEmpty)
                        LoggingEngine.LogLine("" + output);
            */

            if (m_JobStack.TryPop(out pid))
            {

                if (!m_JobStack.IsEmpty)
                {
                    //LoggingEngine.LogLine("[!] discrading previous foreground processes ");
                }
                m_JobStack.Clear();

                
                success = true;
            }
            else
            {
                pid = -1;
                success = false;
            }

            /* EXPERIEMTNAL
             * This will evaluate if (in rare cases) the current foreground PID from the event handler (WinEventProc) is equal to the PID provided by 
             * a new polling function (PollForegroundProcessId). 
             * 
             TODO: in the future, remove the whole WinEventProc (and the related stack) and replace them with the following piece of code which does the same job more reliably */
            int poll_fpid = PollForegroundProcessId();

            if (m_lastForegroundId != poll_fpid)
            {
                success = true;
                m_lastForegroundId = poll_fpid;
            }
            else
            {
                success = false;
                return (success, poll_fpid);

            }


            string poll_fname = "";
            string fname = "";
            if (poll_fpid != pid)
            {
                try
                {
                    poll_fname = Process.GetProcessById(poll_fpid).ProcessName;

                }
                catch (Exception e)
                {
                    poll_fname = "<unknown>";
                }

                try
                {
                    fname = Process.GetProcessById(pid).ProcessName;

                }
                catch (Exception e)
                {
                    fname = "<unknown>";
                }

                if (pid != -1)
                    LoggingEngine.LogLine($"[!] Assertion failed for {fname}({pid}) <> (polled){poll_fname}({poll_fpid}) ", Color.Yellow);

                //overwriting pid!
                pid = poll_fpid;

               


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

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        public int PollForegroundProcessId()
        {

            uint processID = 0;
            IntPtr hWnd = GetForegroundWindow(); // Get foreground window handle
            uint threadID = GetWindowThreadProcessId(hWnd, out processID); // Get PID from window handle
            //Process fgProc = Process.GetProcessById(Convert.ToInt32(processID)); // Get it as a C# obj.
            // NOTE: In some rare cases ProcessID will be NULL. Handle this how you want. 
            return Convert.ToInt32(processID);
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
            uint testpid = 0;
            GetWindowThreadProcessId(hwnd, out testpid);

            int ftestpidpid = (int)testpid;

            if (ev == EVENT_SYSTEM_FOREGROUND &&
                idObject == OBJID_WINDOW &&
                idChild == CHILDID_SELF)
            {
                uint pid = 0;
                GetWindowThreadProcessId(hwnd, out pid);

                int fpid = (int)pid;
                Process? foreground = null;
                try
                {
                    foreground = Process.GetProcessById(fpid);
                }
                catch(Exception e)
                {
                    LoggingEngine.LogLine($"[-] Foreground Window {hwnd} and/or process {fpid} do not exist => {e.Message}", Color.Red);
                }
                finally
                {
          
                }

                string pname = String.Empty;

                if (foreground != null)
                {
                    pname = foreground.ProcessName;
                }

                LoggingEngine.LogLine($"[+] Foreground process changed to {pname} - {fpid}", Color.Cyan);

                if ((fpid != 0) && (pname != String.Empty))
                {
                    m_JobStack.Push(fpid);
                }
            }
        }
    }
}
