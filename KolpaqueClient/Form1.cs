﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace KolpaqueClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            livestreamerPath = "C:\\Program Files (x86)\\Livestreamer\\livestreamer.exe";

            if (File.Exists(livestreamerPath))
            {
                textBox1.Text = livestreamerPath;
            }
            else
            {
                textBox1.Enabled = true;
                textBox1.Text = "https://github.com/chrippa/livestreamer/releases";
            }

            iniFilePath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\KolpaqueClient.ini";
            textBox2.Text = iniFilePath;

            if (File.Exists(iniFilePath))
            {
                ReadIniFile();
                RefreshInterface();
            }
            else
            {
                MessageBox.Show("KolpaqueClient.ini not found.");

                SaveIniFile();
                ReadIniFile();
                RefreshInterface();
            }

            newClientVersion = clientVersion;

            label2.Text = "Version " + clientVersion.ToString();

            GetStats(false);
        }

        string livestreamerPath;
        string iniFilePath;
        List<string> poddyChannelsList = new List<string>(new string[] { "rtmp://dedick.podkolpakom.net/live/liveevent", "rtmp://dedick.podkolpakom.net/live/tvstream", "rtmp://dedick.podkolpakom.net/live/murshun", "rtmp://vps.podkolpakom.net/live/liveevent" });
        List<string> poddyChannelsChatList = new List<string>(new string[] { "http://podkolpakom.net/stream/admin/", "http://podkolpakom.net/tv/admin/", "http://podkolpakom.net/murshun/admin/", "http://vps.podkolpakom.net/" });
        int twitchCooldown = 0;
        
        double clientVersion = 0.251;
        double newClientVersion;

        bool newVersionBalloonShown = false;

        ListViewItem listView2LastSelectedItem;

        public void ReadIniFile()
        {
            foreach (string X in poddyChannelsList)
            {
                listView2.Items.Add(X);
                poddyChannelsToolStripMenuItem.DropDownItems.Add(X, null, new EventHandler(contextMenu_Click));
            }

            string[] infoFromIniFile = File.ReadAllLines(iniFilePath);

            foreach (string X in infoFromIniFile)
            {
                if (X.Contains("LQ=True"))
                {
                    checkBox1.Checked = true;
                }
                if (X.Contains("OPENCHAT=True"))
                {
                    checkBox2.Checked = true;
                }
                if (X.Contains("SHOWNOTIFICATIONS=False"))
                {
                    checkBox3.Checked = false;
                }
                if (X.Contains("AUTOPLAY=True"))
                {
                    checkBox4.Checked = true;
                }
                if (X.Contains("CUSTOMCHANNEL="))
                {
                    listView2.Items.Add(X.Replace("CUSTOMCHANNEL=", ""));
                    customChannelsToolStripMenuItem.DropDownItems.Add(X.Replace("CUSTOMCHANNEL=", ""), null, new EventHandler(contextMenu_Click));
                }
                if (X.Contains("MINIMIZEATSTART=True"))
                {
                    checkBox5.Checked = true;
                }
            }
        }
        
        public void SaveIniFile()
        {
            List<string> infoForSave = new List<string>();

            infoForSave.Add("LIVESTREAMERPATH=" + livestreamerPath);

            infoForSave.Add("LQ=" + checkBox1.Checked);

            infoForSave.Add("OPENCHAT=" + checkBox2.Checked);

            infoForSave.Add("SHOWNOTIFICATIONS=" + checkBox3.Checked);

            infoForSave.Add("AUTOPLAY=" + checkBox4.Checked);

            foreach (ListViewItem X in listView2.Items)
            {
                if (!poddyChannelsList.Contains(X.Text))
                {
                    infoForSave.Add("CUSTOMCHANNEL=" + X.Text);
                }
            }

            infoForSave.Add("MINIMIZEATSTART=" + checkBox5.Checked);
            
            File.WriteAllLines(iniFilePath, infoForSave);
        }

        public void RefreshInterface()
        {
            listView1.Items.Clear();
            listView1.Update();
            listView1.Refresh();

            listView1.Items.Add("LIVESTREAMERPATH=" + livestreamerPath);

            listView1.Items.Add("LQ=" + checkBox1.Checked);

            listView1.Items.Add("OPENCHAT=" + checkBox2.Checked);

            listView1.Items.Add("SHOWNOTIFICATIONS=" + checkBox3.Checked);

            listView1.Items.Add("AUTOPLAY=" + checkBox4.Checked);

            foreach (ListViewItem X in listView2.Items)
            {
                if (!poddyChannelsList.Contains(X.Text))
                {
                    listView1.Items.Add("CUSTOMCHANNEL=" + X.Text);
                }
            }

            listView1.Items.Add("MINIMIZEATSTART=" + checkBox5.Checked);
        }

        public void GetPoddyStatsNewThread(ListViewItem item, string S, bool showBalloon)
        {
            WebClient client = new WebClient();

            try
            {
                string amsStatsString = client.DownloadString("http://dedick.podkolpakom.net/stats/ams/gib_stats.php?stream=" + S.Replace("podkolpakom.net/live/", ""));

                if (amsStatsString.Contains("\"live\":\"Online\""))
                {
                    if (item.BackColor != Color.Green)
                    {
                        ChannelWentOnline(item, showBalloon);
                    }
                }
                if (amsStatsString.Contains("\"live\":\"Offline\""))
                {
                    if (item.BackColor != Color.White)
                    {
                        ChannelWentOffline(item);
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public void GetPoddyVpsStatsNewThread(ListViewItem item, string S, bool showBalloon)
        {
            WebClient client = new WebClient();

            try
            {
                string vpsStatsString = client.DownloadString("http://vps.podkolpakom.net/stats" + S.Replace("podkolpakom.net/live/", ""));

                List<string> vpsStatsStringList = vpsStatsString.Split(new string[] { "<stream>" }, StringSplitOptions.None).ToList();

                foreach (string X in vpsStatsStringList)
                {
                    if (X.Contains("<name>" + S.Replace("podkolpakom.net/live/", "") + "</name>"))
                    {
                        if (X.Contains("publishing/"))
                        {
                            if (item.BackColor != Color.Green)
                            {
                                ChannelWentOnline(item, showBalloon);
                            }
                        }
                        if (!X.Contains("publishing/"))
                        {
                            if (item.BackColor != Color.White)
                            {
                                ChannelWentOffline(item);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public void GetTwitchStatsNewThread(ListViewItem item, string S, bool showBalloon)
        {
            WebClient client = new WebClient();

            try
            {
                string twitchStatsString = client.DownloadString("https://api.twitch.tv/kraken/streams?channel=" + S.Replace("twitch.tv/", ""));

                if (twitchStatsString.Contains("created_at"))
                {
                    if (item.BackColor != Color.Green)
                    {
                        ChannelWentOnline(item, showBalloon);
                    }
                }
                if (twitchStatsString.Contains("\"streams\":[]"))
                {
                    if (item.BackColor != Color.White)
                    {
                        ChannelWentOffline(item);
                    }
                }
            }
            catch (Exception)
            {
                
            }
        }

        public void ChannelWentOnline(ListViewItem item, bool showBalloon)
        {
            this.Invoke(new Action(() => item.BackColor = Color.Green));

            foreach (ToolStripMenuItem toolStripMenuitem1 in contextMenuStrip1.Items)
            {
                foreach (ToolStripMenuItem toolStripMenuitem2 in toolStripMenuitem1.DropDownItems)
                {
                    if (toolStripMenuitem2.Text == item.Text)
                    {
                        toolStripMenuitem2.BackColor = Color.Green;
                    }
                }
            }

            if (showBalloon)
            {
                PrintBalloon(item);
            }

            if (checkBox4.Checked)
            {
                PlayStream(item);
            }
        }

        public void ChannelWentOffline(ListViewItem item)
        {
            this.Invoke(new Action(() => item.BackColor = Color.White));

            foreach (ToolStripMenuItem toolStripMenuitem1 in contextMenuStrip1.Items)
            {
                foreach (ToolStripMenuItem toolStripMenuitem2 in toolStripMenuitem1.DropDownItems)
                {
                    if (toolStripMenuitem2.Text == item.Text)
                    {
                        toolStripMenuitem2.BackColor = Color.White;
                    }
                }
            }
        }

        public void PrintBalloon(ListViewItem item)
        {
            if (checkBox3.Checked)
            {
                notifyIcon1.BalloonTipTitle = "Stream is Live";
                notifyIcon1.BalloonTipText = item.Text;
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(5000);
            }
        }

        public void GetNewVersionNewThread()
        {
            WebClient client = new WebClient();

            try
            {
                string amsStatsString = client.DownloadString("http://dedick.podkolpakom.net/stats/ams/gib_stats.php");

                amsStatsString = amsStatsString.Replace("\"", "");
                amsStatsString = amsStatsString.Replace("{", "");
                amsStatsString = amsStatsString.Replace("}", "");

                List<string> amsStatsList = amsStatsString.Split(',').ToList();

                foreach (string X in amsStatsList)
                {
                    if (X.Contains("KolpaqueClientVersion"))
                    {
                        newClientVersion = Double.Parse(X.Replace("KolpaqueClientVersion:", ""));
                    }
                }

                if (newClientVersion > clientVersion)
                {
                    this.Invoke(new Action(() => linkLabel3.Visible = true));

                    if (!newVersionBalloonShown)
                    {
                        notifyIcon1.BalloonTipTitle = "New Version Available";
                        notifyIcon1.BalloonTipText = "ftp://podkolpakom.net:359/KolpaqueClient.exe";
                        notifyIcon1.Visible = true;
                        notifyIcon1.ShowBalloonTip(5000);

                        newVersionBalloonShown = true;
                    }
                }
                else
                {
                    if (linkLabel3.Visible)
                    {
                        this.Invoke(new Action(() => linkLabel3.Visible = false));
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public void GetStatsPerItem(ListViewItem item, bool showBalloon, int twitchCooldownLocal)
        {
            string S = item.Text;
            
            if (S.Contains("podkolpakom.net") && !S.Contains("vps."))
            {
                S = S.Replace("rtmp://", "");
                S = S.Replace("dedick.", "");
                
                Thread NewThread = new Thread(() => GetPoddyStatsNewThread(item, S, showBalloon));
                NewThread.Start();
            }

            if (S.Contains("podkolpakom.net") && S.Contains("vps."))
            {
                S = S.Replace("rtmp://", "");
                S = S.Replace("vps.", "");

                Thread NewThread = new Thread(() => GetPoddyVpsStatsNewThread(item, S, showBalloon));
                NewThread.Start();
            }
            
            if (S.Contains("twitch.tv"))
            {
                if (twitchCooldownLocal == 0)
                {
                    S = S.Replace("http://", "");
                    S = S.Replace("https://", "");
                    S = S.Replace("www.", "");
                    
                    Thread NewThread = new Thread(() => GetTwitchStatsNewThread(item, S, showBalloon));
                    NewThread.Start();
                }
            }
        }

        public void GetStats(bool showBalloon)
        {
            if (twitchCooldown > 5)
            {
                twitchCooldown = 0;
            }

            int twitchCooldownLocal = twitchCooldown;

            foreach (ListViewItem item in listView2.Items)
            {
                Thread NewThread = new Thread(() => GetStatsPerItem(item, showBalloon, twitchCooldownLocal));
                NewThread.Start();
            }

            twitchCooldown++;

            Thread NewVersionThread = new Thread(() => GetNewVersionNewThread());
            NewVersionThread.Start();
        }

        public void PlayStream(ListViewItem X)
        {
                string commandLine = "";

                if (poddyChannelsList.Contains(X.Text))
                {
                    commandLine = "\"" + X.Text + " live=1\"" + " best";

                    if (checkBox1.Checked)
                    {
                        commandLine = commandLine.Replace("podkolpakom.net/live", "podkolpakom.net/restream");
                    }

                    if (checkBox2.Checked)
                    {
                        System.Diagnostics.Process.Start(poddyChannelsChatList[poddyChannelsList.IndexOf(X.Text)]);
                    }
                }
                else
                {
                    if (checkBox1.Checked)
                    {
                        commandLine = "\"" + X.Text + "\"" + " high";
                    }
                    else
                    {
                        commandLine = "\"" + X.Text + "\"" + " best";
                    }

                    if (X.Text.Contains("http") && checkBox2.Checked)
                    {
                        System.Diagnostics.Process.Start(X.Text);
                    }
                }

                commandLine = commandLine.Replace("https://","http://");

                if (File.Exists(livestreamerPath))
                {
                    Process myProcess = new Process();

                    myProcess.StartInfo.FileName = livestreamerPath;
                    myProcess.StartInfo.Arguments = commandLine;
                    myProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    myProcess.Start();
                }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveIniFile();
            RefreshInterface();

            foreach (ListViewItem X in listView2.CheckedItems)
            {
                PlayStream(X);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "" && !listView2.Items.Cast<ListViewItem>().Select(X => X.Text).Contains(textBox3.Text.Replace(" ", "")))
            {
                listView2.Items.Add(textBox3.Text.Replace(" ",""));
                customChannelsToolStripMenuItem.DropDownItems.Add(textBox3.Text.Replace(" ", ""), null, new EventHandler(contextMenu_Click));
                
                SaveIniFile();
                RefreshInterface();
            }

            textBox3.Text = "";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://podkolpakom.net/");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://podkolpakom.net/stream/admin/");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GetStats(true);
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            if (notifyIcon1.BalloonTipTitle == "Stream is Live")
            {
                PlayStream(new ListViewItem(notifyIcon1.BalloonTipText));
            }
            if (notifyIcon1.BalloonTipTitle == "New Version Available")
            {
                System.Diagnostics.Process.Start("ftp://podkolpakom.net:359/KolpaqueClient.exe");
            }
        }

        private void contextMenu_Click(object sender, EventArgs e)
        {
            PlayStream(new ListViewItem(sender.ToString()));
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Minimized;
            }
            }
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            SaveIniFile();
            RefreshInterface();
        }

        private void checkBox3_Click(object sender, EventArgs e)
        {
            SaveIniFile();
            RefreshInterface();
        }

        private void checkBox2_Click(object sender, EventArgs e)
        {
            SaveIniFile();
            RefreshInterface();
        }

        private void checkBox4_Click(object sender, EventArgs e)
        {
            SaveIniFile();
            RefreshInterface();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("ftp://podkolpakom.net:359/KolpaqueClient.exe");
        }

        private void closeClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void playFromClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.GetText().Contains("http") || Clipboard.GetText().Contains("rtmp"))
            {
                PlayStream(new ListViewItem(Clipboard.GetText()));

                notifyIcon1.BalloonTipTitle = "Launching the Stream";
                notifyIcon1.BalloonTipText = Clipboard.GetText();
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(5000);
            }            
        }

        private void listView2_MouseClick(object sender, MouseEventArgs e)
        {
            listView2LastSelectedItem = listView2.SelectedItems[0];
            
            if (e.Button == MouseButtons.Right)
            {
                if (poddyChannelsList.Contains(listView2LastSelectedItem.Text))
                {
                    removeChannelToolStripMenuItem.Visible = false;
                }
                else
                {
                    removeChannelToolStripMenuItem.Visible = true;
                }

                var hitTestInfo = listView2.HitTest(e.X, e.Y);

                if (hitTestInfo.Item != null)
                {
                    var loc = e.Location;
                    loc.Offset(listView2.Location);

                    this.contextMenuStrip2.Show(this, loc);
                }
            }

            if (e.Button == MouseButtons.Left)
            {
                listView2LastSelectedItem.Selected = false;
            }
        }

        private void playStreamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlayStream(listView2LastSelectedItem);
        }

        private void Form1_ClientSizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }            
        }

        private void checkBox5_Click(object sender, EventArgs e)
        {
            SaveIniFile();
            RefreshInterface();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }

        private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(listView2LastSelectedItem.Text);
        }

        private void removeChannelToolStripMenuItem_Click(object sender, EventArgs e)
        {
                for (int i = 0; i < customChannelsToolStripMenuItem.DropDownItems.Count; i++)
                {
                    if (customChannelsToolStripMenuItem.DropDownItems[i].Text.Contains(listView2LastSelectedItem.Text))
                    {
                        customChannelsToolStripMenuItem.DropDownItems.RemoveAt(i);
                    }
                }

                listView2LastSelectedItem.Remove();

                SaveIniFile();
                RefreshInterface();
        }

        private void openChatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (poddyChannelsList.Contains(listView2LastSelectedItem.Text))
            {
                System.Diagnostics.Process.Start(poddyChannelsChatList[poddyChannelsList.IndexOf(listView2LastSelectedItem.Text)]);
            }

            if (listView2LastSelectedItem.Text.Contains("http"))
            {
                System.Diagnostics.Process.Start(listView2LastSelectedItem.Text);
            }
        }

        private void listView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PlayStream(listView2LastSelectedItem);

                notifyIcon1.BalloonTipTitle = "Launching the Stream";
                notifyIcon1.BalloonTipText = listView2LastSelectedItem.Text;
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(5000);
            }
        }

        private void contextMenuStrip2_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            listView2LastSelectedItem.Selected = false;
        }
    }
}