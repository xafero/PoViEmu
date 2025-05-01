using Avalonia;
using PoViEmu.Base.CPU;
using PoViEmu.UI.Dbg.ViewModels;
using PoViEmu.UI.Tools;
using PoViEmu.UI.ViewModels;

// ReSharper disable MergeIntoPattern

namespace PoViEmu.UI.Dbg.Core
{
    public static class DbgUiTool
    {
        public record DbgRun(
            RunInstViewModel Run,
            RunDbgViewModel Dbg
        );

        public static DbgRun? GetCurrentRun(this StyledElement control)
        {
            if (control.FindData<RunDbgViewModel>() is { } debug)
                if (debug.Base is { } core)
                    return new DbgRun(core, debug);
            return null;
        }

        public static IState? GetState(this RunDbgViewModel rvm)
        {
            if (rvm.StateH is { } stateH) return stateH;
            if (rvm.StateN is { } stateN) return stateN;
            return null;
        }
    }
}