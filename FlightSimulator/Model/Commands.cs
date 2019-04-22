using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Text;

namespace FlightSimulator.Model
{
    class Commands
    {
        private int ipNumber;
        private int portNumber;
        private IPEndPoint endPoint;
        private TcpClient currentDeviceClient;
        private bool isConnectedToSimulator = false;
        private StreamWriter writer;
        private static Commands instance = null;
       
        // isSimulatorConnected Accessors 
        public bool IsConnectedToSimulator
        {
            get
            {
                return this.isConnectedToSimulator;
            }
            set
            {
                this.isSimulatorConnected = value;
            }
        }

        // instance Accessors 
        public static Commands Instance
        {
            // singleton design pattern
            get
            {
                if (this.instance == null)
                {
                    this.instance = new Commands();
                }
                return this.instance;
            }
        }

        public void Connect(string ip, int port)
        {
            this.ipNumber = ip;
            this.portpNumber = port;
            this.endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            this.currentDeviceClient = new TcpClient();
            // try to connect to the server until sucess
            while (this.currentDeviceClient.Connected == false)
            {
                try
                {
                    currentDeviceClient.Connect(this.endPoint);
                }
                catch (Exception e) {
                    continue;
                }
            }
            this.IsConnectedToSimulator = true;
            this.writer = new StreamWriter(_currentDeviceClient.GetStream());
        }

         public void SendMessageToSimulator(string info)
        {
            string delimiter = "";
            int sleepMillisecons = 2000;
            string currentCommand = "";
            if (info == null || info == "")
            {
                return;
            }
            string[] commandsStr = info.Split(delimiter);
            for (int i=0; i< commandsStr; i++)
            {
                currentCommand = commandsStr[i];
                // sleep for 2000 milliseconds (delay of 2 seconds)
                Thread.Sleep(sleepMillisecons);
                this.writer.WriteLine(currentCommand);
            }
        }
    }
}
