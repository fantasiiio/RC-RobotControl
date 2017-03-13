using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsManager.Services;
using WindowsManager;
namespace RobotControl
{
    public partial class ucConnect : UserControl
    {
        SerialService serial;
        List<string> commandList;
        Timer timeOutTimer;

        public ucConnect()
        {
            InitializeComponent();

            timeOutTimer = new Timer();
            timeOutTimer.Interval = 2000;
            timeOutTimer.Tick += timeOutTimer_Tick;

            commandList = new List<string>();
            serial = SerialService.GetInstance();
            serial.OnConnected += serial_OnConnected;
            serial.OnConnectError += serial_OnConnectError;
            serial.OnDataReceived += serial_OnDataReceived;
            serial.OnDisconnected += serial_OnDisconnected;
            serial.OnLog += serial_OnLog;
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
                txtLogs.AppendText(msg + "\r\n");
        }

        void timeOutTimer_Tick(object sender, EventArgs e)
        {
            logMessage("Timeout: Assurez-vous de la connexion.");
            serial.Disconnect();
            timeOutTimer.Stop();
        }

        void serial_OnDataReceived(object sender, DataReceivedEventArgs e)
        {
            logMessage("Received: " + e.Data);

            timeOutTimer.Stop();
            if (e.Data == "OK")
            {
                if (commandList.Count > 0)
                {
                    sendData(commandList[0]);
                    commandList.RemoveAt(0);
                    timeOutTimer.Start();
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
            cboBaudRates.SelectedIndex = 0;
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
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            serial.Disconnect();
        }

    }
}
