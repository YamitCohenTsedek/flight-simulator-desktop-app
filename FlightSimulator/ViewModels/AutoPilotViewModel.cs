using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using FlightSimulator.Model;

namespace FlightSimulator.ViewModels
{
    class AutoPilotViewModel : BaseNotify
    {
        private AutoPilotModel autoPilotModel = new AutoPilotModel();

        private Brush brush;

        public AutoPilotViewModel()
        {
            Back = Brushes.White;
        }

        public Brush Back
        {
            get { return brush; }
            set
            {
                brush = value;
                NotifyPropertyChanged("Background");
            }
        }

    }
}
