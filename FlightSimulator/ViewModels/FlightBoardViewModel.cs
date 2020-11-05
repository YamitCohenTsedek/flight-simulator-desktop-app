using FlightSimulator.Model;
using FlightSimulator.Views.Windows;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Input;

namespace FlightSimulator.ViewModels
{
    public class FlightBoardViewModel : BaseNotify
    {
        private FlightBoardModel flightBoardModel;

        public event PropertyChangedEventHandler PropertyChanged;

        // Create the view of the settings window.
        private Settings settings = new Settings();

        public double Lon { get; set; }

        public double Lat { get; set; }

        public FlightBoardViewModel()
        {
            flightBoardModel = new FlightBoardModel(InfoServer.Instance);
            flightBoardModel.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "Lat")
                {
                    Lat = flightBoardModel.Lat;
                }
                else if (e.PropertyName == "Lon") Lon = flightBoardModel.Lon;
                NotifyPropertyChanged(e.PropertyName);
            };

        }

        #region Connect Command
        private ICommand connectCommand;
        public ICommand ConnectCommand { get { return connectCommand ?? (connectCommand = new CommandHandler(() => OnConnectClick())); } }

        void OnConnectClick()
        {
            // If there is a connection with the flight simulator:
            if (flightBoardModel.IsSimulatorConnected())
            {
                flightBoardModel.StopGetInfo();
                CommandsClient.Instance.Initialize();
                System.Threading.Thread.Sleep(1000);
            }

            // Open a new thread, connect the commands client to the simulator and open the info server.
            new Thread(delegate ()
            {
                CommandsClient.Instance.Connect(ApplicationSettingsModel.Instance.FlightServerIP, ApplicationSettingsModel.Instance.FlightCommandPort); 
            }).Start();
            flightBoardModel.OpenServer(ApplicationSettingsModel.Instance.FlightServerIP, ApplicationSettingsModel.Instance.FlightInfoPort);
        }


        #region Setting Command
        // The command of the settings button.
        private ICommand settingsCommand;
        public ICommand SettingsCommand { get { return settingsCommand ?? (settingsCommand = new CommandHandler(() => OnSettingsClick())); } }

        // Create the settings window.
        void OnSettingsClick()
        {
            if (!settings.IsLoaded)
            {
                settings = new Settings();
                settings.Show();
            }
            else settings.Show();
        }

        #endregion
        #region disconnectCommand Command
        // The command of the disconnect button.
        private ICommand disconnectCommand;
        public ICommand DisconnectCommand { get { return disconnectCommand ?? (disconnectCommand = new CommandHandler(() => OnClickDisconnect())); } }
        public void OnClickDisconnect()
        {
            if (flightBoardModel.IsSimulatorConnected())
            {
                flightBoardModel.StopGetInfo();
                CommandsClient.Instance.Initialize();
            }
        }
        #endregion
        #endregion

        public new void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}