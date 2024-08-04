using System;
using PoViEmu.Common;
using PoViEmu.Core;
using PoViEmu.Core.Machine;
using PoViEmu.Core.Machine.Core;
using PoViEmu.Core.Machine.Ops;

namespace CpuFuzzer
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
            var humanO = humanPtsT.StartsWith("vc") || humanPtsT.StartsWith("vp") || humanPtsT.StartsWith("vm")
                ? default
                : Enum.Parse<OpCode>(humanPtsT);
            var humanR = humanPt.Length == 2 ? humanPt[1] : null;
            var bits = bytes.ToBinary();
            return new NasmLine(bits, hex, humanO, humanR);
        }
    }
}