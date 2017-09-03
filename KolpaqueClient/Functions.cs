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
using Newtonsoft.Json.Linq;

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
                notifications_checkBox.Checked = ClientSettings.notifications_checkBox;
                autoPlay_checkBox.Checked = ClientSettings.autoPlay_checkBox;

                foreach (string X in ClientSettings.channels_listView)
                {
                    AddChannel(X);
                }

                minimizeAtStart_checkBox.Checked = ClientSettings.minimizeAtStart_checkBox;
                columnHeader2.Width = ClientSettings.channels_listView_ColumnWidth;
                launchStreamOnBalloonClick_checkBox.Checked = ClientSettings.launchStreamOnBalloonClick_checkBox;
                enableLog_checkBox.Checked = ClientSettings.enableLog;
            }
            catch (Exception e)
            {
                reader.Close();

                DialogResult dialogResult = MessageBox.Show(e + "\n\nCreate a new one?", "Xml file is corrupted.", MessageBoxButtons.YesNo);

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
                ClientSettings.notifications_checkBox = notifications_checkBox.Checked;
                ClientSettings.autoPlay_checkBox = autoPlay_checkBox.Checked;
                ClientSettings.channels_listView = allChannels.Keys.ToList();
                ClientSettings.minimizeAtStart_checkBox = minimizeAtStart_checkBox.Checked;
                ClientSettings.channels_listView_ColumnWidth = columnHeader2.Width;
                ClientSettings.form1_size = new int[] { this.Width, this.Height };
                ClientSettings.launchStreamOnBalloonClick_checkBox = launchStreamOnBalloonClick_checkBox.Checked;
                ClientSettings.enableLog = enableLog_checkBox.Checked;

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

        public void GetPoddyMenStatsNewThread(Channel channelObj, bool showBalloon)
        {
            WebClient client = new WebClient();

            try
            {
                string amsStatsString = client.DownloadString("http://stats.klpq.men/channel/" + channelObj.name);

                dynamic amsStatsJSON = JsonConvert.DeserializeObject(amsStatsString);

                if ((bool)amsStatsJSON.isLive)
                {
                    ChannelIsOnline(channelObj, showBalloon);
                }
                if (!(bool)amsStatsJSON.isLive)
                {
                    ChannelIsOffline(channelObj);
                }
            }
            catch (Exception e)
            {
                WriteLog("GetPoddyStatsNewThread Crashed " + channelObj.name + " " + e.Message);
            }
        }

        public void GetTwitchStatsNewThread(Channel channelObj, bool showBalloon)
        {
            WebClient client = new WebClient();

            try
            {
                string twitchStatsString = client.DownloadString("https://api.twitch.tv/kraken/streams?channel=" + channelObj.name + "&client_id=" + twitchApiAppKey);

                dynamic twitchAPIStats = JsonConvert.DeserializeObject(twitchStatsString);

                if (twitchAPIStats.streams.Count > 0)
                {
                    ChannelIsOnline(channelObj, showBalloon);
                }
                if (twitchAPIStats.streams.Count == 0)
                {
                    ChannelIsOffline(channelObj);
                }
            }
            catch (Exception e)
            {
                WriteLog("GetTwitchStatsNewThread Crashed " + channelObj.name + " " + e.Message);
            }
        }

        public void ChannelIsOnline(Channel channelObj, bool showBalloon)
        {
            if (onlineChannels.ContainsKey(channelObj.link))
            {
                onlineChannels[channelObj.link] = 0;
                return;
            }

            WriteLog("ChannelWentOnline " + channelObj.link);

            onlineChannels[channelObj.link] = 0;

            this.Invoke(new Action(() =>
            {
                channelObj.SetOnline();

                AddTrayChannel(channelObj.link);
            }));

            if (showBalloon)
            {
                PrintBalloon("Stream is Live (" + DateTime.Now.ToString("d/MMM, H:mm") + ")", channelObj.link);
            }

            if (autoPlay_checkBox.Checked)
            {
                PlayStream(channelObj.link, LQ_checkBox.Checked);
            }
        }

        public void ChannelIsOffline(Channel channelObj)
        {
            if (!onlineChannels.ContainsKey(channelObj.link))
            {
                return;
            }

            onlineChannels[channelObj.link]++;

            if (onlineChannels[channelObj.link] < 3)
            {
                return;
            }

            WriteLog("ChannelWentOffline " + channelObj.link);

            onlineChannels.Remove(channelObj.link);

            this.Invoke(new Action(() =>
            {
                channelObj.SetOffline();

                RemoveTrayChannel(channelObj.link);
            }));
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

        public void GetStatsPerItem(Channel channelObj, bool showBalloon, int schedule)
        {
            if (schedule == 0 || schedule == 5)
            {
                if (channelObj.service == "klpq-main")
                {
                    Thread NewThread = new Thread(() => GetPoddyMenStatsNewThread(channelObj, showBalloon));
                    NewThread.IsBackground = true;
                    NewThread.Start();
                }
            }

            if (schedule == 0 || schedule == 30)
            {
                if (channelObj.service == "twitch")
                {
                    Thread NewThread = new Thread(() => GetTwitchStatsNewThread(channelObj, showBalloon));
                    NewThread.IsBackground = true;
                    NewThread.Start();
                }
            }
        }

        public void GetStats(bool showBalloon, int schedule)
        {
            foreach (KeyValuePair<string, Channel> channel in allChannels)
            {
                GetStatsPerItem(channel.Value, showBalloon, schedule);
            }
        }

        public void PlayStream(string channelLink, bool launchLowQuality)
        {
            WriteLog("PlayStream " + channelLink);

            string commandLine = "";

            if (channelLink.StartsWith("rtmp"))
            {
                commandLine = "\"" + channelLink + " live=1\"" + " best";

                if (channelLink.Contains("klpq.men/live/") && launchLowQuality)
                {
                    commandLine = commandLine.Replace("/live/", "/restream/");
                }
            }
            else
            {
                if (launchLowQuality)
                {
                    commandLine = "\"" + channelLink + "\"" + " best --stream-sorting-excludes=>=720p,>=high";
                }
                else
                {
                    commandLine = "\"" + channelLink + "\"" + " best";
                }
            }

            commandLine += " --twitch-disable-hosting";

            if (File.Exists(livestreamerPath_textBox.Text))
            {
                Task.Run(() =>
                {
                    Process myProcess = new Process();

                    myProcess.StartInfo.FileName = livestreamerPath_textBox.Text;
                    myProcess.StartInfo.Arguments = commandLine;
                    myProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    myProcess.StartInfo.UseShellExecute = false;
                    myProcess.StartInfo.RedirectStandardOutput = true;
                    myProcess.StartInfo.RedirectStandardError = true;
                    myProcess.StartInfo.CreateNoWindow = true;
                    myProcess.Start();

                    while (!myProcess.StandardOutput.EndOfStream)
                    {
                        string stdOut = myProcess.StandardOutput.ReadLine();

                        if (stdOut.Contains("error: "))
                        {
                            string[] error = stdOut.Split(new string[] { "error: " }, StringSplitOptions.None);

                            PrintBalloon("Error", error[1]);
                        }
                    }
                });
            }
            else
            {
                MessageBox.Show(Path.GetFileName(livestreamerPath_textBox.Text) + " not found.");
            }
        }

        public void WriteLog(string log)
        {
            if (!enableLog_checkBox.Checked)
                return;

            string logFilePath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\KolpaqueClient.log";

            log = DateTime.Now.ToString() + " " + log;

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

        public void ImportChannel(string twitchName)
        {
            WebClient client = new WebClient();

            try
            {
                string twitchFollowsString = client.DownloadString("https://api.twitch.tv/kraken/users/" + twitchName + "/follows/channels?direction=ASC&limit=100&sortby=created_at&user=" + twitchName + "&client_id=" + twitchApiAppKey);

                JObject twitchFollowsJSON = JObject.Parse(twitchFollowsString);

                int newAdded = 0;

                IList<JToken> channels = twitchFollowsJSON["follows"].Children().ToList();

                if (channels.Count() == 0)
                    return;

                foreach (dynamic X in channels)
                {
                    string channel = X.channel.url.ToString();

                    if ((bool)this.Invoke(new Func<Boolean>(() => AddChannel(channel))))
                    {
                        newAdded++;
                    }
                }

                while (channels.Count() != 0)
                {
                    twitchFollowsString = client.DownloadString(twitchFollowsJSON["_links"]["next"] + "&client_id=" + twitchApiAppKey);

                    twitchFollowsJSON = JObject.Parse(twitchFollowsString);

                    channels = twitchFollowsJSON["follows"].Children().ToList();

                    foreach (dynamic X in channels)
                    {
                        string channel = X.channel.url.ToString();

                        if ((bool)this.Invoke(new Func<Boolean>(() => AddChannel(channel))))
                        {
                            newAdded++;
                        }
                    }
                }

                MessageBox.Show(newAdded + " channels added.");
            }
            catch (Exception e)
            {
                WriteLog("ImportChannelsNewThread Crashed\n" + e);
                MessageBox.Show("Import Crashed.\n\n" + e.Message);
            }
        }

        public bool AddChannel(string channelLink)
        {
            Channel channelClass = Channel.Create(channelLink);

            if (channelClass == null)
            {
                return false;
            }

            if (allChannels.ContainsKey(channelClass.link))
            {
                return false;
            }

            ListViewItem item = channels_listView.Items.Add(channelClass.link);

            channelClass.item = item;

            allChannels[channelClass.link] = channelClass;

            return true;
        }

        public bool RemoveChannel(string channelLink)
        {
            allChannels.Remove(channelLink);

            if (onlineChannels.ContainsKey(channelLink))
            {
                onlineChannels.Remove(channelLink);
            }

            var item = channels_listView.FindItemWithText(channelLink);

            if (item != null)
            {
                item.Remove();
            }

            return true;
        }

        public void AddTrayChannel(string channelLink)
        {
            customChannelsToolStripMenuItem.DropDownItems.Add(channelLink, null, new EventHandler(contextMenu_Click));
        }

        public void RemoveTrayChannel(string channelLink)
        {
            for (int i = 0; i < customChannelsToolStripMenuItem.DropDownItems.Count; i++)
            {
                if (customChannelsToolStripMenuItem.DropDownItems[i].Text.Contains(channelLink))
                {
                    customChannelsToolStripMenuItem.DropDownItems.RemoveAt(i);
                }
            }
        }
    }
}
