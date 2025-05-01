using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Threading;

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

        public static async void Invoke<T>(Func<Task<T>> action, Action<T>? onDone, Action<Exception>? onError)
        {
            try
            {
                var thread = Dispatcher.UIThread;
                var result = await thread.InvokeAsync(action);
                onDone?.Invoke(result);
            }
            catch (Exception e)
            {
                onError?.Invoke(e);
            }
        }

        public static Task Await<T>(this T obj, Func<T, bool> func, int delayMs)
        {
            return Task.Run(async () =>
            {
                while (!func(obj))
                    await Task.Delay(delayMs);
            });
        }
    }
}