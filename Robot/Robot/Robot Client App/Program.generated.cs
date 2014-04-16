//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34011
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Robot_Client_App {
    using Gadgeteer;
    using GTM = Gadgeteer.Modules;
    
    
    public partial class Program : Gadgeteer.Program {
        
        /// <summary>The UsbClientDP module using socket 1 of the mainboard.</summary>
        private Gadgeteer.Modules.GHIElectronics.UsbClientDP usbClientDP;
        
        /// <summary>The MotorControllerL298 module using socket 11 of the mainboard.</summary>
        private Gadgeteer.Modules.GHIElectronics.MotorControllerL298 motorControllerL298;
        
        /// <summary>The WiFi_RS21 (Premium) module using socket 9 of the mainboard.</summary>
        private Gadgeteer.Modules.GHIElectronics.WiFi_RS21 wifi_RS21;
        
        /// <summary>The Camera (Premium) module using socket 3 of the mainboard.</summary>
        private Gadgeteer.Modules.GHIElectronics.Camera camera;
        
        /// <summary>The SDCard module using socket 5 of the mainboard.</summary>
        private Gadgeteer.Modules.GHIElectronics.SDCard sdCard;
        
        /// <summary>This property provides access to the Mainboard API. This is normally not necessary for an end user program.</summary>
        protected new static GHIElectronics.Gadgeteer.FEZSpider Mainboard {
            get {
                return ((GHIElectronics.Gadgeteer.FEZSpider)(Gadgeteer.Program.Mainboard));
            }
            set {
                Gadgeteer.Program.Mainboard = value;
            }
        }
        
        /// <summary>This method runs automatically when the device is powered, and calls ProgramStarted.</summary>
        public static void Main() {
            // Important to initialize the Mainboard first
            Program.Mainboard = new GHIElectronics.Gadgeteer.FEZSpider();
            Program p = new Program();
            p.InitializeModules();
            p.ProgramStarted();
            // Starts Dispatcher
            p.Run();
        }
        
        private void InitializeModules() {
            this.usbClientDP = new GTM.GHIElectronics.UsbClientDP(1);
            this.motorControllerL298 = new GTM.GHIElectronics.MotorControllerL298(11);
            this.wifi_RS21 = new GTM.GHIElectronics.WiFi_RS21(9);
            this.camera = new GTM.GHIElectronics.Camera(3);
            this.sdCard = new GTM.GHIElectronics.SDCard(5);
        }
    }
}
