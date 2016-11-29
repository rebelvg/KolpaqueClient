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
using System.Drawing.Imaging;

namespace KolpaqueClient
{
    public partial class Form1 : Form
    {
        public void ReadXmlFile()
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(KolpaqueClientXmlSettings));

            StreamReader reader = new StreamReader(xmlPath_textBox.Text);

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
                launchStreamOnBalloonClick_checkBox.Checked = ClientSettings.launchStreamOnBalloonClick_checkBox;
                debugMode = ClientSettings.debugMode;
                enableLog_checkBox.Checked = ClientSettings.enableLog;
                screenshotsPath_textBox.Text = ClientSettings.screenshotsPath_textBox;
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
                ClientSettings.launchStreamOnBalloonClick_checkBox = launchStreamOnBalloonClick_checkBox.Checked;
                ClientSettings.debugMode = debugMode;
                ClientSettings.enableLog = enableLog_checkBox.Checked;
                ClientSettings.screenshotsPath_textBox = screenshotsPath_textBox.Text;

                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(KolpaqueClientXmlSettings));

                System.IO.FileStream writer = System.IO.File.Create(xmlPath_textBox.Text);
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
            catch
            {
                WriteLog("GetPoddyStatsNewThread Crashed " + S);
            }
        }

        public void GetPoddyMenStatsNewThread(ListViewItem item, string S, bool showBalloon)
        {
            WebClient client = new WebClient();

            try
            {
                string amsStatsString = client.DownloadString("http://main.klpq.men/stats/ams/gib_stats.php?stream=" + S.Replace("klpq.men/live/", ""));

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
            catch
            {
                WriteLog("GetPoddyStatsNewThread Crashed " + S);
            }
        }

        public void GetTwitchStatsNewThread(ListViewItem item, string S, bool showBalloon)
        {
            WebClient client = new WebClient();

            try
            {
                string twitchStatsString = client.DownloadString("https://api.twitch.tv/kraken/streams?channel=" + S.Replace("twitch.tv/", "") + "&client_id=dk330061dv4t81s21utnhhdona0a91x");

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
                WriteLog("GetTwitchStatsNewThread Crashed " + S);
            }
        }

        public void ChannelWentOnline(ListViewItem item, bool showBalloon)
        {
            if (item.BackColor != Color.Green)
            {
                WriteLog("ChannelWentOnline " + item.Text);

                if (offlineChannelsDictionary.ContainsKey(item.Text))
                {
                    offlineChannelsDictionary.Remove(item.Text);
                }

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
                    PlayStream(item, "autoPlay");
                }
            }
        }

        public void ChannelWentOffline(ListViewItem item)
        {
            if (item.BackColor == Color.Green)
            {
                WriteLog("ChannelWentOffline " + item.Text);

                if (offlineChannelsDictionary.ContainsKey(item.Text))
                {
                    offlineChannelsDictionary[item.Text] = offlineChannelsDictionary[item.Text] + 1;

                    if (offlineChannelsDictionary[item.Text] == 3)
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

                        offlineChannelsDictionary.Remove(item.Text);
                    }
                }
                else
                {
                    offlineChannelsDictionary.Add(item.Text, 1);
                }
            }
        }

        public void PrintBalloon(string balloonTitle, string balloonText)
        {
            WriteLog("PrintBalloon " + balloonTitle + " " + balloonText + " " + notifications_checkBox.Checked);

            if (notifications_checkBox.Checked)
            {
                notifyIcon1.BalloonTipTitle = balloonTitle;
                notifyIcon1.BalloonTipText = balloonText;
                notifyIcon1.ShowBalloonTip(15000);

                lastBalloonPrint = 0;
            }
        }

        public void GetNewVersionNewThread()
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

                string newClientVersion = gitHubAPIStats[0].tag_name;

                string newClientVersionLink = gitHubAPIStats[0].assets[0].browser_download_url;

                if (newClientVersion != clientVersion && !debugMode)
                {
                    this.Invoke(new Action(() => linkLabel3.Visible = true));

                    PrintBalloon("New Version Available", newClientVersionLink);

                    clientVersion = newClientVersion;
                }
            }
            catch
            {
                WriteLog("GetNewVersionNewThread Crashed");
            }
        }

        public void GetStatsPerItem(ListViewItem item, bool showBalloon, int schedule)
        {
            string S = item.Text;

            if (schedule == 0 || schedule == 1)
            {
                if (S.Contains("dedick.podkolpakom.net"))
                {
                    S = S.Replace("rtmp://", "");
                    S = S.Replace("dedick.", "");

                    Thread NewThread = new Thread(() => GetPoddyStatsNewThread(item, S, showBalloon));
                    NewThread.Start();
                }

                if (S.Contains("main.klpq.men"))
                {
                    S = S.Replace("rtmp://", "");
                    S = S.Replace("main.", "");

                    Thread NewThread = new Thread(() => GetPoddyMenStatsNewThread(item, S, showBalloon));
                    NewThread.Start();
                }
            }

            if (schedule == 0 || schedule == 2)
            {
                if (S.Contains("twitch.tv"))
                {
                    S = S.Replace("https://", "");
                    S = S.Replace("http://", "");
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

        public void PlayStream(ListViewItem X, string whoCalled)
        {
            WriteLog("PlayStream " + X.Text + " " + whoCalled);

            string commandLine = "";

            if (X.Text.Contains("podkolpakom.net") || X.Text.Contains("klpq.men"))
            {
                commandLine = "\"" + X.Text + " live=1\"" + " best";

                if (LQ_checkBox.Checked)
                {
                    commandLine = commandLine.Replace("/live/", "/restream/");
                }

                if (openChat_checkBox.Checked)
                {
                    try
                    {
                        System.Diagnostics.Process.Start(poddyChannelsChatList[poddyChannelsList.IndexOf(X.Text)]);
                    }
                    catch
                    {

                    }
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
                    System.Diagnostics.Process.Start(X.Text + "/chat/");
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
                System.Diagnostics.Process.Start("https://github.com/chrippa/livestreamer/releases");
            }
        }

        public void WriteLog(string log)
        {
            string logFilePath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\KolpaqueClient.log";

            if (!enableLog_checkBox.Checked)
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

        public void ImportChannel()
        {
            if (twitchImport_textBox.Text.Replace(" ", "") != "")
            {
                WebClient client = new WebClient();

                try
                {
                    string twitchFollowsString = client.DownloadString("https://api.twitch.tv/kraken/users/" + twitchImport_textBox.Text + "/follows/channels" + "?client_id=dk330061dv4t81s21utnhhdona0a91x");

                    dynamic twitchFollowsJSON = JsonConvert.DeserializeObject(twitchFollowsString);

                    int newAdded = 0;

                    if (twitchFollowsJSON.follows.Count > 0)
                    {
                        foreach (dynamic X in twitchFollowsJSON.follows)
                        {
                            string channel = X.channel.url.ToString();

                            if (!channels_listView.Items.Cast<ListViewItem>().Select(x => x.Text).Contains(channel))
                            {
                                channels_listView.Items.Add(channel);
                                customChannelsToolStripMenuItem.DropDownItems.Add(channel, null, new EventHandler(contextMenu_Click));
                                newAdded++;
                            }
                        }
                    }

                    MessageBox.Show(newAdded + " channels added.");
                }
                catch (Exception e)
                {
                    WriteLog("ImportChannelsNewThread Crashed\n" + e);
                    MessageBox.Show("Import Crashed\n" + e);
                }
            }

            twitchImport_textBox.Text = "";
        }
    }
}
