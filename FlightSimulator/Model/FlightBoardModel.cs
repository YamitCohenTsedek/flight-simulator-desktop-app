using FlightSimulator.ViewModels;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    class FlightBoardModel : BaseNotify
    {
        private double lon;
        private double lat;
        // An instance of the info server.
        private InfoServer infoServer;

        // PropertyChanged event of the model.
        public new event PropertyChangedEventHandler PropertyChanged;

        public FlightBoardModel(InfoServer info) => infoServer = info;

        public double Lon
        {
            get => lon;
            set
            {
                lon = value;
                // Notify the observer (FlightBoaredViewModel) that Lon property has changed.
                NotifyPropertyChanged("Lon");
            }
        }

        public double Lat
        {
            get => lat;

            set
            {
                lat = value;
                // Notify the observer (FlightBoaredViewModel) that Lat property has changed.
                NotifyPropertyChanged("Lat");
            }
        }

        // Notify the observers when the PropertyChanged event occures.
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

        // Start getting info from the server.
        void StartGetInfo()
        {
            // Handle the receiving of the information in a new task.
            Thread t = new Thread(() =>
            {
                while (infoServer.IsServerShouldStop == false)
                {
                    SimulatorInfo simulatorInfo = infoServer.GetInfoFromSimulator();
                    Lon = simulatorInfo.Lon;
                    Lat = simulatorInfo.Lat;
                }
            });

            // Start the task
            t.Start();
        }

        // Stop getting info from the server.
        public void StopGetInfo()
        {
            infoServer.IsServerShouldStop = true;
        }
    }
}