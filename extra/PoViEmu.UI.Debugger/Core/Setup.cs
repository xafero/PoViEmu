using Avalonia.Controls;
using PoViEmu.UI.Extensions;
using System;
using Avalonia;
using DebugModel = PoViEmu.UI.Dbg.ViewModels.MainViewModel;
using DebugWindow = PoViEmu.UI.Dbg.Core.MainWindow;

namespace PoViEmu.UI.Dbg.Core
{
    internal static class Setup
    {
        public static void OnInit(object? sender, GenArgs<Window> e)
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

                var debug = new DebugWindow
                {
                    WindowStartupLocation = WindowStartupLocation.Manual, Position = pos,
                    DataContext = new DebugModel()
                };
                debug.Show(owner);
            }
        }
    }
}