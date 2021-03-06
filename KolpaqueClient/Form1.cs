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
using Newtonsoft.Json;

namespace KolpaqueClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

#if TRACE
            debugMode = false;
#else
            debugMode = true;
#endif

            try
            {
                xmlPath_textBox.Text = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\KolpaqueClient.xml";

                if (File.Exists(xmlPath_textBox.Text))
                {
                    ReadXmlFile();
                }
                else
                {
                    try
                    {
                        ClientSettings = new KolpaqueClientXmlSettings();

                        System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(KolpaqueClientXmlSettings));

                        System.IO.FileStream writer = System.IO.File.Create(xmlPath_textBox.Text);
                        serializer.Serialize(writer, ClientSettings);
                        writer.Close();

                        ReadXmlFile();

                        foreach (string X in poddyChannelsList)
                        {
                            AddChannel(X);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Saving xml settings failed.");
                    }
                }

                label2.Text = "Version " + clientVersion;

                Thread NewVersionThread = new Thread(() => GetNewVersionNewThread());
                NewVersionThread.Start();

                WriteLog("---KolpaqueClient Launched---");
                WriteLog("Client Version - " + clientVersion);
            }
            catch (Exception e)
            {
                MessageBox.Show("Client crashed while initializing.\n\n" + e.Message);
                notifyIcon1.Visible = false;
                System.Environment.Exit(1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string channelLink = addChannel_textBox.Text.Replace(" ", "");

            if (!string.IsNullOrWhiteSpace(channelLink))
            {
                AddChannel(channelLink);
            }

            addChannel_textBox.Text = "";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://klpq.men/");
        }

        private void fiveSecTimer_Tick(object sender, EventArgs e)
        {
            lastBalloonPrint = lastBalloonPrint + 5;

            if (lastBalloonPrint == 20)
            {
                notifyIcon1.BalloonTipTitle = "";
                notifyIcon1.BalloonTipText = "";
            }

            GetStats(true, 5);
        }

        private void thirtySecTimer_Tick(object sender, EventArgs e)
        {
            GetStats(true, 30);
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            WriteLog("notifyIcon1_BalloonTipClicked");

            if (lastBalloonPrint > 15)
                return;

            if (notifyIcon1.BalloonTipTitle.Contains("Stream is Live"))
            {
                if (launchStreamOnBalloonClick_checkBox.Checked)
                {
                    PlayStream(notifyIcon1.BalloonTipText, LQ_checkBox.Checked);
                }
            }

            if (notifyIcon1.BalloonTipTitle.Contains("New Version Available"))
            {
                System.Diagnostics.Process.Start(notifyIcon1.BalloonTipText);
            }

            notifyIcon1.BalloonTipTitle = "";
            notifyIcon1.BalloonTipText = "";
        }

        private void contextMenu_Click(object sender, EventArgs e)
        {
            PlayStream(sender.ToString(), LQ_checkBox.Checked);
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            WriteLog("notifyIcon1_MouseClick " + e.Button);

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

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/rebelvg/KolpaqueClient/releases");
        }

        private void closeClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void playFromClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlayStream(Clipboard.GetText(), LQ_checkBox.Checked);
        }

        private void listView2_MouseClick(object sender, MouseEventArgs e)
        {
            channelsLastSelectedItem = channels_listView.SelectedItems[0];

            if (e.Button == MouseButtons.Right)
            {
                var hitTestInfo = channels_listView.HitTest(e.X, e.Y);

                if (hitTestInfo.Item != null)
                {
                    var loc = e.Location;
                    loc.Offset(tabPage1.Location);

                    this.contextMenuStrip2.Show(this, loc);
                }
            }

            if (e.Button == MouseButtons.Left)
            {
                channelsLastSelectedItem.Selected = false;
            }
        }

        private void playStreamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlayStream(channelsLastSelectedItem.Text, false);
        }

        private void Form1_ClientSizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            GetStats(false, 0);

            this.Width = ClientSettings.form1_size[0];
            this.Height = ClientSettings.form1_size[1];

            if (minimizeAtStart_checkBox.Checked && !debugMode)
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }

        private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(channelsLastSelectedItem.Text);
        }

        private void removeChannelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveTrayChannel(channelsLastSelectedItem.Text);

            RemoveChannel(channelsLastSelectedItem.Text);
        }

        private void openChatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (channelsLastSelectedItem.Text.StartsWith("rtmp") && channelsLastSelectedItem.Text.Contains("klpq.men"))
            {
                System.Diagnostics.Process.Start("http://stream.klpq.men/chat");
            }

            if (channelsLastSelectedItem.Text.Contains("http"))
            {
                System.Diagnostics.Process.Start(channelsLastSelectedItem.Text + "/chat/");
            }
        }

        private void listView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PlayStream(channelsLastSelectedItem.Text, LQ_checkBox.Checked);

                channelsLastSelectedItem.Selected = false;
            }
        }

        private void contextMenuStrip2_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            channelsLastSelectedItem.Selected = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog selectExePath = new OpenFileDialog();

            selectExePath.Title = "Select livestreamer.exe";
            selectExePath.Filter = "Executable File (.exe) | *.exe";
            selectExePath.RestoreDirectory = true;

            if (selectExePath.ShowDialog() == DialogResult.OK)
            {
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
            System.Diagnostics.Process.Start("https://github.com/rebelvg/KolpaqueClient");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;

            SaveXmlFile();
        }

        private void twitchImport_button_Click(object sender, EventArgs e)
        {
            string twitchChannel = twitchImport_textBox.Text.Replace(" ", "");

            if (!string.IsNullOrWhiteSpace(twitchChannel))
            {
                Thread NewThread = new Thread(() => ImportChannel(twitchChannel));
                NewThread.Start();
            }

            twitchImport_textBox.Text = "";
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            SaveXmlFile();
        }

        private void playLowQualityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlayStream(channelsLastSelectedItem.Text, true);
        }

        private void openPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (channelsLastSelectedItem.Text.StartsWith("rtmp") && channelsLastSelectedItem.Text.Contains("klpq.men"))
            {
                string[] name = channelsLastSelectedItem.Text.Split(new string[] { "/" }, StringSplitOptions.None);

                System.Diagnostics.Process.Start("http://stream.klpq.men/" + name.Last());
            }

            if (channelsLastSelectedItem.Text.Contains("http"))
            {
                System.Diagnostics.Process.Start(channelsLastSelectedItem.Text);
            }
        }
    }
}
