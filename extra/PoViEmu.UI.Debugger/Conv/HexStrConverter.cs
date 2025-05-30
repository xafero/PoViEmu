using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia;
using Avalonia.Data;
using Avalonia.Data.Converters;
using PoViEmu.Base;
using ConvT = System.Convert;

namespace PoViEmu.UI.Tools
{
    public sealed class HexStrConverter : IValueConverter, IMultiValueConverter
    {
        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            if (values.Any(x => x is UnsetValueType))
                return false;

            if (targetType == typeof(string))
            {
                if (values is [bool a, bool b, bool c, bool d])
                    return $"{Convert(a, targetType, null, culture)}" +
                           $"{Convert(b, targetType, null, culture)}" +
                           $"{Convert(c, targetType, null, culture)}" +
                           $"{Convert(d, targetType, null, culture)}";
            }
            return AvaloniaProperty.UnsetValue;
        }

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (targetType == typeof(string))
            {
                if (value is Enum e)
                {
                    var enumType = e.GetType().GetEnumUnderlyingType();
                    value = ConvT.ChangeType(value, enumType);
                }
                switch (value)
                {
                    case bool b: return $"{(b ? 1 : 0)}";
                    case ushort us: return $"{us:x4}";
                    case uint ui: return $"{ui:x8}";
                    case null: return ((uint?)value).NullStr();
                }
            }
            if (targetType == typeof(object))
            {
                return value;
            }
            var err = new InvalidOperationException($"{value} {targetType} {parameter}");
            return new BindingNotification(err, BindingErrorType.Error);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string str)
            {
                var parsed = false;
                object? target = null;
                switch (str.Length)
                {
                    case 1:
                        target = ConvT.ToByte(str) == 1;
                        parsed = true;
                        break;
                    case 4:
                        target = ConvT.ToUInt16(str, 16);
                        parsed = true;
                        break;
                    case 8:
                        target = ConvT.ToUInt32(str, 16);
                        parsed = true;
                        break;
                }
                if (targetType.IsEnum)
                {
                    target = Enum.Parse(targetType, $"{target}");
                    parsed = true;
                }
                if (parsed) return target;
            }
            var err = new InvalidOperationException($"{value} {targetType} {parameter}");
            return new BindingNotification(err, BindingErrorType.Error);
        }
    }
}