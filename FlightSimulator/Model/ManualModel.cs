using System.Threading;

namespace FlightSimulator.Model
{
    class ManualModel
    {
        // send every command to the simulator in a new thread
        public void SendCommandsToSimulator(string info)
        {
            if (CommandsClient.Instance.IsConnectedToSimulator)
            {
                Thread t = new Thread(() =>
                {
                    CommandsClient.Instance.SendCommandsToSimulator(info);
                });
                t.Start();
            }
        }
    }
}