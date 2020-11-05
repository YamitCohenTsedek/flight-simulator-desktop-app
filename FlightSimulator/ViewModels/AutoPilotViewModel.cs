using FlightSimulator.Model;
using System.Windows.Input;

namespace FlightSimulator.ViewModels
{
    class AutoPilotViewModel : BaseNotify
    {
        private AutoPilotModel model = new AutoPilotModel();

        // The default background color of the auto pilot window is white.
        private string backgroundColor = "White";

        private ICommand okCommand;

        private ICommand clearCommand;

        private string commands = "";

        public string Commands
        {
            get { return commands; }

            set
            {
                commands = value;
                // If the current text of the commands is not empty and the current background is white -
                // update the background color to be light-pink.
                if (Commands != null && Commands != "" && BackgroundColor == "White")
                {
                    BackgroundColor = "LightPink";
                }
                // If the current text of the commands is empty - update the background color to be white.
                else if (Commands == null || Commands == "")
                {
                    BackgroundColor = "White";
                }
            }
        }

        // This property is bounded to the view.
        public string BackgroundColor
        {
            get
            {
                return backgroundColor;
            }
            set
            {
                backgroundColor = value;
                NotifyPropertyChanged("BackgroundColor");
            }
        }

        public ICommand OkCommand
        {
            get
            {
                return okCommand ?? (okCommand = new CommandHandler(() =>
                {
                    string commandsToSend = Commands;
                    // Initiaize the text of the commands to be empty.
                    Commands = "";
                    // Notify the view that the Commands property has changed.
                    NotifyPropertyChanged(Commands);
                    // After sending the commands, the background should be white again.
                    BackgroundColor = "White";
                    // Send the commands to the simulator.
                    model.SendCommandsToSimulator(commandsToSend);
                }));
            }
        }

        public ICommand ClearCommand
        {
            get
            {
                return clearCommand ?? (clearCommand = new CommandHandler(() =>
                {
                    // Initiaize the text of the commands to be empty.
                    Commands = "";
                    // After clicking "Clear", the background should be white.
                    BackgroundColor = "White";
                    // Notify the view that the Commands property has changed.
                    NotifyPropertyChanged(Commands);
                }));
            }
        }
    }
}