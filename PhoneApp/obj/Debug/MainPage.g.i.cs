﻿#pragma checksum "C:\Users\Dimitar\documents\visual studio 2012\Projects\BFUROVER\PhoneApp\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "AA52CE882FD5466F4F1CB9806CA99820"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace PhoneApp {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.StackPanel ContentPanel;
        
        internal System.Windows.Controls.TextBox ServerIP;
        
        internal System.Windows.Controls.TextBox ServerPort;
        
        internal System.Windows.Controls.Button ConnectButton;
        
        internal System.Windows.Controls.Button sockStop;
        
        internal System.Windows.Controls.Button sockForward;
        
        internal System.Windows.Controls.Button sockBack;
        
        internal System.Windows.Controls.Button sockLeft;
        
        internal System.Windows.Controls.Button sockRight;
        
        internal System.Windows.Controls.Button webStart;
        
        internal System.Windows.Controls.Button webStop;
        
        internal System.Windows.Controls.ProgressBar PictureProgress;
        
        internal System.Windows.Controls.ListBox Log;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/PhoneApp;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.ContentPanel = ((System.Windows.Controls.StackPanel)(this.FindName("ContentPanel")));
            this.ServerIP = ((System.Windows.Controls.TextBox)(this.FindName("ServerIP")));
            this.ServerPort = ((System.Windows.Controls.TextBox)(this.FindName("ServerPort")));
            this.ConnectButton = ((System.Windows.Controls.Button)(this.FindName("ConnectButton")));
            this.sockStop = ((System.Windows.Controls.Button)(this.FindName("sockStop")));
            this.sockForward = ((System.Windows.Controls.Button)(this.FindName("sockForward")));
            this.sockBack = ((System.Windows.Controls.Button)(this.FindName("sockBack")));
            this.sockLeft = ((System.Windows.Controls.Button)(this.FindName("sockLeft")));
            this.sockRight = ((System.Windows.Controls.Button)(this.FindName("sockRight")));
            this.webStart = ((System.Windows.Controls.Button)(this.FindName("webStart")));
            this.webStop = ((System.Windows.Controls.Button)(this.FindName("webStop")));
            this.PictureProgress = ((System.Windows.Controls.ProgressBar)(this.FindName("PictureProgress")));
            this.Log = ((System.Windows.Controls.ListBox)(this.FindName("Log")));
        }
    }
}

