using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;


namespace FlightSimulator.Model
{
    public struct SimulatorInfo
    {
        public double lon;
        public double lat;

        public SimulatorInfo(double lonValue, double latValue)
        {
            lon = lonValue;
            lat = latValue;
        }
    }

    class Info
    {
        private string ipNumber;
        private int portNumber;
        private IPEndPoint endPoint;
        private TcpListener currentDeviceServer;
        private TcpClient simulatorClient;
        private bool isSimulatorConnected = false;
        private bool isServerShouldStop = false;
        private BinaryReader reader;
        private static Info instance = null;
      
        // isSimulatorConnected Accessors 
        public int IsSimulatorConnected
        { 
            get
            {
                return this.isSimulatorConnected;
            }
            set
            {
                this.isSimulatorConnected = value;
            }
        }

        // serverShouldStop Accessors 
        public bool ServerShouldStop
        {
            get
            {
                return this.isServerShouldStop;
            }
            set
            {
                this.isServerShouldStop = value;
            }
        }

        // instance Accessors 
        public static InfoServer Instance
        {
            get
            {
                if (this.instance == null)
                    this.instance = new InfoServer();
                return this.instance;
            }
        }

        public void OpenSocket(int ip, int port)
        {
            if (this.currentDeviceServer != null)
            {
                this.CloseSocket();
            }
            this.ipNumber = ip;
            this.portNumber = port;
            this.endPoint = new IPEndPoint(IPAddress.Parse(ipNumber), portNumber);
            this.currentDeviceAServer = new TcpListener(endPoint);
            this.currentDeviceAServer.Start();
        }

        public void CloseSocket()
        {
            this.currentDeviceServer.Stop();
            this.simulatorClient.Close();
        }

        public SimulatorInfo GetInfoFromSimulator()
        {
            string infoStr = "";
            char delimiter = ',';
            if (this.isClientConnected == false)
            {
                this.simulatorClient = currentDeviceServer.AcceptTcpClient();
                this.isClientConnected = true;
                this.reader = new BinaryReader(_simulatorClient.GetStream());
            }
            char currentChar = reader.ReadChar();
            // read the info as long as it is not a '\n' character
            while (currentChar != '\n')
            {
                infoStr += currentChar;
                currentChar = reader.ReadChar();
            }
            string[] splitInfo = infoStr.Split(delimiter);
            // splitInfo[0] is the lan and splitInfo[1] is the lot 
            return new simulatorInfo(Convert.ToDouble(splitInfo[0]),
                Convert.ToDouble(splitInfo[1]));
        }
    }
}