using FlightSimulator.Model;
using System;

namespace FlightSimulator.ViewModels.Windows
{
    class ManualViewModel
    {
        private ManualModel manualModel = new ManualModel();
        private double aileron = 0;
        private double elevator = 0;
        private double rudder = 0;
        private double throttle = 0;

        public double Aileron
        {
            get => aileron;
            set
            {
                string aileronValueStr = Convert.ToString(value);
                string aileronCommnad = "set /controls/flight/aileron " + aileronValueStr;
                manualModel.SendCommandsToSimulator(aileronCommnad);
            }
        }

        public double Elevator
        {
            get => elevator;
            set
            {
                string elevatorValueStr = Convert.ToString(value);
                string elevatorCommnad = "set /controls/flight/elevator " + elevatorValueStr;
                manualModel.SendCommandsToSimulator(elevatorCommnad);
            }
        }

        public double Rudder
        {
            get => rudder;
            set
            {
                string rudderValueStr = Convert.ToString(value);
                string rudderCommnad = "set /controls/flight/rudder " + rudderValueStr;
                manualModel.SendCommandsToSimulator(rudderCommnad);
            }
        }

        public double Throttle
        {
            get => throttle;
            set
            {
                string throttleValueStr = Convert.ToString(value);
                string throttleCommnad = "set /controls/engines/current-engine/throttle " + throttleValueStr;
                manualModel.SendCommandsToSimulator(throttleCommnad);
            }
        }
    }
}
