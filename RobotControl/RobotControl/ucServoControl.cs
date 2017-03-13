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
    public partial class ucServoControl : UserControl
    {
        public event EventHandler<CommandEventArgs> OnCommand;
        public bool Updating { get; set; }

        int chNumber;
        string command;
        public ucServoControl(int channelNumber, int min, int max, string command, int startValue)
        {
            InitializeComponent();
            Updating = true;
            this.command = command;
            sliderValue.Minimum = min;
            sliderValue.Maximum = max;
            txtValue.Minimum = min;
            txtValue.Maximum = max;
            sliderValue.Value = startValue;
            chNumber = channelNumber - 1;
            grpAngle.Text = "ch" + chNumber.ToString();
            Updating = false;
        }

        void SendCommand(int amount)
        {
            if (OnCommand != null)
            {
                OnCommand(this, new CommandEventArgs(command, new object[] { chNumber, amount }));
            }
        }

        private void sliderAngle_Scroll(object sender, EventArgs e)
        {
            if (Updating)
                return;

            txtValue.Value = sliderValue.Value;
        }

        private void ucServoControl_Load(object sender, EventArgs e)
        {
            txtValue.Value = sliderValue.Value;
        }

        private void txtAngle_Leave(object sender, EventArgs e)
        {

        }

        private void txtValue_ValueChanged(object sender, EventArgs e)
        {
            if (Updating)
                return;
            try
            {
                int angle = (int)txtValue.Value;
                sliderValue.Value = angle;
                SendCommand(angle);
            }
            catch (Exception)
            {
            } 
        }

        public void SetValue(int value)
        {
            txtValue.Value = value;
            sliderValue.Value = value;
        }
    }
}
