using Avalonia.Data.Converters;
using KPI.Pulse.UI.Models.Enums;
using System;
using System.Globalization;
using Avalonia.Media;

namespace KPI.Pulse.UI.Converters
{
    public class StatusToBrushConverter : IValueConverter
    {
        public static StatusToBrushConverter Instance = new();

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is TableDataStatus status)
            {
                var parameterStr = parameter?.ToString()?.ToLowerInvariant() ?? "";
                if (status == TableDataStatus.InProcess)
                {
                    switch (parameterStr)
                    {
                        case "background":
                            return new SolidColorBrush(Color.FromUInt32(0xfffffaf0));
                        case "foreground":
                            return new SolidColorBrush(Color.FromUInt32(0xffed8936));
                    }
                }
                
                if (status == TableDataStatus.Ready)
                {
                    switch (parameterStr)
                    {
                        case "background":
                            return new SolidColorBrush(Color.FromUInt32(0xffebf4ff));
                        case "foreground":
                            return new SolidColorBrush(Color.FromUInt32(0xff4299e1));
                    }
                }
            }

            return "";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
