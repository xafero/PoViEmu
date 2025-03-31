using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;
using PoViEmu.Base;

namespace PoViEmu.UI.Conv
{
    public sealed class TernaryConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var args = ValueHelper.AsStringArray(parameter);
            var dest = targetType.FullName;
            if (value is bool bv)
            {
                if (dest == "System.Double")
                {
                    var doubles = ValueHelper.AsDoubleArray(args);
                    if (doubles is [{ } trueV, { } falseV])
                    {
                        return bv ? trueV : falseV;
                    }
                }
            }
            return AvaloniaProperty.UnsetValue;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return AvaloniaProperty.UnsetValue;
        }
    }
}