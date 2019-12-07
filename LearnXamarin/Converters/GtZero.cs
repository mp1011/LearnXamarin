using System;
using System.Globalization;
using Xamarin.Forms;

namespace LearnXamarin.Converters
{
    public class GtZero : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int valueAsInt)
                return valueAsInt > 0;
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
