using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop_Socket_Server
{
    public partial class RobotControl : Form
    {
        // Variables
        public static ListBoxWriter Status;
        public static String IP;
        public static String PORT;

        // Constructor
        public RobotControl()
        {
            InitializeComponent();
            Status = new ListBoxWriter(StatusLog);

            // message
            Status.WriteLine("Hello!");
        }

        // connect
        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            connectToolStripMenuItem.Enabled = false;
            IP = boxIP.Text;
            PORT = boxPORT.Text;
            RobotService.Current.Connect();
        }

        // Exit
        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RobotService.Current.Disconnect();
            Close();
        }

        // Stop
        private void buttonStop_Click(object sender, EventArgs e)
        {
            RobotService.Current.StopMoving();
            RobotService.Current.StopTurning();
        }

        // Forward
        private void buttonUp_Click(object sender, EventArgs e)
        {
            RobotService.Current.StartMovingForward();
        }       

        // Backward
        private void buttonDown_Click(object sender, EventArgs e)
        {
            RobotService.Current.StartMovingBackward();
        }

        // Left
        private void buttonLeft_Click(object sender, EventArgs e)
        {
            RobotService.Current.StartTurningLeft();
        }

        // Right
        private void buttonRight_Click(object sender, EventArgs e)
        {
            RobotService.Current.StartTurningRight();
        }

        // About
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Desktop Socket Server\nRobot Control Version 1.0\nby Dimitar Minchev, PhD of Informatics");
        }

        // Take A Picture
        private void pictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Web Server Load
            webBrowser1.Navigate("http://192.168.0.101:8080/");

            // Message
            Status.WriteLine("New Picture Taken!");
        }        
        
    }
}
