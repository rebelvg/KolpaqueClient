using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KolpaqueClient
{
    class Channel
    {
        private string[] allowedProtocols = new string[] { "rtmp", "http", "https" };
        private Dictionary<string, dynamic> registeredServices = new Dictionary<string, dynamic>();

        public string service = "custom";
        public string name = null;
        public string link = null;
        public string protocol = null;

        private void RegisterServices()
        {
            Dictionary<string, dynamic> service = new Dictionary<string, dynamic>();

            service["protocols"] = new string[] { "rtmp" };
            service["hosts"] = new string[] { "main.klpq.men", "stream.klpq.men" };
            service["paths"] = new string[] { "/live/" };
            service["name"] = 2;
            registeredServices["klpq-main"] = service;

            service["protocols"] = new string[] { "https", "http" };
            service["hosts"] = new string[] { "www.twitch.tv", "twitch.tv" };
            service["paths"] = new string[] { "/" };
            service["name"] = 1;
            registeredServices["twitch"] = service;
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

        public static Channel CreateChannel(string channelLink)
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
    }
}
