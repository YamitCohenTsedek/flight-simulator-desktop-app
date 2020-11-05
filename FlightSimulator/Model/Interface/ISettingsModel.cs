using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model.Interface
{
    public interface ISettingsModel
    {
        // The IP Of the flight server.
        string FlightServerIP { get; set; }
        // The info port of the flight server.
        int FlightInfoPort { get; set; }
        // The command port of the flight server.
        int FlightCommandPort { get; set; } 
        void SaveSettings();
        void ReloadSettings();
    }
}