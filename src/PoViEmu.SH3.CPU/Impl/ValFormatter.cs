using System;
using System.Linq;
using System.Text;
using PoViEmu.Base;
using PoViEmu.Base.CPU;
using PoViEmu.Base.ISA;
using PoViEmu.SH3.ISA;

namespace PoViEmu.SH3.CPU.Impl
{
    internal sealed class ValFormatter : IValFormatter
    {
        public string GetFull(IInstruction current, IState state)
        {
            var m = (MachineState)state;
            var pre = $"{current}";
            return pre;
        }

        public string GetAdrTitle() => "Offset";

        public string Format(object value) => FormatIt(value);

        private static string FormatIt(object? obj, bool withPrefix = true)
        {
            return obj switch
            {
                bool l => l ? "1" : "0",
                byte b => $"0x{b:X2}"[(withPrefix ? 0 : 2)..],
                ushort u => $"0x{u:X4}"[(withPrefix ? 0 : 2)..],
                uint u => $"0x{u:X8}"[(withPrefix ? 0 : 2)..],
                Flagged f => ToFlagStr(f),
                byte[] ba => TextHelper.FormatRle(ba.Select(u => FormatIt(u, withPrefix))),
                ushort[] ua => TextHelper.FormatRle(ua.Select(u => FormatIt(u, withPrefix))),
                _ => throw new InvalidOperationException($"[{obj?.GetType()}] {obj}")
            };
        }

        private static string ToFlagStr(Flagged fl)
        {
            var bld = new StringBuilder();
            if (fl.HasFlag(Flagged.Mode_bit)) bld.Append('O').Append(' ');
            if (fl.HasFlag(Flagged.Bank_bit)) bld.Append('R').Append(' ');
            if (fl.HasFlag(Flagged.Block_bit)) bld.Append('B').Append(' ');
            if (fl.HasFlag(Flagged.M_bit)) bld.Append('M').Append(' ');
            if (fl.HasFlag(Flagged.Q_bit)) bld.Append('Q').Append(' ');
            if (fl.HasFlag(Flagged.I0)) bld.Append('I').Append(' ');
            if (fl.HasFlag(Flagged.I1)) bld.Append('I').Append(' ');
            if (fl.HasFlag(Flagged.I2)) bld.Append('I').Append(' ');
            if (fl.HasFlag(Flagged.I3)) bld.Append('I').Append(' ');
            if (fl.HasFlag(Flagged.S_bit)) bld.Append('S').Append(' ');
            if (fl.HasFlag(Flagged.T_bit)) bld.Append('T').Append(' ');
            return bld.ToString().Trim();
        }
    }
}