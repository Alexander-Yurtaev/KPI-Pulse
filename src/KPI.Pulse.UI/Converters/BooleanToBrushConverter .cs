using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace KPI.Pulse.UI.Converters
{
    public class BooleanToBrushConverter : IValueConverter
    {
        public static BooleanToBrushConverter Instance = new();

        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var parts = parameter?.ToString()?.Split(':');
            if (parts is null || parts.Length != 2)
            {
                parts = "red:green".Split(':');
            }

            try
            {
                var falseHex = parts[0].TrimStart('#');
                var falseColorUint = System.Convert.ToUInt32(falseHex, 16);
                if (falseHex.Length == 6) falseColorUint = 0xFF000000 | falseColorUint;
                var falseBrush = new SolidColorBrush(falseColorUint);

                var trueHex = parts[1].TrimStart('#');
                var trueColorUint = System.Convert.ToUInt32(trueHex, 16);
                if (trueHex.Length == 6) trueColorUint = 0xFF000000 | trueColorUint;
                var trueBrush = new SolidColorBrush(trueColorUint);

                var brush = (bool)(value ?? false) ? trueBrush : falseBrush;

                return brush;
            }
            catch (Exception ex)
            {
                return new SolidColorBrush(Colors.Gray); // Возвращаем серый при ошибке
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
