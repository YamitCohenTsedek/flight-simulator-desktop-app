using FlightSimulator.ViewModels;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    class FlightBoardModel: BaseNotify
    {
        private double lon;
        private double lat;
        // an instance of Info class (the server)
        private InfoServer infoServer;

        // PropertyChanged event of the model
        public new event PropertyChangedEventHandler PropertyChanged;

        public FlightBoardModel(InfoServer info) => infoServer = info;

        public double Lon
        {
            get => lon;
            set
            {
                lon = value;
                // notify the observer (FlightBoaredViewModel) that Lon property has changed)
                NotifyPropertyChanged("Lon");
            }
        }

        public double Lat
        {
            get => lat;

            set
            {
                lat = value;
                // notify the observer (FlightBoaredViewModel) that Lat property has changed)
                NotifyPropertyChanged("Lat");
            }
        }

        // notify the observers when the event PropertyChanged occures
        public new void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool IsSimulatorConnected()
        {
            return infoServer.IsSimulatorConnected;
        }


        public void OpenServer(string ip, int port)
        {
            infoServer.OpenSocket(ip, port);
            StartGetInfo();
        }

        // start getting info from the server
        void StartGetInfo()
        {
            // handling the information receiving in a new task
            Thread t = new Thread(() =>
            {
                while (infoServer.IsServerShouldStop == false)
                {
                    SimulatorInfo simulatorInfo = infoServer.GetInfoFromSimulator();
                    Lon = simulatorInfo.Lon;
                    Lat = simulatorInfo.Lat;
                }
            });

            // start the task
            t.Start();
        }

        public void StopGetInfo()
        {
            infoServer.IsServerShouldStop = true;
        }
    }
}