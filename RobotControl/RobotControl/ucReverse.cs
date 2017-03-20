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
    public partial class ucReverse : UserControl
    {
        public event EventHandler<CommandEventArgs> OnCommand;

        int chNumber;

        public ucReverse()
        {
            InitializeComponent();
        }

        void SetChecked(bool chk)
        {
            if (this.InvokeRequired)
                this.Invoke((MethodInvoker)delegate { SetChecked(chk); });
            else
            {
                chkReverse.Checked = chk;
            }
        }

        public bool Checked
        {
            get { return chkReverse.Checked; }
            set { SetChecked(value); }
        }

        public ucReverse(int chNumber)
        {
            this.chNumber = chNumber;
            InitializeComponent();
        }

        void SendCommand()
        {
            if (OnCommand != null)
            {
                OnCommand(this, new CommandEventArgs("reverse", new object[] { chNumber, chkReverse.Checked ? 1 : 0 }));
            }
        }

        private void ucReverse_Load(object sender, EventArgs e)
        {
            grpReverse.Text = "ch" + chNumber;
        }

        private void chkReverse_CheckedChanged(object sender, EventArgs e)
        {
            SendCommand();
        }
    }
}
