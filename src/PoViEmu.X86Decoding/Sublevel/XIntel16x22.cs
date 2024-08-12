﻿// ReSharper disable InconsistentNaming

using System.IO;
using PoViEmu.Core.Machine.Core;
using PoViEmu.Core.Machine.Decoding;
using O = PoViEmu.Core.Machine.Ops.OpCode;
using R = PoViEmu.Core.Machine.Ops.Register;

namespace PoViEmu.Expert
{
    internal static class Intel16x22
    {
        internal static Instruction? Parse(Stream s, byte[] buff, long pos, byte first)
        {
            var second = s.NextByte(buff);
            switch (second)
            {
                case 0x00:
                    return new(pos, first, 2, O.and, [R.AL, R.BX.Plus(R.SI)], [second]);
                case 0x01:
                    return new(pos, first, 2, O.and, [R.AL, R.BX.Plus(R.DI)], [second]);
                case 0x03:
                    return new(pos, first, 2, O.and, [R.AL, R.BP.Plus(R.DI)], [second]);
                case 0x04:
                    return new(pos, first, 2, O.and, [R.AL, R.SI.Box()], [second]);
                case 0x05:
                    return new(pos, first, 2, O.and, [R.AL, R.DI.Box()], [second]);
                case 0x06:
                    return new(pos, first, 4, O.and, [R.AL, s.NextShort(buff).Box()], [second]);
                case 0x07:
                    return new(pos, first, 2, O.and, [R.AL, R.BX.Box()], [second]);
                case 0x08:
                    return new(pos, first, 2, O.and, [R.CL, R.BX.Plus(R.SI)], [second]);
                case 0x09:
                    return new(pos, first, 2, O.and, [R.CL, R.BX.Plus(R.DI)], [second]);
                case 0x0A:
                    return new(pos, first, 2, O.and, [R.CL, R.BP.Plus(R.SI)], [second]);
                case 0x0B:
                    return new(pos, first, 2, O.and, [R.CL, R.BP.Plus(R.DI)], [second]);
                case 0x0C:
                    return new(pos, first, 2, O.and, [R.CL, R.SI.Box()], [second]);
                case 0x0D:
                    return new(pos, first, 2, O.and, [R.CL, R.DI.Box()], [second]);
                case 0x0E:
                    return new(pos, first, 4, O.and, [R.CL, s.NextShort(buff).Box()], [second]);
                case 0x0F:
                    return new(pos, first, 2, O.and, [R.CL, R.BX.Box()], [second]);
                case 0x11:
                    return new(pos, first, 2, O.and, [R.DL, R.BX.Plus(R.DI)], [second]);
                case 0x13:
                    return new(pos, first, 2, O.and, [R.DL, R.BP.Plus(R.DI)], [second]);
                case 0x14:
                    return new(pos, first, 2, O.and, [R.DL, R.SI.Box()], [second]);
                case 0x15:
                    return new(pos, first, 2, O.and, [R.DL, R.DI.Box()], [second]);
                case 0x16:
                    return new(pos, first, 4, O.and, [R.DL, s.NextShort(buff).Box()], [second]);
                case 0x17:
                    return new(pos, first, 2, O.and, [R.DL, R.BX.Box()], [second]);
                case 0x18:
                    return new(pos, first, 2, O.and, [R.BL, R.BX.Plus(R.SI)], [second]);
                case 0x19:
                    return new(pos, first, 2, O.and, [R.BL, R.BX.Plus(R.DI)], [second]);
                case 0x1A:
                    return new(pos, first, 2, O.and, [R.BL, R.BP.Plus(R.SI)], [second]);
                case 0x1B:
                    return new(pos, first, 2, O.and, [R.BL, R.BP.Plus(R.DI)], [second]);
                case 0x1C:
                    return new(pos, first, 2, O.and, [R.BL, R.SI.Box()], [second]);
                case 0x1E:
                    return new(pos, first, 4, O.and, [R.BL, s.NextShort(buff).Box()], [second]);
                case 0x1F:
                    return new(pos, first, 2, O.and, [R.BL, R.BX.Box()], [second]);
                case 0x20:
                    return new(pos, first, 2, O.and, [R.AH, R.BX.Plus(R.SI)], [second]);
                case 0x21:
                    return new(pos, first, 2, O.and, [R.AH, R.BX.Plus(R.DI)], [second]);
                case 0x22:
                    return new(pos, first, 2, O.and, [R.AH, R.BP.Plus(R.SI)], [second]);
                case 0x23:
                    return new(pos, first, 2, O.and, [R.AH, R.BP.Plus(R.DI)], [second]);
                case 0x25:
                    return new(pos, first, 2, O.and, [R.AH, R.DI.Box()], [second]);
                case 0x26:
                    return new(pos, first, 4, O.and, [R.AH, s.NextShort(buff).Box()], [second]);
                case 0x27:
                    return new(pos, first, 2, O.and, [R.AH, R.BX.Box()], [second]);
                case 0x28:
                    return new(pos, first, 2, O.and, [R.CH, R.BX.Plus(R.SI)], [second]);
                case 0x29:
                    return new(pos, first, 2, O.and, [R.CH, R.BX.Plus(R.DI)], [second]);
                case 0x2C:
                    return new(pos, first, 2, O.and, [R.CH, R.SI.Box()], [second]);
                case 0x2D:
                    return new(pos, first, 2, O.and, [R.CH, R.DI.Box()], [second]);
                case 0x2E:
                    return new(pos, first, 4, O.and, [R.CH, s.NextShort(buff).Box()], [second]);
                case 0x2F:
                    return new(pos, first, 2, O.and, [R.CH, R.BX.Box()], [second]);
                case 0x30:
                    return new(pos, first, 2, O.and, [R.DH, R.BX.Plus(R.SI)], [second]);
                case 0x31:
                    return new(pos, first, 2, O.and, [R.DH, R.BX.Plus(R.DI)], [second]);
                case 0x32:
                    return new(pos, first, 2, O.and, [R.DH, R.BP.Plus(R.SI)], [second]);
                case 0x33:
                    return new(pos, first, 2, O.and, [R.DH, R.BP.Plus(R.DI)], [second]);
                case 0x34:
                    return new(pos, first, 2, O.and, [R.DH, R.SI.Box()], [second]);
                case 0x35:
                    return new(pos, first, 2, O.and, [R.DH, R.DI.Box()], [second]);
                case 0x36:
                    return new(pos, first, 4, O.and, [R.DH, s.NextShort(buff).Box()], [second]);
                case 0x37:
                    return new(pos, first, 2, O.and, [R.DH, R.BX.Box()], [second]);
                case 0x38:
                    return new(pos, first, 2, O.and, [R.BH, R.BX.Plus(R.SI)], [second]);
                case 0x39:
                    return new(pos, first, 2, O.and, [R.BH, R.BX.Plus(R.DI)], [second]);
                case 0x3A:
                    return new(pos, first, 2, O.and, [R.BH, R.BP.Plus(R.SI)], [second]);
                case 0x3B:
                    return new(pos, first, 2, O.and, [R.BH, R.BP.Plus(R.DI)], [second]);
                case 0x3E:
                    return new(pos, first, 4, O.and, [R.BH, s.NextShort(buff).Box()], [second]);
                case 0x3F:
                    return new(pos, first, 2, O.and, [R.BH, R.BX.Box()], [second]);
                case 0x40:
                    return new(pos, first, 3, O.and, [R.AL, R.BX.Plus(R.SI).Minus(s.NextByte(buff))], [second]);
                case 0x41:
                    return new(pos, first, 3, O.and, [R.AL, R.BX.Plus(R.DI).Signed(s.NextByte(buff))], [second]);
                case 0x42:
                    return new(pos, first, 3, O.and, [R.AL, R.BP.Plus(R.SI).Signed(s.NextByte(buff))], [second]);
                case 0x43:
                    return new(pos, first, 3, O.and, [R.AL, R.BP.Plus(R.DI).Signed(s.NextByte(buff))], [second]);
                case 0x44:
                    return new(pos, first, 3, O.and, [R.AL, R.SI.Minus(s.NextByte(buff))], [second]);
                case 0x45:
                    return new(pos, first, 3, O.and, [R.AL, R.DI.Signed(s.NextByte(buff))], [second]);
                case 0x46:
                    return new(pos, first, 3, O.and, [R.AL, R.BP.Signed(s.NextByte(buff))], [second]);
                case 0x47:
                    return new(pos, first, 3, O.and, [R.AL, R.BX.Signed(s.NextByte(buff))], [second]);
                case 0x4A:
                    return new(pos, first, 3, O.and, [R.CL, R.BP.Plus(R.SI).Signed(s.NextByte(buff))], [second]);
                case 0x4B:
                    return new(pos, first, 3, O.and, [R.CL, R.BP.Plus(R.DI).Signed(s.NextByte(buff))], [second]);
                case 0x4C:
                    return new(pos, first, 3, O.and, [R.CL, R.SI.Signed(s.NextByte(buff))], [second]);
                case 0x4D:
                    return new(pos, first, 3, O.and, [R.CL, R.DI.Signed(s.NextByte(buff))], [second]);
                case 0x4E:
                    return new(pos, first, 3, O.and, [R.CL, R.BP.Signed(s.NextByte(buff))], [second]);
                case 0x4F:
                    return new(pos, first, 3, O.and, [R.CL, R.BX.Signed(s.NextByte(buff))], [second]);
                case 0x50:
                    return new(pos, first, 3, O.and, [R.DL, R.BX.Plus(R.SI).Signed(s.NextByte(buff))], [second]);
                case 0x51:
                    return new(pos, first, 3, O.and, [R.DL, R.BX.Plus(R.DI).Minus(s.NextByte(buff))], [second]);
                case 0x52:
                    return new(pos, first, 3, O.and, [R.DL, R.BP.Plus(R.SI).Signed(s.NextByte(buff))], [second]);
                case 0x53:
                    return new(pos, first, 3, O.and, [R.DL, R.BP.Plus(R.DI).Minus(s.NextByte(buff))], [second]);
                case 0x54:
                    return new(pos, first, 3, O.and, [R.DL, R.SI.Signed(s.NextByte(buff))], [second]);
                case 0x55:
                    return new(pos, first, 3, O.and, [R.DL, R.DI.Signed(s.NextByte(buff))], [second]);
                case 0x56:
                    return new(pos, first, 3, O.and, [R.DL, R.BP.Minus(s.NextByte(buff))], [second]);
                case 0x58:
                    return new(pos, first, 3, O.and, [R.BL, R.BX.Plus(R.SI).Signed(s.NextByte(buff))], [second]);
                case 0x59:
                    return new(pos, first, 3, O.and, [R.BL, R.BX.Plus(R.DI).Minus(s.NextByte(buff))], [second]);
                case 0x5A:
                    return new(pos, first, 3, O.and, [R.BL, R.BP.Plus(R.SI).Signed(s.NextByte(buff))], [second]);
                case 0x5B:
                    return new(pos, first, 3, O.and, [R.BL, R.BP.Plus(R.DI).Signed(s.NextByte(buff))], [second]);
                case 0x5C:
                    return new(pos, first, 3, O.and, [R.BL, R.SI.Minus(s.NextByte(buff))], [second]);
                case 0x5D:
                    return new(pos, first, 3, O.and, [R.BL, R.DI.Signed(s.NextByte(buff))], [second]);
                case 0x5E:
                    return new(pos, first, 3, O.and, [R.BL, R.BP.Minus(s.NextByte(buff))], [second]);
                case 0x5F:
                    return new(pos, first, 3, O.and, [R.BL, R.BX.Minus(s.NextByte(buff))], [second]);
                case 0x60:
                    return new(pos, first, 3, O.and, [R.AH, R.BX.Plus(R.SI).Signed(s.NextByte(buff))], [second]);
                case 0x61:
                    return new(pos, first, 3, O.and, [R.AH, R.BX.Plus(R.DI).Signed(s.NextByte(buff))], [second]);
                case 0x62:
                    return new(pos, first, 3, O.and, [R.AH, R.BP.Plus(R.SI).Signed(s.NextByte(buff))], [second]);
                case 0x63:
                    return new(pos, first, 3, O.and, [R.AH, R.BP.Plus(R.DI).Signed(s.NextByte(buff))], [second]);
                case 0x64:
                    return new(pos, first, 3, O.and, [R.AH, R.SI.Minus(s.NextByte(buff))], [second]);
                case 0x65:
                    return new(pos, first, 3, O.and, [R.AH, R.DI.Signed(s.NextByte(buff))], [second]);
                case 0x67:
                    return new(pos, first, 3, O.and, [R.AH, R.BX.Signed(s.NextByte(buff))], [second]);
                case 0x68:
                    return new(pos, first, 3, O.and, [R.CH, R.BX.Plus(R.SI).Signed(s.NextByte(buff))], [second]);
                case 0x69:
                    return new(pos, first, 3, O.and, [R.CH, R.BX.Plus(R.DI).Minus(s.NextByte(buff))], [second]);
                case 0x6A:
                    return new(pos, first, 3, O.and, [R.CH, R.BP.Plus(R.SI).Signed(s.NextByte(buff))], [second]);
                case 0x6B:
                    return new(pos, first, 3, O.and, [R.CH, R.BP.Plus(R.DI).Minus(s.NextByte(buff))], [second]);
                case 0x6C:
                    return new(pos, first, 3, O.and, [R.CH, R.SI.Signed(s.NextByte(buff))], [second]);
                case 0x6D:
                    return new(pos, first, 3, O.and, [R.CH, R.DI.Signed(s.NextByte(buff))], [second]);
                case 0x6E:
                    return new(pos, first, 3, O.and, [R.CH, R.BP.Signed(s.NextByte(buff))], [second]);
                case 0x6F:
                    return new(pos, first, 3, O.and, [R.CH, R.BX.Signed(s.NextByte(buff))], [second]);
                case 0x70:
                    return new(pos, first, 3, O.and, [R.DH, R.BX.Plus(R.SI).Signed(s.NextByte(buff))], [second]);
                case 0x71:
                    return new(pos, first, 3, O.and, [R.DH, R.BX.Plus(R.DI).Signed(s.NextByte(buff))], [second]);
                case 0x72:
                    return new(pos, first, 3, O.and, [R.DH, R.BP.Plus(R.SI).Minus(s.NextByte(buff))], [second]);
                case 0x73:
                    return new(pos, first, 3, O.and, [R.DH, R.BP.Plus(R.DI).Minus(s.NextByte(buff))], [second]);
                case 0x74:
                    return new(pos, first, 3, O.and, [R.DH, R.SI.Signed(s.NextByte(buff))], [second]);
                case 0x75:
                    return new(pos, first, 3, O.and, [R.DH, R.DI.Minus(s.NextByte(buff))], [second]);
                case 0x78:
                    return new(pos, first, 3, O.and, [R.BH, R.BX.Plus(R.SI).Signed(s.NextByte(buff))], [second]);
                case 0x79:
                    return new(pos, first, 3, O.and, [R.BH, R.BX.Plus(R.DI).Signed(s.NextByte(buff))], [second]);
                case 0x7A:
                    return new(pos, first, 3, O.and, [R.BH, R.BP.Plus(R.SI).Minus(s.NextByte(buff))], [second]);
                case 0x7B:
                    return new(pos, first, 3, O.and, [R.BH, R.BP.Plus(R.DI).Signed(s.NextByte(buff))], [second]);
                case 0x7C:
                    return new(pos, first, 3, O.and, [R.BH, R.SI.Signed(s.NextByte(buff))], [second]);
                case 0x7D:
                    return new(pos, first, 3, O.and, [R.BH, R.DI.Signed(s.NextByte(buff))], [second]);
                case 0x80:
                    return new(pos, first, 4, O.and, [R.AL, R.BX.Plus(R.SI).Signed(s.NextShort(buff))], [second]);
                case 0x81:
                    return new(pos, first, 4, O.and, [R.AL, R.BX.Plus(R.DI).Signed(s.NextShort(buff))], [second]);
                case 0x83:
                    return new(pos, first, 4, O.and, [R.AL, R.BP.Plus(R.DI).Signed(s.NextShort(buff))], [second]);
                case 0x84:
                    return new(pos, first, 4, O.and, [R.AL, R.SI.Signed(s.NextShort(buff))], [second]);
                case 0x85:
                    return new(pos, first, 4, O.and, [R.AL, R.DI.Signed(s.NextShort(buff))], [second]);
                case 0x86:
                    return new(pos, first, 4, O.and, [R.AL, R.BP.Signed(s.NextShort(buff))], [second]);
                case 0x87:
                    return new(pos, first, 4, O.and, [R.AL, R.BX.Signed(s.NextShort(buff))], [second]);
                case 0x88:
                    return new(pos, first, 4, O.and, [R.CL, R.BX.Plus(R.SI).Signed(s.NextShort(buff))], [second]);
                case 0x89:
                    return new(pos, first, 4, O.and, [R.CL, R.BX.Plus(R.DI).Signed(s.NextShort(buff))], [second]);
                case 0x8A:
                    return new(pos, first, 4, O.and, [R.CL, R.BP.Plus(R.SI).Signed(s.NextShort(buff))], [second]);
                case 0x8B:
                    return new(pos, first, 4, O.and, [R.CL, R.BP.Plus(R.DI).Signed(s.NextShort(buff))], [second]);
                case 0x8C:
                    return new(pos, first, 4, O.and, [R.CL, R.SI.Signed(s.NextShort(buff))], [second]);
                case 0x8D:
                    return new(pos, first, 4, O.and, [R.CL, R.DI.Signed(s.NextShort(buff))], [second]);
                case 0x8E:
                    return new(pos, first, 4, O.and, [R.CL, R.BP.Signed(s.NextShort(buff))], [second]);
                case 0x8F:
                    return new(pos, first, 4, O.and, [R.CL, R.BX.Signed(s.NextShort(buff))], [second]);
                case 0x90:
                    return new(pos, first, 4, O.and, [R.DL, R.BX.Plus(R.SI).Signed(s.NextShort(buff))], [second]);
                case 0x91:
                    return new(pos, first, 4, O.and, [R.DL, R.BX.Plus(R.DI).Signed(s.NextShort(buff))], [second]);
                case 0x93:
                    return new(pos, first, 4, O.and, [R.DL, R.BP.Plus(R.DI).Signed(s.NextShort(buff))], [second]);
                case 0x94:
                    return new(pos, first, 4, O.and, [R.DL, R.SI.Signed(s.NextShort(buff))], [second]);
                case 0x95:
                    return new(pos, first, 4, O.and, [R.DL, R.DI.Signed(s.NextShort(buff))], [second]);
                case 0x96:
                    return new(pos, first, 4, O.and, [R.DL, R.BP.Signed(s.NextShort(buff))], [second]);
                case 0x97:
                    return new(pos, first, 4, O.and, [R.DL, R.BX.Signed(s.NextShort(buff))], [second]);
                case 0x98:
                    return new(pos, first, 4, O.and, [R.BL, R.BX.Plus(R.SI).Signed(s.NextShort(buff))], [second]);
                case 0x99:
                    return new(pos, first, 4, O.and, [R.BL, R.BX.Plus(R.DI).Signed(s.NextShort(buff))], [second]);
                case 0x9A:
                    return new(pos, first, 4, O.and, [R.BL, R.BP.Plus(R.SI).Signed(s.NextShort(buff))], [second]);
                case 0x9B:
                    return new(pos, first, 4, O.and, [R.BL, R.BP.Plus(R.DI).Signed(s.NextShort(buff))], [second]);
                case 0x9C:
                    return new(pos, first, 4, O.and, [R.BL, R.SI.Signed(s.NextShort(buff))], [second]);
                case 0x9D:
                    return new(pos, first, 4, O.and, [R.BL, R.DI.Signed(s.NextShort(buff))], [second]);
                case 0x9E:
                    return new(pos, first, 4, O.and, [R.BL, R.BP.Signed(s.NextShort(buff))], [second]);
                case 0xA0:
                    return new(pos, first, 4, O.and, [R.AH, R.BX.Plus(R.SI).Signed(s.NextShort(buff))], [second]);
                case 0xA1:
                    return new(pos, first, 4, O.and, [R.AH, R.BX.Plus(R.DI).Signed(s.NextShort(buff))], [second]);
                case 0xA2:
                    return new(pos, first, 4, O.and, [R.AH, R.BP.Plus(R.SI).Signed(s.NextShort(buff))], [second]);
                case 0xA3:
                    return new(pos, first, 4, O.and, [R.AH, R.BP.Plus(R.DI).Signed(s.NextShort(buff))], [second]);
                case 0xA4:
                    return new(pos, first, 4, O.and, [R.AH, R.SI.Signed(s.NextShort(buff))], [second]);
                case 0xA5:
                    return new(pos, first, 4, O.and, [R.AH, R.DI.Signed(s.NextShort(buff))], [second]);
                case 0xA8:
                    return new(pos, first, 4, O.and, [R.CH, R.BX.Plus(R.SI).Signed(s.NextShort(buff))], [second]);
                case 0xA9:
                    return new(pos, first, 4, O.and, [R.CH, R.BX.Plus(R.DI).Signed(s.NextShort(buff))], [second]);
                case 0xAA:
                    return new(pos, first, 4, O.and, [R.CH, R.BP.Plus(R.SI).Signed(s.NextShort(buff))], [second]);
                case 0xAB:
                    return new(pos, first, 4, O.and, [R.CH, R.BP.Plus(R.DI).Signed(s.NextShort(buff))], [second]);
                case 0xAC:
                    return new(pos, first, 4, O.and, [R.CH, R.SI.Signed(s.NextShort(buff))], [second]);
                case 0xAD:
                    return new(pos, first, 4, O.and, [R.CH, R.DI.Signed(s.NextShort(buff))], [second]);
                case 0xAE:
                    return new(pos, first, 4, O.and, [R.CH, R.BP.Signed(s.NextShort(buff))], [second]);
                case 0xAF:
                    return new(pos, first, 4, O.and, [R.CH, R.BX.Signed(s.NextShort(buff))], [second]);
                case 0xB0:
                    return new(pos, first, 4, O.and, [R.DH, R.BX.Plus(R.SI).Signed(s.NextShort(buff))], [second]);
                case 0xB1:
                    return new(pos, first, 4, O.and, [R.DH, R.BX.Plus(R.DI).Signed(s.NextShort(buff))], [second]);
                case 0xB2:
                    return new(pos, first, 4, O.and, [R.DH, R.BP.Plus(R.SI).Signed(s.NextShort(buff))], [second]);
                case 0xB4:
                    return new(pos, first, 4, O.and, [R.DH, R.SI.Signed(s.NextShort(buff))], [second]);
                case 0xB5:
                    return new(pos, first, 4, O.and, [R.DH, R.DI.Signed(s.NextShort(buff))], [second]);
                case 0xB6:
                    return new(pos, first, 4, O.and, [R.DH, R.BP.Signed(s.NextShort(buff))], [second]);
                case 0xB8:
                    return new(pos, first, 4, O.and, [R.BH, R.BX.Plus(R.SI).Signed(s.NextShort(buff))], [second]);
                case 0xB9:
                    return new(pos, first, 4, O.and, [R.BH, R.BX.Plus(R.DI).Signed(s.NextShort(buff))], [second]);
                case 0xBA:
                    return new(pos, first, 4, O.and, [R.BH, R.BP.Plus(R.SI).Signed(s.NextShort(buff))], [second]);
                case 0xBB:
                    return new(pos, first, 4, O.and, [R.BH, R.BP.Plus(R.DI).Signed(s.NextShort(buff))], [second]);
                case 0xBC:
                    return new(pos, first, 4, O.and, [R.BH, R.SI.Signed(s.NextShort(buff))], [second]);
                case 0xBE:
                    return new(pos, first, 4, O.and, [R.BH, R.BP.Signed(s.NextShort(buff))], [second]);
                case 0xBF:
                    return new(pos, first, 4, O.and, [R.BH, R.BX.Signed(s.NextShort(buff))], [second]);
                case 0xC1:
                    return new(pos, first, 2, O.and, [R.AL, R.CL], [second]);
                case 0xC2:
                    return new(pos, first, 2, O.and, [R.AL, R.DL], [second]);
                case 0xC5:
                    return new(pos, first, 2, O.and, [R.AL, R.CH], [second]);
                case 0xC6:
                    return new(pos, first, 2, O.and, [R.AL, R.DH], [second]);
                case 0xC7:
                    return new(pos, first, 2, O.and, [R.AL, R.BH], [second]);
                case 0xC8:
                    return new(pos, first, 2, O.and, [R.CL, R.AL], [second]);
                case 0xC9:
                    return new(pos, first, 2, O.and, [R.CL, R.CL], [second]);
                case 0xCB:
                    return new(pos, first, 2, O.and, [R.CL, R.BL], [second]);
                case 0xCC:
                    return new(pos, first, 2, O.and, [R.CL, R.AH], [second]);
                case 0xCD:
                    return new(pos, first, 2, O.and, [R.CL, R.CH], [second]);
                case 0xCE:
                    return new(pos, first, 2, O.and, [R.CL, R.DH], [second]);
                case 0xCF:
                    return new(pos, first, 2, O.and, [R.CL, R.BH], [second]);
                case 0xD1:
                    return new(pos, first, 2, O.and, [R.DL, R.CL], [second]);
                case 0xD2:
                    return new(pos, first, 2, O.and, [R.DL, R.DL], [second]);
                case 0xD3:
                    return new(pos, first, 2, O.and, [R.DL, R.BL], [second]);
                case 0xD4:
                    return new(pos, first, 2, O.and, [R.DL, R.AH], [second]);
                case 0xD6:
                    return new(pos, first, 2, O.and, [R.DL, R.DH], [second]);
                case 0xD7:
                    return new(pos, first, 2, O.and, [R.DL, R.BH], [second]);
                case 0xD8:
                    return new(pos, first, 2, O.and, [R.BL, R.AL], [second]);
                case 0xD9:
                    return new(pos, first, 2, O.and, [R.BL, R.CL], [second]);
                case 0xDA:
                    return new(pos, first, 2, O.and, [R.BL, R.DL], [second]);
                case 0xDB:
                    return new(pos, first, 2, O.and, [R.BL, R.BL], [second]);
                case 0xDC:
                    return new(pos, first, 2, O.and, [R.BL, R.AH], [second]);
                case 0xDD:
                    return new(pos, first, 2, O.and, [R.BL, R.CH], [second]);
                case 0xDE:
                    return new(pos, first, 2, O.and, [R.BL, R.DH], [second]);
                case 0xDF:
                    return new(pos, first, 2, O.and, [R.BL, R.BH], [second]);
                case 0xE1:
                    return new(pos, first, 2, O.and, [R.AH, R.CL], [second]);
                case 0xE2:
                    return new(pos, first, 2, O.and, [R.AH, R.DL], [second]);
                case 0xE3:
                    return new(pos, first, 2, O.and, [R.AH, R.BL], [second]);
                case 0xE4:
                    return new(pos, first, 2, O.and, [R.AH, R.AH], [second]);
                case 0xE5:
                    return new(pos, first, 2, O.and, [R.AH, R.CH], [second]);
                case 0xE6:
                    return new(pos, first, 2, O.and, [R.AH, R.DH], [second]);
                case 0xE7:
                    return new(pos, first, 2, O.and, [R.AH, R.BH], [second]);
                case 0xE8:
                    return new(pos, first, 2, O.and, [R.CH, R.AL], [second]);
                case 0xE9:
                    return new(pos, first, 2, O.and, [R.CH, R.CL], [second]);
                case 0xEA:
                    return new(pos, first, 2, O.and, [R.CH, R.DL], [second]);
                case 0xEB:
                    return new(pos, first, 2, O.and, [R.CH, R.BL], [second]);
                case 0xED:
                    return new(pos, first, 2, O.and, [R.CH, R.CH], [second]);
                case 0xEE:
                    return new(pos, first, 2, O.and, [R.CH, R.DH], [second]);
                case 0xEF:
                    return new(pos, first, 2, O.and, [R.CH, R.BH], [second]);
                case 0xF0:
                    return new(pos, first, 2, O.and, [R.DH, R.AL], [second]);
                case 0xF1:
                    return new(pos, first, 2, O.and, [R.DH, R.CL], [second]);
                case 0xF2:
                    return new(pos, first, 2, O.and, [R.DH, R.DL], [second]);
                case 0xF3:
                    return new(pos, first, 2, O.and, [R.DH, R.BL], [second]);
                case 0xF6:
                    return new(pos, first, 2, O.and, [R.DH, R.DH], [second]);
                case 0xF7:
                    return new(pos, first, 2, O.and, [R.DH, R.BH], [second]);
                case 0xF8:
                    return new(pos, first, 2, O.and, [R.BH, R.AL], [second]);
                case 0xF9:
                    return new(pos, first, 2, O.and, [R.BH, R.CL], [second]);
                case 0xFA:
                    return new(pos, first, 2, O.and, [R.BH, R.DL], [second]);
                case 0xFB:
                    return new(pos, first, 2, O.and, [R.BH, R.BL], [second]);
                case 0xFC:
                    return new(pos, first, 2, O.and, [R.BH, R.AH], [second]);
                case 0xFD:
                    return new(pos, first, 2, O.and, [R.BH, R.CH], [second]);
                case 0xFE:
                    return new(pos, first, 2, O.and, [R.BH, R.DH], [second]);
                case 0xFF:
                    return new(pos, first, 2, O.and, [R.BH, R.BH], [second]);
            }
            return null;
        }
    }
}