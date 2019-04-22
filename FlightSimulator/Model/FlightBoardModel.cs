using FlightSimulator.ViewModel;
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
        // an instance of Info class (the server)
        private Info info;

        // PropertyChange event of the model
        public event PropertyChangedEventHandler PropertyChanged;

        public FlightBoardModel(Info info)
        {
            this.info = info;
        }

        //  lon Accessors
        public double Lon
        {
            get { return this.lon; }
            set
            {
                this.lon = value;
                // notify the observer (FlightBoaredViewModel) that Lon property has changed)
                NotifyPropertyChanged("Lon");
            }
        }

        //  lat Accessors
        public double Lat
        {
            get { return this.lat; }
            set
            {
                this.lat = value;
                // notify the observer (FlightBoaredViewModel) that Lat property has changed)
                NotifyPropertyChanged("Lat");
            }
        }

        // notify the observers when the event PropertyChanged accured
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool isSimulatorConnected()
        {
            return this.info.isSimulatorConnected();
        }


        public void OpenServer(string ip, int port)
        {
            this.info.OpenSocket(ip, port);
            this.getInfo();
        }

        // start getting info from the server
        void startGetInfo()
        {
            // handling the information receiving in a new task
            Task t = new Task(() =>
            {
                while (this.info.ServerShouldStop == false)
                {
                    SimulatorInfo simulatorInfo = this.info.getInfoFromSimulator();
                    this.Lon = simulatorInfo.Lon;
                    this.Lat = simulatorInfo.Lat;
                }
            });
            // start the task
            t.start();
        }

        void stopGetInfo()
        {
            this.info.IsSimulatorShouldStop = true;
        }

    }
}