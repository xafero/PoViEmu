using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PoViEmu.Core.Hardware
{
    public static class ValueHelp
    {
        public static object Parse(this object? obj, Type to)
        {
            return obj switch
            {
                string { Length: 1 } s => Convert.ToByte(s) == 1,
                string { Length: 4 } s => Convert.ToByte(s.Replace("0x", ""), 16),
                string { Length: 6 } s => Convert.ToUInt16(s.Replace("0x", ""), 16),
                string s when to == typeof(Flagged) => AsFlagStr(s),
                _ => throw new InvalidOperationException($"[{obj?.GetType()}] {obj} ({to})")
            };
        }

        public static string Format(this object? obj, bool withPrefix = true)
        {
            return obj switch
            {
                bool l => l ? "1" : "0",
                byte b => $"0x{b:X2}"[(withPrefix ? 0 : 2)..],
                ushort u => $"0x{u:X4}"[(withPrefix ? 0 : 2)..],
                Flagged f => ToFlagStr(f),
                byte[] ba => FormatRle(ba.Select(u => Format(u, withPrefix))),
                ushort[] ua => FormatRle(ua.Select(u => Format(u, withPrefix))),
                _ => throw new InvalidOperationException($"[{obj?.GetType()}] {obj}")
            };
        }

        private static string FormatRle(IEnumerable<string> texts)
        {
            var count = 1;
            var last = string.Empty;
            return string.Join("", texts.Concat([string.Empty]).Select(text =>
            {
                var res = string.Empty;
                if (text.Equals(last))
                {
                    count++;
                }
                else if (last.Length != 0)
                {
                    res = $"{count}°{last.Replace("0x", "")} ";
                    count = 1;
                }
                last = text;
                return res;
            })).Trim();
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

        private static Flagged AsFlagStr(string fl)
        {
            Flagged bld = default;
            if (fl.Contains('C')) bld |= Flagged.Carry;
            if (fl.Contains('P')) bld |= Flagged.Parity;
            if (fl.Contains('A')) bld |= Flagged.Auxiliary;
            if (fl.Contains('Z')) bld |= Flagged.Zero;
            if (fl.Contains('S')) bld |= Flagged.Sign;
            if (fl.Contains('T')) bld |= Flagged.Trap;
            if (fl.Contains('I')) bld |= Flagged.Interrupt;
            if (fl.Contains('D')) bld |= Flagged.Direction;
            if (fl.Contains('O')) bld |= Flagged.Overflow;
            return bld;
        }
    }
}