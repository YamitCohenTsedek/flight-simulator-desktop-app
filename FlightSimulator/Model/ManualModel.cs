using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    class ManualModel
    {
        public void SendCommandsToSimulator(string command)
        {
            if (CommandsClient.Instance.IsConnectedToSimulator)
            {
                Thread t = new Thread(() =>
                {
                    CommandsClient.Instance.SendCommandsToSimulator(command);
                });
                t.Start();
            }
        }
    }
}
