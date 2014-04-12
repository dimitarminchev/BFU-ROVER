namespace Desktop_Socket_Server
{
    partial class RobotControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.forwardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backwardsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.boxPORT = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.boxIP = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonDown = new System.Windows.Forms.Button();
            this.buttonUp = new System.Windows.Forms.Button();
            this.buttonRight = new System.Windows.Forms.Button();
            this.buttonLeft = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.StatusLog = new System.Windows.Forms.ListBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.pictureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.pictureToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.exitToolStripMenuItem,
            this.exitToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1060, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "MenuStrip";
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.connectToolStripMenuItem.Text = "Connect";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.forwardToolStripMenuItem,
            this.backwardsToolStripMenuItem,
            this.leftToolStripMenuItem,
            this.rightToolStripMenuItem,
            this.stopToolStripMenuItem1});
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.stopToolStripMenuItem.Text = "Controls";
            // 
            // forwardToolStripMenuItem
            // 
            this.forwardToolStripMenuItem.Name = "forwardToolStripMenuItem";
            this.forwardToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)));
            this.forwardToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.forwardToolStripMenuItem.Text = "Forward";
            this.forwardToolStripMenuItem.Click += new System.EventHandler(this.buttonUp_Click);
            // 
            // backwardsToolStripMenuItem
            // 
            this.backwardsToolStripMenuItem.Name = "backwardsToolStripMenuItem";
            this.backwardsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
            this.backwardsToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.backwardsToolStripMenuItem.Text = "Backwards";
            this.backwardsToolStripMenuItem.Click += new System.EventHandler(this.buttonDown_Click);
            // 
            // leftToolStripMenuItem
            // 
            this.leftToolStripMenuItem.Name = "leftToolStripMenuItem";
            this.leftToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)));
            this.leftToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.leftToolStripMenuItem.Text = "Left";
            this.leftToolStripMenuItem.Click += new System.EventHandler(this.buttonLeft_Click);
            // 
            // rightToolStripMenuItem
            // 
            this.rightToolStripMenuItem.Name = "rightToolStripMenuItem";
            this.rightToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)));
            this.rightToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.rightToolStripMenuItem.Text = "Right";
            this.rightToolStripMenuItem.Click += new System.EventHandler(this.buttonRight_Click);
            // 
            // stopToolStripMenuItem1
            // 
            this.stopToolStripMenuItem1.Name = "stopToolStripMenuItem1";
            this.stopToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Space)));
            this.stopToolStripMenuItem1.Size = new System.Drawing.Size(195, 22);
            this.stopToolStripMenuItem1.Text = "Stop";
            this.stopToolStripMenuItem1.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.exitToolStripMenuItem.Text = "About";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.exitToolStripMenuItem1.Text = "Exit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.boxPORT);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.boxIP);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox2.Location = new System.Drawing.Point(12, 39);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(312, 64);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Socket Server Settings";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(196, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port";
            // 
            // boxPORT
            // 
            this.boxPORT.Location = new System.Drawing.Point(236, 25);
            this.boxPORT.Name = "boxPORT";
            this.boxPORT.Size = new System.Drawing.Size(50, 23);
            this.boxPORT.TabIndex = 1;
            this.boxPORT.Text = "5150";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "IP Address";
            // 
            // boxIP
            // 
            this.boxIP.Location = new System.Drawing.Point(90, 25);
            this.boxIP.Name = "boxIP";
            this.boxIP.Size = new System.Drawing.Size(100, 23);
            this.boxIP.TabIndex = 0;
            this.boxIP.Text = "192.168.0.100";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonStop);
            this.groupBox1.Controls.Add(this.buttonDown);
            this.groupBox1.Controls.Add(this.buttonUp);
            this.groupBox1.Controls.Add(this.buttonRight);
            this.groupBox1.Controls.Add(this.buttonLeft);
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox1.Location = new System.Drawing.Point(12, 109);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(312, 180);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Control";
            // 
            // buttonStop
            // 
            this.buttonStop.Font = new System.Drawing.Font("Wingdings", 23F);
            this.buttonStop.Location = new System.Drawing.Point(11, 115);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(49, 50);
            this.buttonStop.TabIndex = 13;
            this.buttonStop.TabStop = false;
            this.buttonStop.Text = "p";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonDown
            // 
            this.buttonDown.Font = new System.Drawing.Font("Wingdings", 23F);
            this.buttonDown.Location = new System.Drawing.Point(133, 115);
            this.buttonDown.Name = "buttonDown";
            this.buttonDown.Size = new System.Drawing.Size(50, 50);
            this.buttonDown.TabIndex = 9;
            this.buttonDown.TabStop = false;
            this.buttonDown.Text = "к";
            this.buttonDown.UseVisualStyleBackColor = true;
            this.buttonDown.Click += new System.EventHandler(this.buttonDown_Click);
            // 
            // buttonUp
            // 
            this.buttonUp.Font = new System.Drawing.Font("Wingdings", 23F);
            this.buttonUp.Location = new System.Drawing.Point(133, 59);
            this.buttonUp.Name = "buttonUp";
            this.buttonUp.Size = new System.Drawing.Size(50, 50);
            this.buttonUp.TabIndex = 8;
            this.buttonUp.TabStop = false;
            this.buttonUp.Text = "й";
            this.buttonUp.UseVisualStyleBackColor = true;
            this.buttonUp.Click += new System.EventHandler(this.buttonUp_Click);
            // 
            // buttonRight
            // 
            this.buttonRight.Font = new System.Drawing.Font("Wingdings", 23F);
            this.buttonRight.Location = new System.Drawing.Point(189, 115);
            this.buttonRight.Name = "buttonRight";
            this.buttonRight.Size = new System.Drawing.Size(50, 50);
            this.buttonRight.TabIndex = 7;
            this.buttonRight.TabStop = false;
            this.buttonRight.Text = "и";
            this.buttonRight.UseVisualStyleBackColor = true;
            this.buttonRight.Click += new System.EventHandler(this.buttonRight_Click);
            // 
            // buttonLeft
            // 
            this.buttonLeft.Font = new System.Drawing.Font("Wingdings", 23F);
            this.buttonLeft.Location = new System.Drawing.Point(77, 115);
            this.buttonLeft.Name = "buttonLeft";
            this.buttonLeft.Size = new System.Drawing.Size(50, 50);
            this.buttonLeft.TabIndex = 6;
            this.buttonLeft.TabStop = false;
            this.buttonLeft.Text = "з";
            this.buttonLeft.UseVisualStyleBackColor = true;
            this.buttonLeft.Click += new System.EventHandler(this.buttonLeft_Click);
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.SystemColors.Control;
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox1.Enabled = false;
            this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.listBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Items.AddRange(new object[] {
            "Move Forward\t\t\t[Ctrl + Up]",
            "Move Backward\t\t\t[Ctrl + Down]",
            "Turn Left\t\t\t\t[Ctrl + Left]",
            "Turn Right\t\t\t[Ctrl + Right]",
            "Stop\t\t\t\t[Ctrl + Space]"});
            this.listBox1.Location = new System.Drawing.Point(7, 22);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(297, 75);
            this.listBox1.TabIndex = 12;
            this.listBox1.TabStop = false;
            // 
            // StatusLog
            // 
            this.StatusLog.BackColor = System.Drawing.Color.Black;
            this.StatusLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.StatusLog.ForeColor = System.Drawing.Color.Lime;
            this.StatusLog.FormattingEnabled = true;
            this.StatusLog.ItemHeight = 16;
            this.StatusLog.Location = new System.Drawing.Point(667, 49);
            this.StatusLog.Name = "StatusLog";
            this.StatusLog.ScrollAlwaysVisible = true;
            this.StatusLog.Size = new System.Drawing.Size(382, 244);
            this.StatusLog.TabIndex = 12;
            this.StatusLog.TabStop = false;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(334, 49);
            this.webBrowser1.Margin = new System.Windows.Forms.Padding(0);
            this.webBrowser1.MaximumSize = new System.Drawing.Size(320, 240);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(320, 240);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScrollBarsEnabled = false;
            this.webBrowser1.Size = new System.Drawing.Size(320, 240);
            this.webBrowser1.TabIndex = 15;
            this.webBrowser1.Url = new System.Uri("http://192.168.0.101", System.UriKind.Absolute);
            // 
            // pictureToolStripMenuItem
            // 
            this.pictureToolStripMenuItem.Name = "pictureToolStripMenuItem";
            this.pictureToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.pictureToolStripMenuItem.Text = "Picture";
            this.pictureToolStripMenuItem.Click += new System.EventHandler(this.pictureToolStripMenuItem_Click);
            // 
            // RobotControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1060, 302);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.StatusLog);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "RobotControl";
            this.Text = "Robot Control 1.0";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem forwardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backwardsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem leftToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox boxPORT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox boxIP;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonDown;
        private System.Windows.Forms.Button buttonUp;
        private System.Windows.Forms.Button buttonRight;
        private System.Windows.Forms.Button buttonLeft;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox StatusLog;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.ToolStripMenuItem pictureToolStripMenuItem;
        private System.Windows.Forms.WebBrowser webBrowser1;
    }
}

