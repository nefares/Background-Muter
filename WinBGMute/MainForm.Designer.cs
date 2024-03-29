﻿/*
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            panel1 = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            LogTextBox = new RichTextBox();
            TopTableLayout = new TableLayoutPanel();
            groupBox1 = new GroupBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            button2 = new Button();
            ReloadAudioButton = new Button();
            NeverMuteTextBox = new TextBox();
            settingsBindingSource = new BindingSource(components);
            label1 = new Label();
            SaveChangesButton = new Button();
            groupBox2 = new GroupBox();
            tableLayoutPanel4 = new TableLayoutPanel();
            NeverMuteListBox = new ListBox();
            ProcessListListBox = new ListBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            ProcessToMuteButton = new Button();
            MuteToProcessButton = new Button();
            groupBox3 = new GroupBox();
            tableLayoutPanel5 = new TableLayoutPanel();
            ConsoleLogging = new CheckBox();
            LoggerCheckbox = new CheckBox();
            DarkModeCheckbox = new CheckBox();
            AutostartCheckbox = new CheckBox();
            MuteConditionGroupBox = new GroupBox();
            MinimizedRadioButton = new RadioButton();
            BackGroundRadioButton = new RadioButton();
            AdvancedButton = new Button();
            statusStrip1 = new StatusStrip();
            LastLogLabel = new ToolStripStatusLabel();
            StatusBox = new ToolStripStatusLabel();
            AdvancedMenuStrip = new ContextMenuStrip(components);
            HookMenuItem = new ToolStripMenuItem();
            TrayIcon = new NotifyIcon(components);
            TrayContextMenu = new ContextMenuStrip(components);
            OpenMenuTray = new ToolStripMenuItem();
            SeparatorMenuTray = new ToolStripSeparator();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            CloseMenuTray = new ToolStripMenuItem();
            MuterTimer = new System.Windows.Forms.Timer(components);
            KeepAliveTimer = new System.Windows.Forms.Timer(components);
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            TopTableLayout.SuspendLayout();
            groupBox1.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)settingsBindingSource).BeginInit();
            groupBox2.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            groupBox3.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            MuteConditionGroupBox.SuspendLayout();
            statusStrip1.SuspendLayout();
            AdvancedMenuStrip.SuspendLayout();
            TrayContextMenu.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(tableLayoutPanel1);
            panel1.Controls.Add(statusStrip1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(549, 314);
            panel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(LogTextBox, 0, 1);
            tableLayoutPanel1.Controls.Add(TopTableLayout, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(549, 292);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // LogTextBox
            // 
            LogTextBox.BorderStyle = BorderStyle.None;
            LogTextBox.Dock = DockStyle.Fill;
            LogTextBox.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            LogTextBox.Location = new Point(3, 246);
            LogTextBox.Name = "LogTextBox";
            LogTextBox.ScrollBars = RichTextBoxScrollBars.Vertical;
            LogTextBox.Size = new Size(543, 59);
            LogTextBox.TabIndex = 0;
            LogTextBox.Text = "";
            // 
            // TopTableLayout
            // 
            TopTableLayout.ColumnCount = 3;
            TopTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            TopTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            TopTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            TopTableLayout.Controls.Add(groupBox1, 0, 0);
            TopTableLayout.Controls.Add(groupBox2, 1, 0);
            TopTableLayout.Controls.Add(groupBox3, 2, 0);
            TopTableLayout.Dock = DockStyle.Fill;
            TopTableLayout.Location = new Point(3, 3);
            TopTableLayout.Name = "TopTableLayout";
            TopTableLayout.RowCount = 1;
            TopTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            TopTableLayout.Size = new Size(543, 237);
            TopTableLayout.TabIndex = 1;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(tableLayoutPanel3);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(129, 231);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Control";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(button2, 0, 4);
            tableLayoutPanel3.Controls.Add(ReloadAudioButton, 0, 2);
            tableLayoutPanel3.Controls.Add(NeverMuteTextBox, 0, 1);
            tableLayoutPanel3.Controls.Add(label1, 0, 0);
            tableLayoutPanel3.Controls.Add(SaveChangesButton, 0, 3);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 19);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 5;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel3.Size = new Size(123, 209);
            tableLayoutPanel3.TabIndex = 0;
            tableLayoutPanel3.Paint += tableLayoutPanel3_Paint;
            // 
            // button2
            // 
            button2.Dock = DockStyle.Fill;
            button2.Location = new Point(3, 172);
            button2.Name = "button2";
            button2.Size = new Size(117, 34);
            button2.TabIndex = 1;
            button2.Text = "Restore Defaults";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // ReloadAudioButton
            // 
            ReloadAudioButton.Dock = DockStyle.Fill;
            ReloadAudioButton.Location = new Point(3, 92);
            ReloadAudioButton.Name = "ReloadAudioButton";
            ReloadAudioButton.Size = new Size(117, 34);
            ReloadAudioButton.TabIndex = 11;
            ReloadAudioButton.Text = "Reload Audio";
            ReloadAudioButton.UseVisualStyleBackColor = true;
            ReloadAudioButton.Click += ReloadAudioButton_Click;
            // 
            // NeverMuteTextBox
            // 
            NeverMuteTextBox.DataBindings.Add(new Binding("Text", settingsBindingSource, "NeverMuteProcs", true));
            NeverMuteTextBox.Dock = DockStyle.Fill;
            NeverMuteTextBox.Location = new Point(3, 28);
            NeverMuteTextBox.Multiline = true;
            NeverMuteTextBox.Name = "NeverMuteTextBox";
            NeverMuteTextBox.ScrollBars = ScrollBars.Vertical;
            NeverMuteTextBox.Size = new Size(117, 58);
            NeverMuteTextBox.TabIndex = 8;
            NeverMuteTextBox.TextChanged += NeverMuteTextBox_TextChanged;
            // 
            // settingsBindingSource
            // 
            settingsBindingSource.DataSource = typeof(Properties.Settings);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(117, 25);
            label1.TabIndex = 5;
            label1.Text = "Mute Exceptions";
            // 
            // SaveChangesButton
            // 
            SaveChangesButton.Dock = DockStyle.Fill;
            SaveChangesButton.Location = new Point(3, 132);
            SaveChangesButton.Name = "SaveChangesButton";
            SaveChangesButton.Size = new Size(117, 34);
            SaveChangesButton.TabIndex = 6;
            SaveChangesButton.Text = "Save Changes";
            SaveChangesButton.UseVisualStyleBackColor = true;
            SaveChangesButton.Click += SaveChangesButton_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(tableLayoutPanel4);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(138, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(265, 231);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Mute Exception Changer";
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 3;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 43.7037F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.85185F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 44.44444F));
            tableLayoutPanel4.Controls.Add(NeverMuteListBox, 2, 0);
            tableLayoutPanel4.Controls.Add(ProcessListListBox, 0, 0);
            tableLayoutPanel4.Controls.Add(tableLayoutPanel2, 1, 0);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(3, 19);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel4.Size = new Size(259, 209);
            tableLayoutPanel4.TabIndex = 0;
            // 
            // NeverMuteListBox
            // 
            NeverMuteListBox.Dock = DockStyle.Fill;
            NeverMuteListBox.FormattingEnabled = true;
            NeverMuteListBox.ItemHeight = 15;
            NeverMuteListBox.Location = new Point(146, 3);
            NeverMuteListBox.Name = "NeverMuteListBox";
            NeverMuteListBox.Size = new Size(110, 203);
            NeverMuteListBox.TabIndex = 1;
            // 
            // ProcessListListBox
            // 
            ProcessListListBox.Dock = DockStyle.Fill;
            ProcessListListBox.FormattingEnabled = true;
            ProcessListListBox.ItemHeight = 15;
            ProcessListListBox.Location = new Point(3, 3);
            ProcessListListBox.Name = "ProcessListListBox";
            ProcessListListBox.Size = new Size(107, 203);
            ProcessListListBox.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Controls.Add(ProcessToMuteButton, 0, 1);
            tableLayoutPanel2.Controls.Add(MuteToProcessButton, 0, 3);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(116, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 5;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 28.07882F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 15.27094F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 13.7931F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 13.7931F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 28.57143F));
            tableLayoutPanel2.Size = new Size(24, 203);
            tableLayoutPanel2.TabIndex = 3;
            // 
            // ProcessToMuteButton
            // 
            ProcessToMuteButton.Dock = DockStyle.Fill;
            ProcessToMuteButton.Location = new Point(3, 60);
            ProcessToMuteButton.Name = "ProcessToMuteButton";
            ProcessToMuteButton.Size = new Size(18, 25);
            ProcessToMuteButton.TabIndex = 0;
            ProcessToMuteButton.Text = ">";
            ProcessToMuteButton.UseVisualStyleBackColor = true;
            ProcessToMuteButton.Click += ProcessToMuteButton_Click;
            // 
            // MuteToProcessButton
            // 
            MuteToProcessButton.Dock = DockStyle.Fill;
            MuteToProcessButton.Location = new Point(3, 119);
            MuteToProcessButton.Name = "MuteToProcessButton";
            MuteToProcessButton.Size = new Size(18, 22);
            MuteToProcessButton.TabIndex = 1;
            MuteToProcessButton.Text = "<";
            MuteToProcessButton.UseVisualStyleBackColor = true;
            MuteToProcessButton.Click += MuteToProcessButton_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(tableLayoutPanel5);
            groupBox3.Dock = DockStyle.Fill;
            groupBox3.Location = new Point(409, 3);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(131, 231);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Settings";
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 1;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Controls.Add(ConsoleLogging, 0, 1);
            tableLayoutPanel5.Controls.Add(LoggerCheckbox, 0, 1);
            tableLayoutPanel5.Controls.Add(DarkModeCheckbox, 0, 0);
            tableLayoutPanel5.Controls.Add(AutostartCheckbox, 0, 3);
            tableLayoutPanel5.Controls.Add(MuteConditionGroupBox, 0, 4);
            tableLayoutPanel5.Controls.Add(AdvancedButton, 0, 5);
            tableLayoutPanel5.Dock = DockStyle.Fill;
            tableLayoutPanel5.Location = new Point(3, 19);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 6;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 68F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tableLayoutPanel5.Size = new Size(125, 209);
            tableLayoutPanel5.TabIndex = 0;
            // 
            // ConsoleLogging
            // 
            ConsoleLogging.AutoSize = true;
            ConsoleLogging.Dock = DockStyle.Fill;
            ConsoleLogging.Location = new Point(3, 28);
            ConsoleLogging.Name = "ConsoleLogging";
            ConsoleLogging.Size = new Size(119, 19);
            ConsoleLogging.TabIndex = 14;
            ConsoleLogging.Text = "Enable Console Logging";
            ConsoleLogging.UseVisualStyleBackColor = true;
            ConsoleLogging.CheckedChanged += ConsoleLogging_CheckedChanged;
            // 
            // LoggerCheckbox
            // 
            LoggerCheckbox.AutoSize = true;
            LoggerCheckbox.Dock = DockStyle.Fill;
            LoggerCheckbox.Location = new Point(3, 53);
            LoggerCheckbox.Name = "LoggerCheckbox";
            LoggerCheckbox.Size = new Size(119, 19);
            LoggerCheckbox.TabIndex = 13;
            LoggerCheckbox.Text = "Activate Logger";
            LoggerCheckbox.UseVisualStyleBackColor = true;
            LoggerCheckbox.CheckedChanged += LoggerCheckbox_CheckedChanged;
            // 
            // DarkModeCheckbox
            // 
            DarkModeCheckbox.AutoSize = true;
            DarkModeCheckbox.Dock = DockStyle.Fill;
            DarkModeCheckbox.Location = new Point(3, 3);
            DarkModeCheckbox.Name = "DarkModeCheckbox";
            DarkModeCheckbox.Size = new Size(119, 19);
            DarkModeCheckbox.TabIndex = 11;
            DarkModeCheckbox.Text = "Dark Mode";
            DarkModeCheckbox.UseVisualStyleBackColor = true;
            DarkModeCheckbox.CheckedChanged += DarkModeCheckbox_CheckedChanged;
            // 
            // AutostartCheckbox
            // 
            AutostartCheckbox.AutoSize = true;
            AutostartCheckbox.Location = new Point(3, 78);
            AutostartCheckbox.Name = "AutostartCheckbox";
            AutostartCheckbox.Size = new Size(113, 19);
            AutostartCheckbox.TabIndex = 15;
            AutostartCheckbox.Text = "Enable Autostart";
            AutostartCheckbox.UseVisualStyleBackColor = true;
            AutostartCheckbox.CheckedChanged += AutostartCheckbox_CheckedChanged;
            // 
            // MuteConditionGroupBox
            // 
            MuteConditionGroupBox.Controls.Add(MinimizedRadioButton);
            MuteConditionGroupBox.Controls.Add(BackGroundRadioButton);
            MuteConditionGroupBox.Location = new Point(3, 104);
            MuteConditionGroupBox.Name = "MuteConditionGroupBox";
            MuteConditionGroupBox.Size = new Size(113, 62);
            MuteConditionGroupBox.TabIndex = 16;
            MuteConditionGroupBox.TabStop = false;
            MuteConditionGroupBox.Text = "MuteCondition";
            // 
            // MinimizedRadioButton
            // 
            MinimizedRadioButton.AutoSize = true;
            MinimizedRadioButton.Location = new Point(10, 36);
            MinimizedRadioButton.Name = "MinimizedRadioButton";
            MinimizedRadioButton.Size = new Size(81, 19);
            MinimizedRadioButton.TabIndex = 1;
            MinimizedRadioButton.TabStop = true;
            MinimizedRadioButton.Text = "Minimized";
            MinimizedRadioButton.UseVisualStyleBackColor = true;
            MinimizedRadioButton.CheckedChanged += MinimizedRadioButton_CheckedChanged;
            // 
            // BackGroundRadioButton
            // 
            BackGroundRadioButton.AutoSize = true;
            BackGroundRadioButton.Location = new Point(10, 16);
            BackGroundRadioButton.Name = "BackGroundRadioButton";
            BackGroundRadioButton.Size = new Size(90, 19);
            BackGroundRadioButton.TabIndex = 0;
            BackGroundRadioButton.TabStop = true;
            BackGroundRadioButton.Text = "BackGround";
            BackGroundRadioButton.UseVisualStyleBackColor = true;
            BackGroundRadioButton.CheckedChanged += BackGroundRadioButton_CheckedChanged;
            // 
            // AdvancedButton
            // 
            AdvancedButton.AutoSize = true;
            AdvancedButton.Dock = DockStyle.Fill;
            AdvancedButton.Location = new Point(3, 172);
            AdvancedButton.Name = "AdvancedButton";
            AdvancedButton.Size = new Size(119, 34);
            AdvancedButton.TabIndex = 17;
            AdvancedButton.Text = "Advanced...";
            AdvancedButton.UseVisualStyleBackColor = true;
            AdvancedButton.MouseClick += AdvancedButton_MouseClick;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { LastLogLabel, StatusBox });
            statusStrip1.Location = new Point(0, 292);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(549, 22);
            statusStrip1.TabIndex = 0;
            statusStrip1.Text = "statusStrip1";
            // 
            // LastLogLabel
            // 
            LastLogLabel.Name = "LastLogLabel";
            LastLogLabel.Size = new Size(0, 17);
            // 
            // StatusBox
            // 
            StatusBox.Name = "StatusBox";
            StatusBox.Size = new Size(39, 17);
            StatusBox.Text = "Ready";
            // 
            // AdvancedMenuStrip
            // 
            AdvancedMenuStrip.Items.AddRange(new ToolStripItem[] { HookMenuItem });
            AdvancedMenuStrip.Name = "AdvancedMenuStrip";
            AdvancedMenuStrip.Size = new Size(150, 26);
            // 
            // HookMenuItem
            // 
            HookMenuItem.Checked = true;
            HookMenuItem.CheckState = CheckState.Checked;
            HookMenuItem.Name = "HookMenuItem";
            HookMenuItem.Size = new Size(149, 22);
            HookMenuItem.Text = "<placehoder>";
            // 
            // TrayIcon
            // 
            TrayIcon.BalloonTipIcon = ToolTipIcon.Info;
            TrayIcon.BalloonTipText = "Background Muter is running in the Background!";
            TrayIcon.BalloonTipTitle = "BGMuter";
            TrayIcon.ContextMenuStrip = TrayContextMenu;
            TrayIcon.Icon = (Icon)resources.GetObject("TrayIcon.Icon");
            TrayIcon.Text = "Background Muter";
            TrayIcon.Visible = true;
            TrayIcon.DoubleClick += TrayIcon_DoubleClick;
            // 
            // TrayContextMenu
            // 
            TrayContextMenu.Items.AddRange(new ToolStripItem[] { OpenMenuTray, SeparatorMenuTray, aboutToolStripMenuItem, CloseMenuTray });
            TrayContextMenu.Name = "TrayContextMenu";
            TrayContextMenu.Size = new Size(108, 76);
            // 
            // OpenMenuTray
            // 
            OpenMenuTray.Name = "OpenMenuTray";
            OpenMenuTray.Size = new Size(107, 22);
            OpenMenuTray.Text = "Open";
            OpenMenuTray.Click += OpenMenuTray_Click;
            // 
            // SeparatorMenuTray
            // 
            SeparatorMenuTray.Name = "SeparatorMenuTray";
            SeparatorMenuTray.Size = new Size(104, 6);
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(107, 22);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // CloseMenuTray
            // 
            CloseMenuTray.Name = "CloseMenuTray";
            CloseMenuTray.Size = new Size(107, 22);
            CloseMenuTray.Text = "Exit";
            CloseMenuTray.Click += CloseMenuTray_Click;
            // 
            // MuterTimer
            // 
            MuterTimer.Interval = 250;
            MuterTimer.Tick += MuterTimer_Tick;
            // 
            // KeepAliveTimer
            // 
            KeepAliveTimer.Enabled = true;
            KeepAliveTimer.Interval = 600000;
            KeepAliveTimer.Tick += KeepAliveTimer_Tick;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(549, 314);
            Controls.Add(panel1);
            HelpButton = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "Background Muter (GUI)";
            FormClosed += MainForm_FormClosed;
            Load += MainForm_Load;
            Resize += MainForm_Resize;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            TopTableLayout.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)settingsBindingSource).EndInit();
            groupBox2.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            MuteConditionGroupBox.ResumeLayout(false);
            MuteConditionGroupBox.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            AdvancedMenuStrip.ResumeLayout(false);
            TrayContextMenu.ResumeLayout(false);
            ResumeLayout(false);
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
        private TextBox NeverMuteTextBox;
        private GroupBox groupBox2;
        private TableLayoutPanel tableLayoutPanel4;
        private ListBox NeverMuteListBox;
        private ListBox ProcessListListBox;
        private Button ReloadAudioButton;
        private Button SaveChangesButton;
        private TableLayoutPanel tableLayoutPanel2;
        private Button ProcessToMuteButton;
        private Button MuteToProcessButton;
        private ToolStripStatusLabel StatusBox;
        private Button button2;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private GroupBox groupBox3;
        private TableLayoutPanel tableLayoutPanel5;
        private CheckBox DarkModeCheckbox;
        private CheckBox ConsoleLogging;
        private CheckBox LoggerCheckbox;
        private CheckBox AutostartCheckbox;
        private GroupBox MuteConditionGroupBox;
        private RadioButton MinimizedRadioButton;
        private RadioButton BackGroundRadioButton;
        private Button AdvancedButton;
        private ContextMenuStrip AdvancedMenuStrip;
        private ToolStripMenuItem HookMenuItem;
        private System.Windows.Forms.Timer KeepAliveTimer;
    }
}