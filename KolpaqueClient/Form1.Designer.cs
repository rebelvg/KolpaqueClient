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
            this.LQ_checkBox = new System.Windows.Forms.CheckBox();
            this.addChannel_button = new System.Windows.Forms.Button();
            this.addChannel_textBox = new System.Windows.Forms.TextBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.poddyChannelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customChannelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playFromClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeClientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.openChat_checkBox = new System.Windows.Forms.CheckBox();
            this.channels_listView = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.notifications_checkBox = new System.Windows.Forms.CheckBox();
            this.autoPlay_checkBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.playStreamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openChatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeChannelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minimizeAtStart_checkBox = new System.Windows.Forms.CheckBox();
            this.changeLivestreamerPath_button = new System.Windows.Forms.Button();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.thirtySecTimer = new System.Windows.Forms.Timer(this.components);
            this.makeAPrintScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.screenshotsPath_textBox = new System.Windows.Forms.TextBox();
            this.changeScreenshotsPath_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // livestreamerPath_textBox
            // 
            this.livestreamerPath_textBox.Enabled = false;
            this.livestreamerPath_textBox.Location = new System.Drawing.Point(6, 58);
            this.livestreamerPath_textBox.Name = "livestreamerPath_textBox";
            this.livestreamerPath_textBox.ReadOnly = true;
            this.livestreamerPath_textBox.Size = new System.Drawing.Size(340, 20);
            this.livestreamerPath_textBox.TabIndex = 2;
            // 
            // xmlPath_textBox
            // 
            this.xmlPath_textBox.Enabled = false;
            this.xmlPath_textBox.Location = new System.Drawing.Point(6, 19);
            this.xmlPath_textBox.Name = "xmlPath_textBox";
            this.xmlPath_textBox.ReadOnly = true;
            this.xmlPath_textBox.Size = new System.Drawing.Size(372, 20);
            this.xmlPath_textBox.TabIndex = 7;
            // 
            // LQ_checkBox
            // 
            this.LQ_checkBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.LQ_checkBox.AutoSize = true;
            this.LQ_checkBox.Location = new System.Drawing.Point(297, 515);
            this.LQ_checkBox.Name = "LQ_checkBox";
            this.LQ_checkBox.Size = new System.Drawing.Size(81, 17);
            this.LQ_checkBox.TabIndex = 10;
            this.LQ_checkBox.Text = "Low Quality";
            this.LQ_checkBox.UseVisualStyleBackColor = true;
            this.LQ_checkBox.Click += new System.EventHandler(this.checkBox1_Click);
            // 
            // addChannel_button
            // 
            this.addChannel_button.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.addChannel_button.Location = new System.Drawing.Point(274, 486);
            this.addChannel_button.Name = "addChannel_button";
            this.addChannel_button.Size = new System.Drawing.Size(104, 23);
            this.addChannel_button.TabIndex = 11;
            this.addChannel_button.Text = "Add Channel";
            this.addChannel_button.UseVisualStyleBackColor = true;
            this.addChannel_button.Click += new System.EventHandler(this.button2_Click);
            // 
            // addChannel_textBox
            // 
            this.addChannel_textBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.addChannel_textBox.Location = new System.Drawing.Point(6, 460);
            this.addChannel_textBox.Name = "addChannel_textBox";
            this.addChannel_textBox.Size = new System.Drawing.Size(372, 20);
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
            this.poddyChannelsToolStripMenuItem,
            this.customChannelsToolStripMenuItem,
            this.playFromClipboardToolStripMenuItem,
            this.makeAPrintScreenToolStripMenuItem,
            this.closeClientToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(168, 114);
            // 
            // poddyChannelsToolStripMenuItem
            // 
            this.poddyChannelsToolStripMenuItem.Name = "poddyChannelsToolStripMenuItem";
            this.poddyChannelsToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.poddyChannelsToolStripMenuItem.Text = "Poddy Channels";
            // 
            // customChannelsToolStripMenuItem
            // 
            this.customChannelsToolStripMenuItem.Name = "customChannelsToolStripMenuItem";
            this.customChannelsToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.customChannelsToolStripMenuItem.Text = "Custom Channels";
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
            // linkLabel1
            // 
            this.linkLabel1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(3, 554);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(83, 13);
            this.linkLabel1.TabIndex = 16;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Kolpaque Home";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // openChat_checkBox
            // 
            this.openChat_checkBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.openChat_checkBox.AutoSize = true;
            this.openChat_checkBox.Location = new System.Drawing.Point(297, 538);
            this.openChat_checkBox.Name = "openChat_checkBox";
            this.openChat_checkBox.Size = new System.Drawing.Size(77, 17);
            this.openChat_checkBox.TabIndex = 18;
            this.openChat_checkBox.Text = "Open Chat";
            this.openChat_checkBox.UseVisualStyleBackColor = true;
            this.openChat_checkBox.Click += new System.EventHandler(this.checkBox2_Click);
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
            this.channels_listView.Size = new System.Drawing.Size(372, 448);
            this.channels_listView.TabIndex = 19;
            this.channels_listView.UseCompatibleStateImageBehavior = false;
            this.channels_listView.View = System.Windows.Forms.View.Details;
            this.channels_listView.ColumnWidthChanged += new System.Windows.Forms.ColumnWidthChangedEventHandler(this.channels_listView_ColumnWidthChanged);
            this.channels_listView.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.channels_listView_ColumnWidthChanging);
            this.channels_listView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView2_MouseClick);
            this.channels_listView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView2_MouseDoubleClick);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Channels";
            this.columnHeader2.Width = 348;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // notifications_checkBox
            // 
            this.notifications_checkBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.notifications_checkBox.AutoSize = true;
            this.notifications_checkBox.Checked = true;
            this.notifications_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.notifications_checkBox.Location = new System.Drawing.Point(6, 486);
            this.notifications_checkBox.Name = "notifications_checkBox";
            this.notifications_checkBox.Size = new System.Drawing.Size(114, 17);
            this.notifications_checkBox.TabIndex = 20;
            this.notifications_checkBox.Text = "Show Notifications";
            this.notifications_checkBox.UseVisualStyleBackColor = true;
            this.notifications_checkBox.Click += new System.EventHandler(this.checkBox3_Click);
            // 
            // autoPlay_checkBox
            // 
            this.autoPlay_checkBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.autoPlay_checkBox.AutoSize = true;
            this.autoPlay_checkBox.Location = new System.Drawing.Point(297, 561);
            this.autoPlay_checkBox.Name = "autoPlay_checkBox";
            this.autoPlay_checkBox.Size = new System.Drawing.Size(71, 17);
            this.autoPlay_checkBox.TabIndex = 21;
            this.autoPlay_checkBox.Text = "Auto-Play";
            this.autoPlay_checkBox.UseVisualStyleBackColor = true;
            this.autoPlay_checkBox.Click += new System.EventHandler(this.checkBox4_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 593);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Version 0.99";
            // 
            // linkLabel3
            // 
            this.linkLabel3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Location = new System.Drawing.Point(75, 593);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(113, 13);
            this.linkLabel3.TabIndex = 23;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "New Version Available";
            this.linkLabel3.Visible = false;
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playStreamToolStripMenuItem,
            this.openChatToolStripMenuItem,
            this.copyToClipboardToolStripMenuItem,
            this.removeChannelToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(161, 92);
            this.contextMenuStrip2.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.contextMenuStrip2_Closed);
            // 
            // playStreamToolStripMenuItem
            // 
            this.playStreamToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.playStreamToolStripMenuItem.Name = "playStreamToolStripMenuItem";
            this.playStreamToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.playStreamToolStripMenuItem.Text = "Play Stream";
            this.playStreamToolStripMenuItem.Click += new System.EventHandler(this.playStreamToolStripMenuItem_Click);
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
            // minimizeAtStart_checkBox
            // 
            this.minimizeAtStart_checkBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.minimizeAtStart_checkBox.AutoSize = true;
            this.minimizeAtStart_checkBox.Location = new System.Drawing.Point(6, 509);
            this.minimizeAtStart_checkBox.Name = "minimizeAtStart_checkBox";
            this.minimizeAtStart_checkBox.Size = new System.Drawing.Size(103, 17);
            this.minimizeAtStart_checkBox.TabIndex = 24;
            this.minimizeAtStart_checkBox.Text = "Minimize at Start";
            this.minimizeAtStart_checkBox.UseVisualStyleBackColor = true;
            this.minimizeAtStart_checkBox.Click += new System.EventHandler(this.checkBox5_Click);
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
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 60000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // linkLabel2
            // 
            this.linkLabel2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(3, 572);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(40, 13);
            this.linkLabel2.TabIndex = 26;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "GitHub";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // thirtySecTimer
            // 
            this.thirtySecTimer.Enabled = true;
            this.thirtySecTimer.Interval = 30000;
            this.thirtySecTimer.Tick += new System.EventHandler(this.thirtySecTimer_Tick);
            // 
            // makeAPrintScreenToolStripMenuItem
            // 
            this.makeAPrintScreenToolStripMenuItem.Name = "makeAPrintScreenToolStripMenuItem";
            this.makeAPrintScreenToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.makeAPrintScreenToolStripMenuItem.Text = "PrintScreen";
            this.makeAPrintScreenToolStripMenuItem.Click += new System.EventHandler(this.makeAPrintScreenToolStripMenuItem_Click);
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
            this.tabControl1.Size = new System.Drawing.Size(392, 640);
            this.tabControl1.TabIndex = 27;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.channels_listView);
            this.tabPage1.Controls.Add(this.addChannel_textBox);
            this.tabPage1.Controls.Add(this.addChannel_button);
            this.tabPage1.Controls.Add(this.linkLabel2);
            this.tabPage1.Controls.Add(this.LQ_checkBox);
            this.tabPage1.Controls.Add(this.minimizeAtStart_checkBox);
            this.tabPage1.Controls.Add(this.openChat_checkBox);
            this.tabPage1.Controls.Add(this.linkLabel3);
            this.tabPage1.Controls.Add(this.autoPlay_checkBox);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.notifications_checkBox);
            this.tabPage1.Controls.Add(this.linkLabel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(384, 611);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.changeScreenshotsPath_button);
            this.tabPage2.Controls.Add(this.screenshotsPath_textBox);
            this.tabPage2.Controls.Add(this.xmlPath_textBox);
            this.tabPage2.Controls.Add(this.livestreamerPath_textBox);
            this.tabPage2.Controls.Add(this.changeLivestreamerPath_button);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(384, 611);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // screenshotsPath_textBox
            // 
            this.screenshotsPath_textBox.Enabled = false;
            this.screenshotsPath_textBox.Location = new System.Drawing.Point(6, 97);
            this.screenshotsPath_textBox.Name = "screenshotsPath_textBox";
            this.screenshotsPath_textBox.ReadOnly = true;
            this.screenshotsPath_textBox.Size = new System.Drawing.Size(340, 20);
            this.screenshotsPath_textBox.TabIndex = 26;
            // 
            // changeScreenshotsPath_button
            // 
            this.changeScreenshotsPath_button.Location = new System.Drawing.Point(352, 97);
            this.changeScreenshotsPath_button.Name = "changeScreenshotsPath_button";
            this.changeScreenshotsPath_button.Size = new System.Drawing.Size(26, 20);
            this.changeScreenshotsPath_button.TabIndex = 27;
            this.changeScreenshotsPath_button.Text = "...";
            this.changeScreenshotsPath_button.UseVisualStyleBackColor = true;
            this.changeScreenshotsPath_button.Click += new System.EventHandler(this.changeScreenshotsPath_button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Xml Path";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 29;
            this.label3.Text = "Livestreamer Path";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 13);
            this.label4.TabIndex = 30;
            this.label4.Text = "Screenshots Folder";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 640);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(400, 1200);
            this.MinimumSize = new System.Drawing.Size(400, 667);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kolpaque Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
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
        private System.Windows.Forms.CheckBox LQ_checkBox;
        private System.Windows.Forms.Button addChannel_button;
        private System.Windows.Forms.TextBox addChannel_textBox;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.CheckBox openChat_checkBox;
        private System.Windows.Forms.ListView channels_listView;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox notifications_checkBox;
        private System.Windows.Forms.CheckBox autoPlay_checkBox;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem closeClientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem poddyChannelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customChannelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playFromClipboardToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem playStreamToolStripMenuItem;
        private System.Windows.Forms.CheckBox minimizeAtStart_checkBox;
        private System.Windows.Forms.ToolStripMenuItem copyToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeChannelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openChatToolStripMenuItem;
        private System.Windows.Forms.Button changeLivestreamerPath_button;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Timer thirtySecTimer;
        private System.Windows.Forms.ToolStripMenuItem makeAPrintScreenToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button changeScreenshotsPath_button;
        private System.Windows.Forms.TextBox screenshotsPath_textBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
    }
}
