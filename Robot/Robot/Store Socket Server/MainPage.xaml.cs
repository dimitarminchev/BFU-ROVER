using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
// additional namespaces
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using System.Threading;
using System.Diagnostics;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238
namespace Store_Socket_Server
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // variables
        private string IP = null;
        private string PORT = null;
        private StreamSocket _socket = new StreamSocket();
        private StreamSocketListener _listener = new StreamSocketListener();
        private bool _connecting = false;

        // Construtor
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        // Connect 
        async private void Connect_Click(object sender, RoutedEventArgs e)
        {
            // IP & PORT
            IP = ServerIP.Text;
            PORT = ServerPort.Text;
            try
            {
                _listener.ConnectionReceived += listenerConnectionReceived;
                await _listener.BindEndpointAsync(new HostName(IP), PORT);
                LogMessage(string.Format("TCP Server is started on IP address {0} and port {1}", IP, PORT));
            }
            catch (Exception ex)
            {
                LogMessage(string.Format("Error: {0}", ex.Message));
            }
        }
        void listenerConnectionReceived(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
        {
            _socket = args.Socket;
            LogMessage(string.Format("Robot ({0}( connected.", args.Socket.Information.RemoteHostName.DisplayName));
        }

        // Log the Message
        async private void LogMessage(string message)
        {
            if (Dispatcher.HasThreadAccess) Log.Text += message + Environment.NewLine;
            else await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => { Log.Text += message + Environment.NewLine; });
        }

        // Sending Buttons
        private void Send_Click(object sender, RoutedEventArgs e)
        {
            Button s = (Button)sender;
            String command = s.Content.ToString();
            switch (command)
            {
                case "Forward": SendMessage(_socket, "/robot/move/forward"); break;
                case "Backward": SendMessage(_socket, "/robot/move/backward"); break;
                case "Left": SendMessage(_socket, "/robot/turn/left"); break;
                case "Right": SendMessage(_socket, "/robot/turn/right"); break;
                case "Stop": SendMessage(_socket, "/robot/turn/stop"); break;
            }
        }

        // Send the Message
        async private void SendMessage(StreamSocket socket, string message)
        {
            var writer = new DataWriter(socket.OutputStream);
            var len = writer.MeasureString(message); // Gets the UTF-8 string length.
            writer.WriteInt32((int)len);
            writer.WriteString(message);
            var ret = await writer.StoreAsync();
            writer.DetachStream();

            LogMessage(string.Format("Command ({0}) sent to robot ({1}).", message, socket.Information.RemoteHostName.DisplayName));
        }

        // Picture Button Click
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String RobotIP = _socket.Information.RemoteHostName.DisplayName;
                Uri RobotURI = new Uri("http://" + RobotIP + ":80");
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
