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
        string clientVersion = "0.288";

        string twitchApiAppKey = "dk330061dv4t81s21utnhhdona0a91x";

        List<string> poddyChannelsList = new List<string>(new string[] { "rtmp://main.klpq.men/live/liveevent", "rtmp://main.klpq.men/live/tvstream", "rtmp://main.klpq.men/live/murshun" });

        List<string> poddyChannelsChatList = new List<string>(new string[] { "http://klpq.men/stream/main/chat/", "http://klpq.men/stream/tv/chat/", "http://klpq.men/stream/murshun/chat/" });

        ListViewItem channelsLastSelectedItem;

        int lastBalloonPrint = 0;

        Dictionary<string, int> offlineChannelsDictionary = new Dictionary<string, int>();

        KolpaqueClientXmlSettings ClientSettings;

        bool debugMode;
    }
}
