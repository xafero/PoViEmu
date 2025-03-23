using System;
using Avalonia.Threading;

namespace PoViEmu.UI.Graphics
{
    public static class LoopTool
    {
        public static DispatcherTimer CreateTimer(int fps)
        {
            var delay = TimeSpan.FromMilliseconds(1000.0 / fps);
            var timer = new DispatcherTimer { Interval = delay };
            return timer;
        }
    }
}