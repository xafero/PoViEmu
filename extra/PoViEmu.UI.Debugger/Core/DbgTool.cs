using Avalonia;
using PoViEmu.UI.Dbg.ViewModels;
using PoViEmu.UI.Tools;
using PoViEmu.UI.ViewModels;

// ReSharper disable MergeIntoPattern

namespace PoViEmu.UI.Dbg.Core
{
    public static class DbgUiTool
    {
        public record DbgRun(RunInstViewModel Run);

        public static DbgRun? GetCurrentRun(this StyledElement control)
        {
            if (control.FindData<RunDbgViewModel>() is { } debug)
                if (debug.Base is { } core)
                    return new DbgRun(core);
            return null;
        }
    }
}