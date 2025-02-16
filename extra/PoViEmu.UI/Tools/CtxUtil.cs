using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Threading;
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

        public static T GetOrCreateData<T>(this StyledElement control) where T : new()
        {
            if (control.DataContext is T found)
                return found;

            var data = new T();
            control.DataContext = data;
            return data;
        }

        public static async void Invoke<T>(Func<Task<T>> action, Action<T> onDone, Action<Exception> onError)
        {
            try
            {
                var thread = Dispatcher.UIThread;
                var result = await thread.InvokeAsync(action);
                onDone(result);
            }
            catch (Exception e)
            {
                onError(e);
            }
        }
    }
}