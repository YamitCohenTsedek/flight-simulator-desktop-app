using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace FlightSimulator.ViewModels
{
    class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string colorName = (string)value;
            if (string.Equals(colorName, "Pink"))
            {
                return Brushes.LightPink;
            }
            return Brushes.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
