namespace RobotControl
{
    partial class ucServoControl
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
            this.sliderValue = new System.Windows.Forms.TrackBar();
            this.grpAngle = new System.Windows.Forms.GroupBox();
            this.txtValue = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.sliderValue)).BeginInit();
            this.grpAngle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtValue)).BeginInit();
            this.SuspendLayout();
            // 
            // sliderValue
            // 
            this.sliderValue.Location = new System.Drawing.Point(4, 19);
            this.sliderValue.Maximum = 180;
            this.sliderValue.Minimum = -180;
            this.sliderValue.Name = "sliderValue";
            this.sliderValue.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.sliderValue.Size = new System.Drawing.Size(45, 200);
            this.sliderValue.TabIndex = 0;
            this.sliderValue.Scroll += new System.EventHandler(this.sliderAngle_Scroll);
            // 
            // grpAngle
            // 
            this.grpAngle.Controls.Add(this.txtValue);
            this.grpAngle.Controls.Add(this.sliderValue);
            this.grpAngle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpAngle.Location = new System.Drawing.Point(0, 0);
            this.grpAngle.Name = "grpAngle";
            this.grpAngle.Size = new System.Drawing.Size(55, 254);
            this.grpAngle.TabIndex = 2;
            this.grpAngle.TabStop = false;
            this.grpAngle.Text = "ch1";
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(0, 225);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(55, 20);
            this.txtValue.TabIndex = 2;
            this.txtValue.ValueChanged += new System.EventHandler(this.txtValue_ValueChanged);
            this.txtValue.Leave += new System.EventHandler(this.txtAngle_Leave);
            // 
            // ucServoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpAngle);
            this.Name = "ucServoControl";
            this.Size = new System.Drawing.Size(55, 254);
            this.Load += new System.EventHandler(this.ucServoControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sliderValue)).EndInit();
            this.grpAngle.ResumeLayout(false);
            this.grpAngle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TrackBar sliderValue;
        private System.Windows.Forms.GroupBox grpAngle;
        private System.Windows.Forms.NumericUpDown txtValue;
    }
}
