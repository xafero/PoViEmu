﻿// ReSharper disable InconsistentNaming

using System.Collections.Generic;
using System.IO;
using PoViEmu.Core.Machine.Args;
using PoViEmu.Core.Machine.Core;
using PoViEmu.Core.Machine.Ops;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PoViEmu.Core.Machine.Args;
using PoViEmu.Core.Machine.Core;
using PoViEmu.Core.Machine.Decoding;
using PoViEmu.Core.Machine.Ops;
using O = PoViEmu.Core.Machine.Ops.OpCode;
using R = PoViEmu.Core.Machine.Ops.Register;
using M = PoViEmu.Core.Machine.Ops.Modifier;
using A = PoViEmu.Core.Machine.Ops.OpArg;
using C = PoViEmu.Core.Machine.Decoding.Constants;

namespace PoViEmu.Expert
{
    internal static class Intel16xC5
    {
        internal static Instruction? Parse(Stream s, byte[] buff, long pos, byte first)
        {
            var second = s.NextByte(buff);
            switch (second)
            {
                case 0x00:
                    return new(pos, first, 2, O.lds, [R.AX, R.BX.Plus(R.SI)], [second]);
                case 0x01:
                    return new(pos, first, 2, O.lds, [R.AX, R.BX.Plus(R.DI)], [second]);
                case 0x03:
                    return new(pos, first, 2, O.lds, [R.AX, R.BP.Plus(R.DI)], [second]);
                case 0x04:
                    return new(pos, first, 2, O.lds, [R.AX, R.SI.Box()], [second]);
                case 0x06:
                    return new(pos, first, 4, O.lds, [R.AX, s.NextShort(buff).Box()]);
                case 0x07:
                    return new(pos, first, 2, O.lds, [R.AX, R.BX.Box()], [second]);
                case 0x08:
                    return new(pos, first, 2, O.lds, [R.CX, R.BX.Plus(R.SI)], [second]);
                case 0x09:
                    return new(pos, first, 2, O.lds, [R.CX, R.BX.Plus(R.DI)], [second]);
                case 0x0A:
                    return new(pos, first, 2, O.lds, [R.CX, R.BP.Plus(R.SI)], [second]);
                case 0x0B:
                    return new(pos, first, 2, O.lds, [R.CX, R.BP.Plus(R.DI)], [second]);
                case 0x0D:
                    return new(pos, first, 2, O.lds, [R.CX, R.DI.Box()], [second]);
                case 0x0E:
                    return new(pos, first, 4, O.lds, [R.CX, s.NextShort(buff).Box()]);
                case 0x11:
                    return new(pos, first, 2, O.lds, [R.DX, R.BX.Plus(R.DI)], [second]);
                case 0x12:
                    return new(pos, first, 2, O.lds, [R.DX, R.BP.Plus(R.SI)], [second]);
                case 0x14:
                    return new(pos, first, 2, O.lds, [R.DX, R.SI.Box()], [second]);
                case 0x16:
                    return new(pos, first, 4, O.lds, [R.DX, s.NextShort(buff).Box()]);
                case 0x17:
                    return new(pos, first, 2, O.lds, [R.DX, R.BX.Box()], [second]);
                case 0x18:
                    return new(pos, first, 2, O.lds, [R.BX, R.BX.Plus(R.SI)], [second]);
                case 0x19:
                    return new(pos, first, 2, O.lds, [R.BX, R.BX.Plus(R.DI)], [second]);
                case 0x1A:
                    return new(pos, first, 2, O.lds, [R.BX, R.BP.Plus(R.SI)], [second]);
                case 0x1B:
                    return new(pos, first, 2, O.lds, [R.BX, R.BP.Plus(R.DI)], [second]);
                case 0x1C:
                    return new(pos, first, 2, O.lds, [R.BX, R.SI.Box()], [second]);
                case 0x1D:
                    return new(pos, first, 2, O.lds, [R.BX, R.DI.Box()], [second]);
                case 0x1E:
                    return new(pos, first, 4, O.lds, [R.BX, s.NextShort(buff).Box()]);
                case 0x20:
                    return new(pos, first, 2, O.lds, [R.SP, R.BX.Plus(R.SI)], [second]);
                case 0x21:
                    return new(pos, first, 2, O.lds, [R.SP, R.BX.Plus(R.DI)], [second]);
                case 0x22:
                    return new(pos, first, 2, O.lds, [R.SP, R.BP.Plus(R.SI)], [second]);
                case 0x23:
                    return new(pos, first, 2, O.lds, [R.SP, R.BP.Plus(R.DI)], [second]);
                case 0x24:
                    return new(pos, first, 2, O.lds, [R.SP, R.SI.Box()], [second]);
                case 0x25:
                    return new(pos, first, 2, O.lds, [R.SP, R.DI.Box()], [second]);
                case 0x27:
                    return new(pos, first, 2, O.lds, [R.SP, R.BX.Box()], [second]);
                case 0x28:
                    return new(pos, first, 2, O.lds, [R.BP, R.BX.Plus(R.SI)], [second]);
                case 0x29:
                    return new(pos, first, 2, O.lds, [R.BP, R.BX.Plus(R.DI)], [second]);
                case 0x2A:
                    return new(pos, first, 2, O.lds, [R.BP, R.BP.Plus(R.SI)], [second]);
                case 0x2B:
                    return new(pos, first, 2, O.lds, [R.BP, R.BP.Plus(R.DI)], [second]);
                case 0x2C:
                    return new(pos, first, 2, O.lds, [R.BP, R.SI.Box()], [second]);
                case 0x2D:
                    return new(pos, first, 2, O.lds, [R.BP, R.DI.Box()], [second]);
                case 0x2E:
                    return new(pos, first, 4, O.lds, [R.BP, s.NextByte(buff).Box()]);
                case 0x2F:
                    return new(pos, first, 2, O.lds, [R.BP, R.BX.Box()], [second]);
                case 0x30:
                    return new(pos, first, 2, O.lds, [R.SI, R.BX.Plus(R.SI)], [second]);
                case 0x31:
                    return new(pos, first, 2, O.lds, [R.SI, R.BX.Plus(R.DI)], [second]);
                case 0x32:
                    return new(pos, first, 2, O.lds, [R.SI, R.BP.Plus(R.SI)], [second]);
                case 0x33:
                    return new(pos, first, 2, O.lds, [R.SI, R.BP.Plus(R.DI)], [second]);
                case 0x34:
                    return new(pos, first, 2, O.lds, [R.SI, R.SI.Box()], [second]);
                case 0x35:
                    return new(pos, first, 2, O.lds, [R.SI, R.DI.Box()], [second]);
                case 0x36:
                    return new(pos, first, 4, O.lds, [R.SI, s.NextShort(buff).Box()]);
                case 0x38:
                    return new(pos, first, 2, O.lds, [R.DI, R.BX.Plus(R.SI)], [second]);
                case 0x39:
                    return new(pos, first, 2, O.lds, [R.DI, R.BX.Plus(R.DI)], [second]);
                case 0x3A:
                    return new(pos, first, 2, O.lds, [R.DI, R.BP.Plus(R.SI)], [second]);
                case 0x3C:
                    return new(pos, first, 2, O.lds, [R.DI, R.SI.Box()], [second]);
                case 0x3D:
                    return new(pos, first, 2, O.lds, [R.DI, R.DI.Box()], [second]);
                case 0x3E:
                    return new(pos, first, 4, O.lds, [R.DI, s.NextShort(buff).Box()]);
                case 0x3F:
                    return new(pos, first, 2, O.lds, [R.DI, R.BX.Box()], [second]);
                case 0x40:
                    return new(pos, first, 3, O.lds, [R.AX, R.BX.Plus(R.SI).Plus(s.NextByte(buff))]);
                case 0x41:
                    return new(pos, first, 3, O.lds, [R.AX, R.BX.Plus(R.DI).Plus(s.NextByte(buff))]);
                case 0x42:
                    return new(pos, first, 3, O.lds, [R.AX, R.BP.Plus(R.SI).Plus(s.NextByte(buff))]);
                case 0x43:
                    return new(pos, first, 3, O.lds, [R.AX, R.BP.Plus(R.DI).Plus(s.NextByte(buff))]);
                case 0x44:
                    return new(pos, first, 3, O.lds, [R.AX, R.SI.Plus(s.NextByte(buff))]);
                case 0x45:
                    return new(pos, first, 3, O.lds, [R.AX, R.DI.Plus(s.NextByte(buff))]);
                case 0x46:
                    return new(pos, first, 3, O.lds, [R.AX, R.BP.Plus(s.NextByte(buff))]);
                case 0x47:
                    return new(pos, first, 3, O.lds, [R.AX, R.BX.Plus(s.NextByte(buff))]);
                case 0x48:
                    return new(pos, first, 3, O.lds, [R.CX, R.BX.Plus(R.SI).Plus(s.NextByte(buff))]);
                case 0x49:
                    return new(pos, first, 3, O.lds, [R.CX, R.BX.Plus(R.DI).Minus(s.NextByte(buff))]);
                case 0x4A:
                    return new(pos, first, 3, O.lds, [R.CX, R.BP.Plus(R.SI).Plus(s.NextByte(buff))]);
                case 0x4C:
                    return new(pos, first, 3, O.lds, [R.CX, R.SI.Plus(s.NextByte(buff))]);
                case 0x4D:
                    return new(pos, first, 3, O.lds, [R.CX, R.DI.Plus(s.NextByte(buff))]);
                case 0x4E:
                    return new(pos, first, 3, O.lds, [R.CX, R.BP.Minus(s.NextByte(buff))]);
                case 0x4F:
                    return new(pos, first, 3, O.lds, [R.CX, R.BX.Minus(s.NextByte(buff))]);
                case 0x50:
                    return new(pos, first, 3, O.lds, [R.DX, R.BX.Plus(R.SI).Plus(s.NextByte(buff))]);
                case 0x51:
                    return new(pos, first, 3, O.lds, [R.DX, R.BX.Plus(R.DI).Plus(s.NextByte(buff))]);
                case 0x52:
                    return new(pos, first, 3, O.lds, [R.DX, R.BP.Plus(R.SI).Plus(s.NextByte(buff))]);
                case 0x53:
                    return new(pos, first, 3, O.lds, [R.DX, R.BP.Plus(R.DI).Minus(s.NextByte(buff))]);
                case 0x54:
                    return new(pos, first, 3, O.lds, [R.DX, R.SI.Plus(s.NextByte(buff))]);
                case 0x56:
                    return new(pos, first, 3, O.lds, [R.DX, R.BP.Plus(s.NextByte(buff))]);
                case 0x58:
                    return new(pos, first, 3, O.lds, [R.BX, R.BX.Plus(R.SI).Plus(s.NextByte(buff))]);
                case 0x59:
                    return new(pos, first, 3, O.lds, [R.BX, R.BX.Plus(R.DI).Plus(s.NextByte(buff))]);
                case 0x5A:
                    return new(pos, first, 3, O.lds, [R.BX, R.BP.Plus(R.SI).Plus(s.NextByte(buff))]);
                case 0x5B:
                    return new(pos, first, 3, O.lds, [R.BX, R.BP.Plus(R.DI).Minus(s.NextByte(buff))]);
                case 0x5C:
                    return new(pos, first, 3, O.lds, [R.BX, R.SI.Plus(s.NextByte(buff))]);
                case 0x5D:
                    return new(pos, first, 3, O.lds, [R.BX, R.DI.Plus(s.NextByte(buff))]);
                case 0x5E:
                    return new(pos, first, 3, O.lds, [R.BX, R.BP.Minus(s.NextByte(buff))]);
                case 0x5F:
                    return new(pos, first, 3, O.lds, [R.BX, R.BX.Plus(s.NextByte(buff))]);
                case 0x60:
                    return new(pos, first, 3, O.lds, [R.SP, R.BX.Plus(R.SI).Plus(s.NextByte(buff))]);
                case 0x61:
                    return new(pos, first, 3, O.lds, [R.SP, R.BX.Plus(R.DI).Plus(s.NextByte(buff))]);
                case 0x62:
                    return new(pos, first, 3, O.lds, [R.SP, R.BP.Plus(R.SI).Plus(s.NextByte(buff))]);
                case 0x63:
                    return new(pos, first, 3, O.lds, [R.SP, R.BP.Plus(R.DI).Plus(s.NextByte(buff))]);
                case 0x64:
                    return new(pos, first, 3, O.lds, [R.SP, R.SI.Plus(s.NextByte(buff))]);
                case 0x65:
                    return new(pos, first, 3, O.lds, [R.SP, R.DI.Plus(s.NextByte(buff))]);
                case 0x66:
                    return new(pos, first, 3, O.lds, [R.SP, R.BP.Plus(s.NextByte(buff))]);
                case 0x67:
                    return new(pos, first, 3, O.lds, [R.SP, R.BX.Minus(s.NextByte(buff))]);
                case 0x68:
                    return new(pos, first, 3, O.lds, [R.BP, R.BX.Plus(R.SI).Minus(s.NextByte(buff))]);
                case 0x69:
                    return new(pos, first, 3, O.lds, [R.BP, R.BX.Plus(R.DI).Plus(s.NextByte(buff))]);
                case 0x6A:
                    return new(pos, first, 3, O.lds, [R.BP, R.BP.Plus(R.SI).Plus(s.NextByte(buff))]);
                case 0x6C:
                    return new(pos, first, 3, O.lds, [R.BP, R.SI.Plus(s.NextByte(buff))]);
                case 0x6D:
                    return new(pos, first, 3, O.lds, [R.BP, R.DI.Plus(s.NextByte(buff))]);
                case 0x6E:
                    return new(pos, first, 3, O.lds, [R.BP, R.BP.Plus(s.NextByte(buff))]);
                case 0x70:
                    return new(pos, first, 3, O.lds, [R.SI, R.BX.Plus(R.SI).Plus(s.NextByte(buff))]);
                case 0x71:
                    return new(pos, first, 3, O.lds, [R.SI, R.BX.Plus(R.DI).Plus(s.NextByte(buff))]);
                case 0x72:
                    return new(pos, first, 3, O.lds, [R.SI, R.BP.Plus(R.SI).Plus(s.NextByte(buff))]);
                case 0x73:
                    return new(pos, first, 3, O.lds, [R.SI, R.BP.Plus(R.DI).Plus(s.NextByte(buff))]);
                case 0x75:
                    return new(pos, first, 3, O.lds, [R.SI, R.DI.Minus(s.NextByte(buff))]);
                case 0x76:
                    return new(pos, first, 3, O.lds, [R.SI, R.BP.Plus(s.NextByte(buff))]);
                case 0x77:
                    return new(pos, first, 3, O.lds, [R.SI, R.BX.Plus(s.NextByte(buff))]);
                case 0x78:
                    return new(pos, first, 3, O.lds, [R.DI, R.BX.Plus(R.SI).Plus(s.NextByte(buff))]);
                case 0x79:
                    return new(pos, first, 3, O.lds, [R.DI, R.BX.Plus(R.DI).Plus(s.NextByte(buff))]);
                case 0x7A:
                    return new(pos, first, 3, O.lds, [R.DI, R.BP.Plus(R.SI).Plus(s.NextByte(buff))]);
                case 0x7B:
                    return new(pos, first, 3, O.lds, [R.DI, R.BP.Plus(R.DI).Plus(s.NextByte(buff))]);
                case 0x7C:
                    return new(pos, first, 3, O.lds, [R.DI, R.SI.Minus(s.NextByte(buff))]);
                case 0x7D:
                    return new(pos, first, 3, O.lds, [R.DI, R.DI.Plus(s.NextByte(buff))]);
                case 0x7E:
                    return new(pos, first, 3, O.lds, [R.DI, R.BP.Plus(s.NextByte(buff))]);
                case 0x7F:
                    return new(pos, first, 3, O.lds, [R.DI, R.BX.Plus(s.NextByte(buff))]);
                case 0x80:
                    return new(pos, first, 4, O.lds, [R.AX, R.BX.Plus(R.SI).Minus(s.NextShort(buff))]);
                case 0x81:
                    return new(pos, first, 4, O.lds, [R.AX, R.BX.Plus(R.DI).Minus(s.NextShort(buff))]);
                case 0x82:
                    return new(pos, first, 4, O.lds, [R.AX, R.BP.Plus(R.SI).Minus(s.NextShort(buff))]);
                case 0x83:
                    return new(pos, first, 4, O.lds, [R.AX, R.BP.Plus(R.DI).Plus(s.NextShort(buff))]);
                case 0x84:
                    return new(pos, first, 4, O.lds, [R.AX, R.SI.Minus(s.NextShort(buff))]);
                case 0x86:
                    return new(pos, first, 4, O.lds, [R.AX, R.BP.Plus(s.NextShort(buff))]);
                case 0x87:
                    return new(pos, first, 4, O.lds, [R.AX, R.BX.Minus(s.NextShort(buff))]);
                case 0x88:
                    return new(pos, first, 4, O.lds, [R.CX, R.BX.Plus(R.SI).Plus(s.NextShort(buff))]);
                case 0x89:
                    return new(pos, first, 4, O.lds, [R.CX, R.BX.Plus(R.DI).Minus(s.NextShort(buff))]);
                case 0x8A:
                    return new(pos, first, 4, O.lds, [R.CX, R.BP.Plus(R.SI).Plus(s.NextShort(buff))]);
                case 0x8B:
                    return new(pos, first, 4, O.lds, [R.CX, R.BP.Plus(R.DI).Plus(s.NextShort(buff))]);
                case 0x8C:
                    return new(pos, first, 4, O.lds, [R.CX, R.SI.Plus(s.NextShort(buff))]);
                case 0x8D:
                    return new(pos, first, 4, O.lds, [R.CX, R.DI.Minus(s.NextShort(buff))]);
                case 0x8E:
                    return new(pos, first, 4, O.lds, [R.CX, R.BP.Plus(s.NextShort(buff))]);
                case 0x8F:
                    return new(pos, first, 4, O.lds, [R.CX, R.BX.Minus(s.NextShort(buff))]);
                case 0x90:
                    return new(pos, first, 4, O.lds, [R.DX, R.BX.Plus(R.SI).Minus(s.NextShort(buff))]);
                case 0x91:
                    return new(pos, first, 4, O.lds, [R.DX, R.BX.Plus(R.DI).Minus(s.NextShort(buff))]);
                case 0x92:
                    return new(pos, first, 4, O.lds, [R.DX, R.BP.Plus(R.SI).Plus(s.NextShort(buff))]);
                case 0x93:
                    return new(pos, first, 4, O.lds, [R.DX, R.BP.Plus(R.DI).Minus(s.NextShort(buff))]);
                case 0x94:
                    return new(pos, first, 4, O.lds, [R.DX, R.SI.Minus(s.NextShort(buff))]);
                case 0x95:
                    return new(pos, first, 4, O.lds, [R.DX, R.DI.Plus(s.NextShort(buff))]);
                case 0x96:
                    return new(pos, first, 4, O.lds, [R.DX, R.BP.Plus(s.NextShort(buff))]);
                case 0x97:
                    return new(pos, first, 4, O.lds, [R.DX, R.BX.Plus(s.NextShort(buff))]);
                case 0x98:
                    return new(pos, first, 4, O.lds, [R.BX, R.BX.Plus(R.SI).Plus(s.NextShort(buff))]);
                case 0x99:
                    return new(pos, first, 4, O.lds, [R.BX, R.BX.Plus(R.DI).Plus(s.NextShort(buff))]);
                case 0x9A:
                    return new(pos, first, 4, O.lds, [R.BX, R.BP.Plus(R.SI).Plus(s.NextShort(buff))]);
                case 0x9B:
                    return new(pos, first, 4, O.lds, [R.BX, R.BP.Plus(R.DI).Minus(s.NextShort(buff))]);
                case 0x9C:
                    return new(pos, first, 4, O.lds, [R.BX, R.SI.Minus(s.NextShort(buff))]);
                case 0x9D:
                    return new(pos, first, 4, O.lds, [R.BX, R.DI.Plus(s.NextShort(buff))]);
                case 0x9E:
                    return new(pos, first, 4, O.lds, [R.BX, R.BP.Minus(s.NextShort(buff))]);
                case 0x9F:
                    return new(pos, first, 4, O.lds, [R.BX, R.BX.Plus(s.NextShort(buff))]);
                case 0xA0:
                    return new(pos, first, 4, O.lds, [R.SP, R.BX.Plus(R.SI).Minus(s.NextShort(buff))]);
                case 0xA1:
                    return new(pos, first, 4, O.lds, [R.SP, R.BX.Plus(R.DI).Plus(s.NextShort(buff))]);
                case 0xA2:
                    return new(pos, first, 4, O.lds, [R.SP, R.BP.Plus(R.SI).Plus(s.NextShort(buff))]);
                case 0xA4:
                    return new(pos, first, 4, O.lds, [R.SP, R.SI.Minus(s.NextShort(buff))]);
                case 0xA5:
                    return new(pos, first, 4, O.lds, [R.SP, R.DI.Plus(s.NextShort(buff))]);
                case 0xA6:
                    return new(pos, first, 4, O.lds, [R.SP, R.BP.Plus(s.NextShort(buff))]);
                case 0xA7:
                    return new(pos, first, 4, O.lds, [R.SP, R.BX.Plus(s.NextShort(buff))]);
                case 0xA8:
                    return new(pos, first, 4, O.lds, [R.BP, R.BX.Plus(R.SI).Minus(s.NextShort(buff))]);
                case 0xA9:
                    return new(pos, first, 4, O.lds, [R.BP, R.BX.Plus(R.DI).Minus(s.NextShort(buff))]);
                case 0xAA:
                    return new(pos, first, 4, O.lds, [R.BP, R.BP.Plus(R.SI).Plus(s.NextShort(buff))]);
                case 0xAB:
                    return new(pos, first, 4, O.lds, [R.BP, R.BP.Plus(R.DI).Plus(s.NextShort(buff))]);
                case 0xAC:
                    return new(pos, first, 4, O.lds, [R.BP, R.SI.Minus(s.NextShort(buff))]);
                case 0xAD:
                    return new(pos, first, 4, O.lds, [R.BP, R.DI.Plus(s.NextShort(buff))]);
                case 0xAE:
                    return new(pos, first, 4, O.lds, [R.BP, R.BP.Minus(s.NextShort(buff))]);
                case 0xAF:
                    return new(pos, first, 4, O.lds, [R.BP, R.BX.Minus(s.NextShort(buff))]);
                case 0xB0:
                    return new(pos, first, 4, O.lds, [R.SI, R.BX.Plus(R.SI).Minus(s.NextShort(buff))]);
                case 0xB1:
                    return new(pos, first, 4, O.lds, [R.SI, R.BX.Plus(R.DI).Minus(s.NextShort(buff))]);
                case 0xB2:
                    return new(pos, first, 4, O.lds, [R.SI, R.BP.Plus(R.SI).Minus(s.NextShort(buff))]);
                case 0xB3:
                    return new(pos, first, 4, O.lds, [R.SI, R.BP.Plus(R.DI).Plus(s.NextShort(buff))]);
                case 0xB4:
                    return new(pos, first, 4, O.lds, [R.SI, R.SI.Plus(s.NextShort(buff))]);
                case 0xB5:
                    return new(pos, first, 4, O.lds, [R.SI, R.DI.Minus(s.NextShort(buff))]);
                case 0xB6:
                    return new(pos, first, 4, O.lds, [R.SI, R.BP.Plus(s.NextShort(buff))]);
                case 0xB7:
                    return new(pos, first, 4, O.lds, [R.SI, R.BX.Plus(s.NextShort(buff))]);
                case 0xB8:
                    return new(pos, first, 4, O.lds, [R.DI, R.BX.Plus(R.SI).Plus(s.NextShort(buff))]);
                case 0xBA:
                    return new(pos, first, 4, O.lds, [R.DI, R.BP.Plus(R.SI).Minus(s.NextShort(buff))]);
                case 0xBB:
                    return new(pos, first, 4, O.lds, [R.DI, R.BP.Plus(R.DI).Plus(s.NextShort(buff))]);
                case 0xBC:
                    return new(pos, first, 4, O.lds, [R.DI, R.SI.Plus(s.NextShort(buff))]);
                case 0xBD:
                    return new(pos, first, 4, O.lds, [R.DI, R.DI.Plus(s.NextShort(buff))]);
                case 0xBE:
                    return new(pos, first, 4, O.lds, [R.DI, R.BP.Plus(s.NextShort(buff))]);
                case 0xBF:
                    return new(pos, first, 4, O.lds, [R.DI, R.BX.Plus(s.NextShort(buff))]);
            }
            return null;
        }
    }
}
