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
    public partial class ucMappingBase : UserControl, IucMapping
    {
        public event EventHandler<CommandEventArgs> OnCommand;
        public event EventHandler<ChMapTypeChangedEventArgs> OnChMapTypeChanged;
        public ChannelMappingData chMapData { get; set; }
        
        protected int inputCount;
        protected int outputCount;

        protected bool loading = false;

        public ucMappingBase()
        {
        }

        public ucMappingBase(int inputCount, int outputCount)
        {
            this.inputCount = inputCount;
            this.outputCount = outputCount;
            InitializeComponent();
        }

        protected void FillEnumCombo(ComboBox cbo, Type enumType)
        {
            cbo.Items.Clear();
            cbo.DisplayMember = "Text";
            cbo.ValueMember = "Value";
            List<ComboboxItem> items = new List<ComboboxItem>();
            foreach (var name in Enum.GetNames(enumType))
            {
                ComboboxItem item = new ComboboxItem();
                item.Value = (int)Enum.Parse(enumType, name);
                item.Text = name;
                items.Add(item);
            }
            cbo.DataSource = items;
        }

        protected  void FillNumericCombo(ComboBox cbo, int maxValue)
        {
            for (int i = 0; i < maxValue; i++)
            {
                cbo.Items.Add(i);
            }
        }

        protected void SendCommand(string command)
        {
            if (OnCommand != null)
            {
                OnCommand(this, new CommandEventArgs("chMap", new object[] { command, chMapData }));
            }
        }

        protected void ChMapTypeChangedEventArgs()
        {
            if (OnChMapTypeChanged != null)
            {
                OnChMapTypeChanged(this, new ChMapTypeChangedEventArgs(chMapData));
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            UpdateData();
            SendCommand(chMapData.IsNew ? "ins" : "upd");
            chMapData.IsNew = false;
            BackColor = default(Color);
        }

        public virtual void UpdateData()
        {
        }

        public virtual void LoadData()
        {
            if (chMapData.IsNew)
                BackColor = Color.LightGreen;
            else
                BackColor = default(Color);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete this mapping ?", "Delete", MessageBoxButtons.YesNo))
            {
                SendCommand("del");
            }
        }

        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loading)
                return;
            chMapData.Type = (MappingType)cboType.SelectedValue;
            ChMapTypeChangedEventArgs();
        }

        private void ucMappingBase_Load(object sender, EventArgs e)
        {
            loading = true;
            LoadData();
            loading = false;
        }

    }
}
