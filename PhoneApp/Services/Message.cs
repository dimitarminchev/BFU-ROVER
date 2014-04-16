using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneApp.Services
{
    class Message
    {
        public string Data { get; set; }
        public Message(string s)
        {
            Data = s;
        }
    }
}
