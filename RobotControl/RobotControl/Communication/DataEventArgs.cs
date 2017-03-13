using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsManager.Services
{
    public class DataEventArgs : EventArgs
    {
        public string Data { get; set; }

        public DataEventArgs(string data)
            : base()
        {
            this.Data = data;
        }
    }
}
