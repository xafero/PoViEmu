using System;
using Avalonia;
using Avalonia.Controls;
using PoViEmu.UI.Extensions;
using DebugWindow = PoViEmu.UI.Debugger.Core.MainWindow;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace PoViEmu.UI.Debugger
{
    internal sealed class Program
    {
        private static void OnInit(object? sender, GenArgs<Window> e)
        {
            var owner = e.Value;
            owner.PositionChanged += OnOpen;
            return;

            void OnOpen(object? o, EventArgs a)
            {
                owner.PositionChanged -= OnOpen;

                var x = owner.Position.X + 5 + owner.Width;
                var y = owner.Position.Y;
                var pos = new PixelPoint((int)x, y);

                var debug = new DebugWindow { WindowStartupLocation = WindowStartupLocation.Manual, Position = pos };
                debug.Show(owner);
            }
        }

        [STAThread]
        public static void Main(string[] args)
        {
            ExtPoints.Instance.DesktopInit += OnInit;
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .WithInterFont()
                .LogToTrace();
    }
}