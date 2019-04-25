
using System.Windows;
using System.Windows.Controls;


namespace FlightSimulator.Views
{
    // Interaction logic for Manual.xaml
    public partial class Manual : UserControl
    {
        public Manual()
        {
            InitializeComponent();
            DataContext = new ManualViewModel();
        }

    }
}
