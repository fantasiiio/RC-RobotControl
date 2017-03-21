using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RobotControl
{
    public partial class ucServoCalibrate : UserControl
    {
        public event EventHandler<CommandEventArgs> OnCommand;

        int chNumber;

        public ucServoCalibrate()
        {
            InitializeComponent();
        }

        public ucServoCalibrate(int chNumber)
        {
            this.chNumber = chNumber;
            InitializeComponent();
        }

        void SendCommand()
        {
            if (OnCommand != null)
            {
                OnCommand(this, new CommandEventArgs("calibrate", new object[] { chNumber, numMaxPWM.Value, numMaxAngle.Value }));
            }
        }

        private void ucServoCalibrate_Load(object sender, EventArgs e)
        {
            groupBox1.Text = "ch" + chNumber;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            SendCommand();
        }

        public void SetValues(decimal maxPWM, decimal maxAngle)
        {
            if (this.InvokeRequired)
                this.Invoke((MethodInvoker)delegate { SetValues(maxPWM, maxAngle); });
            else
            {
                numMaxPWM.Value = maxPWM;
                numMaxAngle.Value = maxAngle;
            }
        }
    }
}
