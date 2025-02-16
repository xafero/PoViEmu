using Avalonia;
using Avalonia.Controls;
using PoViEmu.UI.Views;

namespace PoViEmu.UI.Tools
{
    public static class CtxUtil
    {
        public static T? FindData<T>(this StyledElement control)
        {
            if (control.DataContext is T found)
                return found;

            if (control.Parent is { } parent)
                return FindData<T>(parent);

            return default;
        }
    }
}