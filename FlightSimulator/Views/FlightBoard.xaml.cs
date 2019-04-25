using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using FlightSimulator.ViewModels;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;

namespace FlightSimulator.Views
{
    // Interaction logic for FlightBoard.xaml
    public partial class FlightBoard : UserControl
    {
        ObservableDataSource<Point> planeLocations;
        FlightBoardViewModel flightBoardViewModel;

        public FlightBoard()
        {
            InitializeComponent();
            flightBoardViewModel = new FlightBoardViewModel();
            DataContext = flightBoardViewModel;
            flightBoardViewModel.PropertyChanged += Vm_PropertyChanged;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Set identity mapping of point in collection to point on plot
            planeLocations = new ObservableDataSource<Point>();
            planeLocations.SetXYMapping(p => p);
            plotter.AddLineGraph(planeLocations, 2, "Route");
        }

        private void Vm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName.Equals("Lat") || e.PropertyName.Equals("Lon"))
            {
                Point p1 = new Point(flightBoardViewModel.Lat, flightBoardViewModel.Lon);
                planeLocations.AppendAsync(Dispatcher, p1);
            }
        }

    }

}

