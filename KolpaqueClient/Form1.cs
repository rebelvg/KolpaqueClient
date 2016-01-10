using System;
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
using Newtonsoft.Json;

namespace KolpaqueClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            if (Process.GetProcessesByName("KolpaqueClient").Length > 1)
            {
                MessageBox.Show("Client is already running.");
                notifyIcon1.Visible = false;
                System.Environment.Exit(1);       
            }

            xmlFilePath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\KolpaqueClient.xml";
            xmlPath_textBox.Text = xmlFilePath;

            logFilePath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\KolpaqueClient.log";

            poddyChannelsList = new List<string>(new string[] { "rtmp://dedick.podkolpakom.net/live/liveevent", "rtmp://dedick.podkolpakom.net/live/tvstream", "rtmp://dedick.podkolpakom.net/live/murshun", "rtmp://vps.podkolpakom.net/live/liveevent" });
            poddyChannelsChatList = new List<string>(new string[] { "http://podkolpakom.net/stream/admin/", "http://podkolpakom.net/tv/admin/", "http://podkolpakom.net/murshun/admin/", "http://vps.podkolpakom.net/" });
            
            clientVersion = "0.267";

            foreach (string X in poddyChannelsList)
            {
                if (!channels_listView.Items.Cast<ListViewItem>().Select(x => x.Text).Contains(X))
                {
                    channels_listView.Items.Add(X);
                    poddyChannelsToolStripMenuItem.DropDownItems.Add(X, null, new EventHandler(contextMenu_Click));
                }
            }
            
            if (File.Exists(xmlFilePath))
            {
                ReadXmlFile();
            }
            else
            {
                try
                {
                    ClientSettings = new KolpaqueClientXmlSettings();

                    System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(KolpaqueClientXmlSettings));

                    System.IO.FileStream writer = System.IO.File.Create(xmlFilePath);
                    serializer.Serialize(writer, ClientSettings);
                    writer.Close();

                    ReadXmlFile();
                }
                catch
                {
                    MessageBox.Show("Saving xml settings failed.");
                }
            }

            if (!File.Exists(livestreamerPath_textBox.Text))
            {
                livestreamerPath_textBox.Enabled = true;
                livestreamerPath_textBox.Text = "https://github.com/chrippa/livestreamer/releases";
            }

            label2.Text = "Version " + clientVersion;

            GetStats(false, 0);

            Thread NewVersionThread = new Thread(() => GetNewVersionNewThread());
            NewVersionThread.Start();
        }

        string xmlFilePath;
        string clientVersion;
        string newClientVersionLink;
        string logFilePath;

        List<string> poddyChannelsList;
        List<string> poddyChannelsChatList;
        
        bool newVersionBalloonShown;
        bool ignoreUpdates;
        bool debugLog;

        DateTime balloonLastShown;

        ListViewItem listView2LastSelectedItem;

        KolpaqueClientXmlSettings ClientSettings;

        public class KolpaqueClientXmlSettings
        {
            public string livestreamerPath_textBox = "C:\\Program Files (x86)\\Livestreamer\\livestreamer.exe";
            public bool LQ_checkBox;
            public bool openChat_checkBox;
            public bool notifications_checkBox = true;
            public bool autoPlay_checkBox;
            public List<string> channels_listView;
            public bool minimizeAtStart_checkBox;
            public int channels_listView_ColumnWidth = 348;
            public int[] form1_size = {400, 667};
            public bool ignoreUpdates;
            public bool debugLog;
        }

        public void ReadXmlFile()
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(KolpaqueClientXmlSettings));

            StreamReader reader = new StreamReader(xmlFilePath);

            try
            {
                ClientSettings = (KolpaqueClientXmlSettings)serializer.Deserialize(reader);
                reader.Close();
                
                livestreamerPath_textBox.Text = ClientSettings.livestreamerPath_textBox;
                LQ_checkBox.Checked = ClientSettings.LQ_checkBox;
                openChat_checkBox.Checked = ClientSettings.openChat_checkBox;
                notifications_checkBox.Checked = ClientSettings.notifications_checkBox;
                autoPlay_checkBox.Checked = ClientSettings.autoPlay_checkBox;

                foreach (string X in ClientSettings.channels_listView)
                {
                    if (!channels_listView.Items.Cast<ListViewItem>().Select(x => x.Text).Contains(X))
                    {
                        channels_listView.Items.Add(X);
                        customChannelsToolStripMenuItem.DropDownItems.Add(X, null, new EventHandler(contextMenu_Click));
                    }
                }

                minimizeAtStart_checkBox.Checked = ClientSettings.minimizeAtStart_checkBox;
                columnHeader2.Width = ClientSettings.channels_listView_ColumnWidth;
                ignoreUpdates = ClientSettings.ignoreUpdates;
                debugLog = ClientSettings.debugLog;
            }
            catch
            {
                reader.Close();

                DialogResult dialogResult = MessageBox.Show("Create a new one?", "Xml file is corrupted.", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    SaveXmlFile();
                }
                if (dialogResult == DialogResult.No)
                {
                    notifyIcon1.Visible = false;
                    System.Environment.Exit(1);
                }
            }
        }
        
        public void SaveXmlFile()
        {
            try
            {
                ClientSettings = new KolpaqueClientXmlSettings();

                ClientSettings.livestreamerPath_textBox = livestreamerPath_textBox.Text;
                ClientSettings.LQ_checkBox = LQ_checkBox.Checked;
                ClientSettings.openChat_checkBox = openChat_checkBox.Checked;
                ClientSettings.notifications_checkBox = notifications_checkBox.Checked;
                ClientSettings.autoPlay_checkBox = autoPlay_checkBox.Checked;
                ClientSettings.channels_listView = channels_listView.Items.Cast<ListViewItem>().Select(x => x.Text).Where(s => !poddyChannelsList.Contains(s)).ToList();
                ClientSettings.minimizeAtStart_checkBox = minimizeAtStart_checkBox.Checked;
                ClientSettings.channels_listView_ColumnWidth = columnHeader2.Width;
                ClientSettings.form1_size = new int[] { this.Width, this.Height };
                ClientSettings.ignoreUpdates = ignoreUpdates;
                ClientSettings.debugLog = debugLog;

                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(KolpaqueClientXmlSettings));

                System.IO.FileStream writer = System.IO.File.Create(xmlFilePath);
                serializer.Serialize(writer, ClientSettings);
                writer.Close();
            }
            catch
            {
                MessageBox.Show("Saving xml settings failed.");
            }
        }

        public void GetPoddyStatsNewThread(ListViewItem item, string S, bool showBalloon)
        {
            WebClient client = new WebClient();

            try
            {
                string amsStatsString = client.DownloadString("http://dedick.podkolpakom.net/stats/ams/gib_stats.php?stream=" + S.Replace("podkolpakom.net/live/", ""));

                dynamic amsStatsJSON = JsonConvert.DeserializeObject(amsStatsString);
                
                if (amsStatsJSON.live.ToString() == "Online")
                {
                        ChannelWentOnline(item, showBalloon);
                }
                if (amsStatsJSON.live.ToString() == "Offline")
                {
                        ChannelWentOffline(item);
                }
            }
            catch (Exception)
            {
                writeLog("GetPoddyStatsNewThread Crashed " + S);
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
                            ChannelWentOnline(item, showBalloon);
                        }
                        if (!X.Contains("publishing/"))
                        {
                            ChannelWentOffline(item);
                        }
                    }
                    else
                    {
                        ChannelWentOffline(item);
                    }
                }
            }
            catch (Exception)
            {
                writeLog("GetPoddyVpsStatsNewThread Crashed " + S);
            }
        }

        public void GetTwitchStatsNewThread(ListViewItem item, string S, bool showBalloon)
        {
            WebClient client = new WebClient();

            try
            {
                string twitchStatsString = client.DownloadString("https://api.twitch.tv/kraken/streams?channel=" + S.Replace("twitch.tv/", ""));

                dynamic twitchAPIStats = JsonConvert.DeserializeObject(twitchStatsString);

                if (twitchAPIStats.streams.Count > 0)
                {
                    ChannelWentOnline(item, showBalloon);
                }
                if (twitchAPIStats.streams.Count == 0)
                {
                    ChannelWentOffline(item);
                }
            }
            catch
            {
                writeLog("GetTwitchStatsNewThread Crashed " + S);
            }
        }

        public void ChannelWentOnline(ListViewItem item, bool showBalloon)
        {
            if (item.BackColor != Color.Green)
            {
                writeLog("ChannelWentOnline " + item.Text);

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
                    PrintBalloon("Stream is Live (" + DateTime.Now + ")", item.Text);
                }

                if (autoPlay_checkBox.Checked)
                {
                    PlayStream(item);
                }
            }
        }

        public void ChannelWentOffline(ListViewItem item)
        {
            if (item.BackColor == Color.Green)
            {
                writeLog("ChannelWentOffline " + item.Text);

                this.Invoke(new Action(() => item.BackColor = default(Color)));

                foreach (ToolStripMenuItem toolStripMenuitem1 in contextMenuStrip1.Items)
                {
                    foreach (ToolStripMenuItem toolStripMenuitem2 in toolStripMenuitem1.DropDownItems)
                    {
                        if (toolStripMenuitem2.Text == item.Text)
                        {
                            toolStripMenuitem2.BackColor = default(Color);
                        }
                    }
                }
            }
        }

        public void PrintBalloon(string balloonTitle, string balloonText)
        {
            writeLog("PrintBalloon " + balloonTitle + " " + balloonText + " " + notifications_checkBox.Checked);

            if (notifications_checkBox.Checked)
            {
                notifyIcon1.BalloonTipTitle = balloonTitle;
                notifyIcon1.BalloonTipText = balloonText;
                notifyIcon1.ShowBalloonTip(10000);

                balloonLastShown = DateTime.Now;
            }
        }

        public void GetNewVersionNewThread()
        {
            if (ignoreUpdates)
                return;

            try
            {
                HttpWebRequest request = WebRequest.Create("https://api.github.com/repos/rebelvg/KolpaqueClient/releases") as HttpWebRequest;

                request.UserAgent = "KolpaqueClient";

                string gitHubApiString;
                        
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        gitHubApiString = reader.ReadToEnd();
                    }
                }

                dynamic gitHubAPIStats = JsonConvert.DeserializeObject(gitHubApiString);
            
                string newClientVersion = gitHubAPIStats[0].tag_name;

                newClientVersionLink = gitHubAPIStats[0].assets[0].browser_download_url;

                if (newClientVersion != clientVersion)
                {
                    this.Invoke(new Action(() => linkLabel3.Visible = true));

                    if (!newVersionBalloonShown)
                    {
                        PrintBalloon("New Version Available", newClientVersionLink);

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
            catch
            {
                writeLog("GetNewVersionNewThread Crashed");
            }
        }

        public void GetStatsPerItem(ListViewItem item, bool showBalloon, int schedule)
        {
            string S = item.Text;

            if (schedule == 0 || schedule == 1)
            {
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
            }

            if (schedule == 0 || schedule == 2)
            {
                if (S.Contains("twitch.tv"))
                {
                    S = S.Replace("http://", "");
                    S = S.Replace("https://", "");
                    S = S.Replace("www.", "");

                    Thread NewThread = new Thread(() => GetTwitchStatsNewThread(item, S, showBalloon));
                    NewThread.Start();
                }
            }
        }

        public void GetStats(bool showBalloon, int schedule)
        {
            foreach (ListViewItem item in channels_listView.Items)
            {
                Thread NewThread = new Thread(() => GetStatsPerItem(item, showBalloon, schedule));
                NewThread.Start();
            }
        }

        public void PlayStream(ListViewItem X)
        {
            writeLog("PlayStream " + X.Text);

            string commandLine = "";

            if (poddyChannelsList.Contains(X.Text))
            {
                commandLine = "\"" + X.Text + " live=1\"" + " best";

                if (LQ_checkBox.Checked)
                {
                    commandLine = commandLine.Replace("podkolpakom.net/live", "podkolpakom.net/restream");
                }

                if (openChat_checkBox.Checked)
                {
                    System.Diagnostics.Process.Start(poddyChannelsChatList[poddyChannelsList.IndexOf(X.Text)]);
                }
            }
            else
            {
                if (LQ_checkBox.Checked)
                {
                    commandLine = "\"" + X.Text + "\"" + " high";
                }
                else
                {
                    commandLine = "\"" + X.Text + "\"" + " best";
                }

                if (X.Text.Contains("http") && openChat_checkBox.Checked)
                {
                    System.Diagnostics.Process.Start(X.Text);
                }
            }

            commandLine = commandLine.Replace("https://", "http://");

            if (File.Exists(livestreamerPath_textBox.Text))
            {
                Process myProcess = new Process();

                myProcess.StartInfo.FileName = livestreamerPath_textBox.Text;
                myProcess.StartInfo.Arguments = commandLine;
                myProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                myProcess.Start();
            }
            else
            {
                MessageBox.Show("livestreamer.exe not found.");
            }
        }

        public void writeLog(string log)
        {
            if (!debugLog)
                return;

            log = DateTime.Now.ToString() + " " + log;
            
            if (!File.Exists(logFilePath))
            {
                try
                {
                    System.IO.StreamWriter file = new System.IO.StreamWriter(logFilePath, true);
                    file.WriteLine(log);
                    file.Close();
                }
                catch
                {

                }
            }
            else
            {
                try
                {
                    System.IO.StreamWriter file = new System.IO.StreamWriter(logFilePath, true);
                    file.WriteLine(log);
                    file.Close();
                }
                catch
                {

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (addChannel_textBox.Text != "" && !channels_listView.Items.Cast<ListViewItem>().Select(X => X.Text).Contains(addChannel_textBox.Text.Replace(" ", "")))
            {
                channels_listView.Items.Add(addChannel_textBox.Text.Replace(" ",""));
                customChannelsToolStripMenuItem.DropDownItems.Add(addChannel_textBox.Text.Replace(" ", ""), null, new EventHandler(contextMenu_Click));
            }

            addChannel_textBox.Text = "";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://podkolpakom.net/");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GetStats(true, 1);
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            writeLog("notifyIcon1_BalloonTipClicked");

            TimeSpan timeSpanLastBalloonShown;

            timeSpanLastBalloonShown = DateTime.Now - balloonLastShown;

            if (timeSpanLastBalloonShown.Seconds > 9)
                return;

            if (notifyIcon1.BalloonTipTitle.Contains("Stream is Live"))
            {
                PlayStream(new ListViewItem(notifyIcon1.BalloonTipText));
            }
            if (notifyIcon1.BalloonTipTitle.Contains("New Version Available"))
            {
                System.Diagnostics.Process.Start(notifyIcon1.BalloonTipText);
            }
        }

        private void contextMenu_Click(object sender, EventArgs e)
        {
            PlayStream(new ListViewItem(sender.ToString()));
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            writeLog("notifyIcon1_MouseClick " + e.Button);

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

        }

        private void checkBox3_Click(object sender, EventArgs e)
        {

        }

        private void checkBox2_Click(object sender, EventArgs e)
        {

        }

        private void checkBox4_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(newClientVersionLink);
        }

        private void closeClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void playFromClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.GetText().Contains("http") || Clipboard.GetText().Contains("rtmp"))
            {
                PrintBalloon("Launching the Stream", Clipboard.GetText());

                PlayStream(new ListViewItem(Clipboard.GetText()));
            }            
        }

        private void listView2_MouseClick(object sender, MouseEventArgs e)
        {
            listView2LastSelectedItem = channels_listView.SelectedItems[0];
            
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

                var hitTestInfo = channels_listView.HitTest(e.X, e.Y);

                if (hitTestInfo.Item != null)
                {
                    var loc = e.Location;
                    loc.Offset(channels_listView.Location);

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

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Width = ClientSettings.form1_size[0];            
            this.Height = ClientSettings.form1_size[1];

            if (minimizeAtStart_checkBox.Checked)
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

                PrintBalloon("Launching the Stream", listView2LastSelectedItem.Text);
            }
        }

        private void contextMenuStrip2_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            listView2LastSelectedItem.Selected = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog selectExePath = new OpenFileDialog();

            selectExePath.Title = "Select livestreamer.exe";
            selectExePath.Filter = "Executable File (.exe) | *.exe";
            selectExePath.RestoreDirectory = true;

            if (selectExePath.ShowDialog() == DialogResult.OK)
            {
                livestreamerPath_textBox.Enabled = false;
                livestreamerPath_textBox.Text = selectExePath.FileName;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Thread NewVersionThread = new Thread(() => GetNewVersionNewThread());
            NewVersionThread.Start();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/rebelvg/KolpaqueClient/releases");
        }

        private void channels_listView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {

        }

        private void channels_listView_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {

        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveXmlFile();
        }

        private void thirtySecTimer_Tick(object sender, EventArgs e)
        {
            GetStats(true, 2);
        }
    }
}
