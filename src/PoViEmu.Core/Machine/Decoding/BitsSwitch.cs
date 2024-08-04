// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.IO;
using PoViEmu.Core.Machine.Core;
using PoViEmu.Core.Machine.Ops;
using Off1Tool = PoViEmu.Common.EnumHelper<PoViEmu.Core.Machine.Decoding.FloatOp1>;
using Off2Tool = PoViEmu.Common.EnumHelper<PoViEmu.Core.Machine.Decoding.FloatOp2>;
using Off3Tool = PoViEmu.Common.EnumHelper<PoViEmu.Core.Machine.Decoding.FloatOp3>;
using Off4Tool = PoViEmu.Common.EnumHelper<PoViEmu.Core.Machine.Decoding.FloatOp4>;
using Off5Tool = PoViEmu.Common.EnumHelper<PoViEmu.Core.Machine.Decoding.FloatOp5>;
using Off6Tool = PoViEmu.Common.EnumHelper<PoViEmu.Core.Machine.Decoding.FloatOp6>;
using Off7Tool = PoViEmu.Common.EnumHelper<PoViEmu.Core.Machine.Decoding.FloatOp7>;
using Orr1Tool = PoViEmu.Common.EnumHelper<PoViEmu.Core.Machine.Decoding.Opa1R>;
using OpTool = PoViEmu.Common.EnumHelper<PoViEmu.Core.Machine.Decoding.Opa>;
using OfaTool = PoViEmu.Common.EnumHelper<PoViEmu.Core.Machine.Decoding.Opa1A>;
using OfbTool = PoViEmu.Common.EnumHelper<PoViEmu.Core.Machine.Decoding.Opa1B>;
using OfcTool = PoViEmu.Common.EnumHelper<PoViEmu.Core.Machine.Decoding.Opa1C>;
using RgTool = PoViEmu.Common.EnumHelper<PoViEmu.Core.Machine.Decoding.Rgo>;
using SgTool = PoViEmu.Common.EnumHelper<PoViEmu.Core.Machine.Decoding.Sgo>;
using FgTool = PoViEmu.Common.EnumHelper<PoViEmu.Core.Machine.Decoding.Fgo>;
using RaTool = PoViEmu.Common.EnumHelper<PoViEmu.Core.Machine.Decoding.Rga>;
using OdTool = PoViEmu.Common.EnumHelper<PoViEmu.Core.Machine.Decoding.OpDir>;
using ObTool = PoViEmu.Common.EnumHelper<PoViEmu.Core.Machine.Decoding.OpBit>;
using O = PoViEmu.Core.Machine.Ops.OpCode;
using R = PoViEmu.Core.Machine.Ops.Register;
using M = PoViEmu.Core.Machine.Ops.Modifier;

namespace PoViEmu.Core.Machine.Decoding
{
    public class BitsSwitch
    {
        public static object? FindRegFlag(byte op, byte flagged, int? shift = null)
        {
            switch (op)
            {
                case 0x8E:
                case 0x8C:
                    return SgTool.FindFlag(flagged, shift);
                default:
                    return RgTool.FindFlag(flagged, shift);
            }
        }

        public static OpDir Inverse(OpDir dir)
        {
            return dir == OpDir.Left ? OpDir.Right : OpDir.Left;
        }

        public static object? FindOpFlag(byte op, byte flagged)
        {
            switch (op)
            {
                case 0xD0:
                case 0xD1:
                case 0xD2:
                case 0xD3:
                    return Orr1Tool.FindFlag(flagged);
                case 0xDD:
                    return Off7Tool.FindFlag(flagged);
                case 0xDE:
                    return Off6Tool.FindFlag(flagged);
                case 0xDB:
                    return Off5Tool.FindFlag(flagged);
                case 0xDA:
                    return Off4Tool.FindFlag(flagged);
                case 0xDF:
                    return Off3Tool.FindFlag(flagged);
                case 0xD9:
                    return Off2Tool.FindFlag(flagged);
                case 0xD8:
                    return Off1Tool.FindFlag(flagged);
                case 0x8F:
                    return OfcTool.FindFlag(flagged);
                case 0xF6:
                case 0xF7:
                    return OfbTool.FindFlag(flagged);
                case 0xFE:
                case 0xFF:
                    return OfaTool.FindFlag(flagged);
                default:
                    throw new ArgumentException($"0x{op:x2} 0x{flagged:x2}");
            }
        }
    }
}