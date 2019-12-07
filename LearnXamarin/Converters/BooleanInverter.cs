using System;
using System.Globalization;
using Xamarin.Forms;

namespace LearnXamarin.Converters
{
    public class BooleanInverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool valueAsBool)
                return !valueAsBool;
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool valueAsBool)
                return !valueAsBool;
            else
                return null;
        }
    }
}
