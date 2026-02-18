using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using KPI.Pulse.UI.Models.Enums;

namespace KPI.Pulse.UI.Converters
{
    public class TrendStatusToBrushConverter : IValueConverter
    {
        public static TrendStatusToBrushConverter Instance = new();

        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var parts = parameter?.ToString()?.Split(':');
            if (parts is null || parts.Length != 3)
            {
                parts = "green:orange:red".Split(':');
            }

            try
            {
                var successHex = parts[0].TrimStart('#');
                var succesColorUint = System.Convert.ToUInt32(successHex, 16);
                if (successHex.Length == 6) succesColorUint = 0xFF000000 | succesColorUint;
                var succesBrush = new SolidColorBrush(succesColorUint);

                var warningHex = parts[1].TrimStart('#');
                var warningColorUint = System.Convert.ToUInt32(warningHex, 16);
                if (warningHex.Length == 6) warningColorUint = 0xFF000000 | warningColorUint;
                var warningBrush = new SolidColorBrush(warningColorUint);

                var dangerHex = parts[2].TrimStart('#');
                var dangerColorUint = System.Convert.ToUInt32(dangerHex, 16);
                if (dangerHex.Length == 6) dangerColorUint = 0xFF000000 | dangerColorUint;
                var dangerBrush = new SolidColorBrush(dangerColorUint);

                var trendStatus = (TrendStatus)(value ?? TrendStatus.Success);
                switch (trendStatus)
                {
                    case TrendStatus.Success:
                        return succesBrush;
                    case TrendStatus.Warning:
                        return warningBrush;
                    case TrendStatus.Danger:
                        return dangerBrush;
                    default:
                        return new SolidColorBrush(Colors.Gray); // Возвращаем серый
                }
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
