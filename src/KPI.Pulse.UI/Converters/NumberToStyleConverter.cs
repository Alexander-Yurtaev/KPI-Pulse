using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace KPI.Pulse.UI.Converters
{
    public class NumberToStyleConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value?.ToString()?.StartsWith("-") == true
                ? "NegativeStatusStyle"
                : "PositiveStatusStyle";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
