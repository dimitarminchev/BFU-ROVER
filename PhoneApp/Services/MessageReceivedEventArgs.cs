using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking;

namespace PhoneApp.Services
{
    class MessageReceivedEventArgs
    {
        public Message Message { get; set; }
        public HostName RemoteHostName { get; set; }
        public string RemotePort { get; set; }
    }
}
