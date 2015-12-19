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

            livestreamerPath_textBox.Text = "C:\\Program Files (x86)\\Livestreamer\\livestreamer.exe";
            
            xmlPath_textBox.Text = xmlFilePath;

            if (File.Exists(iniFilePath))
            {
                ReadIniFile();
                SaveXmlFile();
                File.Delete(iniFilePath);
            }
            
            if (File.Exists(xmlFilePath))
            {      
                ReadXmlFile();
            }
            else
            {
                SaveXmlFile();
            }

            if (!File.Exists(livestreamerPath_textBox.Text))
            {
                livestreamerPath_textBox.Enabled = true;
                livestreamerPath_textBox.Text = "https://github.com/chrippa/livestreamer/releases";
            }

            newClientVersion = clientVersion;

            label2.Text = "Version " + clientVersion.ToString();

            GetStats(false);

            Thread NewVersionThread = new Thread(() => GetNewVersionNewThread());
            NewVersionThread.Start();
        }

        string iniFilePath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\KolpaqueClient.ini";
        string xmlFilePath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\KolpaqueClient.xml";
        List<string> poddyChannelsList = new List<string>(new string[] { "rtmp://dedick.podkolpakom.net/live/liveevent", "rtmp://dedick.podkolpakom.net/live/tvstream", "rtmp://dedick.podkolpakom.net/live/murshun", "rtmp://vps.podkolpakom.net/live/liveevent" });
        List<string> poddyChannelsChatList = new List<string>(new string[] { "http://podkolpakom.net/stream/admin/", "http://podkolpakom.net/tv/admin/", "http://podkolpakom.net/murshun/admin/", "http://vps.podkolpakom.net/" });
        int twitchCooldown = 0;
        
        double clientVersion = 0.261;
        double newClientVersion;
        string newClientVersionLink = "https://github.com/rebelvg/KolpaqueClient/releases";

        bool newVersionBalloonShown = false;
        bool channelsListViewIsActive;

        ListViewItem listView2LastSelectedItem;

        KolpaqueClientXmlSettings ClientSettings;

        public void ReadIniFile()
        {
            foreach (string X in poddyChannelsList)
            {
                if (!channels_listView.Items.Cast<ListViewItem>().Select(x => x.Text).Contains(X))
                {
                    channels_listView.Items.Add(X);
                    poddyChannelsToolStripMenuItem.DropDownItems.Add(X, null, new EventHandler(contextMenu_Click));
                }
            }

            string[] infoFromIniFile = File.ReadAllLines(iniFilePath);
                        
            foreach (string X in infoFromIniFile)
            {
                if (X.Contains("CUSTOMCHANNEL="))
                {
                    if (!channels_listView.Items.Cast<ListViewItem>().Select(x => x.Text).Contains(X))
                    {
                        channels_listView.Items.Add(X.Replace("CUSTOMCHANNEL=", ""));
                        customChannelsToolStripMenuItem.DropDownItems.Add(X.Replace("CUSTOMCHANNEL=", ""), null, new EventHandler(contextMenu_Click));
                    }
                }
            }
        }

        public class KolpaqueClientXmlSettings
        {
            public string livestreamerPath_textBox;
            public bool LQ_checkBox;
            public bool openChat_checkBox;
            public bool notifications_checkBox;
            public bool autoPlay_checkBox;
            public List<string> channels_listView;
            public bool minimizeAtStart_checkBox;
            public int channels_listView_ColumnWidth;
            public int[] form1_size;
        }

        public void ReadXmlFile()
        {
            foreach (string X in poddyChannelsList)
            {
                if (!channels_listView.Items.Cast<ListViewItem>().Select(x => x.Text).Contains(X))
                {
                    channels_listView.Items.Add(X);
                    poddyChannelsToolStripMenuItem.DropDownItems.Add(X, null, new EventHandler(contextMenu_Click));
                }
            }

            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(KolpaqueClientXmlSettings));

            StreamReader reader = new StreamReader(xmlFilePath);

            try
            {
                ClientSettings = (KolpaqueClientXmlSettings)serializer.Deserialize(reader);
                reader.Close();

                if (ClientSettings.livestreamerPath_textBox != "")
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
                if (ClientSettings.channels_listView_ColumnWidth != 0)
                    columnHeader2.Width = ClientSettings.channels_listView_ColumnWidth;               
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
                    System.Environment.Exit(1);
                }
            }
        }
        
        public void SaveXmlFile()
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

            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(KolpaqueClientXmlSettings));

            System.IO.FileStream writer = System.IO.File.Create(xmlFilePath);
            serializer.Serialize(writer, ClientSettings);
            writer.Close();
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

            }
        }

        public async void ChannelWentOnline(ListViewItem item, bool showBalloon)
        {
            if (item.BackColor != Color.Green)
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
                    await PrintBalloon(item);
                }

                if (autoPlay_checkBox.Checked)
                {
                    PlayStream(item);
                }
            }
        }

        public void ChannelWentOffline(ListViewItem item)
        {
            if (item.BackColor != default(Color))
            {
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

        public async Task PrintBalloon(ListViewItem item)
        {
            if (notifications_checkBox.Checked)
            {
                notifyIcon1.BalloonTipTitle = "Stream is Live";
                notifyIcon1.BalloonTipText = item.Text;
                notifyIcon1.ShowBalloonTip(5000);
                
                await Task.Delay(TimeSpan.FromSeconds(5));

                try
                {
                    notifyIcon1.BalloonTipTitle = "";
                    notifyIcon1.BalloonTipText = "";
                }
                catch
                {

                }
            }
        }

        public async void GetNewVersionNewThread()
        {
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
            
                newClientVersion = Double.Parse(gitHubAPIStats[0].tag_name.ToString());

                newClientVersionLink = gitHubAPIStats[0].assets[0].browser_download_url;

                if (newClientVersion > clientVersion)
                {
                    this.Invoke(new Action(() => linkLabel3.Visible = true));

                    if (!newVersionBalloonShown)
                    {
                        notifyIcon1.BalloonTipTitle = "New Version Available";
                        notifyIcon1.BalloonTipText = newClientVersionLink;
                        notifyIcon1.ShowBalloonTip(5000);

                        newVersionBalloonShown = true;

                        await Task.Delay(TimeSpan.FromSeconds(5));

                        try
                        {
                            notifyIcon1.BalloonTipTitle = "";
                            notifyIcon1.BalloonTipText = "";
                        }
                        catch
                        {

                        }
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

            foreach (ListViewItem item in channels_listView.Items)
            {
                Thread NewThread = new Thread(() => GetStatsPerItem(item, showBalloon, twitchCooldownLocal));
                NewThread.Start();
            }

            twitchCooldown++;
        }

        public void PlayStream(ListViewItem X)
        {
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

                commandLine = commandLine.Replace("https://","http://");

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

        private void button2_Click(object sender, EventArgs e)
        {
            if (addChannel_textBox.Text != "" && !channels_listView.Items.Cast<ListViewItem>().Select(X => X.Text).Contains(addChannel_textBox.Text.Replace(" ", "")))
            {
                channels_listView.Items.Add(addChannel_textBox.Text.Replace(" ",""));
                customChannelsToolStripMenuItem.DropDownItems.Add(addChannel_textBox.Text.Replace(" ", ""), null, new EventHandler(contextMenu_Click));
                
                SaveXmlFile();
            }

            addChannel_textBox.Text = "";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://podkolpakom.net/");
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
                System.Diagnostics.Process.Start(notifyIcon1.BalloonTipText);
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
            SaveXmlFile();
        }

        private void checkBox3_Click(object sender, EventArgs e)
        {
            SaveXmlFile();
        }

        private void checkBox2_Click(object sender, EventArgs e)
        {
            SaveXmlFile();
        }

        private void checkBox4_Click(object sender, EventArgs e)
        {
            SaveXmlFile();
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
                PlayStream(new ListViewItem(Clipboard.GetText()));

                notifyIcon1.BalloonTipTitle = "Launching the Stream";
                notifyIcon1.BalloonTipText = Clipboard.GetText();
                notifyIcon1.ShowBalloonTip(5000);
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
            SaveXmlFile();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (minimizeAtStart_checkBox.Checked)
            {
                this.WindowState = FormWindowState.Minimized;
            }

            if (ClientSettings.form1_size[0] != 0)
                Form1.ActiveForm.Height = ClientSettings.form1_size[0];
            if (ClientSettings.form1_size[1] != 0)
                Form1.ActiveForm.Width = ClientSettings.form1_size[1];
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

            SaveXmlFile();
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
                notifyIcon1.ShowBalloonTip(5000);
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

            SaveXmlFile();
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
            channelsListViewIsActive = true;
        }

        private void channels_listView_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            if (channelsListViewIsActive)
                SaveXmlFile();

            channelsListViewIsActive = false;
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            SaveXmlFile();
        }
    }
}
