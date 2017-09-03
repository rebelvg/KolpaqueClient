using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolpaqueClient
{
    public class KolpaqueClientXmlSettings
    {
        public string livestreamerPath_textBox = "C:\\Program Files (x86)\\Streamlink\\bin\\streamlink.exe";
        public bool LQ_checkBox;
        public bool notifications_checkBox = true;
        public bool autoPlay_checkBox;
        public List<string> channels_listView;
        public bool minimizeAtStart_checkBox;
        public int channels_listView_ColumnWidth = 348;
        public int[] form1_size = { 400, 667 };
        public bool launchStreamOnBalloonClick_checkBox = true;
        public bool enableLog;
    }
}
