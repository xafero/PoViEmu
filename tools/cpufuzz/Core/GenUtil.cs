using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PoViEmu.Common;
using PoViEmu.Core.Machine;
using PoViEmu.Core.Machine.Ops;
using PoViEmu.CpuFuzzer.Core;
using SortedOps = System.Collections.Generic.SortedDictionary<PoViEmu.Core.Machine.Ops.OpCode,
    System.Collections.Generic.SortedDictionary<string, 
        System.Collections.Generic.HashSet<PoViEmu.CpuFuzzer.Core.NasmLine>>>;

namespace PoViEmu.CpuFuzzer.Core
{
    public static class GenUtil
    {
        public static IEnumerable<NasmLine> Iter(this SortedOps dict)
        {
            foreach (var (_, b) in dict)
            foreach (var (_, d) in b)
            foreach (var l in d)
                yield return l;
        }

        public static string ToNotKeyword(this OpCode code)
        {
            return code.ToString();
        }

        public static string ParseArg(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;
            switch (text)
            {
                // 16 bit
                case "ax": return "GenReg16.AX";
                case "bx": return "GenReg16.BX";
                case "cx": return "GenReg16.CX";
                case "dx": return "GenReg16.DX";
                // 8 bit 
                case "ah": return "GenReg8.AH";
                case "al": return "GenReg8.AL";
                case "bh": return "GenReg8.BH";
                case "bl": return "GenReg8.BL";
                case "ch": return "GenReg8.CH";
                case "cl": return "GenReg8.CL";
                case "dh": return "GenReg8.DH";
                case "dl": return "GenReg8.DL";
                // segment
                case "cs": return "SegReg.CS";
                case "ds": return "SegReg.DS";
                case "es": return "SegReg.ES";
                case "fs": return "SegReg.FS";
                case "gs": return "SegReg.GS";
                case "ss": return "SegReg.SS";
                // pointer
                case "si": return "IdxReg.SI";
                case "di": return "IdxReg.DI";
                case "bp": return "IdxReg.BP";
                case "ip": return "IdxReg.IP";
                case "sp": return "IdxReg.SP";
            }
            var parts = text.Split(',');
            if (parts.Length >= 2)
                return string.Join(", ", parts.Select(ParseArg));
            throw new NotImplementedException($"'{text}' ?!");
        }
    }
}