using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Newtonsoft.Json.Linq;

namespace WindowsManager
{
    public class SerialDevice
    {
        public string DeviceType { get { return "serial"; } }
        public string Description { get; set; }
        public string Name { get; set; }
        public int BaudRate { get; set; }
        public int PortNumber
        {
            get
            {
                int portNum;
                int.TryParse(Name.Substring(3), out portNum);
                return portNum;
            }
        }

        /*public SerialDevice(string json)
        {
            var parsedJson = JObject.Parse(json);

            this.Name = (string)parsedJson["name"];
            this.BaudRate = (int)parsedJson["baudRate"];
            this.Description = (string)parsedJson["description"];
        }*/

        public SerialDevice(string name, string description, int baudRate )
        {
            this.Name = name;
            this.BaudRate = baudRate;
            this.Description = description;
        }

        public override string ToString()
        {
            return String.Format("{0} - ({1}) {2} bauds", this.Name, this.Description, this.BaudRate);
        }

    }
}
