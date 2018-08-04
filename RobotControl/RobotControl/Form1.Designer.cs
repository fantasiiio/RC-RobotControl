namespace RobotControl
{
    partial class Form1
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabConnect = new System.Windows.Forms.TabPage();
            this.txtLogs = new System.Windows.Forms.RichTextBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtCommand = new System.Windows.Forms.TextBox();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cboSerialDevices = new System.Windows.Forms.ComboBox();
            this.cboBaudRates = new System.Windows.Forms.ComboBox();
            this.tabCenter = new System.Windows.Forms.TabPage();
            this.tabReverse = new System.Windows.Forms.TabPage();
            this.tabCalibrate = new System.Windows.Forms.TabPage();
            this.tabAngle = new System.Windows.Forms.TabPage();
            this.tabPWM = new System.Windows.Forms.TabPage();
            this.tabMappings = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.numChModeMax = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numChModeMin = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.cboMappingMode = new System.Windows.Forms.ComboBox();
            this.cboModeChannel = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAddChMap = new System.Windows.Forms.Button();
            this.panelChannelMap = new System.Windows.Forms.Panel();
            this.tabInput = new System.Windows.Forms.TabPage();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.inputOutputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EnableMappingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EnablePPMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chkIncoming = new System.Windows.Forms.CheckBox();
            this.chkOutgoing = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabConnect.SuspendLayout();
            this.tabMappings.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numChModeMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numChModeMin)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabConnect);
            this.tabControl1.Controls.Add(this.tabCenter);
            this.tabControl1.Controls.Add(this.tabReverse);
            this.tabControl1.Controls.Add(this.tabCalibrate);
            this.tabControl1.Controls.Add(this.tabAngle);
            this.tabControl1.Controls.Add(this.tabPWM);
            this.tabControl1.Controls.Add(this.tabMappings);
            this.tabControl1.Controls.Add(this.tabInput);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(645, 432);
            this.tabControl1.TabIndex = 12;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tabConnect
            // 
            this.tabConnect.Controls.Add(this.chkOutgoing);
            this.tabConnect.Controls.Add(this.chkIncoming);
            this.tabConnect.Controls.Add(this.txtLogs);
            this.tabConnect.Controls.Add(this.btnRefresh);
            this.tabConnect.Controls.Add(this.btnSend);
            this.tabConnect.Controls.Add(this.txtCommand);
            this.tabConnect.Controls.Add(this.btnDisconnect);
            this.tabConnect.Controls.Add(this.btnConnect);
            this.tabConnect.Controls.Add(this.label2);
            this.tabConnect.Controls.Add(this.label4);
            this.tabConnect.Controls.Add(this.cboSerialDevices);
            this.tabConnect.Controls.Add(this.cboBaudRates);
            this.tabConnect.Location = new System.Drawing.Point(4, 22);
            this.tabConnect.Name = "tabConnect";
            this.tabConnect.Padding = new System.Windows.Forms.Padding(3);
            this.tabConnect.Size = new System.Drawing.Size(637, 406);
            this.tabConnect.TabIndex = 0;
            this.tabConnect.Text = "Connect";
            this.tabConnect.UseVisualStyleBackColor = true;
            // 
            // txtLogs
            // 
            this.txtLogs.Location = new System.Drawing.Point(6, 86);
            this.txtLogs.Name = "txtLogs";
            this.txtLogs.Size = new System.Drawing.Size(625, 286);
            this.txtLogs.TabIndex = 20;
            this.txtLogs.Text = "";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(562, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 19;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point(556, 376);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 18;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtCommand
            // 
            this.txtCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCommand.Location = new System.Drawing.Point(6, 378);
            this.txtCommand.Name = "txtCommand";
            this.txtCommand.Size = new System.Drawing.Size(544, 20);
            this.txtCommand.TabIndex = 17;
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(87, 57);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(75, 23);
            this.btnDisconnect.TabIndex = 16;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(6, 57);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 15;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Baud rate:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Serial port:";
            // 
            // cboSerialDevices
            // 
            this.cboSerialDevices.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSerialDevices.FormattingEnabled = true;
            this.cboSerialDevices.Location = new System.Drawing.Point(71, 3);
            this.cboSerialDevices.Name = "cboSerialDevices";
            this.cboSerialDevices.Size = new System.Drawing.Size(488, 21);
            this.cboSerialDevices.TabIndex = 10;
            // 
            // cboBaudRates
            // 
            this.cboBaudRates.FormattingEnabled = true;
            this.cboBaudRates.Location = new System.Drawing.Point(71, 30);
            this.cboBaudRates.Name = "cboBaudRates";
            this.cboBaudRates.Size = new System.Drawing.Size(121, 21);
            this.cboBaudRates.TabIndex = 11;
            // 
            // tabCenter
            // 
            this.tabCenter.Location = new System.Drawing.Point(4, 22);
            this.tabCenter.Name = "tabCenter";
            this.tabCenter.Padding = new System.Windows.Forms.Padding(3);
            this.tabCenter.Size = new System.Drawing.Size(637, 406);
            this.tabCenter.TabIndex = 1;
            this.tabCenter.Text = "Center";
            this.tabCenter.UseVisualStyleBackColor = true;
            // 
            // tabReverse
            // 
            this.tabReverse.Location = new System.Drawing.Point(4, 22);
            this.tabReverse.Name = "tabReverse";
            this.tabReverse.Size = new System.Drawing.Size(637, 406);
            this.tabReverse.TabIndex = 6;
            this.tabReverse.Text = "Reverse";
            this.tabReverse.UseVisualStyleBackColor = true;
            // 
            // tabCalibrate
            // 
            this.tabCalibrate.Location = new System.Drawing.Point(4, 22);
            this.tabCalibrate.Name = "tabCalibrate";
            this.tabCalibrate.Size = new System.Drawing.Size(637, 406);
            this.tabCalibrate.TabIndex = 7;
            this.tabCalibrate.Text = "Calibrate";
            this.tabCalibrate.UseVisualStyleBackColor = true;
            // 
            // tabAngle
            // 
            this.tabAngle.Location = new System.Drawing.Point(4, 22);
            this.tabAngle.Name = "tabAngle";
            this.tabAngle.Size = new System.Drawing.Size(637, 406);
            this.tabAngle.TabIndex = 2;
            this.tabAngle.Text = "Angle";
            this.tabAngle.UseVisualStyleBackColor = true;
            // 
            // tabPWM
            // 
            this.tabPWM.Location = new System.Drawing.Point(4, 22);
            this.tabPWM.Name = "tabPWM";
            this.tabPWM.Size = new System.Drawing.Size(637, 406);
            this.tabPWM.TabIndex = 3;
            this.tabPWM.Text = "PWM";
            this.tabPWM.UseVisualStyleBackColor = true;
            // 
            // tabMappings
            // 
            this.tabMappings.Controls.Add(this.groupBox1);
            this.tabMappings.Controls.Add(this.cboModeChannel);
            this.tabMappings.Controls.Add(this.label3);
            this.tabMappings.Controls.Add(this.btnAddChMap);
            this.tabMappings.Controls.Add(this.panelChannelMap);
            this.tabMappings.Location = new System.Drawing.Point(4, 22);
            this.tabMappings.Name = "tabMappings";
            this.tabMappings.Size = new System.Drawing.Size(637, 406);
            this.tabMappings.TabIndex = 5;
            this.tabMappings.Text = "Mappings";
            this.tabMappings.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.numChModeMax);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.numChModeMin);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cboMappingMode);
            this.groupBox1.Location = new System.Drawing.Point(3, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 72);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(109, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Max";
            // 
            // numChModeMax
            // 
            this.numChModeMax.DecimalPlaces = 1;
            this.numChModeMax.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numChModeMax.Location = new System.Drawing.Point(139, 44);
            this.numChModeMax.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numChModeMax.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numChModeMax.Name = "numChModeMax";
            this.numChModeMax.Size = new System.Drawing.Size(53, 20);
            this.numChModeMax.TabIndex = 8;
            this.numChModeMax.ValueChanged += new System.EventHandler(this.numChModeMax_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Min";
            // 
            // numChModeMin
            // 
            this.numChModeMin.DecimalPlaces = 1;
            this.numChModeMin.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numChModeMin.Location = new System.Drawing.Point(45, 44);
            this.numChModeMin.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numChModeMin.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numChModeMin.Name = "numChModeMin";
            this.numChModeMin.Size = new System.Drawing.Size(53, 20);
            this.numChModeMin.TabIndex = 0;
            this.numChModeMin.ValueChanged += new System.EventHandler(this.numChModeMin_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mode";
            // 
            // cboMappingMode
            // 
            this.cboMappingMode.FormattingEnabled = true;
            this.cboMappingMode.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.cboMappingMode.Location = new System.Drawing.Point(45, 13);
            this.cboMappingMode.Name = "cboMappingMode";
            this.cboMappingMode.Size = new System.Drawing.Size(34, 21);
            this.cboMappingMode.TabIndex = 1;
            this.cboMappingMode.SelectedIndexChanged += new System.EventHandler(this.cboMappingMode_SelectedIndexChanged);
            // 
            // cboModeChannel
            // 
            this.cboModeChannel.FormattingEnabled = true;
            this.cboModeChannel.Items.AddRange(new object[] {
            "None"});
            this.cboModeChannel.Location = new System.Drawing.Point(297, 9);
            this.cboModeChannel.Name = "cboModeChannel";
            this.cboModeChannel.Size = new System.Drawing.Size(61, 21);
            this.cboModeChannel.TabIndex = 5;
            this.cboModeChannel.SelectedIndexChanged += new System.EventHandler(this.cboModeChannel_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(215, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Mode Channel";
            // 
            // btnAddChMap
            // 
            this.btnAddChMap.Location = new System.Drawing.Point(461, 6);
            this.btnAddChMap.Name = "btnAddChMap";
            this.btnAddChMap.Size = new System.Drawing.Size(113, 23);
            this.btnAddChMap.TabIndex = 3;
            this.btnAddChMap.Text = "Add New Mapping";
            this.btnAddChMap.UseVisualStyleBackColor = true;
            this.btnAddChMap.Click += new System.EventHandler(this.btnAddChMap_Click);
            // 
            // panelChannelMap
            // 
            this.panelChannelMap.AutoScroll = true;
            this.panelChannelMap.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelChannelMap.Location = new System.Drawing.Point(0, 76);
            this.panelChannelMap.Name = "panelChannelMap";
            this.panelChannelMap.Size = new System.Drawing.Size(637, 330);
            this.panelChannelMap.TabIndex = 2;
            // 
            // tabInput
            // 
            this.tabInput.Location = new System.Drawing.Point(4, 22);
            this.tabInput.Name = "tabInput";
            this.tabInput.Padding = new System.Windows.Forms.Padding(3);
            this.tabInput.Size = new System.Drawing.Size(637, 406);
            this.tabInput.TabIndex = 4;
            this.tabInput.Text = "Inputs";
            this.tabInput.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inputOutputToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(645, 24);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // inputOutputToolStripMenuItem
            // 
            this.inputOutputToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EnableMappingToolStripMenuItem,
            this.EnablePPMToolStripMenuItem});
            this.inputOutputToolStripMenuItem.Name = "inputOutputToolStripMenuItem";
            this.inputOutputToolStripMenuItem.Size = new System.Drawing.Size(96, 20);
            this.inputOutputToolStripMenuItem.Text = "Input / Output";
            // 
            // EnableMappingToolStripMenuItem
            // 
            this.EnableMappingToolStripMenuItem.Name = "EnableMappingToolStripMenuItem";
            this.EnableMappingToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.EnableMappingToolStripMenuItem.Text = "Enable Mappings";
            this.EnableMappingToolStripMenuItem.Click += new System.EventHandler(this.enableMappingToolStripMenuItem_Click);
            // 
            // EnablePPMToolStripMenuItem
            // 
            this.EnablePPMToolStripMenuItem.Name = "EnablePPMToolStripMenuItem";
            this.EnablePPMToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.EnablePPMToolStripMenuItem.Text = "Enable PPM (RC Control)";
            this.EnablePPMToolStripMenuItem.Click += new System.EventHandler(this.EnablePPMToolStripMenuItem_Click);
            // 
            // chkIncoming
            // 
            this.chkIncoming.AutoSize = true;
            this.chkIncoming.Location = new System.Drawing.Point(562, 61);
            this.chkIncoming.Name = "chkIncoming";
            this.chkIncoming.Size = new System.Drawing.Size(69, 17);
            this.chkIncoming.TabIndex = 21;
            this.chkIncoming.Text = "Incoming";
            this.chkIncoming.UseVisualStyleBackColor = true;
            this.chkIncoming.CheckedChanged += new System.EventHandler(this.chkViewIncoming_CheckedChanged);
            // 
            // chkOutgoing
            // 
            this.chkOutgoing.AutoSize = true;
            this.chkOutgoing.Location = new System.Drawing.Point(465, 61);
            this.chkOutgoing.Name = "chkOutgoing";
            this.chkOutgoing.Size = new System.Drawing.Size(69, 17);
            this.chkOutgoing.TabIndex = 22;
            this.chkOutgoing.Text = "Outgoing";
            this.chkOutgoing.UseVisualStyleBackColor = true;
            this.chkOutgoing.CheckedChanged += new System.EventHandler(this.chkOutcomming_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 456);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.Text = "RobotControl Configurator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabConnect.ResumeLayout(false);
            this.tabConnect.PerformLayout();
            this.tabMappings.ResumeLayout(false);
            this.tabMappings.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numChModeMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numChModeMin)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabConnect;
        private System.Windows.Forms.TabPage tabCenter;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboSerialDevices;
        private System.Windows.Forms.ComboBox cboBaudRates;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtCommand;
        private System.Windows.Forms.TabPage tabAngle;
        private System.Windows.Forms.TabPage tabPWM;
        private System.Windows.Forms.TabPage tabInput;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem inputOutputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EnableMappingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EnablePPMToolStripMenuItem;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TabPage tabMappings;
        private System.Windows.Forms.ComboBox cboMappingMode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddChMap;
        private System.Windows.Forms.TabPage tabReverse;
        private System.Windows.Forms.ComboBox cboModeChannel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numChModeMax;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numChModeMin;
        private System.Windows.Forms.Panel panelChannelMap;
        private System.Windows.Forms.RichTextBox txtLogs;
        private System.Windows.Forms.TabPage tabCalibrate;
        private System.Windows.Forms.CheckBox chkIncoming;
        private System.Windows.Forms.CheckBox chkOutgoing;
    }
}

