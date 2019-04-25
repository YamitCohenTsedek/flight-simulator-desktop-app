using FlightSimulator.ViewModels;
using System.Windows.Controls;

namespace FlightSimulator.Views
{
    public partial class AutoPilot : UserControl
    {
        // Interaction logic for AutoPilot.xaml
        private AutoPilotViewModel autoPilotviewModel;

        public AutoPilot()
        {
            InitializeComponent();
            autoPilotviewModel = new AutoPilotViewModel();
            DataContext = autoPilotviewModel;
        }
    }
}
