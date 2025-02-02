using Avalonia;
using PoViEmu.UI.ViewModels;

namespace PoViEmu.UI.Tools
{
    public static class ContextTool
    {
        public static T GetContext<T>(this StyledElement element) where T : ViewModelBase, new()
        {
            if (element.DataContext is T model)
                return model;

            return (T)(element.DataContext = new T());
        }
    }
}