using System;
using System.Windows;
using FlightSimulator.ViewModels.Windows;

namespace FlightSimulator.Views.Windows
{
    // Interaction logic for Settings.xaml.
    public partial class Settings : Window
    {
        SettingsWindowViewModel setViewModel;

        public Settings()
        {
            InitializeComponent();
            setViewModel = new SettingsWindowViewModel();
            DataContext = setViewModel;
            if (setViewModel.CloseAction == null)
            {
                setViewModel.CloseAction = new Action(Close);
            }
        }
    }
}

