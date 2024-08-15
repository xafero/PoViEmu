﻿// ReSharper disable InconsistentNaming

using System.IO;
using PoViEmu.X86Decoding.Core;
using PoViEmu.X86Decoding.Tools;
using O = PoViEmu.X86Decoding.Ops.OpCode;
using R = PoViEmu.X86Decoding.Ops.Register;

namespace PoViEmu.X86Decoding.Sublevel
{
    internal static class Intel16x21
    {
        internal static Instruction? Parse(Stream s, byte[] buff, long pos, byte first)
        {
            var second = s.NextByte(buff);
            switch (second)
            {
                case 0x00:
                    return new(pos, first, 2, O.and, [R.BX.Plus(R.SI), R.AX], [second]);
                case 0x01:
                    return new(pos, first, 2, O.and, [R.BX.Plus(R.DI), R.AX], [second]);
                case 0x02:
                    return new(pos, first, 2, O.and, [R.BP.Plus(R.SI), R.AX], [second]);
                case 0x03:
                    return new(pos, first, 2, O.and, [R.BP.Plus(R.DI), R.AX], [second]);
                case 0x04:
                    return new(pos, first, 2, O.and, [R.SI.Box(), R.AX], [second]);
                case 0x05:
                    return new(pos, first, 2, O.and, [R.DI.Box(), R.AX], [second]);
                case 0x06:
                    return new(pos, first, 4, O.and, [s.NextShort(buff).Box(), R.AX], [second]);
                case 0x07:
                    return new(pos, first, 2, O.and, [R.BX.Box(), R.AX], [second]);
                case 0x08:
                    return new(pos, first, 2, O.and, [R.BX.Plus(R.SI), R.CX], [second]);
                case 0x09:
                    return new(pos, first, 2, O.and, [R.BX.Plus(R.DI), R.CX], [second]);
                case 0x0A:
                    return new(pos, first, 2, O.and, [R.BP.Plus(R.SI), R.CX], [second]);
                case 0x0B:
                    return new(pos, first, 2, O.and, [R.BP.Plus(R.DI), R.CX], [second]);
                case 0x0C:
                    return new(pos, first, 2, O.and, [R.SI.Box(), R.CX], [second]);
                case 0x0D:
                    return new(pos, first, 2, O.and, [R.DI.Box(), R.CX], [second]);
                case 0x0E:
                    return new(pos, first, 4, O.and, [s.NextShort(buff).Box(), R.CX], [second]);
                case 0x0F:
                    return new(pos, first, 2, O.and, [R.BX.Box(), R.CX], [second]);
                case 0x10:
                    return new(pos, first, 2, O.and, [R.BX.Plus(R.SI), R.DX], [second]);
                case 0x11:
                    return new(pos, first, 2, O.and, [R.BX.Plus(R.DI), R.DX], [second]);
                case 0x12:
                    return new(pos, first, 2, O.and, [R.BP.Plus(R.SI), R.DX], [second]);
                case 0x13:
                    return new(pos, first, 2, O.and, [R.BP.Plus(R.DI), R.DX], [second]);
                case 0x14:
                    return new(pos, first, 2, O.and, [R.SI.Box(), R.DX], [second]);
                case 0x15:
                    return new(pos, first, 2, O.and, [R.DI.Box(), R.DX], [second]);
                case 0x17:
                    return new(pos, first, 2, O.and, [R.BX.Box(), R.DX], [second]);
                case 0x18:
                    return new(pos, first, 2, O.and, [R.BX.Plus(R.SI), R.BX], [second]);
                case 0x19:
                    return new(pos, first, 2, O.and, [R.BX.Plus(R.DI), R.BX], [second]);
                case 0x1A:
                    return new(pos, first, 2, O.and, [R.BP.Plus(R.SI), R.BX], [second]);
                case 0x1B:
                    return new(pos, first, 2, O.and, [R.BP.Plus(R.DI), R.BX], [second]);
                case 0x1C:
                    return new(pos, first, 2, O.and, [R.SI.Box(), R.BX], [second]);
                case 0x1D:
                    return new(pos, first, 2, O.and, [R.DI.Box(), R.BX], [second]);
                case 0x1E:
                    return new(pos, first, 4, O.and, [s.NextShort(buff).Box(), R.BX], [second]);
                case 0x1F:
                    return new(pos, first, 2, O.and, [R.BX.Box(), R.BX], [second]);
                case 0x20:
                    return new(pos, first, 2, O.and, [R.BX.Plus(R.SI), R.SP], [second]);
                case 0x21:
                    return new(pos, first, 2, O.and, [R.BX.Plus(R.DI), R.SP], [second]);
                case 0x22:
                    return new(pos, first, 2, O.and, [R.BP.Plus(R.SI), R.SP], [second]);
                case 0x23:
                    return new(pos, first, 2, O.and, [R.BP.Plus(R.DI), R.SP], [second]);
                case 0x24:
                    return new(pos, first, 2, O.and, [R.SI.Box(), R.SP], [second]);
                case 0x26:
                    return new(pos, first, 4, O.and, [s.NextShort(buff).Box(), R.SP], [second]);
                case 0x27:
                    return new(pos, first, 2, O.and, [R.BX.Box(), R.SP], [second]);
                case 0x28:
                    return new(pos, first, 2, O.and, [R.BX.Plus(R.SI), R.BP], [second]);
                case 0x29:
                    return new(pos, first, 2, O.and, [R.BX.Plus(R.DI), R.BP], [second]);
                case 0x2A:
                    return new(pos, first, 2, O.and, [R.BP.Plus(R.SI), R.BP], [second]);
                case 0x2B:
                    return new(pos, first, 2, O.and, [R.BP.Plus(R.DI), R.BP], [second]);
                case 0x2C:
                    return new(pos, first, 2, O.and, [R.SI.Box(), R.BP], [second]);
                case 0x2D:
                    return new(pos, first, 2, O.and, [R.DI.Box(), R.BP], [second]);
                case 0x2E:
                    return new(pos, first, 4, O.and, [s.NextShort(buff).Box(), R.BP], [second]);
                case 0x2F:
                    return new(pos, first, 2, O.and, [R.BX.Box(), R.BP], [second]);
                case 0x30:
                    return new(pos, first, 2, O.and, [R.BX.Plus(R.SI), R.SI], [second]);
                case 0x32:
                    return new(pos, first, 2, O.and, [R.BP.Plus(R.SI), R.SI], [second]);
                case 0x33:
                    return new(pos, first, 2, O.and, [R.BP.Plus(R.DI), R.SI], [second]);
                case 0x35:
                    return new(pos, first, 2, O.and, [R.DI.Box(), R.SI], [second]);
                case 0x36:
                    return new(pos, first, 4, O.and, [s.NextShort(buff).Box(), R.SI], [second]);
                case 0x37:
                    return new(pos, first, 2, O.and, [R.BX.Box(), R.SI], [second]);
                case 0x38:
                    return new(pos, first, 2, O.and, [R.BX.Plus(R.SI), R.DI], [second]);
                case 0x39:
                    return new(pos, first, 2, O.and, [R.BX.Plus(R.DI), R.DI], [second]);
                case 0x3A:
                    return new(pos, first, 2, O.and, [R.BP.Plus(R.SI), R.DI], [second]);
                case 0x3B:
                    return new(pos, first, 2, O.and, [R.BP.Plus(R.DI), R.DI], [second]);
                case 0x3C:
                    return new(pos, first, 2, O.and, [R.SI.Box(), R.DI], [second]);
                case 0x3E:
                    return new(pos, first, 4, O.and, [s.NextShort(buff).Box(), R.DI], [second]);
                case 0x3F:
                    return new(pos, first, 2, O.and, [R.BX.Box(), R.DI], [second]);
                case 0x40:
                    return new(pos, first, 3, O.and, [R.BX.Plus(R.SI).Signed(s.NextByte(buff)), R.AX], [second]);
                case 0x41:
                    return new(pos, first, 3, O.and, [R.BX.Plus(R.DI).Signed(s.NextByte(buff)), R.AX], [second]);
                case 0x42:
                    return new(pos, first, 3, O.and, [R.BP.Plus(R.SI).Signed(s.NextByte(buff)), R.AX], [second]);
                case 0x43:
                    return new(pos, first, 3, O.and, [R.BP.Plus(R.DI).Signed(s.NextByte(buff)), R.AX], [second]);
                case 0x44:
                    return new(pos, first, 3, O.and, [R.SI.Signed(s.NextByte(buff)), R.AX], [second]);
                case 0x45:
                    return new(pos, first, 3, O.and, [R.DI.Signed(s.NextByte(buff)), R.AX], [second]);
                case 0x46:
                    return new(pos, first, 3, O.and, [R.BP.Signed(s.NextByte(buff)), R.AX], [second]);
                case 0x47:
                    return new(pos, first, 3, O.and, [R.BX.Signed(s.NextByte(buff)), R.AX], [second]);
                case 0x48:
                    return new(pos, first, 3, O.and, [R.BX.Plus(R.SI).Signed(s.NextByte(buff)), R.CX], [second]);
                case 0x49:
                    return new(pos, first, 3, O.and, [R.BX.Plus(R.DI).Signed(s.NextByte(buff)), R.CX], [second]);
                case 0x4A:
                    return new(pos, first, 3, O.and, [R.BP.Plus(R.SI).Signed(s.NextByte(buff)), R.CX], [second]);
                case 0x4B:
                    return new(pos, first, 3, O.and, [R.BP.Plus(R.DI).Signed(s.NextByte(buff)), R.CX], [second]);
                case 0x4D:
                    return new(pos, first, 3, O.and, [R.DI.Signed(s.NextByte(buff)), R.CX], [second]);
                case 0x4E:
                    return new(pos, first, 3, O.and, [R.BP.Signed(s.NextByte(buff)), R.CX], [second]);
                case 0x4F:
                    return new(pos, first, 3, O.and, [R.BX.Minus(s.NextByte(buff)), R.CX], [second]);
                case 0x50:
                    return new(pos, first, 3, O.and, [R.BX.Plus(R.SI).Signed(s.NextByte(buff)), R.DX], [second]);
                case 0x51:
                    return new(pos, first, 3, O.and, [R.BX.Plus(R.DI).Signed(s.NextByte(buff)), R.DX], [second]);
                case 0x52:
                    return new(pos, first, 3, O.and, [R.BP.Plus(R.SI).Minus(s.NextByte(buff)), R.DX], [second]);
                case 0x53:
                    return new(pos, first, 3, O.and, [R.BP.Plus(R.DI).Signed(s.NextByte(buff)), R.DX], [second]);
                case 0x54:
                    return new(pos, first, 3, O.and, [R.SI.Signed(s.NextByte(buff)), R.DX], [second]);
                case 0x55:
                    return new(pos, first, 3, O.and, [R.DI.Signed(s.NextByte(buff)), R.DX], [second]);
                case 0x56:
                    return new(pos, first, 3, O.and, [R.BP.Signed(s.NextByte(buff)), R.DX], [second]);
                case 0x57:
                    return new(pos, first, 3, O.and, [R.BX.Signed(s.NextByte(buff)), R.DX], [second]);
                case 0x58:
                    return new(pos, first, 3, O.and, [R.BX.Plus(R.SI).Signed(s.NextByte(buff)), R.BX], [second]);
                case 0x59:
                    return new(pos, first, 3, O.and, [R.BX.Plus(R.DI).Signed(s.NextByte(buff)), R.BX], [second]);
                case 0x5A:
                    return new(pos, first, 3, O.and, [R.BP.Plus(R.SI).Signed(s.NextByte(buff)), R.BX], [second]);
                case 0x5B:
                    return new(pos, first, 3, O.and, [R.BP.Plus(R.DI).Signed(s.NextByte(buff)), R.BX], [second]);
                case 0x5C:
                    return new(pos, first, 3, O.and, [R.SI.Signed(s.NextByte(buff)), R.BX], [second]);
                case 0x5D:
                    return new(pos, first, 3, O.and, [R.DI.Signed(s.NextByte(buff)), R.BX], [second]);
                case 0x5E:
                    return new(pos, first, 3, O.and, [R.BP.Signed(s.NextByte(buff)), R.BX], [second]);
                case 0x5F:
                    return new(pos, first, 3, O.and, [R.BX.Signed(s.NextByte(buff)), R.BX], [second]);
                case 0x60:
                    return new(pos, first, 3, O.and, [R.BX.Plus(R.SI).Signed(s.NextByte(buff)), R.SP], [second]);
                case 0x61:
                    return new(pos, first, 3, O.and, [R.BX.Plus(R.DI).Signed(s.NextByte(buff)), R.SP], [second]);
                case 0x63:
                    return new(pos, first, 3, O.and, [R.BP.Plus(R.DI).Signed(s.NextByte(buff)), R.SP], [second]);
                case 0x64:
                    return new(pos, first, 3, O.and, [R.SI.Signed(s.NextByte(buff)), R.SP], [second]);
                case 0x65:
                    return new(pos, first, 3, O.and, [R.DI.Minus(s.NextByte(buff)), R.SP], [second]);
                case 0x66:
                    return new(pos, first, 3, O.and, [R.BP.Signed(s.NextByte(buff)), R.SP], [second]);
                case 0x67:
                    return new(pos, first, 3, O.and, [R.BX.Minus(s.NextByte(buff)), R.SP], [second]);
                case 0x68:
                    return new(pos, first, 3, O.and, [R.BX.Plus(R.SI).Signed(s.NextByte(buff)), R.BP], [second]);
                case 0x69:
                    return new(pos, first, 3, O.and, [R.BX.Plus(R.DI).Signed(s.NextByte(buff)), R.BP], [second]);
                case 0x6A:
                    return new(pos, first, 3, O.and, [R.BP.Plus(R.SI).Minus(s.NextByte(buff)), R.BP], [second]);
                case 0x6C:
                    return new(pos, first, 3, O.and, [R.SI.Signed(s.NextByte(buff)), R.BP], [second]);
                case 0x6D:
                    return new(pos, first, 3, O.and, [R.DI.Signed(s.NextByte(buff)), R.BP], [second]);
                case 0x6E:
                    return new(pos, first, 3, O.and, [R.BP.Signed(s.NextByte(buff)), R.BP], [second]);
                case 0x6F:
                    return new(pos, first, 3, O.and, [R.BX.Signed(s.NextByte(buff)), R.BP], [second]);
                case 0x70:
                    return new(pos, first, 3, O.and, [R.BX.Plus(R.SI).Signed(s.NextByte(buff)), R.SI], [second]);
                case 0x71:
                    return new(pos, first, 3, O.and, [R.BX.Plus(R.DI).Signed(s.NextByte(buff)), R.SI], [second]);
                case 0x72:
                    return new(pos, first, 3, O.and, [R.BP.Plus(R.SI).Signed(s.NextByte(buff)), R.SI], [second]);
                case 0x74:
                    return new(pos, first, 3, O.and, [R.SI.Signed(s.NextByte(buff)), R.SI], [second]);
                case 0x76:
                    return new(pos, first, 3, O.and, [R.BP.Signed(s.NextByte(buff)), R.SI], [second]);
                case 0x77:
                    return new(pos, first, 3, O.and, [R.BX.Signed(s.NextByte(buff)), R.SI], [second]);
                case 0x78:
                    return new(pos, first, 3, O.and, [R.BX.Plus(R.SI).Signed(s.NextByte(buff)), R.DI], [second]);
                case 0x79:
                    return new(pos, first, 3, O.and, [R.BX.Plus(R.DI).Minus(s.NextByte(buff)), R.DI], [second]);
                case 0x7A:
                    return new(pos, first, 3, O.and, [R.BP.Plus(R.SI).Signed(s.NextByte(buff)), R.DI], [second]);
                case 0x7B:
                    return new(pos, first, 3, O.and, [R.BP.Plus(R.DI).Signed(s.NextByte(buff)), R.DI], [second]);
                case 0x7C:
                    return new(pos, first, 3, O.and, [R.SI.Signed(s.NextByte(buff)), R.DI], [second]);
                case 0x7D:
                    return new(pos, first, 3, O.and, [R.DI.Minus(s.NextByte(buff)), R.DI], [second]);
                case 0x7E:
                    return new(pos, first, 3, O.and, [R.BP.Signed(s.NextByte(buff)), R.DI], [second]);
                case 0x7F:
                    return new(pos, first, 3, O.and, [R.BX.Signed(s.NextByte(buff)), R.DI], [second]);
                case 0x80:
                    return new(pos, first, 4, O.and, [R.BX.Plus(R.SI).Signed(s.NextShort(buff)), R.AX], [second]);
                case 0x81:
                    return new(pos, first, 4, O.and, [R.BX.Plus(R.DI).Signed(s.NextShort(buff)), R.AX], [second]);
                case 0x82:
                    return new(pos, first, 4, O.and, [R.BP.Plus(R.SI).Signed(s.NextShort(buff)), R.AX], [second]);
                case 0x83:
                    return new(pos, first, 4, O.and, [R.BP.Plus(R.DI).Signed(s.NextShort(buff)), R.AX], [second]);
                case 0x84:
                    return new(pos, first, 4, O.and, [R.SI.Signed(s.NextShort(buff)), R.AX], [second]);
                case 0x85:
                    return new(pos, first, 4, O.and, [R.DI.Signed(s.NextShort(buff)), R.AX], [second]);
                case 0x86:
                    return new(pos, first, 4, O.and, [R.BP.Signed(s.NextShort(buff)), R.AX], [second]);
                case 0x87:
                    return new(pos, first, 4, O.and, [R.BX.Signed(s.NextShort(buff)), R.AX], [second]);
                case 0x88:
                    return new(pos, first, 4, O.and, [R.BX.Plus(R.SI).Signed(s.NextShort(buff)), R.CX], [second]);
                case 0x89:
                    return new(pos, first, 4, O.and, [R.BX.Plus(R.DI).Signed(s.NextShort(buff)), R.CX], [second]);
                case 0x8A:
                    return new(pos, first, 4, O.and, [R.BP.Plus(R.SI).Signed(s.NextShort(buff)), R.CX], [second]);
                case 0x8B:
                    return new(pos, first, 4, O.and, [R.BP.Plus(R.DI).Signed(s.NextShort(buff)), R.CX], [second]);
                case 0x8D:
                    return new(pos, first, 4, O.and, [R.DI.Signed(s.NextShort(buff)), R.CX], [second]);
                case 0x8E:
                    return new(pos, first, 4, O.and, [R.BP.Signed(s.NextShort(buff)), R.CX], [second]);
                case 0x8F:
                    return new(pos, first, 4, O.and, [R.BX.Signed(s.NextShort(buff)), R.CX], [second]);
                case 0x90:
                    return new(pos, first, 4, O.and, [R.BX.Plus(R.SI).Signed(s.NextShort(buff)), R.DX], [second]);
                case 0x91:
                    return new(pos, first, 4, O.and, [R.BX.Plus(R.DI).Signed(s.NextShort(buff)), R.DX], [second]);
                case 0x92:
                    return new(pos, first, 4, O.and, [R.BP.Plus(R.SI).Signed(s.NextShort(buff)), R.DX], [second]);
                case 0x93:
                    return new(pos, first, 4, O.and, [R.BP.Plus(R.DI).Signed(s.NextShort(buff)), R.DX], [second]);
                case 0x94:
                    return new(pos, first, 4, O.and, [R.SI.Signed(s.NextShort(buff)), R.DX], [second]);
                case 0x95:
                    return new(pos, first, 4, O.and, [R.DI.Signed(s.NextShort(buff)), R.DX], [second]);
                case 0x96:
                    return new(pos, first, 4, O.and, [R.BP.Signed(s.NextShort(buff)), R.DX], [second]);
                case 0x97:
                    return new(pos, first, 4, O.and, [R.BX.Signed(s.NextShort(buff)), R.DX], [second]);
                case 0x98:
                    return new(pos, first, 4, O.and, [R.BX.Plus(R.SI).Signed(s.NextShort(buff)), R.BX], [second]);
                case 0x99:
                    return new(pos, first, 4, O.and, [R.BX.Plus(R.DI).Signed(s.NextShort(buff)), R.BX], [second]);
                case 0x9A:
                    return new(pos, first, 4, O.and, [R.BP.Plus(R.SI).Signed(s.NextShort(buff)), R.BX], [second]);
                case 0x9B:
                    return new(pos, first, 4, O.and, [R.BP.Plus(R.DI).Signed(s.NextShort(buff)), R.BX], [second]);
                case 0x9D:
                    return new(pos, first, 4, O.and, [R.DI.Signed(s.NextShort(buff)), R.BX], [second]);
                case 0x9E:
                    return new(pos, first, 4, O.and, [R.BP.Signed(s.NextShort(buff)), R.BX], [second]);
                case 0x9F:
                    return new(pos, first, 4, O.and, [R.BX.Signed(s.NextShort(buff)), R.BX], [second]);
                case 0xA0:
                    return new(pos, first, 4, O.and, [R.BX.Plus(R.SI).Signed(s.NextShort(buff)), R.SP], [second]);
                case 0xA1:
                    return new(pos, first, 4, O.and, [R.BX.Plus(R.DI).Signed(s.NextShort(buff)), R.SP], [second]);
                case 0xA2:
                    return new(pos, first, 4, O.and, [R.BP.Plus(R.SI).Signed(s.NextShort(buff)), R.SP], [second]);
                case 0xA3:
                    return new(pos, first, 4, O.and, [R.BP.Plus(R.DI).Signed(s.NextShort(buff)), R.SP], [second]);
                case 0xA4:
                    return new(pos, first, 4, O.and, [R.SI.Signed(s.NextShort(buff)), R.SP], [second]);
                case 0xA5:
                    return new(pos, first, 4, O.and, [R.DI.Signed(s.NextShort(buff)), R.SP], [second]);
                case 0xA6:
                    return new(pos, first, 4, O.and, [R.BP.Signed(s.NextShort(buff)), R.SP], [second]);
                case 0xA7:
                    return new(pos, first, 4, O.and, [R.BX.Signed(s.NextShort(buff)), R.SP], [second]);
                case 0xA9:
                    return new(pos, first, 4, O.and, [R.BX.Plus(R.DI).Signed(s.NextShort(buff)), R.BP], [second]);
                case 0xAB:
                    return new(pos, first, 4, O.and, [R.BP.Plus(R.DI).Signed(s.NextShort(buff)), R.BP], [second]);
                case 0xAC:
                    return new(pos, first, 4, O.and, [R.SI.Signed(s.NextShort(buff)), R.BP], [second]);
                case 0xAE:
                    return new(pos, first, 4, O.and, [R.BP.Signed(s.NextShort(buff)), R.BP], [second]);
                case 0xAF:
                    return new(pos, first, 4, O.and, [R.BX.Signed(s.NextShort(buff)), R.BP], [second]);
                case 0xB0:
                    return new(pos, first, 4, O.and, [R.BX.Plus(R.SI).Signed(s.NextShort(buff)), R.SI], [second]);
                case 0xB1:
                    return new(pos, first, 4, O.and, [R.BX.Plus(R.DI).Signed(s.NextShort(buff)), R.SI], [second]);
                case 0xB2:
                    return new(pos, first, 4, O.and, [R.BP.Plus(R.SI).Signed(s.NextShort(buff)), R.SI], [second]);
                case 0xB3:
                    return new(pos, first, 4, O.and, [R.BP.Plus(R.DI).Signed(s.NextShort(buff)), R.SI], [second]);
                case 0xB4:
                    return new(pos, first, 4, O.and, [R.SI.Signed(s.NextShort(buff)), R.SI], [second]);
                case 0xB5:
                    return new(pos, first, 4, O.and, [R.DI.Signed(s.NextShort(buff)), R.SI], [second]);
                case 0xB6:
                    return new(pos, first, 4, O.and, [R.BP.Signed(s.NextShort(buff)), R.SI], [second]);
                case 0xB7:
                    return new(pos, first, 4, O.and, [R.BX.Signed(s.NextShort(buff)), R.SI], [second]);
                case 0xB8:
                    return new(pos, first, 4, O.and, [R.BX.Plus(R.SI).Signed(s.NextShort(buff)), R.DI], [second]);
                case 0xB9:
                    return new(pos, first, 4, O.and, [R.BX.Plus(R.DI).Signed(s.NextShort(buff)), R.DI], [second]);
                case 0xBA:
                    return new(pos, first, 4, O.and, [R.BP.Plus(R.SI).Signed(s.NextShort(buff)), R.DI], [second]);
                case 0xBB:
                    return new(pos, first, 4, O.and, [R.BP.Plus(R.DI).Signed(s.NextShort(buff)), R.DI], [second]);
                case 0xBC:
                    return new(pos, first, 4, O.and, [R.SI.Signed(s.NextShort(buff)), R.DI], [second]);
                case 0xBE:
                    return new(pos, first, 4, O.and, [R.BP.Signed(s.NextShort(buff)), R.DI], [second]);
                case 0xBF:
                    return new(pos, first, 4, O.and, [R.BX.Signed(s.NextShort(buff)), R.DI], [second]);
                case 0xC0:
                    return new(pos, first, 2, O.and, [R.AX, R.AX], [second]);
                case 0xC1:
                    return new(pos, first, 2, O.and, [R.CX, R.AX], [second]);
                case 0xC2:
                    return new(pos, first, 2, O.and, [R.DX, R.AX], [second]);
                case 0xC3:
                    return new(pos, first, 2, O.and, [R.BX, R.AX], [second]);
                case 0xC4:
                    return new(pos, first, 2, O.and, [R.SP, R.AX], [second]);
                case 0xC6:
                    return new(pos, first, 2, O.and, [R.SI, R.AX], [second]);
                case 0xC8:
                    return new(pos, first, 2, O.and, [R.AX, R.CX], [second]);
                case 0xC9:
                    return new(pos, first, 2, O.and, [R.CX, R.CX], [second]);
                case 0xCA:
                    return new(pos, first, 2, O.and, [R.DX, R.CX], [second]);
                case 0xCB:
                    return new(pos, first, 2, O.and, [R.BX, R.CX], [second]);
                case 0xCC:
                    return new(pos, first, 2, O.and, [R.SP, R.CX], [second]);
                case 0xCD:
                    return new(pos, first, 2, O.and, [R.BP, R.CX], [second]);
                case 0xCF:
                    return new(pos, first, 2, O.and, [R.DI, R.CX], [second]);
                case 0xD0:
                    return new(pos, first, 2, O.and, [R.AX, R.DX], [second]);
                case 0xD1:
                    return new(pos, first, 2, O.and, [R.CX, R.DX], [second]);
                case 0xD2:
                    return new(pos, first, 2, O.and, [R.DX, R.DX], [second]);
                case 0xD3:
                    return new(pos, first, 2, O.and, [R.BX, R.DX], [second]);
                case 0xD4:
                    return new(pos, first, 2, O.and, [R.SP, R.DX], [second]);
                case 0xD5:
                    return new(pos, first, 2, O.and, [R.BP, R.DX], [second]);
                case 0xD6:
                    return new(pos, first, 2, O.and, [R.SI, R.DX], [second]);
                case 0xD7:
                    return new(pos, first, 2, O.and, [R.DI, R.DX], [second]);
                case 0xD8:
                    return new(pos, first, 2, O.and, [R.AX, R.BX], [second]);
                case 0xD9:
                    return new(pos, first, 2, O.and, [R.CX, R.BX], [second]);
                case 0xDA:
                    return new(pos, first, 2, O.and, [R.DX, R.BX], [second]);
                case 0xDB:
                    return new(pos, first, 2, O.and, [R.BX, R.BX], [second]);
                case 0xDD:
                    return new(pos, first, 2, O.and, [R.BP, R.BX], [second]);
                case 0xDE:
                    return new(pos, first, 2, O.and, [R.SI, R.BX], [second]);
                case 0xDF:
                    return new(pos, first, 2, O.and, [R.DI, R.BX], [second]);
                case 0xE0:
                    return new(pos, first, 2, O.and, [R.AX, R.SP], [second]);
                case 0xE1:
                    return new(pos, first, 2, O.and, [R.CX, R.SP], [second]);
                case 0xE2:
                    return new(pos, first, 2, O.and, [R.DX, R.SP], [second]);
                case 0xE3:
                    return new(pos, first, 2, O.and, [R.BX, R.SP], [second]);
                case 0xE4:
                    return new(pos, first, 2, O.and, [R.SP, R.SP], [second]);
                case 0xE5:
                    return new(pos, first, 2, O.and, [R.BP, R.SP], [second]);
                case 0xE6:
                    return new(pos, first, 2, O.and, [R.SI, R.SP], [second]);
                case 0xE7:
                    return new(pos, first, 2, O.and, [R.DI, R.SP], [second]);
                case 0xE8:
                    return new(pos, first, 2, O.and, [R.AX, R.BP], [second]);
                case 0xE9:
                    return new(pos, first, 2, O.and, [R.CX, R.BP], [second]);
                case 0xEB:
                    return new(pos, first, 2, O.and, [R.BX, R.BP], [second]);
                case 0xEC:
                    return new(pos, first, 2, O.and, [R.SP, R.BP], [second]);
                case 0xED:
                    return new(pos, first, 2, O.and, [R.BP, R.BP], [second]);
                case 0xEE:
                    return new(pos, first, 2, O.and, [R.SI, R.BP], [second]);
                case 0xEF:
                    return new(pos, first, 2, O.and, [R.DI, R.BP], [second]);
                case 0xF0:
                    return new(pos, first, 2, O.and, [R.AX, R.SI], [second]);
                case 0xF1:
                    return new(pos, first, 2, O.and, [R.CX, R.SI], [second]);
                case 0xF2:
                    return new(pos, first, 2, O.and, [R.DX, R.SI], [second]);
                case 0xF3:
                    return new(pos, first, 2, O.and, [R.BX, R.SI], [second]);
                case 0xF4:
                    return new(pos, first, 2, O.and, [R.SP, R.SI], [second]);
                case 0xF5:
                    return new(pos, first, 2, O.and, [R.BP, R.SI], [second]);
                case 0xF6:
                    return new(pos, first, 2, O.and, [R.SI, R.SI], [second]);
                case 0xF9:
                    return new(pos, first, 2, O.and, [R.CX, R.DI], [second]);
                case 0xFA:
                    return new(pos, first, 2, O.and, [R.DX, R.DI], [second]);
                case 0xFB:
                    return new(pos, first, 2, O.and, [R.BX, R.DI], [second]);
                case 0xFC:
                    return new(pos, first, 2, O.and, [R.SP, R.DI], [second]);
                case 0xFD:
                    return new(pos, first, 2, O.and, [R.BP, R.DI], [second]);
                case 0xFE:
                    return new(pos, first, 2, O.and, [R.SI, R.DI], [second]);
                case 0xFF:
                    return new(pos, first, 2, O.and, [R.DI, R.DI], [second]);
            }
            return null;
        }
    }
}