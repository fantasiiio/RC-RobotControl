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
    public partial class ucServoReverse : UserControl
    {
        public event EventHandler<CommandEventArgs> OnCommand;        
        int chNumber;
        bool updating = false;

        public ucServoReverse()
        {
            InitializeComponent();
        }

        void SetChecked(bool chk)
        {
            if (this.InvokeRequired)
                this.Invoke((MethodInvoker)delegate { SetChecked(chk); });
            else
            {
                updating = true;
                chkReverse.Checked = chk;
                updating = false;
            }
        }

        public bool Checked
        {
            get { return chkReverse.Checked; }
            set { SetChecked(value); }
        }

        public ucServoReverse(int chNumber)
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
            if (updating)
                return;
            SendCommand();
        }
    }
}
