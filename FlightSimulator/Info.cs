using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;


namespace FlightSimulator
{
    public struct SimulatorInfo
    {
        public float lon;
        public float lat;

        public SimulatorInfo(float lonValue, float latValue)
        {
            lon = lonValue;
            lat = latValue;
        }
    }

    class Info
    {
        private string _ipNumber;
        private int _portNumber;
        private IPEndPoint _endPoint;
        private TcpListener _currentDeviceServer;
        private TcpClient _simulatorClient;
        private bool _isSimulatorConnected = false;
        private bool _serverShouldStop = false;
        private BinaryReader reader;
        private static Info _instance = null;
      

        // _isSimulatorConnected Accessors 
        public int IsSimulatorConnected
        { 
            get
            {
                return _isSimulatorConnected;
            }
            set
            {
                _isSimulatorConnected = value;
            }
        }

        // _serverShouldStop Accessors 
        public bool ServerShouldStop
        {
            get
            {
                return _serverShouldStop;
            }
            set
            {
                _serverShouldStop = value;
            }
        }

        // _instance Accessors 
        public static InfoServer Instance
        {
            get
            {
                if (instance == null)
                    instance = new InfoServer();
                return instance;
            }
        }

        public void OpenSocket(int ip, int port)
        {
            if(this._currentDeviceServer != null)
            {
                this.CloseSocket();
            }
            _ipNumber = ip;
            _portNumber = port;
            _endPoint = new IPEndPoint(IPAddress.Parse(ipNumber), portNumber);
            _currentDeviceAServer = new TcpListener(endPoint);
            _currentDeviceAServer.Start();
        }

        public void CloseSocket()
        {
            _currentDeviceServer.Stop();
            _simulatorClient.Close();
        }

        public SimulatorInfo getInfoFromSimulator()
        {
            string infoStr = "";
            char delimiter = ',';
            if(_isClientConnected == false)
            {
                _simulatorClient = currentDeviceServer.AcceptTcpClient();
                _isClientConnected = true;
                _reader = new BinaryReader(_simulatorClient.GetStream());
            }
            char currentChar = reader.ReadChar();
            // read the info as long as it is not a '\n' character
            while (currentChar != '\n')
            {
                infoStr += currentChar;
                currentChar = reader.ReadChar();
            }
            string[] splitInfo = info.Split(delimiter);
            // splitInfo[0] is the lan and splitInfo[1] is the lot 
            return new simulatorInfo(splitInfo[0], splitInfo[1]);
        }
    }
}



