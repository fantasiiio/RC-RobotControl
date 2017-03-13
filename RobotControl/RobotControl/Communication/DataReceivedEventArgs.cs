using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsManager.Services
{
    public class DataReceivedEventArgs : DataEventArgs
    {
       
        public DataReceivedEventArgs(string data) : base(data)
        {            
        }
    }
}
