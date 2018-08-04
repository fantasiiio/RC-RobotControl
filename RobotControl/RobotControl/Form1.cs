using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsManager.Services;
using WindowsManager;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading;
using System.Globalization;

namespace RobotControl
{

    public partial class Form1 : Form
    {
        SerialService serial;
        List<string> commandList;
        System.Windows.Forms.Timer timeOutTimer;
        int inputCount = 6;
        int outputCount = 7;
        List<ucServoControl> ucServoAngleList;
        List<ucServoControl> ucServoPWMList;
        List<ucServoControl> ucInputList;
        List<ucServoReverse> ucServoReverseList;
        List<ucServoCalibrate> ucServoCalibrateList;
        bool BuildingChannelMap = false;
        bool buildingReverse = false;
        bool buildingCalibrate = false;
        int chMapIndex;
        bool CreatingChMap = false;
        bool updating = false;
        int buildHeight;
        bool waitPositiveAnswer = false;
        bool viewIncoming = true;
        bool viewOutgoing = true;

        public Form1()
        {
            InitializeComponent();

            chkIncoming.Checked = this.viewIncoming;
            chkOutgoing.Checked = this.viewOutgoing;

            timeOutTimer = new System.Windows.Forms.Timer();
            timeOutTimer.Interval = 2000;
            timeOutTimer.Tick += timeOutTimer_Tick;

            commandList = new List<string>();
            serial = SerialService.GetInstance();
            serial.OnConnected += serial_OnConnected;
            serial.OnConnectError += serial_OnConnectError;
            serial.OnDataReceived += serial_OnDataReceived;
            serial.OnDisconnected += serial_OnDisconnected;
            serial.OnLog += serial_OnLog;

            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
        }

        void serial_OnLog(object sender, LogEventArgs e)
        {
            logMessage(e.Message);
        }

        void UpdateButtonStatus(bool connected)
        {
            if (this.InvokeRequired)
                this.Invoke((MethodInvoker)delegate { UpdateButtonStatus(connected); });
            else
            {
                btnDisconnect.Enabled = connected;
                btnConnect.Enabled = !connected;
            }
        }

        void serial_OnDisconnected(object sender, EventArgs e)
        {
            UpdateButtonStatus(false);
        }

        public void AppendText(RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }

        void logMessage(string msg, Color color = default(Color))
        {
            if (txtLogs.InvokeRequired)
            {
                txtLogs.Invoke((MethodInvoker)delegate { logMessage(msg, color); });
            }
            else
            {
                AppendText(txtLogs, msg + "\r\n", color);
                txtLogs.SelectionStart = txtLogs.Text.Length;
                // scroll it automatically
                txtLogs.ScrollToCaret();
                //txtLogs.AppendText(msg + "\r\n");
            }
        }

        void timeOutTimer_Tick(object sender, EventArgs e)
        {
            logMessage("Timeout: No answer.");
            if (tabControl1.SelectedTab != tabConnect)
                MessageBox.Show("Warning", "No answer");
            //serial.Disconnect();
            timeOutTimer.Stop();
        }

        public static T Deserialize<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            MemoryStream ms = new MemoryStream(Encoding.ASCII.GetBytes(json));
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            obj = (T)serializer.ReadObject(ms);
            ms.Close();
            ms.Dispose();
            return obj;
        }

        List<KeyValue> ParseParams(string[] paramList)
        {
            List<KeyValue> lst = new List<KeyValue>();
            foreach (var parm in paramList)
            {
                KeyValue kv = new KeyValue();
                string[] kvSplit = parm.Split(':');
                kv.key = kvSplit[0];
                if (kvSplit.Length > 1)
                    kv.value = kvSplit[1];
                lst.Add(kv);
            }
            return lst;
        }

