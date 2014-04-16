using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace PhoneApp.Services
{
    class NetworkInterface
    {
        private DatagramSocket _socket;
        public bool IsConnected { get; set; }

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

        // Receive
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
                MessageBox.Show(ex.Message);
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
            catch(Exception ex)
            {
                MessageBox.Show("Send Message Failed: " + ex.Message);
            }
        }
    }
}
