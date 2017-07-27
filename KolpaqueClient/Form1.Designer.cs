namespace KolpaqueClient
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.livestreamerPath_textBox = new System.Windows.Forms.TextBox();
            this.xmlPath_textBox = new System.Windows.Forms.TextBox();
            this.addChannel_button = new System.Windows.Forms.Button();
            this.addChannel_textBox = new System.Windows.Forms.TextBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.customChannelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playFromClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeClientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.channels_listView = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fiveSecTimer = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.playStreamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playLowQualityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openChatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeChannelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeLivestreamerPath_button = new System.Windows.Forms.Button();
            this.sixtySecTimer = new System.Windows.Forms.Timer(this.components);
            this.thirtySecTimer = new System.Windows.Forms.Timer(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.LQ_checkBox = new System.Windows.Forms.CheckBox();
            this.autoPlay_checkBox = new System.Windows.Forms.CheckBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.save_button = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.twitchImport_button = new System.Windows.Forms.Button();
            this.twitchImport_textBox = new System.Windows.Forms.TextBox();
            this.minimizeAtStart_checkBox = new System.Windows.Forms.CheckBox();
            this.notifications_checkBox = new System.Windows.Forms.CheckBox();
            this.enableLog_checkBox = new System.Windows.Forms.CheckBox();
            this.launchStreamOnBalloonClick_checkBox = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // livestreamerPath_textBox
            // 
            this.livestreamerPath_textBox.Location = new System.Drawing.Point(6, 58);
            this.livestreamerPath_textBox.Name = "livestreamerPath_textBox";
            this.livestreamerPath_textBox.ReadOnly = true;
            this.livestreamerPath_textBox.Size = new System.Drawing.Size(340, 20);
            this.livestreamerPath_textBox.TabIndex = 2;
            // 
            // xmlPath_textBox
            // 
            this.xmlPath_textBox.Location = new System.Drawing.Point(6, 19);
            this.xmlPath_textBox.Name = "xmlPath_textBox";
            this.xmlPath_textBox.ReadOnly = true;
            this.xmlPath_textBox.Size = new System.Drawing.Size(372, 20);
            this.xmlPath_textBox.TabIndex = 7;
            // 
            // addChannel_button
            // 
            this.addChannel_button.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.addChannel_button.Location = new System.Drawing.Point(351, 600);
            this.addChannel_button.Name = "addChannel_button";
            this.addChannel_button.Size = new System.Drawing.Size(27, 20);
            this.addChannel_button.TabIndex = 11;
            this.addChannel_button.Text = "+";
            this.addChannel_button.UseVisualStyleBackColor = true;
            this.addChannel_button.Click += new System.EventHandler(this.button2_Click);
            // 
            // addChannel_textBox
            // 
            this.addChannel_textBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.addChannel_textBox.Location = new System.Drawing.Point(6, 600);
            this.addChannel_textBox.Name = "addChannel_textBox";
            this.addChannel_textBox.Size = new System.Drawing.Size(339, 20);
            this.addChannel_textBox.TabIndex = 13;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Kolpaque Client";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.BalloonTipClicked += new System.EventHandler(this.notifyIcon1_BalloonTipClicked);
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.customChannelsToolStripMenuItem,
            this.playFromClipboardToolStripMenuItem,
            this.closeClientToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(168, 70);
            // 
            // customChannelsToolStripMenuItem
            // 
            this.customChannelsToolStripMenuItem.Name = "customChannelsToolStripMenuItem";
            this.customChannelsToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.customChannelsToolStripMenuItem.Text = "Online Channels";
            // 
            // playFromClipboardToolStripMenuItem
            // 
            this.playFromClipboardToolStripMenuItem.Name = "playFromClipboardToolStripMenuItem";
            this.playFromClipboardToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.playFromClipboardToolStripMenuItem.Text = "Play from Clipboard";
            this.playFromClipboardToolStripMenuItem.Click += new System.EventHandler(this.playFromClipboardToolStripMenuItem_Click);
            // 
            // closeClientToolStripMenuItem
            // 
            this.closeClientToolStripMenuItem.Name = "closeClientToolStripMenuItem";
            this.closeClientToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.closeClientToolStripMenuItem.Text = "Close Client";
            this.closeClientToolStripMenuItem.Click += new System.EventHandler(this.closeClientToolStripMenuItem_Click);
            // 
            // channels_listView
            // 
            this.channels_listView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.channels_listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.channels_listView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.channels_listView.FullRowSelect = true;
            this.channels_listView.GridLines = true;
            this.channels_listView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.channels_listView.Location = new System.Drawing.Point(6, 6);
            this.channels_listView.MultiSelect = false;
            this.channels_listView.Name = "channels_listView";
            this.channels_listView.Size = new System.Drawing.Size(372, 588);
            this.channels_listView.TabIndex = 19;
            this.channels_listView.UseCompatibleStateImageBehavior = false;
            this.channels_listView.View = System.Windows.Forms.View.Details;
            this.channels_listView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView2_MouseClick);
            this.channels_listView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView2_MouseDoubleClick);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Channels";
            this.columnHeader2.Width = 348;
            // 
            // fiveSecTimer
            // 
            this.fiveSecTimer.Enabled = true;
            this.fiveSecTimer.Interval = 5000;
            this.fiveSecTimer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playStreamToolStripMenuItem,
            this.playLowQualityToolStripMenuItem,
            this.openPageToolStripMenuItem,
            this.openChatToolStripMenuItem,
            this.copyToClipboardToolStripMenuItem,
            this.removeChannelToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(161, 136);
            this.contextMenuStrip2.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.contextMenuStrip2_Closed);
            // 
            // playStreamToolStripMenuItem
            // 
            this.playStreamToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.playStreamToolStripMenuItem.Name = "playStreamToolStripMenuItem";
            this.playStreamToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.playStreamToolStripMenuItem.Text = "Play Original";
            this.playStreamToolStripMenuItem.Click += new System.EventHandler(this.playStreamToolStripMenuItem_Click);
            // 
            // playLowQualityToolStripMenuItem
            // 
            this.playLowQualityToolStripMenuItem.Name = "playLowQualityToolStripMenuItem";
            this.playLowQualityToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.playLowQualityToolStripMenuItem.Text = "Play Low Quality";
            this.playLowQualityToolStripMenuItem.Click += new System.EventHandler(this.playLowQualityToolStripMenuItem_Click);
            // 
            // openPageToolStripMenuItem
            // 
            this.openPageToolStripMenuItem.Name = "openPageToolStripMenuItem";
            this.openPageToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.openPageToolStripMenuItem.Text = "Open Page";
            this.openPageToolStripMenuItem.Click += new System.EventHandler(this.openPageToolStripMenuItem_Click);
            // 
            // openChatToolStripMenuItem
            // 
            this.openChatToolStripMenuItem.Name = "openChatToolStripMenuItem";
            this.openChatToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.openChatToolStripMenuItem.Text = "Open Chat";
            this.openChatToolStripMenuItem.Click += new System.EventHandler(this.openChatToolStripMenuItem_Click);
            // 
            // copyToClipboardToolStripMenuItem
            // 
            this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
            this.copyToClipboardToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.copyToClipboardToolStripMenuItem.Text = "Copy to Clipboard";
            this.copyToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyToClipboardToolStripMenuItem_Click);
            // 
            // removeChannelToolStripMenuItem
            // 
            this.removeChannelToolStripMenuItem.Name = "removeChannelToolStripMenuItem";
            this.removeChannelToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.removeChannelToolStripMenuItem.Text = "Remove Channel";
            this.removeChannelToolStripMenuItem.Click += new System.EventHandler(this.removeChannelToolStripMenuItem_Click);
            // 
            // changeLivestreamerPath_button
            // 
            this.changeLivestreamerPath_button.Location = new System.Drawing.Point(352, 58);
            this.changeLivestreamerPath_button.Name = "changeLivestreamerPath_button";
            this.changeLivestreamerPath_button.Size = new System.Drawing.Size(26, 20);
            this.changeLivestreamerPath_button.TabIndex = 25;
            this.changeLivestreamerPath_button.Text = "...";
            this.changeLivestreamerPath_button.UseVisualStyleBackColor = true;
            this.changeLivestreamerPath_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // sixtySecTimer
            // 
            this.sixtySecTimer.Enabled = true;
            this.sixtySecTimer.Interval = 60000;
            this.sixtySecTimer.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // thirtySecTimer
            // 
            this.thirtySecTimer.Enabled = true;
            this.thirtySecTimer.Interval = 30000;
            this.thirtySecTimer.Tick += new System.EventHandler(this.thirtySecTimer_Tick);
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(392, 673);
            this.tabControl1.TabIndex = 27;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.linkLabel3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.channels_listView);
            this.tabPage1.Controls.Add(this.addChannel_textBox);
            this.tabPage1.Controls.Add(this.addChannel_button);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(384, 644);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            // 
            // linkLabel3
            // 
            this.linkLabel3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Location = new System.Drawing.Point(77, 626);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(113, 13);
            this.linkLabel3.TabIndex = 47;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "New Version Available";
            this.linkLabel3.Visible = false;
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 626);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 46;
            this.label2.Text = "Version 0.0.0";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.linkLabel2);
            this.tabPage2.Controls.Add(this.LQ_checkBox);
            this.tabPage2.Controls.Add(this.autoPlay_checkBox);
            this.tabPage2.Controls.Add(this.linkLabel1);
            this.tabPage2.Controls.Add(this.save_button);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.twitchImport_button);
            this.tabPage2.Controls.Add(this.twitchImport_textBox);
            this.tabPage2.Controls.Add(this.minimizeAtStart_checkBox);
            this.tabPage2.Controls.Add(this.notifications_checkBox);
            this.tabPage2.Controls.Add(this.enableLog_checkBox);
            this.tabPage2.Controls.Add(this.launchStreamOnBalloonClick_checkBox);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.xmlPath_textBox);
            this.tabPage2.Controls.Add(this.livestreamerPath_textBox);
            this.tabPage2.Controls.Add(this.changeLivestreamerPath_button);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(384, 644);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // linkLabel2
            // 
            this.linkLabel2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(2, 626);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(40, 13);
            this.linkLabel2.TabIndex = 46;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "GitHub";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // LQ_checkBox
            // 
            this.LQ_checkBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.LQ_checkBox.AutoSize = true;
            this.LQ_checkBox.Location = new System.Drawing.Point(6, 84);
            this.LQ_checkBox.Name = "LQ_checkBox";
            this.LQ_checkBox.Size = new System.Drawing.Size(81, 17);
            this.LQ_checkBox.TabIndex = 41;
            this.LQ_checkBox.Text = "Low Quality";
            this.LQ_checkBox.UseVisualStyleBackColor = true;
            // 
            // autoPlay_checkBox
            // 
            this.autoPlay_checkBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.autoPlay_checkBox.AutoSize = true;
            this.autoPlay_checkBox.Location = new System.Drawing.Point(6, 130);
            this.autoPlay_checkBox.Name = "autoPlay_checkBox";
            this.autoPlay_checkBox.Size = new System.Drawing.Size(71, 17);
            this.autoPlay_checkBox.TabIndex = 43;
            this.autoPlay_checkBox.Text = "Auto-Play";
            this.autoPlay_checkBox.UseVisualStyleBackColor = true;
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(2, 608);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(83, 13);
            this.linkLabel1.TabIndex = 42;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Kolpaque Home";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // save_button
            // 
            this.save_button.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.save_button.Location = new System.Drawing.Point(285, 615);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(93, 23);
            this.save_button.TabIndex = 40;
            this.save_button.Text = "Save Settings";
            this.save_button.UseVisualStyleBackColor = true;
            this.save_button.Click += new System.EventHandler(this.save_button_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 254);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 39;
            this.label5.Text = "Twitch Import";
            // 
            // twitchImport_button
            // 
            this.twitchImport_button.Location = new System.Drawing.Point(303, 296);
            this.twitchImport_button.Name = "twitchImport_button";
            this.twitchImport_button.Size = new System.Drawing.Size(75, 23);
            this.twitchImport_button.TabIndex = 38;
            this.twitchImport_button.Text = "Import";
            this.twitchImport_button.UseVisualStyleBackColor = true;
            this.twitchImport_button.Click += new System.EventHandler(this.twitchImport_button_Click);
            // 
            // twitchImport_textBox
            // 
            this.twitchImport_textBox.Location = new System.Drawing.Point(6, 270);
            this.twitchImport_textBox.Name = "twitchImport_textBox";
            this.twitchImport_textBox.Size = new System.Drawing.Size(372, 20);
            this.twitchImport_textBox.TabIndex = 37;
            // 
            // minimizeAtStart_checkBox
            // 
            this.minimizeAtStart_checkBox.AutoSize = true;
            this.minimizeAtStart_checkBox.Location = new System.Drawing.Point(6, 153);
            this.minimizeAtStart_checkBox.Name = "minimizeAtStart_checkBox";
            this.minimizeAtStart_checkBox.Size = new System.Drawing.Size(103, 17);
            this.minimizeAtStart_checkBox.TabIndex = 36;
            this.minimizeAtStart_checkBox.Text = "Minimize at Start";
            this.minimizeAtStart_checkBox.UseVisualStyleBackColor = true;
            // 
            // notifications_checkBox
            // 
            this.notifications_checkBox.AutoSize = true;
            this.notifications_checkBox.Checked = true;
            this.notifications_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.notifications_checkBox.Location = new System.Drawing.Point(6, 107);
            this.notifications_checkBox.Name = "notifications_checkBox";
            this.notifications_checkBox.Size = new System.Drawing.Size(114, 17);
            this.notifications_checkBox.TabIndex = 35;
            this.notifications_checkBox.Text = "Show Notifications";
            this.notifications_checkBox.UseVisualStyleBackColor = true;
            // 
            // enableLog_checkBox
            // 
            this.enableLog_checkBox.AutoSize = true;
            this.enableLog_checkBox.Location = new System.Drawing.Point(6, 199);
            this.enableLog_checkBox.Name = "enableLog_checkBox";
            this.enableLog_checkBox.Size = new System.Drawing.Size(77, 17);
            this.enableLog_checkBox.TabIndex = 34;
            this.enableLog_checkBox.Text = "Write Logs";
            this.enableLog_checkBox.UseVisualStyleBackColor = true;
            // 
            // launchStreamOnBalloonClick_checkBox
            // 
            this.launchStreamOnBalloonClick_checkBox.AutoSize = true;
            this.launchStreamOnBalloonClick_checkBox.Checked = true;
            this.launchStreamOnBalloonClick_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.launchStreamOnBalloonClick_checkBox.Location = new System.Drawing.Point(6, 176);
            this.launchStreamOnBalloonClick_checkBox.Name = "launchStreamOnBalloonClick_checkBox";
            this.launchStreamOnBalloonClick_checkBox.Size = new System.Drawing.Size(125, 17);
            this.launchStreamOnBalloonClick_checkBox.TabIndex = 31;
            this.launchStreamOnBalloonClick_checkBox.Text = "Play on Balloon Click";
            this.launchStreamOnBalloonClick_checkBox.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 29;
            this.label3.Text = "Livestreamer Path";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Xml Path";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 673);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(400, 1200);
            this.MinimumSize = new System.Drawing.Size(400, 700);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kolpaque Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ClientSizeChanged += new System.EventHandler(this.Form1_ClientSizeChanged);
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox livestreamerPath_textBox;
        private System.Windows.Forms.TextBox xmlPath_textBox;
        private System.Windows.Forms.Button addChannel_button;
        private System.Windows.Forms.TextBox addChannel_textBox;
        private System.Windows.Forms.ListView channels_listView;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Timer fiveSecTimer;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem closeClientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customChannelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playFromClipboardToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem playStreamToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeChannelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openChatToolStripMenuItem;
        private System.Windows.Forms.Button changeLivestreamerPath_button;
        private System.Windows.Forms.Timer sixtySecTimer;
        private System.Windows.Forms.Timer thirtySecTimer;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox launchStreamOnBalloonClick_checkBox;
        private System.Windows.Forms.CheckBox enableLog_checkBox;
        private System.Windows.Forms.CheckBox minimizeAtStart_checkBox;
        private System.Windows.Forms.CheckBox notifications_checkBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button twitchImport_button;
        private System.Windows.Forms.TextBox twitchImport_textBox;
        private System.Windows.Forms.Button save_button;
        private System.Windows.Forms.ToolStripMenuItem playLowQualityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openPageToolStripMenuItem;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.CheckBox LQ_checkBox;
        private System.Windows.Forms.CheckBox autoPlay_checkBox;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.Label label2;
    }
}
