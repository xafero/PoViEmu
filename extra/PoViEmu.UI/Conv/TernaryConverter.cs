using System;
using System.Globalization;
using System.Xml.Linq;
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
            }
            return AvaloniaProperty.UnsetValue;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return AvaloniaProperty.UnsetValue;
        }
    }
}