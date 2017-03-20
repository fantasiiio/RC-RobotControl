namespace RobotControl
{
    partial class ucReverse
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
            this.grpReverse = new System.Windows.Forms.GroupBox();
            this.chkReverse = new System.Windows.Forms.CheckBox();
            this.grpReverse.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpReverse
            // 
            this.grpReverse.Controls.Add(this.chkReverse);
            this.grpReverse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpReverse.Location = new System.Drawing.Point(0, 0);
            this.grpReverse.Name = "grpReverse";
            this.grpReverse.Size = new System.Drawing.Size(41, 47);
            this.grpReverse.TabIndex = 5;
            this.grpReverse.TabStop = false;
            this.grpReverse.Text = "Ch1";
            // 
            // chkReverse
            // 
            this.chkReverse.AutoSize = true;
            this.chkReverse.Location = new System.Drawing.Point(6, 19);
            this.chkReverse.Name = "chkReverse";
            this.chkReverse.Size = new System.Drawing.Size(15, 14);
            this.chkReverse.TabIndex = 0;
            this.chkReverse.UseVisualStyleBackColor = true;
            this.chkReverse.CheckedChanged += new System.EventHandler(this.chkReverse_CheckedChanged);
            // 
            // ucReverse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpReverse);
            this.Name = "ucReverse";
            this.Size = new System.Drawing.Size(41, 47);
            this.Load += new System.EventHandler(this.ucReverse_Load);
            this.grpReverse.ResumeLayout(false);
            this.grpReverse.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpReverse;
        private System.Windows.Forms.CheckBox chkReverse;
    }
}
