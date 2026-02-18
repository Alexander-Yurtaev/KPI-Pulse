using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using KPI.Pulse.UI.Models.Enums;

namespace KPI.Pulse.UI.Converters
{
    public class AlertStatusToBrushConverter : IValueConverter
    {
        public static AlertStatusToBrushConverter Instance = new();

        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var parts = parameter?.ToString()?.Split(':');
            if (parts is null || parts.Length != 2)
            {
                parts = "orange:red".Split(':');
            }

            try
            {
                var warningHex = parts[0].TrimStart('#');
                var warningColorUint = System.Convert.ToUInt32(warningHex, 16);
                if (warningHex.Length == 6) warningColorUint = 0xFF000000 | warningColorUint;
                var warningBrush = new SolidColorBrush(warningColorUint);

                var dangerHex = parts[1].TrimStart('#');
                var dangerColorUint = System.Convert.ToUInt32(dangerHex, 16);
                if (dangerHex.Length == 6) dangerColorUint = 0xFF000000 | dangerColorUint;
                var dangerBrush = new SolidColorBrush(dangerColorUint);

                var alertStatus = (AlertStatus)(value ?? AlertStatus.Warning);
                switch (alertStatus)
                {
                    case AlertStatus.Warning:
                        return warningBrush;
                    case AlertStatus.Danger:
                        return dangerBrush;
                    default:
                        return new SolidColorBrush(Colors.Gray); // Возвращаем серый
                }
            }
            catch (Exception)
            {
                return new SolidColorBrush(Colors.Gray); // Возвращаем серый при ошибке
            }
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new InvalidOperationException();
        }
    }
}
