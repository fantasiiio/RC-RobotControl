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

        public Form1()
        {
            InitializeComponent();

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

        void logMessage(string msg)
        {
            if (txtLogs.InvokeRequired)
            {
                txtLogs.Invoke((MethodInvoker)delegate { txtLogs.AppendText(msg + "\r\n"); });
            }
            else
            {
                txtLogs.AppendText(msg + "\r\n");
            }
        }

        void timeOutTimer_Tick(object sender, EventArgs e)
        {
            logMessage("Timeout: No answer.");
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

        void serial_OnDataReceived(object sender, DataReceivedEventArgs e)
        {
            logMessage(e.Data);
            e.Data = e.Data.Trim();
            timeOutTimer.Stop();
            string[] parms = e.Data.Split(' ');
            List<KeyValue> kvList = ParseParams(parms);
            if (parms[0] != "error")
            {
                if (commandList.Count > 0)
                {
                    sendData(commandList[0]);
                    commandList.RemoveAt(0);
                    timeOutTimer.Start();
                }

                if (parms[0] == "servoState")
                {
                    parseServoState(kvList);
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
            logMessage("Send: " + data);
            serial.SendCommand(data, true);
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


        private void Form1_Load(object sender, EventArgs e)
        {
            LoadSerialCombo();
            FillFaudRates();
            UpdateButtonStatus(false);

            txtLogs.VisibleChanged += new EventHandler(txtLogs_VisibleChanged);
            txtCommand.KeyDown += new KeyEventHandler(txtCommand_KeyDown);

            ucServoAngleList = new List<ucServoControl>();
            ucServoPWMList = new List<ucServoControl>();
            ucInputList =new List<ucServoControl>();

            for (int i = 0; i < outputCount; i++)
            {
                ucSubtrim subTrimCtrl = new ucSubtrim(i+1);
                subTrimCtrl.Left = (subTrimCtrl.Width + 10) * i;
                tabSubtrim.Controls.Add(subTrimCtrl);
                subTrimCtrl.OnCommand += new EventHandler<CommandEventArgs>(OnCommandReceived);

                ucServoControl scAngle = new ucServoControl(i + 1, -180, 180, "servoAngle", 0);
                scAngle.Left = (scAngle.Width + 10) * i;
                tabAngle.Controls.Add(scAngle);
                scAngle.OnCommand += new EventHandler<CommandEventArgs>(OnCommandReceived);
                ucServoAngleList.Add(scAngle);

                ucServoControl scPWM = new ucServoControl(i + 1, 0, 3000, "servoPWM", 1500);
                scPWM.Left = (scPWM.Width + 10) * i;
                tabPWM.Controls.Add(scPWM);
                scPWM.OnCommand += new EventHandler<CommandEventArgs>(OnCommandReceived);
                ucServoPWMList.Add(scPWM);
            }
            for (int i = 0; i < inputCount; i++)
            {
                ucServoControl scInput = new ucServoControl(i + 1, 0, 100, "input", 50);
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

    }
}
