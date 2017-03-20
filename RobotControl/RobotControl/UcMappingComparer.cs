using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotControl
{
    public class UcMappingComparer : IComparer<ucMappingBase>
    {
        public int Compare(ucMappingBase x, ucMappingBase y)
        {
            return x.chMapData.chMapIndex.CompareTo(y.chMapData.chMapIndex);
        }
    }
}
