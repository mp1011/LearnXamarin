using System;
using System.Globalization;
using Xamarin.Forms;

namespace LearnXamarin.Converters
{
    public class StringFormatter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string text = value?.ToString() ?? string.Empty;
            string format = parameter as string;

            return format.Replace("{0}", text);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
