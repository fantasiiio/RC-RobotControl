namespace RobotControl
{
    partial class ucServoCalibrate
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
            this.label1 = new System.Windows.Forms.Label();
            this.numMaxPWM = new System.Windows.Forms.NumericUpDown();
            this.numMaxAngle = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.btnApply = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxPWM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxAngle)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Max Angle";
            // 
            // numMaxPWM
            // 
            this.numMaxPWM.Location = new System.Drawing.Point(3, 41);
            this.numMaxPWM.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numMaxPWM.Name = "numMaxPWM";
            this.numMaxPWM.Size = new System.Drawing.Size(54, 20);
            this.numMaxPWM.TabIndex = 2;
            // 
            // numMaxAngle
            // 
            this.numMaxAngle.Location = new System.Drawing.Point(3, 90);
            this.numMaxAngle.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numMaxAngle.Name = "numMaxAngle";
            this.numMaxAngle.Size = new System.Drawing.Size(54, 20);
            this.numMaxAngle.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Max PWM";
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(3, 116);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(54, 23);
            this.btnApply.TabIndex = 5;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnApply);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numMaxAngle);
            this.groupBox1.Controls.Add(this.numMaxPWM);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(63, 145);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Servo1";
            // 
            // ucServoCalibrate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ucServoCalibrate";
            this.Size = new System.Drawing.Size(67, 147);
            this.Load += new System.EventHandler(this.ucServoCalibrate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numMaxPWM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxAngle)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numMaxPWM;
        private System.Windows.Forms.NumericUpDown numMaxAngle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.GroupBox groupBox1;

    }
}
