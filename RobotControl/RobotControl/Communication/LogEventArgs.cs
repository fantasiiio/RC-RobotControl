using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsManager.Services
{
    public class LogEventArgs : EventArgs
    {
        public string Message { get; set; }

        public LogEventArgs(string message) : base()
        {
            this.Message = message;
        }

    }
}
