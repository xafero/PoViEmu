using System;
using System.Text;
using PoViEmu.Core.Hardware;

namespace Hallo
{
    public static class ValueHelp
    {
        public static string Format(this object obj)
        {
            return obj switch
            {
                bool b => b ? "1" : "0",
                ushort us => $"0x{us:X4}",
                Flagged fl => ToStr(fl),
                _ => throw new InvalidOperationException($"[{obj.GetType()}] {obj}")
            };
        }

        private static string ToStr(Flagged fl)
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