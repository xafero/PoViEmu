using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia;
using Avalonia.Data;
using Avalonia.Data.Converters;
using PoViEmu.Base;

namespace PoViEmu.UI.Conv
{
    public sealed class FmtStrConverter : IValueConverter, IMultiValueConverter
    {
        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            if (values.Any(x => x is UnsetValueType))
                return false;

            var kind = EnumHelper.Parse<FmtStrKind>(parameter);

            // TODO

            return AvaloniaProperty.UnsetValue;
        }

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var kind = EnumHelper.Parse<FmtStrKind>(parameter);
            if (targetType == typeof(string))
            {
                if (value is null)
                {
                    return null;
                }
                if (value is Guid id && kind == FmtStrKind.ID)
                {
                    var idT = id.ToShortId();
                    return idT;
                }
                if (value is bool boo && kind == FmtStrKind.BV)
                {
                    var booT = boo ? "✔" : "✘";
                    return booT;
                }
                if (value is string txt && kind == FmtStrKind.FL)
                {
                    var lines = txt.ToLines(noEmpty: true, noSpaces: true);
                    var lineT = lines.FirstOrDefault()?.Trim() ?? "...";
                    return $"{lineT}";
                }
            }
            var err = new InvalidOperationException($"{value} {targetType} {parameter}");
            return new BindingNotification(err, BindingErrorType.Error);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var kind = EnumHelper.Parse<FmtStrKind>(parameter);

            return AvaloniaProperty.UnsetValue;

            // TODO

            var err = new InvalidOperationException($"{value} {targetType} {parameter}");
            return new BindingNotification(err, BindingErrorType.Error);
        }
    }
}