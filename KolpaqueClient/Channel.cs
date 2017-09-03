using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KolpaqueClient
{
    public class Channel
    {
        private string[] allowedProtocols = new string[] { "rtmp", "http", "https" };
        private Dictionary<string, dynamic> registeredServices = new Dictionary<string, dynamic>();

        public string service = "custom";
        public string name;
        public string link;
        public string protocol;
        public ListViewItem item;

        private void RegisterServices()
        {
            Dictionary<string, dynamic> service = new Dictionary<string, dynamic>();

            registeredServices["klpq-main"] = new Dictionary<string, dynamic>()
            {
                { "protocols" , new string[] { "rtmp" } },
                { "hosts" , new string[] { "main.klpq.men", "stream.klpq.men" } },
                { "paths", new string[] { "/live/" } },
                { "name", 2 }
            };

            registeredServices["twitch"] = new Dictionary<string, dynamic>()
            {
                { "protocols" , new string[] { "https", "http" } },
                { "hosts" , new string[] { "www.twitch.tv", "twitch.tv" } },
                { "paths", new string[] { "/" } },
                { "name", 1 }
            };
        }

        public Channel(string channelLink)
        {
            RegisterServices();

            this.link = channelLink;

            Uri channelUri = new Uri(channelLink);

            if (!allowedProtocols.Contains(channelUri.Scheme))
            {
                throw new Exception("This protocol is not allowed.");
            }

            this.protocol = channelUri.Scheme;

            if (channelUri.Host.Length < 1)
            {
                throw new Exception("Hostname can't be empty.");
            }

            if (channelUri.AbsolutePath.Length < 2)
            {
                throw new Exception("Pathname can't be empty.");
            }

            foreach (KeyValuePair<string, dynamic> serviceConfig in registeredServices)
            {
                string serviceName = serviceConfig.Key;
                dynamic serviceObj = serviceConfig.Value;

                if (Array.IndexOf(serviceObj["protocols"], channelUri.Scheme.ToLower()) > -1 && Array.IndexOf(serviceObj["hosts"], channelUri.Host.ToLower()) > -1)
                {
                    string[] nameArray = channelUri.AbsolutePath.Split('/');

                    if (nameArray[serviceObj["name"]] != null)
                    {
                        foreach (string path in serviceObj["paths"])
                        {
                            if (channelUri.AbsolutePath.ToLower().IndexOf(path) == 0)
                            {
                                this.service = serviceName;
                                this.name = nameArray[serviceObj["name"]];

                                UriBuilder newChannelUri = new UriBuilder(channelUri);

                                newChannelUri.Scheme = serviceObj["protocols"][0];
                                newChannelUri.Host = serviceObj["hosts"][0];
                                newChannelUri.Path = serviceObj["paths"][0] + nameArray[serviceObj["name"]];
                                newChannelUri.Port = -1;

                                this.link = newChannelUri.ToString();
                            }
                        }
                    }
                }
            }
        }

        public static Channel Create(string channelLink)
        {
            try
            {
                return new Channel(channelLink);
            }
            catch
            {
                return null;
            }
        }

        public void SetOnline()
        {
            this.item.BackColor = Color.Green;
        }

        public void SetOffline()
        {
            this.item.BackColor = default(Color);
        }
    }
}
