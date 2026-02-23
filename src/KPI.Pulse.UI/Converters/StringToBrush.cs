using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace KPI.Pulse.UI.Converters
{
    public class StringToBrush : IValueConverter
    {
        private readonly SolidColorBrush _positive = new SolidColorBrush(Color.FromUInt32(0xff48bb78));
        private readonly SolidColorBrush _negative = new SolidColorBrush(Color.FromUInt32(0xfff56565));

        public static StringToBrush Instance = new StringToBrush();

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value?.ToString()?.StartsWith("-") == true
                ? _negative
                : _positive;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
