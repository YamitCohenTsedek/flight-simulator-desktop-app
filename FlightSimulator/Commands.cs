using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Text;

namespace FlightSimulator
{
    class Commands
    {
        private int _ipNumber;
        private int _portNumber;
        private IPEndPoint _endPoint;
        private TcpClient _currentDeviceClient;
        private bool _isConnectedToSimulator = false;
        private StreamWriter _writer;
        private static Commands _instance = null;
       
        // _isSimulatorConnected Accessors 
        public int IsConnectedToSimulator
        {
            get
            {
                return _isConnectedToSimulator;
            }
            set
            {
                _isSimulatorConnected = value;
            }
        }

        // _instance Accessors 
        public static Commands Instance
        {
            // singleton design pattern
            get
            {
                if (_instance == null)
                {
                    _instance = new Commands();
                }
                return _instance;
            }
        }

        public void Connect(string ip, int port)
        {
            _ipNumber = ip;
            _portpNumber = port;
            _endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            _currentDeviceClient = new TcpClient();
            // try to connect to the server until sucess
            while (_currentDeviceClient.Connected == false)
            {
                try
                {
                    currentDeviceClient.Connect(this.endPoint);
                }
                catch (Exception e) {
                    continue;
                }
            }
            Connected = true;
            _writer = new StreamWriter(_currentDeviceClient.GetStream());
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
                _writer.WriteLine(currentCommand);
            }
        }

    }
}
