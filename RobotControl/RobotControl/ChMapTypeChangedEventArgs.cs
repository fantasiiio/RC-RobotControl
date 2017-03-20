using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotControl
{
    public class ChMapTypeChangedEventArgs : EventArgs
    {
        public ChannelMappingData chMapData { get; set; }
        public ChMapTypeChangedEventArgs(ChannelMappingData chMapData)
        {
            this.chMapData = chMapData;
        }
    }
}
