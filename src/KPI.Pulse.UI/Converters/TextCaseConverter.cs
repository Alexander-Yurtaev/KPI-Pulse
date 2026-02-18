using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace KPI.Pulse.UI.Converters
{
    public class TextCaseConverter: IValueConverter
    {
        public static readonly TextCaseConverter Instance = new();

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string sourceText)
            {
                return parameter?.ToString()?.ToLowerInvariant() switch
                {
                    "upper" => sourceText.ToUpperInvariant(),
                    "lower" => sourceText.ToLowerInvariant(),
                    _ => sourceText
                };
            }

            return value;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new InvalidOperationException();
        }
    }
}
