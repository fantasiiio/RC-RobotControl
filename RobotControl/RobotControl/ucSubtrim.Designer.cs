namespace RobotControl
{
    partial class ucSubtrim
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
            this.btnUpBig = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnDownBig = new System.Windows.Forms.Button();
            this.grpTrim = new System.Windows.Forms.GroupBox();
            this.grpTrim.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnUpBig
            // 
            this.btnUpBig.Image = global::RobotControl.Properties.Resources.double_arrow_top;
            this.btnUpBig.Location = new System.Drawing.Point(10, 19);
            this.btnUpBig.Name = "btnUpBig";
            this.btnUpBig.Size = new System.Drawing.Size(23, 23);
            this.btnUpBig.TabIndex = 0;
            this.btnUpBig.UseVisualStyleBackColor = true;
            this.btnUpBig.Click += new System.EventHandler(this.btnUpBig_Click);
            // 
            // btnUp
            // 
            this.btnUp.Image = global::RobotControl.Properties.Resources.arrow_top;
            this.btnUp.Location = new System.Drawing.Point(10, 48);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(23, 23);
            this.btnUp.TabIndex = 1;
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.Image = global::RobotControl.Properties.Resources.arrow_bottom;
            this.btnDown.Location = new System.Drawing.Point(10, 98);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(23, 23);
            this.btnDown.TabIndex = 2;
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnDownBig
            // 
            this.btnDownBig.Image = global::RobotControl.Properties.Resources.double_arrow_bottom;
            this.btnDownBig.Location = new System.Drawing.Point(10, 127);
            this.btnDownBig.Name = "btnDownBig";
            this.btnDownBig.Size = new System.Drawing.Size(23, 23);
            this.btnDownBig.TabIndex = 3;
            this.btnDownBig.UseVisualStyleBackColor = true;
            this.btnDownBig.Click += new System.EventHandler(this.btnDownBig_Click);
            // 
            // grpTrim
            // 
            this.grpTrim.Controls.Add(this.btnUpBig);
            this.grpTrim.Controls.Add(this.btnDownBig);
            this.grpTrim.Controls.Add(this.btnUp);
            this.grpTrim.Controls.Add(this.btnDown);
            this.grpTrim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpTrim.Location = new System.Drawing.Point(0, 0);
            this.grpTrim.Name = "grpTrim";
            this.grpTrim.Size = new System.Drawing.Size(45, 162);
            this.grpTrim.TabIndex = 4;
            this.grpTrim.TabStop = false;
            this.grpTrim.Text = "Ch1";
            // 
            // ucSubtrim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpTrim);
            this.Name = "ucSubtrim";
            this.Size = new System.Drawing.Size(45, 162);
            this.Load += new System.EventHandler(this.ucSubtrim_Load);
            this.grpTrim.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnUpBig;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnDownBig;
        private System.Windows.Forms.GroupBox grpTrim;
    }
}
