using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsManager.Services
{
    public class SerialService : ServiceBase
    {
        #region singleton

        private static SerialService instance = null;

        public static SerialService GetInstance()
        {
            if (instance == null)
                instance = new SerialService();

            return instance;
        }

        private SerialService()
        {
            serialPort = new SerialPort();
        }
        #endregion

        Thread ListenerThread { get; set; }
        SerialPort serialPort;

        public bool IsConnected { get; private set; }

        public event EventHandler<EventArgs> OnConnected;
        public event EventHandler<EventArgs> OnDisconnected;
        public event EventHandler<LogEventArgs> OnConnectError;
        public event EventHandler<DataReceivedEventArgs> OnDataReceived;

        public bool Connect(SerialDevice serialDevice)
        {
            try
            {
                if (this.IsConnected)
                    return false;

                serialPort.PortName = serialDevice.Name;
                serialPort.BaudRate = serialDevice.BaudRate;

                serialPort.ReadTimeout = 500;
                serialPort.WriteTimeout = 500;

                serialPort.Open();
                string message = serialPort.ReadExisting();

                this.IsConnected = true;

                this.ListenerThread = new System.Threading.Thread(StartListener);
                this.ListenerThread.IsBackground = true;
                this.ListenerThread.Start();


                if (OnConnected != null)
                    OnConnected(this, new EventArgs());
                return true;
            }
            catch(Exception ex)
            {
                OnConnectError(this, new LogEventArgs("Erreur de connexion. Assurez-vous que le port n'est pas déjà utilisé."));
                return false;
            }
        }

        void StartListener()
        {
            while (this.IsConnected)
            {
                try
                {
                    string message = serialPort.ReadLine(); // Vide le buffer
                    if (OnDataReceived != null)
                        OnDataReceived(this, new DataReceivedEventArgs(message));
                }
                catch (TimeoutException) { }
                catch (Exception ex)
                {
                    Log("Listenning Error: " + ex.Message);
                }
            }

            //Disconnecting
            try
            {
                
                if (OnDisconnected != null)
                    OnDisconnected(this, new EventArgs());

                Log("Disconnected");
            }
            catch (Exception ex)
            {
                Log("Disconnect Error: " + ex.Message);
            }
            serialPort.Close();
        }

        public bool Disconnect()
        {
            if (!this.IsConnected)
                return false;

            this.IsConnected = false; //Ferme la connection dans le Listener. Problème de thread.
            return true;
        }

        public SerialDevice[] GetSerialDevices()
        {
            List<SerialDevice> SerialDeviceList = new List<SerialDevice>();
            int defaultSerialSpeed = 57600;
            //int.TryParse(ConfigurationManager.AppSettings["defaultSerialSpeed"], out defaultSerialSpeed);
            foreach (COMPortInfo portInfo in COMPortInfo.GetCOMPortsInfo())
            {
                SerialDeviceList.Add(new SerialDevice(portInfo.Name, portInfo.Description, defaultSerialSpeed));
            }

            return SerialDeviceList.ToArray();
        }

        public void SendCommand(string command, bool lineFeed)
        {
            try
            {

                if (!this.IsConnected)
                {
                    Log("Not connected");
                    return;
                }

                if (lineFeed)
                    serialPort.Write(command + "\r\n");
                else
                    serialPort.Write(command);
            }
            catch (Exception ex)
            {
                Log("SendCommand Error: " + ex.Message);
            }
        }
    }



}
