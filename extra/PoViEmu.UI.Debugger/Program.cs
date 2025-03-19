using System;
using Avalonia;
using PoViEmu.UI.Dbg.Core;
using PoViEmu.UI.Extensions;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace PoViEmu.UI.Dbg
{
    internal sealed class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            ExtPoints.Instance.DesktopInit += Setup.OnInit;
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .WithInterFont()
                .LogToTrace();
    }
}