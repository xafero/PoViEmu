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

        public static string ToNotKeyword(this object code)
        {
            var codeStr = code.ToString();
            switch (codeStr)
            {
                case "out": return "@out";
                case "int": return "@int";
                case "in": return "@in";
            }
            return codeStr;
        }

        public static string ParseArg(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;
            switch (text)
            {
                // constants
                case "1": return "Constants.One";
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
                // num seg
                case "segr0": return "NumReg.Segr0";
                case "segr1": return "NumReg.Segr1";
                case "segr2": return "NumReg.Segr2";
                case "segr3": return "NumReg.Segr3";
                case "segr4": return "NumReg.Segr4";
                case "segr5": return "NumReg.Segr5";
                case "segr6": return "NumReg.Segr6";
                case "segr7": return "NumReg.Segr7";
                // float reg
                case "st0": return "FloatReg.St0";
                case "st1": return "FloatReg.St1";
                case "st2": return "FloatReg.St2";
                case "st3": return "FloatReg.St3";
                case "st4": return "FloatReg.St4";
                case "st5": return "FloatReg.St5";
                case "st6": return "FloatReg.St6";
                case "st7": return "FloatReg.St7";
                // sse reg
                case "xmm0": return "ExtReg.Xmm0";
                case "xmm1": return "ExtReg.Xmm1";
                case "xmm2": return "ExtReg.Xmm2";
                case "xmm3": return "ExtReg.Xmm3";
                case "xmm4": return "ExtReg.Xmm4";
                case "xmm5": return "ExtReg.Xmm5";
                case "xmm6": return "ExtReg.Xmm6";
                case "xmm7": return "ExtReg.Xmm7";
                case "xmm3{k1}": return "ExtReg.Xmm3_k1";
                case "xmm14": return "ExtReg.Xmm14";
                case "xmm22": return "ExtReg.Xmm22";
                case "ymm0": return "EytReg.ymm0";
                case "ymm1": return "EytReg.ymm1";
                case "ymm2": return "EytReg.ymm2";
                case "ymm3": return "EytReg.ymm3";
                case "ymm4": return "EytReg.ymm4";
                case "ymm5": return "EytReg.ymm5";
                case "ymm6": return "EytReg.ymm6";
                case "ymm7": return "EytReg.ymm7";
            }
            if (text.EndsWith("{1to8}"))
            {
                text = text.Replace("{1to8}", string.Empty);
            }
            if (text.StartsWith("byte +"))
            {
                if (text.Length == 9 || text.Length == 10)
                {
                    var bytePArg = ParseArg(text[6..]);
                    return $"{bytePArg}.Plus()";
                }
            }
            if (text.StartsWith("byte -"))
            {
                if (text.Length == 9 || text.Length == 10)
                {
                    var byteMArg = ParseArg(text[6..]);
                    return $"{byteMArg}.Minus()";
                }
            }
            if (text.StartsWith("oword "))
            {
                if (text.Length == 18 || text.Length == 15 || text.Length == 19
                    || text.Length == 20 || text.Length == 17)
                {
                    var wordArg = ParseArg(text[6..]);
                    return $"M.oword.On({wordArg})";
                }
            }
            if (text.StartsWith("yword "))
            {
                if (text.Length == 13 || text.Length == 18 || text.Length == 15
                    || text.Length == 20 || text.Length == 17)
                {
                    var wordArg = ParseArg(text[6..]);
                    return $"M.yword.On({wordArg})";
                }
            }
            if (text.StartsWith("tword "))
            {
                if (text.Length == 13 || text.Length == 10 || text.Length == 18
                    || text.Length == 17 || text.Length == 15 || text.Length == 14
                    || text.Length == 20 || text.Length == 19 || text.Length == 16)
                {
                    var wordArg = ParseArg(text[6..]);
                    return $"M.tword.On({wordArg})";
                }
            }
            if (text.StartsWith("qword "))
            {
                if (text.Length == 13 || text.Length == 10 || text.Length == 18
                    || text.Length == 17 || text.Length == 15 || text.Length == 14
                    || text.Length == 20 || text.Length == 19 || text.Length == 16)
                {
                    var wordArg = ParseArg(text[6..]);
                    return $"M.qword.On({wordArg})";
                }
            }
            if (text.StartsWith("dword "))
            {
                if (text.Length == 13 || text.Length == 10 || text.Length == 18
                    || text.Length == 17 || text.Length == 15 || text.Length == 14
                    || text.Length == 20 || text.Length == 19 || text.Length == 16)
                {
                    var wordArg = ParseArg(text[6..]);
                    return $"M.dword.On({wordArg})";
                }
            }
            if (text.StartsWith("word "))
            {
                if (text.Length == 12 || text.Length == 9 || text.Length == 11 || text.Length == 10
                    || text.Length == 17 || text.Length == 16 || text.Length == 14
                    || text.Length == 13 || text.Length == 19 || text.Length == 18
                    || text.Length == 15)
                {
                    var wordArg = ParseArg(text[5..]);
                    return $"M.word.On({wordArg})";
                }
            }
            if (text.StartsWith("short "))
            {
                if (text.Length == 9 || text.Length == 10 || text.Length == 12)
                {
                    var wordArg = ParseArg(text[6..]);
                    return $"M.@short.On({wordArg})";
                }
            }
            if (text.StartsWith("byte "))
            {
                if (text.Length == 12 || text.Length == 9 || text.Length == 8 || text.Length == 17
                    || text.Length == 14 || text.Length == 13 || text.Length == 16
                    || text.Length == 18 || text.Length == 19 || text.Length == 15)
                {
                    var wordArg = ParseArg(text[5..]);
                    return $"M.@byte.On({wordArg})";
                }
            }
            if (text.StartsWith("far "))
            {
                if (text.Length == 11 || text.Length == 8 || text.Length == 15
                    || text.Length == 16 || text.Length == 13 || text.Length == 12
                    || text.Length == 18 || text.Length == 17 || text.Length == 14)
                {
                    var wordArg = ParseArg(text[4..]);
                    return $"M.far.On({wordArg})";
                }
            }
            if (text.StartsWith("to "))
            {
                if (text.Length == 6)
                {
                    var wordArg = ParseArg(text[3..]);
                    return $"M.to.On({wordArg})";
                }
            }
            if (text.StartsWith("0x"))
            {
                if (((text.Length == 13 || text.Length == 12) && text[6] == ':')
                    || ((text.Length == 11 || text.Length == 12) && text[5] == ':')
                    || ((text.Length == 11) && text[6] == ':')
                    || ((text.Length == 11) && text[4] == ':')
                    || ((text.Length == 10) && text[3] == ':'))
                {
                    var dpTmp = text.Split(':', 2);
                    var dp1 = ParseArg(dpTmp[0]);
                    var dp2 = ParseArg(dpTmp[1]);
                    return $"{dp1}.ToMem({dp2})";
                }
                if (text.Length == 3 || text.Length == 4)
                {
                    return "s.NextByte()";
                }
                if (text.Length == 6 || text.Length == 5)
                {
                    return "s.NextShort()";
                }
            }
            if (text.StartsWith('[') && text.EndsWith(']'))
            {
                if (text.StartsWith("[0x") && (text.Length == 8 || text.Length == 7 || text.Length == 6
                                               || text.Length == 5))
                {
                    var boxArg = ParseArg(text.TrimStart('[').TrimEnd(']'));
                    return $"{boxArg}.Box()";
                }
                if (text.Length == 4)
                {
                    var boxArg = ParseArg(text.TrimStart('[').TrimEnd(']'));
                    return $"{boxArg}.Box()";
                }
                if (text.Length == 12 || text.Length == 11 || text.Length == 14 || text.Length == 13)
                {
                    if (text[3] == '+' && text[6] == '+')
                    {
                        var plusArg = text.Split('+');
                        var plusX = ParseArg(plusArg[0].TrimStart('['));
                        var plusY = ParseArg(plusArg[1].TrimEnd(']'));
                        var plusZ = ParseArg(plusArg[2].TrimEnd(']'));
                        return $"{plusX}.Plus({plusY}).Plus({plusZ})";
                    }
                    if (text[3] == '+' && text[6] == '-')
                    {
                        var plusArg = text.Split('+', '-');
                        var plusX = ParseArg(plusArg[0].TrimStart('['));
                        var plusY = ParseArg(plusArg[1].TrimEnd(']'));
                        var plusZ = ParseArg(plusArg[2].TrimEnd(']'));
                        return $"{plusX}.Plus({plusY}).Minus({plusZ})";
                    }
                }
                if (text.Length == 7 || text.Length == 9 || text.Length == 8 || text.Length == 11
                    || text.Length == 10)
                {
                    if (text[3] == '+')
                    {
                        var plusArg = text.Split('+');
                        var plusX = ParseArg(plusArg[0].TrimStart('['));
                        var plusY = ParseArg(plusArg[1].TrimEnd(']'));
                        return $"{plusX}.Plus({plusY})";
                    }
                    if (text[3] == '-')
                    {
                        var plusArg = text.Split('-');
                        var plusX = ParseArg(plusArg[0].TrimStart('['));
                        var plusY = ParseArg(plusArg[1].TrimEnd(']'));
                        return $"{plusX}.Minus({plusY})";
                    }
                }
            }
            var parts = text.Split(',');
            if (parts.Length >= 2)
                return string.Join(", ", parts.Select(ParseArg));
            throw new NotImplementedException($"'{text}' ({text.Length}) ?!");
        }
    }
}