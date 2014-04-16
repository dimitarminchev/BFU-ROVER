using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WindowsPhoneSocketServer.Resources;
using Windows.Networking.Sockets;
using Windows.Networking;
using Windows.Storage.Streams;

namespace WindowsPhoneSocketServer
{
    public partial class MainPage : PhoneApplicationPage
    {
        private string ip = null;
        private string port = null;
        private StreamSocket socket = new StreamSocket();
        private StreamSocketListener listener = new StreamSocketListener();

        public MainPage()
        {
            InitializeComponent();

        }

        private async void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            // IP & PORT
            ip = ServerIP.Text;
            port = ServerPort.Text;
            try
            {
                listener.ConnectionReceived += listenerConnectionReceived;
                await listener.BindEndpointAsync(new HostName(ip), port);
                LogMessage(string.Format("TCP Server is started on IP address {0} and port {1}", ip, port));
            }
            catch (Exception ex)
            {
                LogMessage(string.Format("Error: {0}", ex.Message));
            }
        }

        void listenerConnectionReceived(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
        {
            socket = args.Socket;
            LogMessage(string.Format("Robot ({0}( connected.", args.Socket.Information.RemoteHostName.DisplayName));
        }

        // Log the Message
        private void LogMessage(string message)
        {
            Dispatcher.BeginInvoke(delegate
                {
                    Log.Text += message + Environment.NewLine;
                });
        }

        // Sending Buttons
        private void Send_Click(object sender, RoutedEventArgs e)
        {
            Button s = (Button)sender;
            String command = s.Content.ToString();
            switch (command)
            {
                case "Forward": SendMessage(socket, "/robot/move/forward"); break;
                case "Backward": SendMessage(socket, "/robot/move/backward"); break;
                case "Left": SendMessage(socket, "/robot/turn/left"); break;
                case "Right": SendMessage(socket, "/robot/turn/right"); break;
                case "Stop": SendMessage(socket, "/robot/turn/stop"); break;
            }
        }

        // Send the Message
        async private void SendMessage(StreamSocket socket, string message)
        {
            var writer = new DataWriter(socket.OutputStream);
            writer.WriteString(message);
            var ret = await writer.StoreAsync();
            writer.DetachStream();

            LogMessage(string.Format("Command ({0}) sent to robot ({1}).", message, socket.Information.RemoteHostName.DisplayName));
        }

        private void TakePictureBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String RobotIP = socket.Information.RemoteHostName.DisplayName;
                Uri RobotURI = new Uri("http://" + RobotIP + ":8080");
                RobotPicture.Navigate(RobotURI);
                LogMessage("New pictire taken!");
            }
            catch (Exception ex)
            {
                LogMessage(string.Format("Error: {0}", ex.Message));
            }
        }
    }
}