using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace Desktop_Socket_Server
{  
    class RobotService
    {
        private static RobotService _current;
        public static RobotService Current
        {
            get
            {
                if (_current == null)
                    _current = new RobotService();

                return _current;
            }
        }

        // Robots Commands
        public void PanicStop()
        {
            // this is not as cool or as scary as it sounds
            SendMessage("/robot/panic");
        }

        public void StartMovingForward()
        {
            SendMessage("/robot/move/forward");
        }

        public void StartMovingBackward()
        {
            SendMessage("/robot/move/backward");
        }

        public void StopMoving()
        {
            SendMessage("/robot/move/stop");
        }

        public void StartTurningLeft()
        {
            SendMessage("/robot/turn/left");
        }

        public void StartTurningRight()
        {
            SendMessage("/robot/turn/right");
        }

        public void StopTurning()
        {
            SendMessage("/robot/turn/stop");
        }


        // Client and Server
        private TcpListener server = null; 
        private Socket client = null;

        // Disconnect
        public void Disconnect()
        {
            if (client != null)
            {
                client.Close();
                client = null;
            }
            if (server != null)
            {
                server.Stop();
                server = null;
            }
        }

        // Connect 
        public void Connect()
        {
            try
            {
                // Set IP Address and Port
                IPAddress address = IPAddress.Parse(RobotControl.IP);
                Int32 port = Int32.Parse(RobotControl.PORT);
                
                // Stop Server if Running
                if (server != null) server.Stop();

                // Start TcpListener
                server = new TcpListener(address, port);
                server.Start();

                // Process
                Thread t = new Thread(new ThreadStart(Service));
                t.IsBackground = true;
                t.Start();

                // log
                RobotControl.Status.WriteLine("Server: {0}:{1} Started.", RobotControl.IP, RobotControl.PORT);           

                // Wait To Accept Client
                t.Join();

                // Client Accepted
                RobotControl.Status.WriteLine("Client: {0} Connected.", client.RemoteEndPoint);
                SendMessage("/robot/welcome"); 

            }
            catch (Exception e)
            {
                RobotControl.Status.WriteLine("Error: {0}", e.Message);
            }
        }

        // Process the Connection
        private void Service()
        {
            try
            {
                // Accept Client
                client = server.AcceptSocket();        
            }
            catch(Exception e)
            {
                RobotControl.Status.WriteLine("Error: {0}", e.Message);
                client = null;
            }
        }


        // we're talking to only one client, so need only one writer
        // keep a dictionary identified by endpoint if you need multiple
        private void SendMessage(string message)
        {
            try
            {
                // Process the data 
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(message);
                int len = client.Send(msg);
                // log
                RobotControl.Status.WriteLine("Send: {0}", message);
            }
            catch (Exception ex)
            {
                RobotControl.Status.WriteLine("Error: " + ex.Message );
            }
        }
    }
}
