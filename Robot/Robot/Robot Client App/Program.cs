using System;
using System.Collections;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Touch;

// Gadgeteer
using Gadgeteer.Networking;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using GHI.Premium.Net;
using Gadgeteer.Modules.GHIElectronics;

// Sockets
using System.Net.Sockets;
using Microsoft.SPOT.Net.NetworkInformation;
using System.IO;
using System.Text;
using GHI.Premium.System;

namespace Robot_Client_App
{
    public partial class Program
    {
        // Settings      
        private string SSID = null;
        private string PASSWORD = null;
        private string ClientIP = null;
        private string ServerIP = null;
        private ushort SocketPort = 0;
        private ushort WebPort = 0;

        private Socket _socket = null;

        // Timer (1 sec. = 1000 ms)
        GT.Timer SettingsTimer = new GT.Timer(5000);
        GT.Timer NetworkTimer = new GT.Timer(5000); 
        GT.Timer PictureTimer = new GT.Timer(5000);           

        // Picture
        GT.Picture picture = null;
        // uint imageNumber = 0;

        // This method is run when the mainboard is powered up or reset.   
        void ProgramStarted()
        {
            // Step 1. Initialize Motors            
            InitializeMotors();

            // Step 2. Load Settings Timer
            SettingsTimer.Tick += new GT.Timer.TickEventHandler(SettingsTimer_Tick);
            SettingsTimer.Start();        

            // Step 3. Network Timer
            NetworkTimer.Tick += new GT.Timer.TickEventHandler(NetworkTimer_Tick);
            NetworkTimer.Start();

            // Picture Timer
            PictureTimer.Tick += new GT.Timer.TickEventHandler(PictureTimer_Tick);
            PictureTimer.Start();

            // Picture Captured 
            camera.PictureCaptured += new Camera.PictureCapturedEventHandler(camera_PictureCaptured);            
            // camera.CurrentPictureResolution = GTM.GHIElectronics.Camera.PictureResolution.Resolution160x120;
        }

        // Step 1. Initialize Motors   
        void InitializeMotors()
        {
            motorControllerL298.MoveMotor(MotorControllerL298.Motor.Motor1, 0);
            motorControllerL298.MoveMotor(MotorControllerL298.Motor.Motor2, 0);
            Debug.Print("Step 1. Motors Initialized");
        }

        // Step 2. Load Settings
        void SettingsTimer_Tick(GT.Timer timer)
        {
            if (SettingsTimer.IsRunning == true)
            {
                if (sdCard.IsCardInserted) sdCard.MountSDCard();
                if (VerifySDCard() && File.Exists(sdCard.GetStorageDevice().RootDirectory + @"\wifi.txt"))
                {
                    // Get the root directory
                    string rootDirectory = sdCard.GetStorageDevice().RootDirectory;
                    // Use FileStream to read a text file
                    FileStream fileStream = new FileStream(rootDirectory + @"\wifi.txt", FileMode.Open);
                    // load the data from the stream
                    byte[] dataStream = new byte[fileStream.Length];
                    fileStream.Read(dataStream, 0, dataStream.Length);
                    fileStream.Close();
                    // Get the wifi config settings from the file
                    string dataContext = new string(Encoding.UTF8.GetChars(dataStream));
                    string[] lines = dataContext.Split(new char[] { '\n' });
                    string[] L1 = lines[0].Split(new char[] { ':' });
                    string[] L2 = lines[1].Split(new char[] { ':' });
                    string[] L3 = lines[2].Split(new char[] { ':' });
                    string[] L4 = lines[3].Split(new char[] { ':' });
                    string[] L5 = lines[4].Split(new char[] { ':' });

                    // Set Settings
                    SSID = L1[1].ToString().Trim();
                    PASSWORD = L2[1].ToString().Trim();
                    ServerIP = L3[1].ToString().Trim();
                    SocketPort = ushort.Parse(L4[1].ToString().Trim());
                    WebPort = ushort.Parse(L5[1].ToString().Trim());
                    // ok
                    Debug.Print("Step 2. Settings Loaded.");
                    SettingsTimer.Stop();
                }
            }
        }

        // Verify SD Card
        bool VerifySDCard()
        {
            if (!sdCard.IsCardInserted || !sdCard.IsCardMounted) return false;
            return true;
        }

        // Step 3. Network Timer
        void NetworkTimer_Tick(GT.Timer timer)
        {
            if (SettingsTimer.IsRunning == false && NetworkTimer.IsRunning == true)
            {
                if (!wifi_RS21.Interface.IsOpen) wifi_RS21.Interface.Open();
                if (!wifi_RS21.Interface.NetworkInterface.IsDhcpEnabled) wifi_RS21.Interface.NetworkInterface.EnableDhcp();
                if (!wifi_RS21.Interface.IsLinkConnected)
                {
                    wifi_RS21.Interface.NetworkAddressChanged += Interface_NetworkAddressChanged;
                    NetworkInterfaceExtension.AssignNetworkingStackTo(wifi_RS21.Interface);
                    WiFiNetworkInfo[] scanResponse = wifi_RS21.Interface.Scan(SSID);
                    if (scanResponse != null) wifi_RS21.Interface.Join(scanResponse[0], PASSWORD);                    
                }
            }
        }

        // Network Up
        void Interface_NetworkAddressChanged(object sender, EventArgs e)
        {
             ClientIP = wifi_RS21.Interface.NetworkInterface.IPAddress;

            // info
            Debug.Print("Step 3. Network Initialized. IP: " + ClientIP);
            NetworkTimer.Stop();

            // Camera Web Server on Port 80
            StartWebServer();

            // Enable Sockets Communications
            EnableSockets();
        }

