using System;
using Windows.Networking;

namespace StoreApp.Services
{
    class MessageReceivedEventArgs
    {
        public Message Message { get; set; }
        public HostName RemoteHostName { get; set; }
        public string RemotePort { get; set; }
    }
}
