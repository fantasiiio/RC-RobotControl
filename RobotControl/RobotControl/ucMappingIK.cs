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
    public partial class ucMappingIK : ucMappingBase
    {
        public ucMappingIK(int inputCount, int outputCount)
            : base(inputCount, outputCount)
        {
            InitializeComponent();
        }

        void InitControls()
        {
            FillNumericCombo(cboServo1, outputCount);
            FillNumericCombo(cboServo2, outputCount);
            FillNumericCombo(cboServo3, outputCount);
            FillNumericCombo(cboChannel1, inputCount);
            FillNumericCombo(cboChannel2, inputCount);
            FillNumericCombo(cboChannel3, inputCount);
            FillEnumCombo(cboType, typeof(MappingType));
            FillEnumCombo(cboPositionning, typeof(PositionningType));
        }

        public void SetControlValues()
        {
            if (this.InvokeRequired)
                this.Invoke((MethodInvoker)delegate { SetControlValues(); });
            else
            {
                cboServo1.SelectedIndex = chMapData.ServoIndices[0];
                cboServo2.SelectedIndex = chMapData.ServoIndices[1];
                cboServo3.SelectedIndex = chMapData.ServoIndices[2];
                cboChannel1.SelectedIndex = chMapData.ChannelIndices[0];
                cboChannel2.SelectedIndex = chMapData.ChannelIndices[1];
                cboChannel3.SelectedIndex = chMapData.ChannelIndices[2];
                cboPositionning.SelectedValue = (int)chMapData.Positionning;
                cboType.SelectedValue = (int)chMapData.Type;
                numMultiplier.Value = chMapData.Multiplier;
                numParam1.Value = chMapData.Params[0];
                numParam2.Value = chMapData.Params[1];
            }

        }

        override public void LoadData()
        {
            base.LoadData();
            InitControls();
            SetControlValues();
        }

        override public void UpdateData()
        {
            chMapData.ServoIndices[0] = cboServo1.SelectedIndex;
            chMapData.ServoIndices[1] = cboServo2.SelectedIndex;
            chMapData.ServoIndices[2] = cboServo3.SelectedIndex;
            chMapData.ChannelIndices[0] = cboChannel1.SelectedIndex;
            chMapData.ChannelIndices[1] = cboChannel2.SelectedIndex;
            chMapData.ChannelIndices[2] = cboChannel3.SelectedIndex;
            chMapData.Positionning = (PositionningType)cboPositionning.SelectedValue;
            chMapData.Type = (MappingType)cboType.SelectedValue;
            chMapData.Multiplier = numMultiplier.Value;
            chMapData.Params[0] = numParam1.Value;
            chMapData.Params[1] = numParam2.Value;
        }
    }
}
