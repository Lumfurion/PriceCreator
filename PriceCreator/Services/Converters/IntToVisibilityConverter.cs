using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PriceCreator.Services.Converters
{
    class IntToVisibilityConverter:IValueConverter
    {
        public object  Convert(object value,Type targetType,object pameter, CultureInfo culture)
        {
            int v = System.Convert.ToInt32(value);
            return (v == 0) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }


    }
}
