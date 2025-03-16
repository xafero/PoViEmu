using System;
using Avalonia.Controls;

namespace PoViEmu.UI.Extensions
{
    public sealed class ExtPoints
    {
        public static ExtPoints Instance { get; } = new();

        private ExtPoints()
        {
        }

        public event EventHandler<GenArgs<Window>>? DesktopInit;
        internal void OnDesktopInit(object s, Window a) => DesktopInit?.Invoke(s, new GenArgs<Window>(a));

        public event EventHandler<GenArgs<Control>>? MobileInit;
        internal void OnMobileInit(object s, Control a) => MobileInit?.Invoke(s, new GenArgs<Control>(a));
    }
}