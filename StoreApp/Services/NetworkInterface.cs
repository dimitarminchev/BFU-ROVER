using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Windows.UI.Popups;

namespace StoreApp.Services
{
    class NetworkInterface
    {
        public DatagramSocket _socket;
        public bool IsConnected { get; set; }
        public event EventHandler ConnectionLost = delegate { };

        // Constructor
        public NetworkInterface()
        {
            IsConnected = false;
            _socket = new DatagramSocket();
            _socket.MessageReceived += OnSocketMessageReceived;
        }

        // Connect
        public async void Connect(HostName remoteHostName, string remoteServiceNameOrPort)
        {
            await _socket.ConnectAsync(remoteHostName, remoteServiceNameOrPort).AsTask().ContinueWith(task =>
            {
                IsConnected = true;
            });
        }

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        private void OnSocketMessageReceived(DatagramSocket sender, DatagramSocketMessageReceivedEventArgs args)
        {
            try
            {
                var reader = args.GetDataReader();
                var count = reader.UnconsumedBufferLength;
                var data = reader.ReadString(count);
                if (MessageReceived != null)
                {
                    var ea = new MessageReceivedEventArgs();
                    ea.Message = new Message(data);
                    ea.RemoteHostName = args.RemoteAddress;
                    ea.RemotePort = args.RemotePort;
                    MessageReceived(this, ea);
                }
            }
            catch (Exception ex)
            {
                Task.Run(async () =>
                {
                    await new MessageDialog(ex.Message).ShowAsync();
                });
            }

        }

        // Send 
        public async void SendMessage(string message)
        {
            try
            {
                var stream = _socket.OutputStream;
                var _writer = new DataWriter(stream);
                _writer.WriteString(message);
                await _writer.StoreAsync();
            }
            catch
            {
                IsConnected = false;
                this.ConnectionLost(this, new EventArgs());
            }
        }
    }
}
