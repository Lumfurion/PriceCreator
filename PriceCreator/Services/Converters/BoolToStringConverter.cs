using System;
using System.Globalization;
namespace PriceCreator.Services.Converters
{
    class BoolToStringConverter : ConverterBase
    {
      
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool v = System.Convert.ToBoolean(value);
            return v ? "Да" : "Нет";
        }
    }
}
