using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotControl
{
    public class ChannelMappingData
    {
        public int chMapIndex { get; set; }
        public int[] ServoIndices;
        public int[] ChannelIndices;
        public decimal[] Params;
        public MappingType Type { get; set; }
        public PositionningType Positionning { get; set; }
        public decimal Multiplier { get; set; }
        public bool IsNew { get; set; }
    }
}
