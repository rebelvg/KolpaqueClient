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
        string clientVersion = "0.3.3";

        string twitchApiAppKey = "dk330061dv4t81s21utnhhdona0a91x";

        List<string> poddyChannelsList = new List<string>(new string[] { "rtmp://main.klpq.men/live/main" });

        ListViewItem channelsLastSelectedItem;

        int lastBalloonPrint = 0;

        Dictionary<string, Channel> allChannels = new Dictionary<string, Channel>();

        Dictionary<string, int> onlineChannels = new Dictionary<string, int>();

        KolpaqueClientXmlSettings ClientSettings;

        bool debugMode;
    }
}
