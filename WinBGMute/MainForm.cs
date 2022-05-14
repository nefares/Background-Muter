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

namespace WinBGMuter
{
    public partial class MainForm : Form
    {
        private VolumeMixer m_volumeMixer;
        private ForegroundProcessManager m_processManager;

        private string m_neverMuteList;
        
        // @todo untested whether this works
        private static string m_previous_fname = "wininit";

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

        // stores previous foreground process name for fallback in case of error
        private void RunMuter(int fpid, bool doMute = true)
        {
            // get a process PID list of processes with an audio channel
            int[] pids = m_volumeMixer.GetPIDs();

            Dictionary<int, (string, Process[])> dpids = new Dictionary<int, (string, Process[])>();

            // populate dictionary with KEY=<PID>, VALUE=tuple(<PROCESS_NAME>, <Process>)
            ProcessListListBox.Items.Clear();
            foreach (var pid in pids)
            {
                try
                {
                    Process proc = Process.GetProcessById(pid);
                    string pname = proc.ProcessName;

                    if (!NeverMuteListBox.Items.Contains(pname))
                    {
                        ProcessListListBox.Items.Add(pname);
                    }
                    Process[] procs_similar = Process.GetProcessesByName(pname);
                    dpids.Add(pid, (pname, procs_similar));
                }
                catch (Exception ex)
                {
                    LoggingEngine.LogLine("[-] PID access failed at: " + pid.ToString() + ex.ToString());
                }
            }

            if (!doMute)
                return;

            // get foreground process. If failed, revert to last process
            string fname = "";
            try
            {
                Process fproc = Process.GetProcessById(fpid);
                fname = fproc.ProcessName;
                m_previous_fname = fname;
            }
            catch(Exception ex)
            {
                fname = m_previous_fname;
                LoggingEngine.LogLine($"[-] Process name not found for pid {fpid}. Reverting to {fname}. {ex.ToString()}");
            }

            /*LoggingEngine.LogLine($"- [PIDs({dpids.Count}] - ");
            foreach (var pid in pids)
            {
                LoggingEngine.Log("#", Color.Brown);
                LoggingEngine.Log(dpids[pid].Item1, Color.Red);
                LoggingEngine.LogLine($" - {dpids[pid].Item2.Length} - {dpids[pid].Item2[0].MainWindowTitle}");
            }*/

            foreach (var pid in pids)
            {
                if (!dpids.ContainsKey(pid))
                {
                    LoggingEngine.LogLine($"[-] PID with audio channel {pid} not found in process list");
                    continue;

                }
                // unmute all foreground processes with the same name
                if (dpids[pid].Item1 == fname)
                {
                    LoggingEngine.Log($"[Unmuting] {dpids[pid].Item1}({pid}) ", Color.BlueViolet);
                    foreach (var fproc_similar in dpids[pid].Item2)
                    {
                        m_volumeMixer.SetApplicationMute(fproc_similar.Id, false);
                        LoggingEngine.Log(".");
                    }
                    LoggingEngine.Log("\n\r");
                }
                // mute all other processes
                else
                {
                    if (m_neverMuteList.Contains(dpids[pid].Item1))
                    {

                    }
                    else
                    {
                        m_volumeMixer.SetApplicationMute(pid, true);
                    }
                }
            }
        }

        private void MuterCallback(object state)
        {
            var result = m_processManager.GetJobThreadSafe();

            if (result.Item1)
            {
                RunMuter(result.Item2);
            }
        }

        private void SetDark(Control parent, bool dark)
        {
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
            }
        }

        public MainForm()
        {
            InitializeComponent();
        }

        ~MainForm()
        {

        }

        private void OnProcessExit(object sender, EventArgs e)
        {
            m_processManager.CleanUp();
            foreach (var pid in m_volumeMixer.GetPIDs())
            {
                m_volumeMixer.SetApplicationMute(pid, false);
            }
        }

        private void ReloadSettings(object sender, EventArgs e)
        {
            m_neverMuteList = Properties.Settings.Default.NeverMuteProcs;
            NeverMuteTextBox.Text = m_neverMuteList;

            LoggerCheckbox.Checked = Properties.Settings.Default.EnableLogging;
            ConsoleLogging.Checked = Properties.Settings.Default.EnableConsole;
            DarkModeCheckbox.Checked = Properties.Settings.Default.EnableDarkMode;

            LoggerCheckbox_CheckedChanged(sender, EventArgs.Empty);
            ConsoleLogging_CheckedChanged(sender, EventArgs.Empty);
            DarkModeCheckbox_CheckedChanged(sender, EventArgs.Empty);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoggingEngine.LogLine("Inializing...");

            m_volumeMixer = new VolumeMixer();
            m_processManager = new ForegroundProcessManager();

            ReloadSettings(sender, e);

            MuterTimer.Enabled = true;

            m_processManager.Init();

            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);

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
        }

        private void NeverMuteTextBox_TextChanged(object sender, EventArgs e)
        {
            PopulateNeverMuteListBox();
            m_neverMuteList = NeverMuteTextBox.Text;
            Properties.Settings.Default.NeverMuteProcs = m_neverMuteList;
            RunMuter(-1, false);
        }

        private void SaveChangesButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();

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

        private void ReloadAudioButton_Click(object sender, EventArgs e)
        {
            m_volumeMixer.UnloadAudio(true);
            m_volumeMixer.ReloadAudio(true);

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
","About",MessageBoxButtons.OK);

        }
    }
}