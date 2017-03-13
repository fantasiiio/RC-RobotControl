using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotControl
{
    public class CommandEventArgs : EventArgs
    {
        public string CommandName { get; set; }
        public object[] Param { get; set; }

        public CommandEventArgs(string CommandName,object[] Param) : base()
        {
            this.CommandName = CommandName;
            this.Param = Param;
        }
    }
}
