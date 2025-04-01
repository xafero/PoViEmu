using System;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Layout;
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
                if (dest == "Avalonia.Controls.GridLength")
                {
                    var enums = ValueHelper.AsFuncArray<GridLength>(args, ParseGridLength);
                    if (enums is [{ } trueV, { } falseV])
                    {
                        return bv ? trueV : falseV;
                    }
                }
                if (dest == "Avalonia.Controls.Dock")
                {
                    var enums = ValueHelper.AsEnumArray<Dock>(args);
                    if (enums is [{ } trueV, { } falseV])
                    {
                        return bv ? trueV : falseV;
                    }
                }
                if (dest == "Avalonia.Layout.Orientation")
                {
                    var enums = ValueHelper.AsEnumArray<Orientation>(args);
                    if (enums is [{ } trueV, { } falseV])
                    {
                        return bv ? trueV : falseV;
                    }
                }
                if (dest == "System.Double")
                {
                    var doubles = ValueHelper.AsDoubleArray(args);
                    if (doubles is [{ } trueV, { } falseV])
                    {
                        return bv ? trueV : falseV;
                    }
                }
                if (dest == "System.Int32")
                {
                    var ints = ValueHelper.AsIntArray(args);
                    if (ints is [{ } trueV, { } falseV])
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

        private static bool ParseGridLength(string text, GridLength[] res)
        {
            switch (text)
            {
                case "Auto":
                    res[0] = GridLength.Auto;
                    return true;
                case "*":
                    res[0] = GridLength.Star;
                    return true;
                default:
                    throw new InvalidOperationException(text);
            }
        }
    }
}