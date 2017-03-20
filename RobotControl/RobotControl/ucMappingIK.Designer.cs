namespace RobotControl
{
    partial class ucMappingIK
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numParam2 = new System.Windows.Forms.NumericUpDown();
            this.numParam1 = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.numMultiplier = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cboServo1 = new System.Windows.Forms.ComboBox();
            this.cboPositionning = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cboServo2 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboServo3 = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cboChannel1 = new System.Windows.Forms.ComboBox();
            this.cboChannel3 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cboChannel2 = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numParam2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numParam1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMultiplier)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.numParam2);
            this.groupBox1.Controls.Add(this.numParam1);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.numMultiplier);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.cboServo1);
            this.groupBox1.Controls.Add(this.cboPositionning);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.cboServo2);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cboServo3);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.cboChannel1);
            this.groupBox1.Controls.Add(this.cboChannel3);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.cboChannel2);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(697, 127);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mapping";
            // 
            // numParam2
            // 
            this.numParam2.Location = new System.Drawing.Point(313, 104);
            this.numParam2.Name = "numParam2";
            this.numParam2.Size = new System.Drawing.Size(56, 20);
            this.numParam2.TabIndex = 29;
            // 
            // numParam1
            // 
            this.numParam1.Location = new System.Drawing.Point(164, 104);
            this.numParam1.Name = "numParam1";
            this.numParam1.Size = new System.Drawing.Size(56, 20);
            this.numParam1.TabIndex = 28;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(263, 106);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(36, 13);
            this.label11.TabIndex = 27;
            this.label11.Text = "param";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(124, 103);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(36, 13);
            this.label12.TabIndex = 25;
            this.label12.Text = "param";
            // 
            // numMultiplier
            // 
            this.numMultiplier.Location = new System.Drawing.Point(313, 73);
            this.numMultiplier.Name = "numMultiplier";
            this.numMultiplier.Size = new System.Drawing.Size(56, 20);
            this.numMultiplier.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(124, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Servo";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(251, 79);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(48, 13);
            this.label14.TabIndex = 22;
            this.label14.Text = "Multiplier";
            // 
            // cboServo1
            // 
            this.cboServo1.FormattingEnabled = true;
            this.cboServo1.Location = new System.Drawing.Point(164, 19);
            this.cboServo1.Name = "cboServo1";
            this.cboServo1.Size = new System.Drawing.Size(34, 21);
            this.cboServo1.TabIndex = 5;
            // 
            // cboPositionning
            // 
            this.cboPositionning.FormattingEnabled = true;
            this.cboPositionning.Location = new System.Drawing.Point(164, 73);
            this.cboPositionning.Name = "cboPositionning";
            this.cboPositionning.Size = new System.Drawing.Size(81, 21);
            this.cboPositionning.TabIndex = 21;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(211, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Servo";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(102, 76);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(58, 13);
            this.label13.TabIndex = 20;
            this.label13.Text = "Positioning";
            // 
            // cboServo2
            // 
            this.cboServo2.FormattingEnabled = true;
            this.cboServo2.Location = new System.Drawing.Point(251, 19);
            this.cboServo2.Name = "cboServo2";
            this.cboServo2.Size = new System.Drawing.Size(34, 21);
            this.cboServo2.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(295, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Servo";
            // 
            // cboServo3
            // 
            this.cboServo3.FormattingEnabled = true;
            this.cboServo3.Location = new System.Drawing.Point(335, 19);
            this.cboServo3.Name = "cboServo3";
            this.cboServo3.Size = new System.Drawing.Size(34, 21);
            this.cboServo3.TabIndex = 9;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(139, 49);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(19, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "ch";
            // 
            // cboChannel1
            // 
            this.cboChannel1.FormattingEnabled = true;
            this.cboChannel1.Location = new System.Drawing.Point(164, 46);
            this.cboChannel1.Name = "cboChannel1";
            this.cboChannel1.Size = new System.Drawing.Size(34, 21);
            this.cboChannel1.TabIndex = 11;
            // 
            // cboChannel3
            // 
            this.cboChannel3.FormattingEnabled = true;
            this.cboChannel3.Location = new System.Drawing.Point(335, 44);
            this.cboChannel3.Name = "cboChannel3";
            this.cboChannel3.Size = new System.Drawing.Size(34, 21);
            this.cboChannel3.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(226, 47);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(19, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "ch";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(310, 49);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(19, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "ch";
            // 
            // cboChannel2
            // 
            this.cboChannel2.FormattingEnabled = true;
            this.cboChannel2.Location = new System.Drawing.Point(251, 44);
            this.cboChannel2.Name = "cboChannel2";
            this.cboChannel2.Size = new System.Drawing.Size(34, 21);
            this.cboChannel2.TabIndex = 13;
            // 
            // ucMappingIK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ucMappingIK";
            this.Size = new System.Drawing.Size(700, 129);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numParam2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numParam1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMultiplier)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown numMultiplier;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cboServo1;
        private System.Windows.Forms.ComboBox cboPositionning;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cboServo2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboServo3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cboChannel1;
        private System.Windows.Forms.ComboBox cboChannel3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cboChannel2;
        private System.Windows.Forms.NumericUpDown numParam2;
        private System.Windows.Forms.NumericUpDown numParam1;
    }
}