        // Picture Timer
        void PictureTimer_Tick(GT.Timer timer)
        {
            if(camera.CameraReady) camera.TakePicture();
        }

        // Picture Captured 
        void camera_PictureCaptured(Camera sender, GT.Picture pic)
        {
            picture = pic;
            //SavePictureToCard();
        }

        // Save Picture to SD card
        //void SavePictureToCard()
        //{
        //    if (sdCard.IsCardInserted && !sdCard.IsCardMounted) sdCard.MountSDCard();
        //    if (VerifySDCard())
        //    {
        //        GT.StorageDevice storage = sdCard.GetStorageDevice();
        //        string imagesDirectory = storage.RootDirectory + "\\images\\";
        //        //imageNumber = (uint)storage.ListFiles(rootDirectory).Length;
        //        string imageFilePath = imagesDirectory + "pic" + (imageNumber++) + ".bmp";   

        //        Bitmap temp = picture.MakeBitmap();
        //        byte[] bytes = new byte[temp.Width * temp.Height * 3 + 54];
        //        Util.BitmapToBMPFile(temp.GetBitmap(), temp.Width, temp.Height, bytes);
        //        GT.Picture tempPic = new GT.Picture(bytes, GT.Picture.PictureEncoding.BMP);
                          
        //        try
        //        {
        //            storage.WriteFile(imageFilePath, tempPic.PictureData);
        //            Debug.Print("Free space:" + storage.Volume.TotalFreeSpace.ToString());
        //        }
        //        catch (Exception ex)
        //        {
        //            ;;
        //        }
        //    }
        //}

        // Start Camera Web Server on Port 80
        public void StartWebServer()
        {            
            WebServer.StartLocalServer(ClientIP, int.Parse(WebPort.ToString()));
            Debug.Print("Step 4. Web Service: " + ClientIP + ":" + WebPort);
            WebEvent TakePicture = WebServer.SetupWebEvent("");
            TakePicture.WebEventReceived += new WebEvent.ReceivedWebEventHandler(TakePicture_WebEventReceived);
        }
        void TakePicture_WebEventReceived(string path, WebServer.HttpMethod method, Responder responder)
        {
            GT.Picture jpeg = new GT.Picture(picture.PictureData, Gadgeteer.Picture.PictureEncoding.JPEG);
            if (picture != null) responder.Respond(jpeg);
        }

        // Enable Socket Communications    
        private void EnableSockets()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            System.Net.IPAddress ipAdd = System.Net.IPAddress.Parse(ServerIP);
            System.Net.IPEndPoint remoteEP = new System.Net.IPEndPoint(ipAdd, SocketPort);
            try
            {
                _socket.Connect(remoteEP);
                Debug.Print("Step 5: Socket Service: " + ServerIP + ":" + SocketPort);
            }
            catch (Exception ex)
            {
                Debug.Print("Step 5: Connection failed message: " + ex.Message);
            }
            // Read Command from Socket and Execute
            
            var t = new Thread(new ThreadStart(SocketCommands));
            t.Start();
        }

        private void SocketCommands()
        { 
            while (true)
            {
                try
                {
                    byte[] bytes = new byte[_socket.Available];
                    int count = _socket.Receive(bytes);
                    char[] chars = System.Text.Encoding.UTF8.GetChars(bytes);
                    string cmd = new string(chars, 0, count);

                    if (cmd != null) // cmd.Length != 0
                    {
                        ProcessCommand(cmd);
                        Debug.Print("Command: " + cmd);
                    }
                }
                catch (Exception e)
                {
                    Debug.Print("Error: " + e.Message);
                }
            }
        }

        // Process Commands
        private void ProcessCommand(string command)
        {
            // remember, motors are opposite of each other
            // one motor runs faster than the other so it's always moving an an arc

            // commands
            switch (command)
            {
                // stop
                case "/robot/panic":
                    motorControllerL298.MoveMotor(MotorControllerL298.Motor.Motor1, 0);
                    motorControllerL298.MoveMotor(MotorControllerL298.Motor.Motor2, 0);
                    break;
                case "/robot/move/stop":
                    motorControllerL298.MoveMotor(MotorControllerL298.Motor.Motor1, 0);
                    motorControllerL298.MoveMotor(MotorControllerL298.Motor.Motor2, 0);
                    break;
                case "/robot/turn/stop":
                    motorControllerL298.MoveMotor(MotorControllerL298.Motor.Motor1, 0);
                    motorControllerL298.MoveMotor(MotorControllerL298.Motor.Motor2, 0);
                    break;
                // move
                case "/robot/move/forward":
                    motorControllerL298.MoveMotor(MotorControllerL298.Motor.Motor1, 50);
                    motorControllerL298.MoveMotor(MotorControllerL298.Motor.Motor2, 50);
                    break;
                case "/robot/move/backward":
                    motorControllerL298.MoveMotor(MotorControllerL298.Motor.Motor1, -50);
                    motorControllerL298.MoveMotor(MotorControllerL298.Motor.Motor2, -50);
                    break;
                case "/robot/turn/left":
                    motorControllerL298.MoveMotor(MotorControllerL298.Motor.Motor1, 50);
                    motorControllerL298.MoveMotor(MotorControllerL298.Motor.Motor2, -50);
                    break;
                case "/robot/turn/right":
                    motorControllerL298.MoveMotor(MotorControllerL298.Motor.Motor1, -50);
                    motorControllerL298.MoveMotor(MotorControllerL298.Motor.Motor2, 50);
                    break;
            }
        }
    }
}
