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

        // create the instance of settings window
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
        public new void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
        }


        #region Connect Command
        // the command of connect for connect button
        private ICommand connectsCommand;
        public ICommand ConnectsCommand { get { return connectsCommand ?? (connectsCommand = new CommandHandler(() => OnConnectClick())); } }

        void OnConnectClick()
        {
            // if there is a connection  
            if (flightBoardModel.IsSimulatorConnected())
            {
                flightBoardModel.StopGetInfo();
                CommandsClient.Instance.Initialize();
                System.Threading.Thread.Sleep(1000);
            }
            new Thread(delegate ()
            {
                CommandsClient.Instance.Connect(ApplicationSettingsModel.Instance.FlightServerIP, ApplicationSettingsModel.Instance.FlightCommandPort); // conect to simulator
            }).Start();
            // open info server
            flightBoardModel.OpenServer(ApplicationSettingsModel.Instance.FlightServerIP, ApplicationSettingsModel.Instance.FlightInfoPort);


        }
        #endregion

        #region Setting Command
        //the command of the button of setting
        private ICommand settingsCommand;
        public ICommand SettingsCommand { get { return settingsCommand ?? (settingsCommand = new CommandHandler(() => OnSettingsClick())); } }

        // in order to create one setting window
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

        #region Disconnect Command
        private ICommand disconnectCommand;
        public ICommand DisconnectCommand {
            get {
                return disconnectCommand ?? (disconnectCommand = new CommandHandler(() => OnDisconnectClick()));
            }
        }

        public void OnDisconnectClick()
        {
            if (flightBoardModel.IsSimulatorConnected())
            {
                flightBoardModel.StopGetInfo();
            }
        }
        #endregion

    }
}