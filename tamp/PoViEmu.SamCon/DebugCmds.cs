using ConsoleServer;
using PoViEmu.Common;
using PoViEmu.Core.Hardware;
using PoViEmu.Telnet;
using static PoViEmu.Common.CollHelper;

namespace PoViEmu.SamCon
{
    internal static class DebugCmds
    {
        internal static readonly MachineState State = new();

        public static bool Help(TelnetSession s, string cmd, string? arg)
        {
            s.WriteOneLine("PVE Debug v0.10 help screen");
            s.WriteOneLine("quit            Q");
            s.WriteOneLine("register        R");
            return true;
        }

        public static bool Register(TelnetSession s, string cmd, string? arg)
        {
            if (arg.TrimNull() is { } regArg)
            {
                foreach (var (key, val) in ParseKeyValue(regArg))
                    State[key] = val;
                return true;
            }
            var lines = State.ToRegDebugLin();
            s.WriteLines(lines);
            return true;
        }
    }
}