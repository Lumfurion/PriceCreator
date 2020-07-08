using System;
using System.Globalization;
using System.Windows.Data;
using System.Linq;

namespace PriceCreator.Services.Converters
{
    class StringTointConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int.TryParse(value.ToString(), out int valore);
            return valore == 0 ? parameter : valore;
        }

    }
}
