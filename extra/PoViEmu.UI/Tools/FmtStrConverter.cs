using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Data;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Conv = System.Convert;

namespace PoViEmu.UI.Tools
{
    public sealed class FmtStrConverter : IValueConverter, IMultiValueConverter
    {
        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            if (values.Any(x => x is UnsetValueType))
                return false;

            // TODO

            return AvaloniaProperty.UnsetValue;
        }

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return AvaloniaProperty.UnsetValue;

            // TODO

            var err = new InvalidOperationException($"{value} {targetType} {parameter}");
            return new BindingNotification(err, BindingErrorType.Error);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return AvaloniaProperty.UnsetValue;

            // TODO

            var err = new InvalidOperationException($"{value} {targetType} {parameter}");
            return new BindingNotification(err, BindingErrorType.Error);
        }
    }
}