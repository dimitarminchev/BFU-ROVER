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
using StoreApp.Services;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media.Imaging;
using System.Net.Http;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using System.Threading.Tasks;

// Blank Page Template
namespace StoreApp
{
    public sealed partial class MainPage : Page
    {
        // variables
        public static string IP = null;
        public static string PORT = null;
        // ni
        NetworkInterface ni = new NetworkInterface();
        SynchronizationContext _syncContext;

        // Construtor
        public MainPage()
        {
            this.InitializeComponent();

            // Log Communication Messages
            _syncContext = SynchronizationContext.Current;
            ni.MessageReceived += OnMessageReceived;
            ni.ConnectionLost += OnConnectionLost;
            Log.ItemsSource = _messages;
        }

        void OnConnectionLost(object sender, EventArgs e)
        {
            if (ni._socket != null)
                ni._socket.Dispose();

            ni = new NetworkInterface();
            ni.MessageReceived += OnMessageReceived;
            ni.ConnectionLost += OnConnectionLost;

            this.ConnectToRobot();
        }

        // Messages
        private ObservableCollection<Message> _messages = new ObservableCollection<Message>();
        void OnMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            _syncContext.Post((s) => { LogMessage(e.Message.Data); }, null);
        }


        // navigation
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // nothing
        }

        // Log the Message
        private void LogMessage(string message)
        {
            Message m = new Message(message);
            _messages.Add(m);
        }

        // Connect 
        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            ConnectToRobot();
        }

        private void ConnectToRobot()
        {
            // IP & PORT
            IP = ServerIP.Text;
            PORT = ServerPort.Text;
            try
            {
                if (!ni.IsConnected)
                {
                    ni.Connect(new HostName(IP), PORT);
                    LogMessage(string.Format("Socket Initialized. {0}:{1}.", IP, PORT));
                }
                else
                {
                    LogMessage("Socket already connected!");
                }
            }
            catch (Exception ex)
            {
                LogMessage(string.Format("Error Initializing Socket: {0}", ex.Message));
            }
        }

        // Sending Buttons
        private async void Send_Click(object sender, RoutedEventArgs e)
        {
            if (ni.IsConnected)
            {
                Button s = (Button)sender;
                String command = s.Name.ToString();
                switch (command)
                {
                    // Socket Server
                    case "sockForward": ni.SendMessage("/robot/move/forward"); break;
                    case "sockBack": ni.SendMessage("/robot/move/backward"); break;
                    case "sockLeft": ni.SendMessage("/robot/turn/left"); break;
                    case "sockRight": ni.SendMessage("/robot/turn/right"); break;
                    case "sockStop": ni.SendMessage("/robot/turn/stop"); break;
                    // web server
                    case "webStart": ni.SendMessage("/web/server/start"); break;
                    case "webRestart": ni.SendMessage("/web/server/restart"); break;
                    case "webStop": ni.SendMessage("/web/server/stop"); break;
                }
            }
            else
            {
                var dlg = new MessageDialog(
                    "The socket has not yet been connected. Please press the 'connect' button and/or verify that the Gadgeteer listener is running.",
                    "Socket not connected"
                    );

                await dlg.ShowAsync();
            }

        }

        // Button Take New Image Click
        private async void Button_Take_New_Click(object sender, RoutedEventArgs e)
        {
            await TakePicture();
        }

        // Take Picture
        private async Task TakePicture()
        {
            try
            {
                PictureProgress.Value = 0;
                Uri uri = new Uri("http://" + IP + ":8080");
                StorageFile storageFile = await KnownFolders.PicturesLibrary.CreateFileAsync("robot.jpg", CreationCollisionOption.GenerateUniqueName);
                BackgroundDownloader downloader = new BackgroundDownloader();
                DownloadOperation download = downloader.CreateDownload(uri, storageFile);
                await PerformDownload(download);
            }
            catch (Exception ex)
            {
                LogMessage(string.Format("Take New Image Error: {0}", ex.Message));
            }
        }

        // Picture Download Progress
        private async Task<DownloadOperation> PerformDownload(DownloadOperation download)
        {
            Progress<DownloadOperation> callback = new Progress<DownloadOperation>(UpdateDownloadProgress);
            return await download.StartAsync().AsTask(callback);
        }
        private async void UpdateDownloadProgress(DownloadOperation obj)
        {
            double percent = 100;
            if (obj.Progress.TotalBytesToReceive > 0)
            {
                percent = obj.Progress.BytesReceived * 100 / obj.Progress.TotalBytesToReceive;
            }
            await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                PictureProgress.Value = percent;
            });
        }

        // Button View Picture Gallery Click
        private void Button_View_Gallery_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GalleryPage));
        }

        // TODO: Button Panorama Image Click
        private async void Button_Panorama_Click(object sender, RoutedEventArgs e)
        {
            LogMessage("Automatic Panorama Started...");
            for (int k = 1; k < 20; k++)
            {
                ni.SendMessage("/robot/turn/left");
                ni.SendMessage("/robot/turn/stop");
                await TakePicture();
            }
        }
    }

}
