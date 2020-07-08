
using System;
using System.Globalization;
using System.Windows.Data;

namespace PriceCreator.Services.Converters
{
    class StringToDecimalConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal.TryParse(value.ToString(), out decimal valore);
            return valore == 0.00M ? parameter : valore;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal.TryParse(value.ToString(), out decimal valore);
            return valore;
        }
    }
}
