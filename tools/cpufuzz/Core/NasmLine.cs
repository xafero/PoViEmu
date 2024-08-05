using System;
using PoViEmu.Common;
using PoViEmu.Core.Machine.Ops;

namespace PoViEmu.CpuFuzzer.Core
{
    public record NasmLine(string B, string X, OpCode H, string A)
    {
        public static NasmLine ParseLine(string text)
        {
            var parts = text.Split("  ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 3)
                throw new InvalidOperationException(text);
            // var offset = Convert.ToInt32(parts[0], 16);
            var hex = parts[1];
            var bytes = Convert.FromHexString(hex);
            var humanPt = parts[2].Split(' ', 2);
            var humanPtsT = humanPt[0];
            var humanO = humanPtsT.StartsWith("vc") || humanPtsT.StartsWith("vp") ||
                         humanPtsT.StartsWith("vm") || humanPtsT.StartsWith("vf") ||
                         humanPtsT.StartsWith("vs") || humanPtsT.StartsWith("va") ||
                         humanPtsT.StartsWith("vh") || humanPtsT.StartsWith("vd")
                ? default
                : Enum.Parse<OpCode>(humanPtsT);
            var humanR = humanPt.Length == 2 ? humanPt[1] : null;
            var bits = bytes.ToBinary();
            return new NasmLine(bits, hex, humanO, humanR);
        }
    }
}