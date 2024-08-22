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


using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Timers;

namespace WinBGMuter
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// used for dark mode
        /// </summary>
        public enum DWMWINDOWATTRIBUTE
        {
            DWMWA_NCRENDERING_ENABLED,
            DWMWA_NCRENDERING_POLICY,
            DWMWA_TRANSITIONS_FORCEDISABLED,
            DWMWA_ALLOW_NCPAINT,
            DWMWA_CAPTION_BUTTON_BOUNDS,
            DWMWA_NONCLIENT_RTL_LAYOUT,
            DWMWA_FORCE_ICONIC_REPRESENTATION,
            DWMWA_FLIP3D_POLICY,
            DWMWA_EXTENDED_FRAME_BOUNDS,
            DWMWA_HAS_ICONIC_BITMAP,
            DWMWA_DISALLOW_PEEK,
            DWMWA_EXCLUDED_FROM_PEEK,
            DWMWA_CLOAK,
            DWMWA_CLOAKED,
            DWMWA_FREEZE_REPRESENTATION,
            DWMWA_PASSIVE_UPDATE_MODE,
            DWMWA_USE_HOSTBACKDROPBRUSH,
            DWMWA_USE_IMMERSIVE_DARK_MODE = 20,
            DWMWA_WINDOW_CORNER_PREFERENCE = 33,
            DWMWA_BORDER_COLOR,
            DWMWA_CAPTION_COLOR,
            DWMWA_TEXT_COLOR,
            DWMWA_VISIBLE_FRAME_BORDER_THICKNESS,
            DWMWA_SYSTEMBACKDROP_TYPE,
            DWMWA_LAST
        };

        // used for dark mode
        [DllImport("dwmapi.dll", PreserveSig = true)]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE attr, ref int attrValue, int attrSize);

        private VolumeMixer m_volumeMixer;
        private ForegroundProcessManager m_processManager;

        private string m_neverMuteList;
        private bool m_settingsChanged = false;
        private bool m_enableMiniStart = false;
        private bool m_enableDemo = false;
        private int m_errorCount = 0;
        private bool m_isMuteConditionBackground = true;

        // @todo untested whether this works
        private static string m_previous_fname = "wininit";
        private static int m_previous_fpid = -1;

        // keep alive timer @todo replace the Forms timer with the System.Timer
        private static System.Timers.Timer m_keepAliveTimer = new System.Timers.Timer(600000);


        private void InternalLog(object olog, object? ocolor = null, object? ofont = null)
        {
            string log = olog == null ? string.Empty : (string)(olog);
            Color? color = ocolor == null ? null : (Color)ocolor;
            Font? font = ofont == null ? null : (Font)ofont;

            if (this == null)
            {
                return;
            }

            try
            {
                //Invoke method to allow access outside of creator thread
                this.Invoke(() =>
                {
                    this.LogTextBox.SelectionStart = this.LogTextBox.TextLength;
                    this.LogTextBox.SelectionLength = 0;
                    this.LogTextBox.SelectionColor = color == null ? this.LogTextBox.SelectionColor : (Color)color;
                    this.LogTextBox.SelectionFont = font == null ? this.LogTextBox.SelectionFont : (Font)font;
                    this.LogTextBox.AppendText(log);
                    this.LogTextBox.SelectionColor = this.LogTextBox.ForeColor;
                    this.LogTextBox.ScrollToCaret();

                    this.StatusBox.Text = log;

                });
            }
            catch (Exception ex)
            {

            }
        }

        private void InternalLogLine(object olog, object? ocolor = null, object? ofont = null)
        {
            string log = olog == null ? string.Empty : (string)(olog);
            Color? color = ocolor == null ? null : (Color)ocolor;
            Font? font = ofont == null ? null : (Font)ofont;
            InternalLog(log + Environment.NewLine, color, font);
        }

        private void HandleError(Exception ex, object? data = null)
        {
            m_errorCount += 1;

            if (m_errorCount >= 30)
            {
                return;
            }

            int pid = (data is int) ? (int)data : -1;
            LoggingEngine.LogLine("[-] Process access failed for PID " + pid.ToString() + " @" + ex.Source, Color.Red);
            m_volumeMixer.ReloadAudio(true);


        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool IsIconic(IntPtr hWnd);
        // stores previous foreground process name for fallback in case of error
        private void RunMuter(int fpid, bool doMute = true)
        {
            Dictionary<int, (string, Process[])> audio_procs = new Dictionary<int, (string, Process[])>();

            // clear process list
            ProcessListListBox.Items.Clear();

            // get a process PID list of processes with an audio channel
            int[] audio_pids = m_volumeMixer.GetPIDs();

            // populate dictionary audio_procs for each PID in audio_pids with KEY=<PID>, VALUE=tuple(<PROCESS_NAME>, <Process>)
            foreach (var pid in audio_pids)
            {
                try
                {
                    Process proc = Process.GetProcessById(pid);
                    /*
                    if (proc.HasExited)
                    {
                        LoggingEngine.LogLine($"[!] PID with audio channel {pid} has exited! This will likely trigger an error");
                    }

                    */
                    string pname = proc.ProcessName;

                    //add proc name to ListBox if it will be muted
                    if (!NeverMuteListBox.Items.Contains(pname))
                    {
                        ProcessListListBox.Items.Add(pname);
                    }

                    //gather all processes of the same name as the process of the current pid (workaround for some programs)
                    Process[] procs_similar = Process.GetProcessesByName(pname);

                    //add all processes of the same name as pname to the audio_procs corresponding to PID
                    audio_procs.Add(pid, (pname, procs_similar));
                }
                catch (Exception ex)
                {
                    HandleError(ex, (object)pid);
                }
                finally
                {
                    if (!audio_procs.ContainsKey(pid))
                    {
                        LoggingEngine.LogLine($"[-] PID with audio channel {pid} not found in process list (most likely due to an error)");
                        //throw new Exception();
                        Process[] empty_procs = { };
                        audio_procs.Add(pid, ("", empty_procs));
                    }
                }
            }

            if (!doMute)
                return;

            // get foreground process object. If failed (e.g. process exited), revert to last process
            string fname = "";
            try
            {
                Process fproc = Process.GetProcessById(fpid);
                fname = fproc.ProcessName;
                m_previous_fname = fname;
                m_previous_fpid = fpid;
            }
            catch (Exception ex)
            {
                fname = m_previous_fname;
                LoggingEngine.LogLine($"[!] Process name not found for pid {fpid}. Reverting to {fname}. {ex.ToString()}", Color.Orange);
            }


            //Inline function to mute/unmute a list of processes
            Func<Process[], bool, int?, string> InlineMuteProcList = (procs, isMuted, additionalPID) =>
            {
                string log_output = "";

                if (additionalPID != null)
                {
                    m_volumeMixer.SetApplicationMute((int)additionalPID, isMuted);
                }
                foreach (var fproc_similar in procs)
                {
                    var fproc_similar_pid = fproc_similar.Id;
                    m_volumeMixer.SetApplicationMute(fproc_similar_pid, isMuted);
                    log_output += ".";
                }
                //log_output += "\r\n";
                return log_output;
            };

            string log_skipped = "";
            string log_muted = "";

            foreach (var item in audio_procs)
            {
                var audio_pid = item.Key;
                var audio_pname = item.Value.Item1;
                var audio_proc_list = item.Value.Item2;

                // check if this is the foreground process
                // if yes unmute all foreground processes with the same name
                if (audio_pname == fname)
                {
                    string log_output = InlineMuteProcList(audio_proc_list, false, audio_pid);
                    LoggingEngine.LogLine($"[+] Unmuting foreground process {audio_pname}({audio_pid}) {log_output} ", Color.BlueViolet);
                }
                // mute all other processes (with an audio channel), except  the ones on the neverMuteList
                else
                {
                    if (m_neverMuteList.Contains(audio_pname))
                    {
                        //LoggingEngine.LogLine($" [!] Process {audio_pname}({audio_pid}) skipped ", Color.BlueViolet);
                        log_skipped += audio_pname + ", ";
                    }
                    else
                    {
                        // if not on mute list and 
                        if (!m_isMuteConditionBackground)
                        {
                            // if minimize option AND iconic
                            // TODO: fix multi window muting
                            IntPtr handle = Process.GetProcessById(audio_pid).MainWindowHandle;//Error occurs for "Handle", not "MainWindowHandle"
                            if (!IsIconic(handle))
                            {
                                // if minimize option AND NOT minimized: SKIP
                                InlineMuteProcList(audio_proc_list, false, audio_pid);
                                log_skipped += "[M]" + audio_pname + ", ";
                            }
                            else
                            {
                                InlineMuteProcList(audio_proc_list, true, audio_pid);
                                log_muted += audio_pname + ", ";
                            }
                        }
                        else
                        {
                            //if mute condition is background and not on mute list
                            InlineMuteProcList(audio_proc_list, true, audio_pid);
                            log_muted += audio_pname + ", ";
                        }
                    }
                }
            }

            LoggingEngine.LogLine($"[+] Summary: skipped ({log_skipped}) and muted ({log_muted})");
        }


        private void ReloadMuter()
        {
            LoggingEngine.Log("[R]", Color.Aqua, null, LoggingEngine.LOG_LEVEL_TYPE.LOG_DEBUG);
            LoggingEngine.LOG_LEVEL_TYPE currentLogLevel = LoggingEngine.LogLevel;
            LoggingEngine.LogLevel = LoggingEngine.LOG_LEVEL_TYPE.LOG_NONE;
            RunMuter(Environment.ProcessId);
            LoggingEngine.LogLevel = currentLogLevel;

        }
        private void MuterCallback(object state)
        {
            var result = m_processManager.GetJobThreadSafe();

            if (result.Item1)
            {
                RunMuter(result.Item2);
            }

            //LoggingEngine.LogLine("Tick - " + result.ToString());
        }

        private void EnableAutoStart(bool isEnabled)
        {
            string autostartPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            string linkDir = autostartPath;
            string linkName = "Background Muter.lnk";
            string fullPath = Path.Combine(linkDir, linkName);
            string programPath = Application.ExecutablePath;
            string programArgs = "--startMinimized";

            if (File.Exists(fullPath))
            {
                FileInfo fileInfo = new FileInfo(fullPath);
                fileInfo.Delete();
            }

            if (isEnabled)
            {
                ShortcutManager.CreateShortcut(this.Text, programPath, linkName, linkDir, programArgs);
                LoggingEngine.LogLine($"Setting autostart @{linkDir} -> {linkName}");
            }

        }

        /// <summary>
        /// Recursively set dark mode for all underlaying controls by storing the original colors in the control's tags
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="dark"></param>
        private void SetDark(Control parent, bool dark)
        {
            // only works on main window
            int USE_DARK_MODE = dark ? 1 : 0;
            DwmSetWindowAttribute(parent.Handle, DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE, ref USE_DARK_MODE, sizeof(int));

            Color bgcolor;
            Color fgcolor;


            foreach (Control c in parent.Controls)
            {
                bgcolor = c.BackColor;
                fgcolor = c.ForeColor;

                (Color, Color) tag = (bgcolor, fgcolor);

                if ((c.Tag != null) && (!dark))
                {
                    tag = ((Color, Color))c.Tag;
                    bgcolor = tag.Item1;
                    fgcolor = tag.Item2;
                }

                if (c.Tag == null)
                {
                    c.Tag = (c.BackColor, c.ForeColor);
                }
                else
                {

                }

                if (dark)
                {
                    bgcolor = Color.FromArgb(25, 25, 25);
                    fgcolor = Color.White;
                }
                else if (c.Tag != null)
                {
                    fgcolor = tag.Item1;
                    fgcolor = tag.Item2;

                }

                c.BackColor = bgcolor;
                c.ForeColor = fgcolor;


                if (c.Controls.Count > 0)
                    SetDark(c, dark);

                parent.Refresh();

                // if main window, force redraw as refresh does not work
                if (parent.Parent == null)
                {
                    parent.Hide();
                    parent.Show();
                }
            }
        }

        public MainForm(string[] args)
        {
            if (args.Length != 0)
            {
                foreach (string arg in args)
                {
                    switch (arg)
                    {
                        case "--startMinimized":
                            m_enableMiniStart = true;
                            break;
                        case "--demo":
                            m_enableDemo = true;
                            break;
                        default:
                            MessageBox.Show($"Unknown argument {arg}");
                            break;
                    }
                }
            }
            InitializeComponent();
        }

        ~MainForm()
        {

        }

        private void OnProcessExit(object sender, EventArgs e)
        {
            try
            {
                m_processManager.CleanUp();
                foreach (var pid in m_volumeMixer.GetPIDs())
                {
                    m_volumeMixer.SetApplicationMute(pid, false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cleanup failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReloadSettings(object sender, EventArgs e)
        {
            m_neverMuteList = Properties.Settings.Default.NeverMuteProcs;
            NeverMuteTextBox.Text = m_neverMuteList;

            LoggerCheckbox.Checked = Properties.Settings.Default.EnableLogging;
            ConsoleLogging.Checked = Properties.Settings.Default.EnableConsole;
            DarkModeCheckbox.Checked = Properties.Settings.Default.EnableDarkMode;
            AutostartCheckbox.Checked = Properties.Settings.Default.EnableAutostart;

            if (Properties.Settings.Default.IsMuteConditionBackground == true)
            {
                BackGroundRadioButton.Checked = true;
                m_isMuteConditionBackground = true;
            }

            else
            {
                MinimizedRadioButton.Checked = true;
                m_isMuteConditionBackground = false;

            }

            LoggerCheckbox_CheckedChanged(sender, EventArgs.Empty);
            ConsoleLogging_CheckedChanged(sender, EventArgs.Empty);
            DarkModeCheckbox_CheckedChanged(sender, EventArgs.Empty);
            AutostartCheckbox_CheckedChanged(sender, EventArgs.Empty);


        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoggingEngine.LogLevel = LoggingEngine.LOG_LEVEL_TYPE.LOG_DEBUG;
            LoggingEngine.HasDateTime = true;
            LoggingEngine.LogLine("Initializing...");

            m_volumeMixer = new VolumeMixer();
            m_processManager = new ForegroundProcessManager();

            ReloadSettings(sender, e);

            MuterTimer.Enabled = true;

            m_processManager.Init();

            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);

            Properties.Settings.Default.PropertyChanged += Default_PropertyChanged;

            SaveChangesButton.Enabled = false;

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();

            if (assembly.Location.Length == 0)
            {
                MessageBox.Show("Assembly Location not detected. This may be due to a non-standard build process. Beware that this may break some features.");
            }

            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);



            this.Text += " - v" + fvi.ProductVersion;

            m_keepAliveTimer.Elapsed += KeepAliveTimer_Tick;
            m_keepAliveTimer.AutoReset = true;
            m_keepAliveTimer.Enabled = true;


        }

        private void Default_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            m_settingsChanged = true;
            this.SaveChangesButton.Enabled = true;
        }

        private void PopulateNeverMuteListBox()
        {
            string[] neverMuteList = m_neverMuteList.Split(',', StringSplitOptions.RemoveEmptyEntries);


            NeverMuteListBox.Items.Clear();

            foreach (string neverMuteEntry in neverMuteList)
            {
                NeverMuteListBox.Items.Add(neverMuteEntry);
            }


        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            TrayIcon.Visible = false;

            if (m_settingsChanged)
            {
                var res = MessageBox.Show("Settings changed. Would you like to save?", "Saving...", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    SaveChangesButton_Click(sender, e);
                }
            }

        }

        private void NeverMuteTextBox_TextChanged(object sender, EventArgs e)
        {
            PopulateNeverMuteListBox();
            m_neverMuteList = NeverMuteTextBox.Text;
            Properties.Settings.Default.NeverMuteProcs = m_neverMuteList;
            ReloadMuter();
        }

        private void SaveChangesButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            m_settingsChanged = false;
            this.SaveChangesButton.Enabled = false;

        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Minimized;
                Hide();
                TrayIcon.Visible = true;
                TrayIcon.ShowBalloonTip(2000);
            }
        }

        private void TrayIcon_DoubleClick(object sender, EventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void LoggerCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (LoggerCheckbox.Checked)
            {
                Properties.Settings.Default.EnableLogging = true;
                LogTextBox.Enabled = true;
                LogTextBox.Visible = true;
                LoggingEngine.Enabled = true;

                //this.Size = new Size(this.Size.Width, this.Size.Height + 100);

            }
            else
            {
                Properties.Settings.Default.EnableLogging = false;
                LogTextBox.Enabled = false;
                LogTextBox.Visible = false;
                LoggingEngine.Enabled = false;


                //this.Size = new Size(this.Size.Width, this.Size.Height - 100);

            }
        }

        private void MuterTimer_Tick(object sender, EventArgs e)
        {
            MuterCallback((sender, e));
        }

        private void CloseMenuTray_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OpenMenuTray_Click(object sender, EventArgs e)
        {
            TrayIcon_DoubleClick(sender, e);
        }

        private void ConsoleLogging_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.EnableConsole = ConsoleLogging.Checked;

            if (ConsoleLogging.Checked)
            {
                LoggingEngine.RestoreDefault();
            }
            else
            {
                LoggingEngine.SetEngine(InternalLog, InternalLogLine);
            }

        }

        private void DarkModeCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.EnableDarkMode = DarkModeCheckbox.Checked;

            if (DarkModeCheckbox.Checked)
                SetDark(this, true);
            else
                SetDark(this, false);

        }

        private void AutostartCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.EnableAutostart = AutostartCheckbox.Checked;

            if (AutostartCheckbox.Checked)
            {
                EnableAutoStart(true);
            }
            else
            {
                EnableAutoStart(false);
            }
        }

        private void ReloadAudioButton_Click(object sender, EventArgs e)
        {
            m_volumeMixer.UnloadAudio(true);
            m_volumeMixer.ReloadAudio(true);
            ReloadMuter();

        }

        private void ProcessToMuteButton_Click(object sender, EventArgs e)
        {

            try
            {
                NeverMuteTextBox.AppendText("," + ProcessListListBox.Items[ProcessListListBox.SelectedIndex]);
                NeverMuteTextBox_TextChanged(sender, EventArgs.Empty);

                if (ProcessListListBox.SelectedIndex != -1)
                    ProcessListListBox.Items.RemoveAt(ProcessListListBox.SelectedIndex);
            }
            catch (Exception ex)
            {

            }
        }

        private void MuteToProcessButton_Click(object sender, EventArgs e)
        {

            try
            {

                if (NeverMuteListBox.SelectedIndex != -1)
                    NeverMuteListBox.Items.RemoveAt(NeverMuteListBox.SelectedIndex);

                var newText = String.Empty;
                foreach (var item in NeverMuteListBox.Items)
                {
                    newText += item.ToString() + ",";
                }

                NeverMuteTextBox.Text = newText;


                NeverMuteTextBox_TextChanged(sender, EventArgs.Empty);
            }


            catch (Exception ex)
            {
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
            ReloadSettings(sender, e);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

            MessageBox.Show(@"                                                                      
Background Muter - Automatically mute background applications                  
Copyright(C) 2022  Nefares(nefares@protonmail.com) github.com / nefares       
                                                                              
This program is free software: you can redistribute it and / or modify        
it under the terms of the GNU General Public License as published by          
the Free Software Foundation, either version 3 of the License, or             
(at your option) any later version.                                           
                                                                              
                                                                              
This program is distributed in the hope that it will be useful,               
but WITHOUT ANY WARRANTY; without even the implied warranty of                
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the                 
GNU General Public License for more details.                                                                                                    
                                                                              
You should have received a copy of the GNU General Public License             
along with this program.If not, see < https://www.gnu.org/licenses/>          
", "About", MessageBoxButtons.OK);

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }


        private void BackGroundRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.IsMuteConditionBackground = true;
            m_isMuteConditionBackground = true;
            ReloadMuter();
        }

        private void MinimizedRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.IsMuteConditionBackground = false;
            m_isMuteConditionBackground = false;
            ReloadMuter();
        }

        private void AdvancedButton_MouseClick(object sender, MouseEventArgs e)
        {
            AdvancedMenuStrip.Show(AdvancedButton, new Point(e.X, e.Y));
        }

        private void KeepAliveTimer_Tick(object sender, EventArgs e)
        {
            LoggingEngine.Log("<Keep Alive>");
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (m_enableMiniStart)
            {
                this.WindowState = FormWindowState.Minimized;
                this.MainForm_Resize(sender, e);
                //if (!this.IsHandleCreated) CreateHandle();
            }
        }

        private void tableLayoutPanel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void EnableConsole_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.EnableConsole = ConsoleLogging.Checked;

            if (ConsoleLogging.Checked)
            {
                LoggingEngine.RestoreDefault();
            }
            else
            {
                LoggingEngine.SetEngine(InternalLog, InternalLogLine);
            }
        }
    }
}