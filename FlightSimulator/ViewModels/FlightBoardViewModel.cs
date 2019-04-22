using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModels
{
    public class FlightBoardViewModel : BaseNotify
    {
        FlightBoardModel model;
        public event PropertyChangedEventHandler PropertyChanged;
        public FlightBoardViewModel(){
             //InitializeComponent();
            model = new FlightBoardModel(new Info());
            model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName.Equals("Lat"))
                {
                    Lat = model.Lat;
                }
                else {
                    if (e.PropertyName.Equals("Lon"))
                    {
                        Lon = model.Lot;
                    }
                        }
                NotifyPropertyChanged(e.PropertyName);
            };
        }
     
        public double Lon
        { get; set; }

        public double Lat
        {
            get;set;
        }
        void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
    }

}
