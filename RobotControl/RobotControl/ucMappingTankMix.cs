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
    public partial class ucMappingTankMix : ucMappingBase
    {
        public ucMappingTankMix(int inputCount, int outputCount)
            : base(inputCount, outputCount)
        {
            InitializeComponent();
        }

        public override void LoadData()
        {
            base.LoadData();
            FillNumericCombo(cboServo1, outputCount);
            FillNumericCombo(cboServo2, outputCount);
            FillNumericCombo(cboChannel1, inputCount);
            FillNumericCombo(cboChannel2, inputCount);
            FillEnumCombo(cboType, typeof(MappingType));
            FillEnumCombo(cboPositionning, typeof(PositionningType));

            cboServo1.SelectedIndex = chMapData.ServoIndices[0];
            cboServo2.SelectedIndex = chMapData.ServoIndices[1];
            cboChannel1.SelectedIndex = chMapData.ChannelIndices[0];
            cboChannel2.SelectedIndex = chMapData.ChannelIndices[1];
            cboPositionning.SelectedValue = (int)chMapData.Positionning;
            cboType.SelectedValue = (int)chMapData.Type;
            numMultiplier.Value = chMapData.Multiplier;
        }

        override public void UpdateData()
        {
            chMapData.ServoIndices[0] = cboServo1.SelectedIndex;
            chMapData.ServoIndices[1] = cboServo2.SelectedIndex;
            chMapData.ChannelIndices[0] = cboChannel1.SelectedIndex;
            chMapData.ChannelIndices[1] = cboChannel2.SelectedIndex;
            chMapData.Positionning = (PositionningType)cboPositionning.SelectedValue;
            chMapData.Type = (MappingType)cboType.SelectedValue;
            chMapData.Multiplier = numMultiplier.Value;
        }

    }
}
