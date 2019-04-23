using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;


namespace FlightSimulator.Model
{
    public struct SimulatorInfo
    {
        public double Lon;
        public double Lat;

        public SimulatorInfo(double lonValue, double latValue)
        {
            Lon = lonValue;
            Lat = latValue;
        }
    }

    class InfoServer
    {
        private IPEndPoint endPoint;
        private TcpListener currentDeviceServer;
        private TcpClient simulatorClient;
        private BinaryReader reader;
        private static InfoServer instance = null;

        private InfoServer() { }

        // InfoServer singleton
        public static InfoServer Instance
        {
            get
            {
                if (instance == null)
                    instance = new InfoServer();
                return instance;
            }
        }

        // IsSimulatorConnected Accessors 
        public bool IsSimulatorConnected { get; set; } = false;

        // ServerShouldStop Accessors 
        public bool ServerShouldStop { get; set; } = false;

        public void OpenSocket(string ip, int portNumber)
        {
            if (currentDeviceServer != null)
            {
                CloseSocket();
            }
            endPoint = new IPEndPoint(IPAddress.Parse(ip), portNumber);
            currentDeviceServer = new TcpListener(this.endPoint);
            currentDeviceServer.Start();
        }

        public void CloseSocket()
        {
            currentDeviceServer.Stop();
            IsSimulatorConnected = false;
            simulatorClient?.Close();
            reader.Dispose();
        }

        public SimulatorInfo GetInfoFromSimulator()
        {
            string infoStr = "";
            char delimiter = ',';
            if (IsSimulatorConnected == false)
            {
                simulatorClient = currentDeviceServer.AcceptTcpClient();
                IsSimulatorConnected = true;
                reader = new BinaryReader(simulatorClient.GetStream());
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
            return new SimulatorInfo(Convert.ToDouble(splitInfo[0]),
                Convert.ToDouble(splitInfo[1]));
        }
    }
}