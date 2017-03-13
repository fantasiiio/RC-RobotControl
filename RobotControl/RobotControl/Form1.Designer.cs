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
            this.btnSend = new System.Windows.Forms.Button();
            this.txtCommand = new System.Windows.Forms.TextBox();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtLogs = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cboSerialDevices = new System.Windows.Forms.ComboBox();
            this.cboBaudRates = new System.Windows.Forms.ComboBox();
            this.tabSubtrim = new System.Windows.Forms.TabPage();
            this.tabAngle = new System.Windows.Forms.TabPage();
            this.tabPWM = new System.Windows.Forms.TabPage();
            this.tabInput = new System.Windows.Forms.TabPage();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.inputOutputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EnableMappingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EnablePPMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabConnect.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabConnect);
            this.tabControl1.Controls.Add(this.tabSubtrim);
            this.tabControl1.Controls.Add(this.tabAngle);
            this.tabControl1.Controls.Add(this.tabPWM);
            this.tabControl1.Controls.Add(this.tabInput);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(570, 432);
            this.tabControl1.TabIndex = 12;
            // 
            // tabConnect
            // 
            this.tabConnect.Controls.Add(this.btnRefresh);
            this.tabConnect.Controls.Add(this.btnSend);
            this.tabConnect.Controls.Add(this.txtCommand);
            this.tabConnect.Controls.Add(this.btnDisconnect);
            this.tabConnect.Controls.Add(this.btnConnect);
            this.tabConnect.Controls.Add(this.txtLogs);
            this.tabConnect.Controls.Add(this.label2);
            this.tabConnect.Controls.Add(this.label4);
            this.tabConnect.Controls.Add(this.cboSerialDevices);
            this.tabConnect.Controls.Add(this.cboBaudRates);
            this.tabConnect.Location = new System.Drawing.Point(4, 22);
            this.tabConnect.Name = "tabConnect";
            this.tabConnect.Padding = new System.Windows.Forms.Padding(3);
            this.tabConnect.Size = new System.Drawing.Size(562, 406);
            this.tabConnect.TabIndex = 0;
            this.tabConnect.Text = "Connect";
            this.tabConnect.UseVisualStyleBackColor = true;
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point(481, 376);
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
            this.txtCommand.Size = new System.Drawing.Size(469, 20);
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
            // txtLogs
            // 
            this.txtLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLogs.Location = new System.Drawing.Point(3, 86);
            this.txtLogs.Multiline = true;
            this.txtLogs.Name = "txtLogs";
            this.txtLogs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLogs.Size = new System.Drawing.Size(553, 284);
            this.txtLogs.TabIndex = 14;
            this.txtLogs.WordWrap = false;
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
            this.cboSerialDevices.Size = new System.Drawing.Size(413, 21);
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
            // tabSubtrim
            // 
            this.tabSubtrim.Location = new System.Drawing.Point(4, 22);
            this.tabSubtrim.Name = "tabSubtrim";
            this.tabSubtrim.Padding = new System.Windows.Forms.Padding(3);
            this.tabSubtrim.Size = new System.Drawing.Size(562, 406);
            this.tabSubtrim.TabIndex = 1;
            this.tabSubtrim.Text = "Subtrim";
            this.tabSubtrim.UseVisualStyleBackColor = true;
            // 
            // tabAngle
            // 
            this.tabAngle.Location = new System.Drawing.Point(4, 22);
            this.tabAngle.Name = "tabAngle";
            this.tabAngle.Size = new System.Drawing.Size(562, 406);
            this.tabAngle.TabIndex = 2;
            this.tabAngle.Text = "Angle";
            this.tabAngle.UseVisualStyleBackColor = true;
            // 
            // tabPWM
            // 
            this.tabPWM.Location = new System.Drawing.Point(4, 22);
            this.tabPWM.Name = "tabPWM";
            this.tabPWM.Size = new System.Drawing.Size(562, 406);
            this.tabPWM.TabIndex = 3;
            this.tabPWM.Text = "PWM";
            this.tabPWM.UseVisualStyleBackColor = true;
            // 
            // tabInput
            // 
            this.tabInput.Location = new System.Drawing.Point(4, 22);
            this.tabInput.Name = "tabInput";
            this.tabInput.Padding = new System.Windows.Forms.Padding(3);
            this.tabInput.Size = new System.Drawing.Size(562, 406);
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
            this.menuStrip1.Size = new System.Drawing.Size(570, 24);
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
            this.EnableMappingToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.EnableMappingToolStripMenuItem.Text = "Enable Mappings";
            this.EnableMappingToolStripMenuItem.Click += new System.EventHandler(this.enableMappingToolStripMenuItem_Click);
            // 
            // EnablePPMToolStripMenuItem
            // 
            this.EnablePPMToolStripMenuItem.Name = "EnablePPMToolStripMenuItem";
            this.EnablePPMToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.EnablePPMToolStripMenuItem.Text = "Enable PPM";
            this.EnablePPMToolStripMenuItem.Click += new System.EventHandler(this.EnablePPMToolStripMenuItem_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(484, 1);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 19;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 456);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.Text = "RobotControl Configurator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabConnect.ResumeLayout(false);
            this.tabConnect.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabConnect;
        private System.Windows.Forms.TabPage tabSubtrim;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtLogs;
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
    }
}

