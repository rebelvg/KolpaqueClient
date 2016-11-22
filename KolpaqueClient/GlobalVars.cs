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
        string clientVersion = "0.283";

        List<string> poddyChannelsList = new List<string>(new string[] { "rtmp://dedick.podkolpakom.net/live/liveevent", "rtmp://dedick.podkolpakom.net/live/tvstream", "rtmp://dedick.podkolpakom.net/live/murshun" });

        List<string> poddyChannelsChatList = new List<string>(new string[] { "http://podkolpakom.net/stream/main/chat/", "http://podkolpakom.net/stream/tv/chat/", "http://podkolpakom.net/stream/murshun/chat/" });

        ListViewItem channelsLastSelectedItem;

        int lastBalloonPrint = 0;

        Dictionary<string, int> offlineChannelsDictionary = new Dictionary<string, int>();

        KolpaqueClientXmlSettings ClientSettings;

        bool debugMode;
    }
}
