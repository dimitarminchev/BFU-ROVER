using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PhoneApp.Resources;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using PhoneApp.Services;
using System.Threading;
using Windows.Networking;
using System.IO.IsolatedStorage;

namespace PhoneApp
{
    // Main Page Class
    public partial class MainPage : PhoneApplicationPage
    {
        // variables
        private string IP = null;
        private string PORT = null;
        NetworkInterface ni = new NetworkInterface();
        SynchronizationContext _syncContext;   

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            
            // Log Communication Messages
            _syncContext = SynchronizationContext.Current;
            ni.MessageReceived += OnMessageReceived;
            Log.ItemsSource = _messages;
             
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
        private void Send_Click(object sender, RoutedEventArgs e)
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
                MessageBox.Show("The socket has not yet been connected. Please press the 'connect' button and/or verify that the Gadgeteer listener is running.");
            }
        }

        // Button Take New Image Click
        private void Button_Take_New_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PictureProgress.Value = 0;
                Uri uri = new Uri("http://" + IP + ":8080");
                WebClient webClient = new WebClient();
                webClient.OpenReadCompleted += webClient_OpenReadCompleted;
                webClient.OpenReadAsync(uri);
                webClient.DownloadProgressChanged += webClient_DownloadProgressChanged;
            }
            catch (Exception ex)
            {
                LogMessage(string.Format("Take New Image Error: {0}", ex.Message));
            }
        }

        // Download Progress
        void webClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            PictureProgress.Value = e.ProgressPercentage;
        }

        // Picture Completed Download
        void webClient_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            using (var userStore = IsolatedStorageFile.GetUserStoreForApplication())
            {                  
                var fileNames = userStore.GetFileNames("*.jpg");
                var fileCount = fileNames.Count(); // Count the number of pictures
                using (var stream = new IsolatedStorageFileStream((fileCount++) + ".jpg", System.IO.FileMode.Create, userStore))
                {
                    byte[] buffer = new byte[1024];
                    while (e.Result.Read(buffer, 0, buffer.Length) > 0) stream.Write(buffer, 0, buffer.Length);
                }
            }
        }

        // Button View Picture Gallery Click
        private void Button_View_Gallery_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Gallery.xaml", UriKind.Relative));
        }

        // TODO: Button Panorama Image Click
        private void Button_Panorama_Click(object sender, RoutedEventArgs e)
        {
            // 1. picture
            // 2. left
            // 3. sleep
            // 4. stop
            // ...
            // progress bar
            // create panorama
        }

    }
}