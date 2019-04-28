using FlightSimulator.Model;
using System.Windows.Input;

namespace FlightSimulator.ViewModels
{
    class AutoPilotViewModel : BaseNotify
    {

        private AutoPilotModel model = new AutoPilotModel();

        // the default background color is white
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
                // if the current text(commands) are not empty and the current background is white
                if (Commands != null && Commands != "" && BackgroundColor == "White")
                {
                    // update the background color to be light-pink
                    BackgroundColor = "LightPink";
                }
                // if the current commands are not empty
                else if (Commands == null || Commands == "")
                {
                    BackgroundColor = "White"; // if text is not empty
                }
            }
        }

        // this property is bounded to the view
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
                    // initiaize the text (the commands) to be empty
                    Commands = "";
                    // notify the view that the property Commands has changed 
                    NotifyPropertyChanged(Commands);
                    // after the commands were sent, the background should be white again
                    BackgroundColor = "White";
                    // sent the commands to the simulator
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
                    // initiaize the text (the commands) to be empty
                    Commands = "";
                    // after clicking "clear", the background should be white
                    BackgroundColor = "White";
                    // notify the view that the property Commands has changed 
                    NotifyPropertyChanged(Commands);
                }));
            }
        }
    }
}