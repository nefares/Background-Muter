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
            this.SaveChangesButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.NeverMuteListBox = new System.Windows.Forms.ListBox();
            this.ProcessListListBox = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.ProcessToMuteButton = new System.Windows.Forms.Button();
            this.MuteToProcessButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.ConsoleLogging = new System.Windows.Forms.CheckBox();
            this.LoggerCheckbox = new System.Windows.Forms.CheckBox();
            this.DarkModeCheckbox = new System.Windows.Forms.CheckBox();
            this.AutostartCheckbox = new System.Windows.Forms.CheckBox();
            this.MuteConditionGroupBox = new System.Windows.Forms.GroupBox();
            this.MinimizedRadioButton = new System.Windows.Forms.RadioButton();
            this.BackGroundRadioButton = new System.Windows.Forms.RadioButton();
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
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.MuteConditionGroupBox.SuspendLayout();
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
            this.panel1.Size = new System.Drawing.Size(549, 314);
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(549, 292);
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
            this.LogTextBox.Size = new System.Drawing.Size(543, 59);
            this.LogTextBox.TabIndex = 0;
            this.LogTextBox.Text = "";
            // 
            // TopTableLayout
            // 
            this.TopTableLayout.ColumnCount = 3;
            this.TopTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TopTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TopTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TopTableLayout.Controls.Add(this.groupBox1, 0, 0);
            this.TopTableLayout.Controls.Add(this.groupBox2, 1, 0);
            this.TopTableLayout.Controls.Add(this.groupBox3, 2, 0);
            this.TopTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TopTableLayout.Location = new System.Drawing.Point(3, 3);
            this.TopTableLayout.Name = "TopTableLayout";
            this.TopTableLayout.RowCount = 1;
            this.TopTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TopTableLayout.Size = new System.Drawing.Size(543, 237);
            this.TopTableLayout.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(129, 231);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Control";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.button2, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.ReloadAudioButton, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.NeverMuteTextBox, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.SaveChangesButton, 0, 3);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 5;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(123, 209);
            this.tableLayoutPanel3.TabIndex = 0;
            this.tableLayoutPanel3.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel3_Paint);
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.Location = new System.Drawing.Point(3, 172);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(117, 34);
            this.button2.TabIndex = 1;
            this.button2.Text = "Restore Defaults";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ReloadAudioButton
            // 
            this.ReloadAudioButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReloadAudioButton.Location = new System.Drawing.Point(3, 92);
            this.ReloadAudioButton.Name = "ReloadAudioButton";
            this.ReloadAudioButton.Size = new System.Drawing.Size(117, 34);
            this.ReloadAudioButton.TabIndex = 11;
            this.ReloadAudioButton.Text = "Reload Audio";
            this.ReloadAudioButton.UseVisualStyleBackColor = true;
            this.ReloadAudioButton.Click += new System.EventHandler(this.ReloadAudioButton_Click);
            // 
            // NeverMuteTextBox
            // 
            this.NeverMuteTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.settingsBindingSource, "NeverMuteProcs", true));
            this.NeverMuteTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NeverMuteTextBox.Location = new System.Drawing.Point(3, 28);
            this.NeverMuteTextBox.Multiline = true;
            this.NeverMuteTextBox.Name = "NeverMuteTextBox";
            this.NeverMuteTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.NeverMuteTextBox.Size = new System.Drawing.Size(117, 58);
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
            this.label1.Size = new System.Drawing.Size(117, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "Mute Exceptions";
            // 
            // SaveChangesButton
            // 
            this.SaveChangesButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SaveChangesButton.Location = new System.Drawing.Point(3, 132);
            this.SaveChangesButton.Name = "SaveChangesButton";
            this.SaveChangesButton.Size = new System.Drawing.Size(117, 34);
            this.SaveChangesButton.TabIndex = 6;
            this.SaveChangesButton.Text = "Save Changes";
            this.SaveChangesButton.UseVisualStyleBackColor = true;
            this.SaveChangesButton.Click += new System.EventHandler(this.SaveChangesButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel4);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(138, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(265, 231);
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
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(259, 209);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // NeverMuteListBox
            // 
            this.NeverMuteListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NeverMuteListBox.FormattingEnabled = true;
            this.NeverMuteListBox.ItemHeight = 15;
            this.NeverMuteListBox.Location = new System.Drawing.Point(146, 3);
            this.NeverMuteListBox.Name = "NeverMuteListBox";
            this.NeverMuteListBox.Size = new System.Drawing.Size(110, 203);
            this.NeverMuteListBox.TabIndex = 1;
            // 
            // ProcessListListBox
            // 
            this.ProcessListListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProcessListListBox.FormattingEnabled = true;
            this.ProcessListListBox.ItemHeight = 15;
            this.ProcessListListBox.Location = new System.Drawing.Point(3, 3);
            this.ProcessListListBox.Name = "ProcessListListBox";
            this.ProcessListListBox.Size = new System.Drawing.Size(107, 203);
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(116, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.07882F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.27094F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.7931F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.7931F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.57143F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(24, 203);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // ProcessToMuteButton
            // 
            this.ProcessToMuteButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProcessToMuteButton.Location = new System.Drawing.Point(3, 60);
            this.ProcessToMuteButton.Name = "ProcessToMuteButton";
            this.ProcessToMuteButton.Size = new System.Drawing.Size(18, 25);
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
            this.MuteToProcessButton.Size = new System.Drawing.Size(18, 22);
            this.MuteToProcessButton.TabIndex = 1;
            this.MuteToProcessButton.Text = "<";
            this.MuteToProcessButton.UseVisualStyleBackColor = true;
            this.MuteToProcessButton.Click += new System.EventHandler(this.MuteToProcessButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tableLayoutPanel5);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(409, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(131, 231);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Settings";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.ConsoleLogging, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.LoggerCheckbox, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.DarkModeCheckbox, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.AutostartCheckbox, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.MuteConditionGroupBox, 0, 4);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 5;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 14F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(125, 209);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // ConsoleLogging
            // 
            this.ConsoleLogging.AutoSize = true;
            this.ConsoleLogging.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConsoleLogging.Location = new System.Drawing.Point(3, 53);
            this.ConsoleLogging.Name = "ConsoleLogging";
            this.ConsoleLogging.Size = new System.Drawing.Size(119, 19);
            this.ConsoleLogging.TabIndex = 14;
            this.ConsoleLogging.Text = "Enable Console Logging";
            this.ConsoleLogging.UseVisualStyleBackColor = true;
            this.ConsoleLogging.CheckedChanged += new System.EventHandler(this.ConsoleLogging_CheckedChanged);
            // 
            // LoggerCheckbox
            // 
            this.LoggerCheckbox.AutoSize = true;
            this.LoggerCheckbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LoggerCheckbox.Location = new System.Drawing.Point(3, 28);
            this.LoggerCheckbox.Name = "LoggerCheckbox";
            this.LoggerCheckbox.Size = new System.Drawing.Size(119, 19);
            this.LoggerCheckbox.TabIndex = 13;
            this.LoggerCheckbox.Text = "Activate Logger";
            this.LoggerCheckbox.UseVisualStyleBackColor = true;
            this.LoggerCheckbox.CheckedChanged += new System.EventHandler(this.LoggerCheckbox_CheckedChanged);
            // 
            // DarkModeCheckbox
            // 
            this.DarkModeCheckbox.AutoSize = true;
            this.DarkModeCheckbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DarkModeCheckbox.Location = new System.Drawing.Point(3, 3);
            this.DarkModeCheckbox.Name = "DarkModeCheckbox";
            this.DarkModeCheckbox.Size = new System.Drawing.Size(119, 19);
            this.DarkModeCheckbox.TabIndex = 11;
            this.DarkModeCheckbox.Text = "Dark Mode";
            this.DarkModeCheckbox.UseVisualStyleBackColor = true;
            this.DarkModeCheckbox.CheckedChanged += new System.EventHandler(this.DarkModeCheckbox_CheckedChanged);
            // 
            // AutostartCheckbox
            // 
            this.AutostartCheckbox.AutoSize = true;
            this.AutostartCheckbox.Location = new System.Drawing.Point(3, 78);
            this.AutostartCheckbox.Name = "AutostartCheckbox";
            this.AutostartCheckbox.Size = new System.Drawing.Size(113, 19);
            this.AutostartCheckbox.TabIndex = 15;
            this.AutostartCheckbox.Text = "Enable Autostart";
            this.AutostartCheckbox.UseVisualStyleBackColor = true;
            this.AutostartCheckbox.CheckedChanged += new System.EventHandler(this.AutostartCheckbox_CheckedChanged);
            // 
            // MuteConditionGroupBox
            // 
            this.MuteConditionGroupBox.Controls.Add(this.MinimizedRadioButton);
            this.MuteConditionGroupBox.Controls.Add(this.BackGroundRadioButton);
            this.MuteConditionGroupBox.Location = new System.Drawing.Point(3, 104);
            this.MuteConditionGroupBox.Name = "MuteConditionGroupBox";
            this.MuteConditionGroupBox.Size = new System.Drawing.Size(113, 62);
            this.MuteConditionGroupBox.TabIndex = 16;
            this.MuteConditionGroupBox.TabStop = false;
            this.MuteConditionGroupBox.Text = "MuteCondition";
            // 
            // MinimizedRadioButton
            // 
            this.MinimizedRadioButton.AutoSize = true;
            this.MinimizedRadioButton.Location = new System.Drawing.Point(10, 36);
            this.MinimizedRadioButton.Name = "MinimizedRadioButton";
            this.MinimizedRadioButton.Size = new System.Drawing.Size(80, 19);
            this.MinimizedRadioButton.TabIndex = 1;
            this.MinimizedRadioButton.TabStop = true;
            this.MinimizedRadioButton.Text = "Minimized";
            this.MinimizedRadioButton.UseVisualStyleBackColor = true;
            this.MinimizedRadioButton.CheckedChanged += new System.EventHandler(this.MinimizedRadioButton_CheckedChanged);
            // 
            // BackGroundRadioButton
            // 
            this.BackGroundRadioButton.AutoSize = true;
            this.BackGroundRadioButton.Location = new System.Drawing.Point(10, 16);
            this.BackGroundRadioButton.Name = "BackGroundRadioButton";
            this.BackGroundRadioButton.Size = new System.Drawing.Size(90, 19);
            this.BackGroundRadioButton.TabIndex = 0;
            this.BackGroundRadioButton.TabStop = true;
            this.BackGroundRadioButton.Text = "BackGround";
            this.BackGroundRadioButton.UseVisualStyleBackColor = true;
            this.BackGroundRadioButton.CheckedChanged += new System.EventHandler(this.BackGroundRadioButton_CheckedChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LastLogLabel,
            this.StatusBox});
            this.statusStrip1.Location = new System.Drawing.Point(0, 292);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(549, 22);
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
            this.ClientSize = new System.Drawing.Size(549, 314);
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
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.MuteConditionGroupBox.ResumeLayout(false);
            this.MuteConditionGroupBox.PerformLayout();
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
    }
}