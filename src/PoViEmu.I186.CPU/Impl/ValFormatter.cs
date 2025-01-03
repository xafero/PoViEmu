using System;
using System.Linq;
using System.Text;
using PoViEmu.Base;
using PoViEmu.Base.CPU;
using PoViEmu.Base.ISA;
using PoViEmu.I186.ISA;

namespace PoViEmu.I186.CPU.Impl
{
    internal sealed class ValFormatter : IValFormatter
    {
        public string GetFull(IInstruction current, IState state)
        {
            var m = (MachineState)state;
            var pre = $"{m.CS:X4}:{current}";
            return pre;
        }

        public string GetAdrTitle() => "Seg:Off";

        public string Format(object value) => FormatIt(value);

        private static string FormatIt(object? obj, bool withPrefix = true)
        {
            return obj switch
            {
                bool l => l ? "1" : "0",
                byte b => $"0x{b:X2}"[(withPrefix ? 0 : 2)..],
                ushort u => $"0x{u:X4}"[(withPrefix ? 0 : 2)..],
                Flagged f => ToFlagStr(f),
                byte[] ba => TextHelper.FormatRle(ba.Select(u => FormatIt(u, withPrefix))),
                ushort[] ua => TextHelper.FormatRle(ua.Select(u => FormatIt(u, withPrefix))),
                _ => throw new InvalidOperationException($"[{obj?.GetType()}] {obj}")
            };
        }

        private static string ToFlagStr(Flagged fl)
        {
            var bld = new StringBuilder();
            if (fl.HasFlag(Flagged.Carry)) bld.Append('C').Append(' ');
            if (fl.HasFlag(Flagged.Parity)) bld.Append('P').Append(' ');
            if (fl.HasFlag(Flagged.Auxiliary)) bld.Append('A').Append(' ');
            if (fl.HasFlag(Flagged.Zero)) bld.Append('Z').Append(' ');
            if (fl.HasFlag(Flagged.Sign)) bld.Append('S').Append(' ');
            if (fl.HasFlag(Flagged.Trap)) bld.Append('T').Append(' ');
            if (fl.HasFlag(Flagged.Interrupt)) bld.Append('I').Append(' ');
            if (fl.HasFlag(Flagged.Direction)) bld.Append('D').Append(' ');
            if (fl.HasFlag(Flagged.Overflow)) bld.Append('O').Append(' ');
            return bld.ToString().Trim();
        }
    }
}