using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    class AutoPilotModel
    {
        public void SendCommandsToSimulator(string command)
        {
            if (CommandsClient.Instance.IsConnectedToSimulator)
            {
                Task t = new Task(() =>
                {
                    CommandsClient.Instance.SendCommandsToSimulator(command);
                });
                t.Start();
            }
        }
    }
}
