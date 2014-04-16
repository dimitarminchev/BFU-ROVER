using System;
using System.Collections;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Presentation.Shapes;
using Microsoft.SPOT.Touch;
// Gadgeteer
using Gadgeteer.Networking;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using Gadgeteer.Modules.GHIElectronics;
// Sockets
using System.Net.Sockets;
using System.Net;
using GHI.Premium.Net;
using System.IO;
using System.Text;

namespace RobotApp
{
    public partial class Program
    {
        // Settings      
        private string SSID = "minchev.eu";
        private string PASSWORD = "bfurover";
        private string IP = "192.168.0.101";
        private int PORT = 5150;

        // Sockets
        private Socket _socket = null;
        Thread _listenerThread = null;

        // Timer (1 sec. = 1000 ms)
        GT.Timer NetworkTimer = new GT.Timer(5000);
        GT.Timer PictureTimer = new GT.Timer(2000);

        // Picture
        GT.Picture picture = null;

        // This method is run when the mainboard is powered up or reset.   
        void ProgramStarted()
        {
            // Initialize Motors            
            motorControllerL298.MoveMotor(MotorControllerL298.Motor.Motor1, 0);
            motorControllerL298.MoveMotor(MotorControllerL298.Motor.Motor2, 0);

            // Network Timer
            NetworkTimer.Tick += new GT.Timer.TickEventHandler(NetworkTimer_Tick);
            NetworkTimer.Start();

            // Picture Timer
            PictureTimer.Tick += new GT.Timer.TickEventHandler(PictureTimer_Tick);
            PictureTimer.Start();

            // Picture Captured 
            camera.PictureCaptured += new Camera.PictureCapturedEventHandler(camera_PictureCaptured);
            camera.CurrentPictureResolution = GTM.GHIElectronics.Camera.PictureResolution.Resolution320x240; 
        }

        // Step 3. Network Timer
        void NetworkTimer_Tick(GT.Timer timer)
        {
            if (NetworkTimer.IsRunning == true)
            {
                wifi_RS21.Interface.Open();
                wifi_RS21.UseDHCP();
                WiFiNetworkInfo info = new WiFiNetworkInfo();
                info.SSID = SSID;
                info.SecMode = SecurityMode.WPA2;
                info.networkType = NetworkType.AccessPoint;
                wifi_RS21.NetworkUp -= wifi_RS21_NetworkUp;
                wifi_RS21.NetworkDown -= wifi_RS21_NetworkDown;
                wifi_RS21.NetworkUp += wifi_RS21_NetworkUp;
                wifi_RS21.NetworkDown += wifi_RS21_NetworkDown;
                wifi_RS21.Interface.Join(info, PASSWORD);
            }
        }
        // Network Up
        void wifi_RS21_NetworkUp(GTM.Module.NetworkModule sender, GTM.Module.NetworkModule.NetworkState state)
        {
            if (NetworkTimer.IsRunning == true)
            {
                NetworkTimer.Stop();

                // IP = wifi_RS21.NetworkSettings.IPAddress;
                Debug.Print("Network Initialized. IP: " + IP);

                // Enable Sockets Communications
                EnableSockets();
            }
        }

        // Network Down
        void wifi_RS21_NetworkDown(GTM.Module.NetworkModule sender, GTM.Module.NetworkModule.NetworkState state)
        {
            // NetworkTimer.Start();
        }

        // Picture Timer
        void PictureTimer_Tick(GT.Timer timer)
        {
            if (camera.CameraReady) camera.TakePicture();
        }

        // Picture Captured 
        void camera_PictureCaptured(Camera sender, GT.Picture pic)
        {
            picture = pic;
        }

        // Enable Socket Communications    
        private void EnableSockets()
        {
            try
            {
                // Create a socket and connect
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                _listenerThread = new Thread(new ThreadStart(Listen));
                _listenerThread.Start();
            }
            catch (Exception err) 
            { 
                Debug.Print("Enable Sockets: " + err.Message); 
            }
        }

        // Listen for Connections
        private void Listen()
        {
            EndPoint RemoteEndPoint = null;
            try
            {
                RemoteEndPoint = new IPEndPoint(IPAddress.Any, PORT);
                _socket.Bind(RemoteEndPoint);
                _socket.Listen(1);                
                Debug.Print("Socket Initialized. " + IP + ":" + PORT.ToString());
            }
            catch (Exception err)
            {
                Debug.Print("Listen: " + err.ToString());
            }
            // process
            try
            {
                while (true)
                {
                    if (_socket.Poll(-1, SelectMode.SelectRead))
                    {
                        byte[] buffer = new byte[_socket.Available];
                        int length = _socket.ReceiveFrom(buffer, ref RemoteEndPoint);
                        OnDataReceived(buffer, length, RemoteEndPoint);
                    }
                    else
                    {
                        Thread.Sleep(10);
                    }
                }
            }
            catch (Exception err)
            {
                Debug.Print("Socket: " + err.Message);
                NetworkTimer.Start();
            }
        }

        // Received
        void OnDataReceived(byte[] data, int length, EndPoint sender)
        {
            char[] text = new char[length];
            for (int i = 0; i < length; i++) text[i] = (char)data[i];
            string command = new string(text);
            
            // Received Message
            Debug.Print("Robot Received: " + command);

            // Process the command
            ProcessCommand(command, sender);

            // Send Message
            SendMessage("Robot Sent: " + command, sender);            
        }

        // Send
        public void SendMessage(string message, EndPoint sender)
        {
            byte[] bytes = new byte[message.Length];
            for (int i = 0; i < message.Length; i++) bytes[i] = (byte)message[i];
            _socket.SendTo(bytes, sender);
            Debug.Print(message);
        }

        // Process Commands
        private void ProcessCommand(string command, EndPoint sender)
        {
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
                    // start & stop web server
                case "/web/server/start":
                    StartWebServer();
                    SendMessage("Robot Web Server Started.", sender);
                    break;
                case "/web/server/stop":
                    WebServer.StopLocalServer();
                    SendMessage("Robot Web Server Stopped.", sender);
                    break;
                case "/web/server/restart":
                    WebServer.StopLocalServer();
                    StartWebServer();
                    SendMessage("Robot Web Server Restarted.", sender);
                    break;
            }
        }

        // Start Camera Web Server on Port 8080
        public void StartWebServer()
        {
            WebServer.StartLocalServer(IP, 8080);
            WebEvent TakePicture = WebServer.SetupWebEvent("");
            TakePicture.WebEventReceived -= new WebEvent.ReceivedWebEventHandler(TakePicture_WebEventReceived);
            TakePicture.WebEventReceived += new WebEvent.ReceivedWebEventHandler(TakePicture_WebEventReceived);
        }
        void TakePicture_WebEventReceived(string path, WebServer.HttpMethod method, Responder responder)
        {
            GT.Picture jpeg = new GT.Picture(picture.PictureData, Gadgeteer.Picture.PictureEncoding.JPEG);
            if (picture != null) responder.Respond(jpeg);
        }
    }
}