        Nullable<T> GetParamValue<T>(string paramName, List<KeyValue> kvList) where T : struct
        {
            KeyValue kv;
            kv = kvList.Where(v => v.key == paramName).FirstOrDefault();
            if (kv == null)
                return null;
            T value = kv.GetValue<T>();
            return value;
        }

        void UpdateServoControlValues(int index, float? pwm, float? angle)
        {
            if (this.InvokeRequired)
                this.Invoke((MethodInvoker)delegate { UpdateServoControlValues(index, pwm, angle); });
            else
            {
                if (pwm != null)
                {
                    ucServoPWMList[index].Updating = true;
                    ucServoPWMList[index].SetValue((int)pwm);
                    ucServoPWMList[index].Updating = false;
                }
                if (angle != null)
                {
                    ucServoAngleList[index].Updating = true;
                    ucServoAngleList[index].SetValue((int)angle);
                    ucServoAngleList[index].Updating = false;
                }
            }
        
        }


        void parseServoState(List<KeyValue> kvList)
        {
            int? index = GetParamValue<int>("index", kvList);
            float? ppm = GetParamValue<float>("pwm", kvList);
            float? angle = GetParamValue<float>("angle", kvList);

            UpdateServoControlValues((int)index, ppm, angle);
        }


        public T GetValue<T>(string str)
        {
            try
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                if (converter != null)
                {
                    return (T)converter.ConvertFromString(null, CultureInfo.InvariantCulture, str);
                }
                return default(T);
            }
            catch (NotSupportedException)
            {
                return default(T);
            }
        }

        T[] GetValueArrayFromString<T>(string str)
        {
            string[] strArr = str.Split(',');
            T[] intArr = new T[strArr.Length];
            for (int i = 0; i < strArr.Length; i++)
            {
                intArr[i] = GetValue<T>(strArr[i]);
            }
            return intArr;
        }

        void AddControlToContainer(Control container, Control ctrl)
        {
            if (container.InvokeRequired)
                container.Invoke((MethodInvoker)delegate { AddControlToContainer(container, ctrl); });
            else
            {
                container.Controls.Add(ctrl);
            }

        }

        void parseChMap(string[] parms)
        {
            if (!BuildingChannelMap)
                return;
            if (parms[1] == "done")
            {
                BuildingChannelMap = false;
            }
            else if (parms[1] == "ins")
            {
                ChannelMappingData chMapData = new ChannelMappingData();
                int modeIndex = int.Parse(parms[2]);
                chMapData.Type = (MappingType)int.Parse(parms[3]);
                chMapData.ServoIndices = GetValueArrayFromString<int>(parms[4]);
                chMapData.ChannelIndices = GetValueArrayFromString<int>(parms[5]);
                chMapData.Positionning = (PositionningType)int.Parse(parms[6]);

                chMapData.Params = new decimal[chMapData.Type == MappingType.IK ? 2 : 0];
                chMapData.chMapIndex = chMapIndex++;

                ucMappingBase ucMapping = null;
                switch (chMapData.Type)
                {
                    case MappingType.Direct:
                        ucMapping = new ucMappingDirect(inputCount, outputCount);
                        break;
                    case MappingType.TankMix:
                        ucMapping = new ucMappingTankMix(inputCount, outputCount);
                        break;
                    case MappingType.IK:
                        ucMapping = new ucMappingIK(inputCount, outputCount);
                        break;
                }
                ucMapping.chMapData = chMapData;
                ucMapping.Top = buildHeight;
                buildHeight += ucMapping.Height;
                ucMapping.Width = panelChannelMap.Width - 20;
                ucMapping.OnCommand += OnCommandReceived;
                ucMapping.OnChMapTypeChanged += ucMapping_OnChMapTypeChanged;
                AddControlToContainer(panelChannelMap, ucMapping);
            }
            else if (parms[1] == "setParams")
            {
                // chMap setParams <modeIndex> <chMapIndex> <param1>,<param2>...
                int modeIndex = int.Parse(parms[2]);
                int chMapIndex = int.Parse(parms[3]);

                List<ucMappingBase> controlList = new List<ucMappingBase>(panelChannelMap.Controls.OfType<ucMappingBase>());
                ucMappingIK uc = (ucMappingIK)controlList.Where(v => v.chMapData.chMapIndex == chMapIndex).FirstOrDefault();
                ChannelMappingData chMapData = uc.chMapData;
                chMapData.Params = GetValueArrayFromString<decimal>(parms[4]);
                uc.SetControlValues();
            }
        }

        void parseServo(string[] parms)
        {
            if (parms[1] == "reverse")
            {
                if (!buildingReverse)
                    return;
                if (parms[2] == "done")
                {
                    buildingReverse = false;
                }
                else
                {
                    int servoIndex = int.Parse(parms[2]);
                    bool reversed = parms[3] == "1";
                    if (servoIndex < outputCount)
                    {
                        ucServoReverseList[servoIndex].Checked = reversed;
                    }
                }
            }
            else if (parms[1] == "calibrate")
            {
                if (!buildingCalibrate)
                    return;
                if (parms[2] == "done")
                {
                    buildingCalibrate = false;
                }
                else
                {
                    int servoIndex = int.Parse(parms[2]);
                    decimal maxPWM = decimal.Parse(parms[3]);
                    decimal maxAngle = decimal.Parse(parms[4]);
                    if (servoIndex < outputCount)
                    {
                        ucServoCalibrateList[servoIndex].SetValues(maxPWM, maxAngle);
                    }
                }
            }

        }

        void SetcboModeChannel(int value)
        {
            if (this.InvokeRequired)
                this.Invoke((MethodInvoker)delegate { SetcboModeChannel(value); });
            else
            {
                cboModeChannel.SelectedIndex = value;
            }
        }

        void SetcboModeMinMax(decimal min, decimal max)
        {
            if (this.InvokeRequired)
                this.Invoke((MethodInvoker)delegate { SetcboModeMinMax(min, max); });
            else
            {
                numChModeMin.Value = min;
                numChModeMax.Value = max;
            }
        }

        void parsechMode(string[] parms)
        {
            if (!BuildingChannelMap)
                return;
            if (parms[1] == "setChannel")
            {
                int chNumber = int.Parse(parms[2]);
                SetcboModeChannel(chNumber + 1);
            }
            else if (parms[1] == "chValue")
            {
                decimal min = decimal.Parse(parms[3], CultureInfo.InvariantCulture);
                decimal max = decimal.Parse(parms[4], CultureInfo.InvariantCulture);
                SetcboModeMinMax(min, max);
            }
        }

        void serial_OnDataReceived(object sender, DataReceivedEventArgs e)
        {
            e.Data = e.Data.Trim();
            if (this.viewIncoming)
                logMessage("Recv: " + e.Data, Color.Red);
            timeOutTimer.Stop();
            string[] parms = e.Data.Split(' ');
            List<KeyValue> kvList = ParseParams(parms);
            if (waitPositiveAnswer && parms[0].ToLower() != "ok")
            {
                MessageBox.Show("Error", e.Data);
            }
            if (parms[0] != "error")
            {
                if (commandList.Count > 0)
                {
                    sendData(commandList[0]);
                    commandList.RemoveAt(0);
                }

                if (parms[0] == "servoState")
                {
                    parseServoState(kvList);
                }
                else if (parms[0] == "chMap")
                {
                    parseChMap(parms);
                }
                else if (parms[0] == "servo")
                {
                    parseServo(parms);
                }
                else if (parms[0] == "chMode")
                {
                    parsechMode(parms);
                }
                else if (parms[0] == "Welcome")
                {
                    commandList.Add(GetEnableMappingStr(EnableMappingToolStripMenuItem.Checked));
                    sendData(GetEnablePPMStr(EnablePPMToolStripMenuItem.Checked));
                }
            }
        }

        void serial_OnConnectError(object sender, LogEventArgs e)
        {
            logMessage("Connect error: " + e.Message);
        }

        void serial_OnConnected(object sender, EventArgs e)
        {
            logMessage("Connected");
            UpdateButtonStatus(true);
        }

        void sendData(string data)
        {
            if(this.viewOutgoing)
                logMessage("Send: " + data, Color.Blue);
            serial.SendCommand(data, true);
            if(serial.IsConnected)
                timeOutTimer.Start();
        }

        void LoadSerialCombo()
        {
            try
            {
                cboSerialDevices.Items.Clear();
                SerialDevice[] serialDevices;
                serialDevices = serial.GetSerialDevices();
                serialDevices = serialDevices.OrderBy(s => s.PortNumber).ToArray();
                foreach (SerialDevice serialDevice in serialDevices)
                {
                    cboSerialDevices.Items.Add(serialDevice);
                }
            }
            catch
            {
            }
        }

        void FillFaudRates()
        {
            cboBaudRates.Items.Add(9600);
            cboBaudRates.Items.Add(19200);
            cboBaudRates.Items.Add(38400);
            cboBaudRates.Items.Add(57600);
            cboBaudRates.Items.Add(115200);
            cboBaudRates.SelectedIndex = 4;
        }

        bool validateControls()
        {
            if (cboSerialDevices.SelectedIndex == -1)
            {
                MessageBox.Show(this, "Selectionnez un port");
                return false;
            }

            return true;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (!validateControls())
                    return;

                commandList.Clear();

                SerialDevice serialDevice = (SerialDevice)cboSerialDevices.SelectedItem;
                serialDevice.BaudRate = (int)cboBaudRates.SelectedItem;
                if (serial.Connect(serialDevice))
                {
                    //commandList.Add(GetEnableMappingStr(EnableMappingToolStripMenuItem.Checked));
                    //commandList.Add(GetEnablePPMStr(EnablePPMToolStripMenuItem.Checked));
                    sendData("hello");

                    timeOutTimer.Start();
                }
            }
            catch (Exception ex)
            {
                logMessage(ex.Message);
            }

        }

        protected void FillNumericlCombo(ComboBox cbo, int minValue, int maxValue)
        {
            for (int i = minValue; i <= maxValue; i++)
            {
                cbo.Items.Add(i);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cboMappingMode.SelectedIndex = 0;
            LoadSerialCombo();
            FillFaudRates();
            FillNumericlCombo(cboModeChannel, 0, inputCount-1);
            UpdateButtonStatus(false);

            txtLogs.VisibleChanged += new EventHandler(txtLogs_VisibleChanged);
            txtCommand.KeyDown += new KeyEventHandler(txtCommand_KeyDown);

            ucServoAngleList = new List<ucServoControl>();
            ucServoPWMList = new List<ucServoControl>();
            ucInputList = new List<ucServoControl>();
            ucServoReverseList = new List<ucServoReverse>();
            ucServoCalibrateList = new List<ucServoCalibrate>();

            for (int i = 0; i < outputCount; i++)
            {
                ucSubtrim subTrimCtrl = new ucSubtrim(i);
                subTrimCtrl.Left = (subTrimCtrl.Width + 10) * i;
                tabCenter.Controls.Add(subTrimCtrl);
                subTrimCtrl.OnCommand += new EventHandler<CommandEventArgs>(OnCommandReceived);

                ucServoControl scAngle = new ucServoControl(i, -180, 180, "servoAngle", 0);
                scAngle.Left = (scAngle.Width + 10) * i;
                tabAngle.Controls.Add(scAngle);
                scAngle.OnCommand += new EventHandler<CommandEventArgs>(OnCommandReceived);
                ucServoAngleList.Add(scAngle);

                ucServoControl scPWM = new ucServoControl(i, 0, 3000, "servoPWM", 1500);
                scPWM.Left = (scPWM.Width + 10) * i;
                tabPWM.Controls.Add(scPWM);
                scPWM.OnCommand += new EventHandler<CommandEventArgs>(OnCommandReceived);
                ucServoPWMList.Add(scPWM);

                ucServoReverse ctrlReverse = new ucServoReverse(i);
                ctrlReverse.Left = (ctrlReverse.Width + 10) * i;
                tabReverse.Controls.Add(ctrlReverse);
                ctrlReverse.OnCommand += new EventHandler<CommandEventArgs>(OnCommandReceived);
                ucServoReverseList.Add(ctrlReverse);

                ucServoCalibrate ctrlCalibrate = new ucServoCalibrate(i);
                ctrlCalibrate.Left = (ctrlCalibrate.Width + 10) * i;
                tabCalibrate.Controls.Add(ctrlCalibrate);
                ctrlCalibrate.OnCommand += new EventHandler<CommandEventArgs>(OnCommandReceived);
                ucServoCalibrateList.Add(ctrlCalibrate);
            }
            for (int i = 0; i < inputCount; i++)
            {
                ucServoControl scInput = new ucServoControl(i, 0, 100, "input", 50);
                scInput.Left = (scInput.Width + 10) * i;
                tabInput.Controls.Add(scInput);
                scInput.OnCommand += new EventHandler<CommandEventArgs>(OnCommandReceived);
                ucInputList.Add(scInput);
            }
        }

        void txtCommand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                sendData(txtCommand.Text);
            }
        }

        void txtLogs_VisibleChanged(object sender, EventArgs e)
        {
            if (txtLogs.Visible)
            {
                txtLogs.SelectionStart = txtLogs.TextLength;
                txtLogs.ScrollToCaret();
            }
        }

        void OnCommandReceived(object sender, CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "calibrate":
                    sendData("servo calibrate " + e.Param[0].ToString() + " " + e.Param[1].ToString() + " " + e.Param[2].ToString());
                    break;
                case "reverse":
                    sendData("servo reverse " + e.Param[0].ToString() + " " + e.Param[1].ToString());
                    break;
                case "trim":
                    sendData("servo subtrim " + e.Param[0].ToString() + " " + e.Param[1].ToString());
                    break;
                case "servoAngle":
                    sendData("servo angle " + e.Param[0].ToString() + " " + e.Param[1].ToString());
                    break;
                case "servoPWM":
                    sendData("servo pwm " + e.Param[0].ToString() + " " + e.Param[1].ToString());
                    break;
                case "input":
                    sendData("input " + e.Param[0].ToString() + " " + (((float)(int)e.Param[1]) / 100).ToString());
                    break;
                case "chMap":
                    ChannelMappingData chMapData = (ChannelMappingData)e.Param[1];
                    string dataStr = "chMap " + e.Param[0] + " ";
                    // modeIndex
                    dataStr += cboMappingMode.SelectedIndex.ToString() + " ";
                    if ((string)e.Param[0] == "upd" || (string)e.Param[0] == "ins")
                    {
                        // chMap ins <modeIndex> <ChMapType> <servo1>,<servo2>... <ch1>,<ch2>... <positionning>
                        // chMap upd <modeIndex> <ChMapType> <servo1>,<servo2>... <ch1>,<ch2>... <positionning> <chMapIndex>

                        // chMapType
                        dataStr += ((int)chMapData.Type).ToString() + " ";

                        // Servos
                        for (int i = 0; i < chMapData.ServoIndices.Length; i++)
                        {
                            if (i > 0)
                                dataStr += ",";
                            dataStr += chMapData.ServoIndices[i].ToString();
                        }
                        dataStr += " ";

                        // Channels
                        for (int i = 0; i < chMapData.ChannelIndices.Length; i++)
                        {
                            if (i > 0)
                                dataStr += ",";
                            dataStr += chMapData.ChannelIndices[i].ToString();
                        }
                        dataStr += " ";
                        
                        //Positionning
                        dataStr += ((int)chMapData.Positionning).ToString() + " ";
                        // chMapIndex
                        if ((string)e.Param[0] == "upd")
                            dataStr += chMapData.chMapIndex.ToString();

                        // Build params string
                        if (chMapData.Type == MappingType.IK)
                        {
                            // chMap setParams <modeIndex> <chMapIndex> <param1>,<param2>...
                             string paramsStr = "chMap setParams ";
                            // modeIndex
                            paramsStr += cboMappingMode.SelectedIndex.ToString() + " ";

                            // chMapIndex
                            paramsStr += chMapData.chMapIndex.ToString() + " ";

                            // Params
                            for (int i = 0; i < chMapData.Params.Length; i++)
                            {
                                if (i > 0)
                                    paramsStr += ",";
                                paramsStr += chMapData.Params[i].ToString();
                            }
                            commandList.Add(paramsStr);
                        }
                        sendData(dataStr);
                        CreatingChMap = false;
                    }
                    else if ((string)e.Param[0] == "del")
                    {
                        if (!chMapData.IsNew)
                        {
                            BuildchannelMapControls(false);
                            dataStr += chMapData.chMapIndex.ToString();
                            sendData(dataStr);
                        }
                        //panelChannelMap.Controls.RemoveAt(chMapData.chMapIndex);
                    }
                    break;
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            serial.Disconnect();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            sendData(txtCommand.Text);
        }

        string GetEnableMappingStr(bool enabled)
        {
            return ("chMap " + (enabled ? "enable" : "disable"));
        }

        string GetEnablePPMStr(bool enabled)
        {
            return ("ppm " + (enabled ? "enable" : "disable"));
        }

        private void enableMappingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EnableMappingToolStripMenuItem.Checked = !EnableMappingToolStripMenuItem.Checked;
            sendData(GetEnableMappingStr(EnableMappingToolStripMenuItem.Checked));
        }

        private void EnablePPMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EnablePPMToolStripMenuItem.Checked = !EnablePPMToolStripMenuItem.Checked;
            sendData(GetEnablePPMStr(EnablePPMToolStripMenuItem.Checked));
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadSerialCombo();
        }

        void BuildchannelMapControls(bool bsendData = true)
        {
            buildHeight = 0;
            CreatingChMap = false;
            if(serial.IsConnected)
                BuildingChannelMap = true;
            chMapIndex = 0;
            panelChannelMap.Controls.Clear();
            string commandStr = "chMap dumpData " + cboMappingMode.SelectedIndex.ToString();
            if (bsendData)
            {
                sendData(commandStr);
            }
            else
            {
                commandList.Add(commandStr);
            }
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            CreatingChMap = false;
            buildHeight = 0;
            if (e.TabPage == tabMappings)
            {
                BuildchannelMapControls();
            }
            else if (e.TabPage == tabReverse)
            {
                buildingReverse = true;
                sendData("servo reverseDump");
            }
            else if (e.TabPage == tabCalibrate)
            {
                buildingCalibrate = true;
                sendData("servo calibrateDump");
            }
        }

        private void cboMappingMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuildchannelMapControls();
        }

        void AddNewChMap()
        {
            ChannelMappingData chMapData = new ChannelMappingData();
            chMapData.chMapIndex = panelChannelMap.Controls.Count;
            chMapData.ServoIndices = new int[] { 0 };
            chMapData.ChannelIndices = new int[] { 0 };
            chMapData.Params = new decimal[0];
            chMapData.Type = MappingType.Direct;
            chMapData.Positionning = PositionningType.Absolute;
            chMapData.IsNew = true;

            ucMappingBase ucMapping = new ucMappingDirect(inputCount, outputCount); ;
            ucMapping.chMapData = chMapData;
            ucMapping.Width = panelChannelMap.Width- 20;
            ucMapping.Anchor |= AnchorStyles.Right;
            ucMapping.Top = buildHeight;
            buildHeight += ucMapping.Height;
            ucMapping.OnCommand += OnCommandReceived;
            ucMapping.OnChMapTypeChanged += new EventHandler<ChMapTypeChangedEventArgs>(ucMapping_OnChMapTypeChanged);
            AddControlToContainer(panelChannelMap, ucMapping);
            CreatingChMap = true;
        }

        void RearrangeMappingControls()
        {
            buildHeight = 0;
            List<ucMappingBase> controlList = new List<ucMappingBase>();
            controlList.AddRange(panelChannelMap.Controls.OfType<ucMappingBase>());
            controlList.Sort(new UcMappingComparer());

            foreach (ucMappingBase ctrl in controlList)
            {
                ctrl.Top = buildHeight;
                buildHeight += ctrl.Height;
            }
        }

        void ucMapping_OnChMapTypeChanged(object sender, ChMapTypeChangedEventArgs e)
        {
            if (updating)
                return;
            updating = true;
            ChannelMappingData chMapData = e.chMapData;
            ucMappingBase ucMapping = null;
            switch (chMapData.Type)
            {
                case MappingType.Direct:
                    ucMapping = new ucMappingDirect(inputCount, outputCount);
                    Array.Resize(ref chMapData.ChannelIndices, 1);
                    Array.Resize(ref chMapData.ServoIndices, 1);
                    break;
                case MappingType.TankMix:
                    ucMapping = new ucMappingTankMix(inputCount, outputCount);
                    Array.Resize(ref chMapData.ChannelIndices, 2);
                    Array.Resize(ref chMapData.ServoIndices, 2);
                    break;
                case MappingType.IK:
                    ucMapping = new ucMappingIK(inputCount, outputCount);
                    Array.Resize(ref chMapData.ChannelIndices, 3);
                    Array.Resize(ref chMapData.ServoIndices, 3);
                    Array.Resize(ref chMapData.Params, 2);
                    break;
            }
            ucMapping.chMapData = chMapData;
            //ucMapping.Top = ucMapping.Height * chMapData.chMapIndex;
            ucMapping.Width = panelChannelMap.Width - 20;
            ucMapping.OnCommand += OnCommandReceived;
            ucMapping.OnChMapTypeChanged += ucMapping_OnChMapTypeChanged;

            panelChannelMap.Controls.RemoveAt(chMapData.chMapIndex);
            AddControlToContainer(panelChannelMap, ucMapping);
            RearrangeMappingControls();
            updating = false;

        }

        private void btnAddChMap_Click(object sender, EventArgs e)
        {
            if (CreatingChMap)
            {
                MessageBox.Show("Please save the new mapping before adding a new one.");
                return;
            }
            AddNewChMap();
        }

        private void cboModeChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BuildingChannelMap)
                return;
            sendData("chMode setChannel " + (cboModeChannel.SelectedIndex - 1).ToString());
        }

        private void numChModeMin_ValueChanged(object sender, EventArgs e)
        {
            if (BuildingChannelMap)
                return;
            sendData("chMode chValue " + cboMappingMode.SelectedIndex.ToString() + " " + numChModeMin.Value.ToString() + " " + numChModeMax.Value.ToString());
        }

        private void numChModeMax_ValueChanged(object sender, EventArgs e)
        {
            if (BuildingChannelMap)
                return;
            sendData("chMode chValue " + cboMappingMode.SelectedIndex.ToString() + " " + numChModeMin.Value.ToString() + " " + numChModeMax.Value.ToString());
        }

        private void chkViewIncoming_CheckedChanged(object sender, EventArgs e)
        {
            this.viewIncoming = chkIncoming.Checked;
        }

        private void chkOutcomming_CheckedChanged(object sender, EventArgs e)
        {
            this.viewOutgoing = chkOutgoing.Checked;
        }

    }
}
