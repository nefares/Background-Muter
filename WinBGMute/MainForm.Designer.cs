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


namespace WinBGMuter
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.LogTextBox = new System.Windows.Forms.RichTextBox();
            this.TopTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.button2 = new System.Windows.Forms.Button();
            this.ReloadAudioButton = new System.Windows.Forms.Button();
            this.NeverMuteTextBox = new System.Windows.Forms.TextBox();
            this.settingsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.DarkModeCheckbox = new System.Windows.Forms.CheckBox();
            this.LoggerCheckbox = new System.Windows.Forms.CheckBox();
            this.ConsoleLogging = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.NeverMuteListBox = new System.Windows.Forms.ListBox();
            this.ProcessListListBox = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.ProcessToMuteButton = new System.Windows.Forms.Button();
            this.MuteToProcessButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.LastLogLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusBox = new System.Windows.Forms.ToolStripStatusLabel();
            this.TrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.TrayContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.OpenMenuTray = new System.Windows.Forms.ToolStripMenuItem();
            this.SeparatorMenuTray = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseMenuTray = new System.Windows.Forms.ToolStripMenuItem();
            this.MuterTimer = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.TopTableLayout.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.settingsBindingSource)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.TrayContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Controls.Add(this.statusStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(427, 327);
            this.panel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.LogTextBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.TopTableLayout, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(427, 305);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // LogTextBox
            // 
            this.LogTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LogTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogTextBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LogTextBox.Location = new System.Drawing.Point(3, 246);
            this.LogTextBox.Name = "LogTextBox";
            this.LogTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.LogTextBox.Size = new System.Drawing.Size(421, 59);
            this.LogTextBox.TabIndex = 0;
            this.LogTextBox.Text = "";
            // 
            // TopTableLayout
            // 
            this.TopTableLayout.ColumnCount = 2;
            this.TopTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.74194F));
            this.TopTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67.25806F));
            this.TopTableLayout.Controls.Add(this.groupBox1, 0, 0);
            this.TopTableLayout.Controls.Add(this.groupBox2, 1, 0);
            this.TopTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TopTableLayout.Location = new System.Drawing.Point(3, 3);
            this.TopTableLayout.Name = "TopTableLayout";
            this.TopTableLayout.RowCount = 1;
            this.TopTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TopTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TopTableLayout.Size = new System.Drawing.Size(421, 237);
            this.TopTableLayout.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(131, 231);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.button2, 0, 7);
            this.tableLayoutPanel3.Controls.Add(this.ReloadAudioButton, 0, 5);
            this.tableLayoutPanel3.Controls.Add(this.NeverMuteTextBox, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.button1, 0, 6);
            this.tableLayoutPanel3.Controls.Add(this.DarkModeCheckbox, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.LoggerCheckbox, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.ConsoleLogging, 0, 4);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 8;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.655502F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.22488F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.52632F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.91866F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.00478F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.8756F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.8756F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(125, 209);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.Location = new System.Drawing.Point(3, 185);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(119, 21);
            this.button2.TabIndex = 1;
            this.button2.Text = "Restore Defaults";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ReloadAudioButton
            // 
            this.ReloadAudioButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReloadAudioButton.Location = new System.Drawing.Point(3, 127);
            this.ReloadAudioButton.Name = "ReloadAudioButton";
            this.ReloadAudioButton.Size = new System.Drawing.Size(119, 23);
            this.ReloadAudioButton.TabIndex = 11;
            this.ReloadAudioButton.Text = "Reload Audio";
            this.ReloadAudioButton.UseVisualStyleBackColor = true;
            this.ReloadAudioButton.Click += new System.EventHandler(this.ReloadAudioButton_Click);
            // 
            // NeverMuteTextBox
            // 
            this.NeverMuteTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.settingsBindingSource, "NeverMuteProcs", true));
            this.NeverMuteTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NeverMuteTextBox.Location = new System.Drawing.Point(3, 19);
            this.NeverMuteTextBox.Multiline = true;
            this.NeverMuteTextBox.Name = "NeverMuteTextBox";
            this.NeverMuteTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.NeverMuteTextBox.Size = new System.Drawing.Size(119, 30);
            this.NeverMuteTextBox.TabIndex = 8;
            this.NeverMuteTextBox.TextChanged += new System.EventHandler(this.NeverMuteTextBox_TextChanged);
            // 
            // settingsBindingSource
            // 
            this.settingsBindingSource.DataSource = typeof(WinBGMuter.Properties.Settings);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Mute Exceptions";
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Left;
            this.button1.Location = new System.Drawing.Point(3, 156);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Save Changes";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.SaveChangesButton_Click);
            // 
            // DarkModeCheckbox
            // 
            this.DarkModeCheckbox.AutoSize = true;
            this.DarkModeCheckbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DarkModeCheckbox.Location = new System.Drawing.Point(3, 55);
            this.DarkModeCheckbox.Name = "DarkModeCheckbox";
            this.DarkModeCheckbox.Size = new System.Drawing.Size(119, 16);
            this.DarkModeCheckbox.TabIndex = 10;
            this.DarkModeCheckbox.Text = "Dark Mode";
            this.DarkModeCheckbox.UseVisualStyleBackColor = true;
            this.DarkModeCheckbox.CheckedChanged += new System.EventHandler(this.DarkModeCheckbox_CheckedChanged);
            // 
            // LoggerCheckbox
            // 
            this.LoggerCheckbox.AutoSize = true;
            this.LoggerCheckbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LoggerCheckbox.Location = new System.Drawing.Point(3, 77);
            this.LoggerCheckbox.Name = "LoggerCheckbox";
            this.LoggerCheckbox.Size = new System.Drawing.Size(119, 21);
            this.LoggerCheckbox.TabIndex = 7;
            this.LoggerCheckbox.Text = "Activate Logger";
            this.LoggerCheckbox.UseVisualStyleBackColor = true;
            this.LoggerCheckbox.CheckedChanged += new System.EventHandler(this.LoggerCheckbox_CheckedChanged);
            // 
            // ConsoleLogging
            // 
            this.ConsoleLogging.AutoSize = true;
            this.ConsoleLogging.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConsoleLogging.Location = new System.Drawing.Point(3, 104);
            this.ConsoleLogging.Name = "ConsoleLogging";
            this.ConsoleLogging.Size = new System.Drawing.Size(119, 17);
            this.ConsoleLogging.TabIndex = 9;
            this.ConsoleLogging.Text = "Enable Console Logging";
            this.ConsoleLogging.UseVisualStyleBackColor = true;
            this.ConsoleLogging.CheckedChanged += new System.EventHandler(this.ConsoleLogging_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel4);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(140, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(278, 231);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Mute Exception Changer";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.7037F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.85185F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.44444F));
            this.tableLayoutPanel4.Controls.Add(this.NeverMuteListBox, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.ProcessListListBox, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(272, 209);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // NeverMuteListBox
            // 
            this.NeverMuteListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NeverMuteListBox.FormattingEnabled = true;
            this.NeverMuteListBox.ItemHeight = 15;
            this.NeverMuteListBox.Location = new System.Drawing.Point(153, 3);
            this.NeverMuteListBox.Name = "NeverMuteListBox";
            this.NeverMuteListBox.Size = new System.Drawing.Size(116, 203);
            this.NeverMuteListBox.TabIndex = 1;
            // 
            // ProcessListListBox
            // 
            this.ProcessListListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProcessListListBox.FormattingEnabled = true;
            this.ProcessListListBox.ItemHeight = 15;
            this.ProcessListListBox.Location = new System.Drawing.Point(3, 3);
            this.ProcessListListBox.Name = "ProcessListListBox";
            this.ProcessListListBox.Size = new System.Drawing.Size(112, 203);
            this.ProcessListListBox.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.ProcessToMuteButton, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.MuteToProcessButton, 0, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(121, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.07882F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.27094F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.7931F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.7931F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.57143F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(26, 203);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // ProcessToMuteButton
            // 
            this.ProcessToMuteButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProcessToMuteButton.Location = new System.Drawing.Point(3, 60);
            this.ProcessToMuteButton.Name = "ProcessToMuteButton";
            this.ProcessToMuteButton.Size = new System.Drawing.Size(20, 25);
            this.ProcessToMuteButton.TabIndex = 0;
            this.ProcessToMuteButton.Text = ">";
            this.ProcessToMuteButton.UseVisualStyleBackColor = true;
            this.ProcessToMuteButton.Click += new System.EventHandler(this.ProcessToMuteButton_Click);
            // 
            // MuteToProcessButton
            // 
            this.MuteToProcessButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MuteToProcessButton.Location = new System.Drawing.Point(3, 119);
            this.MuteToProcessButton.Name = "MuteToProcessButton";
            this.MuteToProcessButton.Size = new System.Drawing.Size(20, 22);
            this.MuteToProcessButton.TabIndex = 1;
            this.MuteToProcessButton.Text = "<";
            this.MuteToProcessButton.UseVisualStyleBackColor = true;
            this.MuteToProcessButton.Click += new System.EventHandler(this.MuteToProcessButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LastLogLabel,
            this.StatusBox});
            this.statusStrip1.Location = new System.Drawing.Point(0, 305);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(427, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // LastLogLabel
            // 
            this.LastLogLabel.Name = "LastLogLabel";
            this.LastLogLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // StatusBox
            // 
            this.StatusBox.Name = "StatusBox";
            this.StatusBox.Size = new System.Drawing.Size(39, 17);
            this.StatusBox.Text = "Ready";
            // 
            // TrayIcon
            // 
            this.TrayIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.TrayIcon.BalloonTipText = "Background Muter is running in the Background!";
            this.TrayIcon.BalloonTipTitle = "BGMuter";
            this.TrayIcon.ContextMenuStrip = this.TrayContextMenu;
            this.TrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("TrayIcon.Icon")));
            this.TrayIcon.Text = "Background Muter";
            this.TrayIcon.Visible = true;
            this.TrayIcon.DoubleClick += new System.EventHandler(this.TrayIcon_DoubleClick);
            // 
            // TrayContextMenu
            // 
            this.TrayContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenMenuTray,
            this.SeparatorMenuTray,
            this.aboutToolStripMenuItem,
            this.CloseMenuTray});
            this.TrayContextMenu.Name = "TrayContextMenu";
            this.TrayContextMenu.Size = new System.Drawing.Size(108, 76);
            // 
            // OpenMenuTray
            // 
            this.OpenMenuTray.Name = "OpenMenuTray";
            this.OpenMenuTray.Size = new System.Drawing.Size(107, 22);
            this.OpenMenuTray.Text = "Open";
            this.OpenMenuTray.Click += new System.EventHandler(this.OpenMenuTray_Click);
            // 
            // SeparatorMenuTray
            // 
            this.SeparatorMenuTray.Name = "SeparatorMenuTray";
            this.SeparatorMenuTray.Size = new System.Drawing.Size(104, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // CloseMenuTray
            // 
            this.CloseMenuTray.Name = "CloseMenuTray";
            this.CloseMenuTray.Size = new System.Drawing.Size(107, 22);
            this.CloseMenuTray.Text = "Exit";
            this.CloseMenuTray.Click += new System.EventHandler(this.CloseMenuTray_Click);
            // 
            // MuterTimer
            // 
            this.MuterTimer.Interval = 250;
            this.MuterTimer.Tick += new System.EventHandler(this.MuterTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 327);
            this.Controls.Add(this.panel1);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Background Muter (GUI)";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.TopTableLayout.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.settingsBindingSource)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.TrayContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private TableLayoutPanel tableLayoutPanel1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel LastLogLabel;
        private BindingSource settingsBindingSource;
        private NotifyIcon TrayIcon;
        private System.Windows.Forms.Timer MuterTimer;
        private ContextMenuStrip TrayContextMenu;
        private ToolStripMenuItem OpenMenuTray;
        private ToolStripSeparator SeparatorMenuTray;
        private ToolStripMenuItem CloseMenuTray;
        private RichTextBox LogTextBox;
        private TableLayoutPanel TopTableLayout;
        private GroupBox groupBox1;
        private TableLayoutPanel tableLayoutPanel3;
        private Label label1;
        private CheckBox LoggerCheckbox;
        private TextBox NeverMuteTextBox;
        private GroupBox groupBox2;
        private TableLayoutPanel tableLayoutPanel4;
        private CheckBox ConsoleLogging;
        private CheckBox DarkModeCheckbox;
        private ListBox NeverMuteListBox;
        private ListBox ProcessListListBox;
        private Button ReloadAudioButton;
        private Button button1;
        private TableLayoutPanel tableLayoutPanel2;
        private Button ProcessToMuteButton;
        private Button MuteToProcessButton;
        private ToolStripStatusLabel StatusBox;
        private Button button2;
        private ToolStripMenuItem aboutToolStripMenuItem;
    }
}