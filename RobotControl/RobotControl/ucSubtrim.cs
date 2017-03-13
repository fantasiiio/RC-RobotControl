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
    public partial class ucSubtrim : UserControl
    {
        public event EventHandler<CommandEventArgs> OnCommand;

        int chNumber;
        int smallTrim = 1;
        int bigTrim = 10;

        public ucSubtrim(int channelNumber)
        {
            InitializeComponent();
            chNumber = channelNumber-1;
        }

        void SendTrim(int amount)
        {
            if (OnCommand != null)
            {
                OnCommand(this, new CommandEventArgs("trim", new object[] { chNumber, amount }));
            }
        }

        private void ucSubtrim_Load(object sender, EventArgs e)
        {
            grpTrim.Text = "ch" + chNumber;
        }

        private void btnUpBig_Click(object sender, EventArgs e)
        {
            SendTrim(bigTrim);
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            SendTrim(smallTrim);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            SendTrim(-smallTrim);
        }

        private void btnDownBig_Click(object sender, EventArgs e)
        {
            SendTrim(-bigTrim);
        }

    }
}
