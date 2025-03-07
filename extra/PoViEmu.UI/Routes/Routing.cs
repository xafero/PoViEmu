using System;
using Avalonia;

namespace PoViEmu.UI.Tools
{
    public static class Routing
    {
        public static void Push<T>(this IRouter router) where T : IRoutable, new()
        {
            var item = new T();
            router.Push(item);
        }

        public static IRouter GetRouter(this StyledElement view)
        {
            if (view.FindData<IRouter>() is { } r)
                return r;

            throw new InvalidOperationException("No router found!");
        }
    }
}