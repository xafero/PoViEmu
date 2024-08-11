// ReSharper disable InconsistentNaming

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
    internal static class Intel16x39
    {
        internal static Instruction? Parse(Stream s, byte[] buff, long pos, byte first)
        {
            var second = s.NextByte(buff);
            switch (second)
            {
                case 0x00:
                    return new(pos, first, 2, O.cmp, [R.BX.Plus(R.SI), R.AX],  [second]  );
                case 0x01:
                    return new(pos, first, 2, O.cmp, [R.BX.Plus(R.DI), R.AX],  [second]  );
                case 0x02:
                    return new(pos, first, 2, O.cmp, [R.BP.Plus(R.SI), R.AX],  [second]  );
                case 0x04:
                    return new(pos, first, 2, O.cmp, [R.SI.Box(), R.AX],  [second]  );
                case 0x05:
                    return new(pos, first, 2, O.cmp, [R.DI.Box(), R.AX],  [second]  );
                case 0x06:
                    return new(pos, first, 4, O.cmp, [s.NextShort(buff).Box(), R.AX],   [ second ]  );
                case 0x08:
                    return new(pos, first, 2, O.cmp, [R.BX.Plus(R.SI), R.CX],  [second]  );
                case 0x09:
                    return new(pos, first, 2, O.cmp, [R.BX.Plus(R.DI), R.CX],  [second]  );
                case 0x0A:
                    return new(pos, first, 2, O.cmp, [R.BP.Plus(R.SI), R.CX],  [second]  );
                case 0x0B:
                    return new(pos, first, 2, O.cmp, [R.BP.Plus(R.DI), R.CX],  [second]  );
                case 0x0C:
                    return new(pos, first, 2, O.cmp, [R.SI.Box(), R.CX],  [second]  );
                case 0x0D:
                    return new(pos, first, 2, O.cmp, [R.DI.Box(), R.CX],  [second]  );
                case 0x0E:
                    return new(pos, first, 4, O.cmp, [s.NextShort(buff).Box(), R.CX],   [ second ]  );
                case 0x0F:
                    return new(pos, first, 2, O.cmp, [R.BX.Box(), R.CX],  [second]  );
                case 0x10:
                    return new(pos, first, 2, O.cmp, [R.BX.Plus(R.SI), R.DX],  [second]  );
                case 0x11:
                    return new(pos, first, 2, O.cmp, [R.BX.Plus(R.DI), R.DX],  [second]  );
                case 0x12:
                    return new(pos, first, 2, O.cmp, [R.BP.Plus(R.SI), R.DX],  [second]  );
                case 0x13:
                    return new(pos, first, 2, O.cmp, [R.BP.Plus(R.DI), R.DX],  [second]  );
                case 0x14:
                    return new(pos, first, 2, O.cmp, [R.SI.Box(), R.DX],  [second]  );
                case 0x15:
                    return new(pos, first, 2, O.cmp, [R.DI.Box(), R.DX],  [second]  );
                case 0x16:
                    return new(pos, first, 4, O.cmp, [s.NextShort(buff).Box(), R.DX],   [ second ]  );
                case 0x17:
                    return new(pos, first, 2, O.cmp, [R.BX.Box(), R.DX],  [second]  );
                case 0x18:
                    return new(pos, first, 2, O.cmp, [R.BX.Plus(R.SI), R.BX],  [second]  );
                case 0x19:
                    return new(pos, first, 2, O.cmp, [R.BX.Plus(R.DI), R.BX],  [second]  );
                case 0x1B:
                    return new(pos, first, 2, O.cmp, [R.BP.Plus(R.DI), R.BX],  [second]  );
                case 0x1C:
                    return new(pos, first, 2, O.cmp, [R.SI.Box(), R.BX],  [second]  );
                case 0x1D:
                    return new(pos, first, 2, O.cmp, [R.DI.Box(), R.BX],  [second]  );
                case 0x1F:
                    return new(pos, first, 2, O.cmp, [R.BX.Box(), R.BX],  [second]  );
                case 0x20:
                    return new(pos, first, 2, O.cmp, [R.BX.Plus(R.SI), R.SP],  [second]  );
                case 0x21:
                    return new(pos, first, 2, O.cmp, [R.BX.Plus(R.DI), R.SP],  [second]  );
                case 0x22:
                    return new(pos, first, 2, O.cmp, [R.BP.Plus(R.SI), R.SP],  [second]  );
                case 0x23:
                    return new(pos, first, 2, O.cmp, [R.BP.Plus(R.DI), R.SP],  [second]  );
                case 0x24:
                    return new(pos, first, 2, O.cmp, [R.SI.Box(), R.SP],  [second]  );
                case 0x25:
                    return new(pos, first, 2, O.cmp, [R.DI.Box(), R.SP],  [second]  );
                case 0x26:
                    return new(pos, first, 4, O.cmp, [s.NextShort(buff).Box(), R.SP],   [ second ]  );
                case 0x27:
                    return new(pos, first, 2, O.cmp, [R.BX.Box(), R.SP],  [second]  );
                case 0x28:
                    return new(pos, first, 2, O.cmp, [R.BX.Plus(R.SI), R.BP],  [second]  );
                case 0x2A:
                    return new(pos, first, 2, O.cmp, [R.BP.Plus(R.SI), R.BP],  [second]  );
                case 0x2B:
                    return new(pos, first, 2, O.cmp, [R.BP.Plus(R.DI), R.BP],  [second]  );
                case 0x2C:
                    return new(pos, first, 2, O.cmp, [R.SI.Box(), R.BP],  [second]  );
                case 0x2D:
                    return new(pos, first, 2, O.cmp, [R.DI.Box(), R.BP],  [second]  );
                case 0x2E:
                    return new(pos, first, 4, O.cmp, [s.NextShort(buff).Box(), R.BP],   [ second ]  );
                case 0x2F:
                    return new(pos, first, 2, O.cmp, [R.BX.Box(), R.BP],  [second]  );
                case 0x30:
                    return new(pos, first, 2, O.cmp, [R.BX.Plus(R.SI), R.SI],  [second]  );
                case 0x31:
                    return new(pos, first, 2, O.cmp, [R.BX.Plus(R.DI), R.SI],  [second]  );
                case 0x32:
                    return new(pos, first, 2, O.cmp, [R.BP.Plus(R.SI), R.SI],  [second]  );
                case 0x33:
                    return new(pos, first, 2, O.cmp, [R.BP.Plus(R.DI), R.SI],  [second]  );
                case 0x34:
                    return new(pos, first, 2, O.cmp, [R.SI.Box(), R.SI],  [second]  );
                case 0x35:
                    return new(pos, first, 2, O.cmp, [R.DI.Box(), R.SI],  [second]  );
                case 0x37:
                    return new(pos, first, 2, O.cmp, [R.BX.Box(), R.SI],  [second]  );
                case 0x38:
                    return new(pos, first, 2, O.cmp, [R.BX.Plus(R.SI), R.DI],  [second]  );
                case 0x39:
                    return new(pos, first, 2, O.cmp, [R.BX.Plus(R.DI), R.DI],  [second]  );
                case 0x3B:
                    return new(pos, first, 2, O.cmp, [R.BP.Plus(R.DI), R.DI],  [second]  );
                case 0x3C:
                    return new(pos, first, 2, O.cmp, [R.SI.Box(), R.DI],  [second]  );
                case 0x3E:
                    return new(pos, first, 4, O.cmp, [s.NextShort(buff).Box(), R.DI],   [ second ]  );
                case 0x3F:
                    return new(pos, first, 2, O.cmp, [R.BX.Box(), R.DI],  [second]  );
                case 0x40:
                    return new(pos, first, 3, O.cmp, [R.BX.Plus(R.SI).Plus(s.NextByte(buff)), R.AX],   [ second ]  );
                case 0x41:
                    return new(pos, first, 3, O.cmp, [R.BX.Plus(R.DI).Plus(s.NextByte(buff)), R.AX],   [ second ]  );
                case 0x42:
                    return new(pos, first, 3, O.cmp, [R.BP.Plus(R.SI).Minus(s.NextByte(buff)), R.AX],   [ second ]  );
                case 0x43:
                    return new(pos, first, 3, O.cmp, [R.BP.Plus(R.DI).Plus(s.NextByte(buff)), R.AX],   [ second ]  );
                case 0x44:
                    return new(pos, first, 3, O.cmp, [R.SI.Plus(s.NextByte(buff)), R.AX],   [ second ]  );
                case 0x46:
                    return new(pos, first, 3, O.cmp, [R.BP.Plus(s.NextByte(buff)), R.AX],   [ second ]  );
                case 0x47:
                    return new(pos, first, 3, O.cmp, [R.BX.Minus(s.NextByte(buff)), R.AX],   [ second ]  );
                case 0x48:
                    return new(pos, first, 3, O.cmp, [R.BX.Plus(R.SI).Plus(s.NextByte(buff)), R.CX],   [ second ]  );
                case 0x4A:
                    return new(pos, first, 3, O.cmp, [R.BP.Plus(R.SI).Minus(s.NextByte(buff)), R.CX],   [ second ]  );
                case 0x4B:
                    return new(pos, first, 3, O.cmp, [R.BP.Plus(R.DI).Plus(s.NextByte(buff)), R.CX],   [ second ]  );
                case 0x4C:
                    return new(pos, first, 3, O.cmp, [R.SI.Plus(s.NextByte(buff)), R.CX],   [ second ]  );
                case 0x4E:
                    return new(pos, first, 3, O.cmp, [R.BP.Plus(s.NextByte(buff)), R.CX],   [ second ]  );
                case 0x4F:
                    return new(pos, first, 3, O.cmp, [R.BX.Plus(s.NextByte(buff)), R.CX],   [ second ]  );
                case 0x50:
                    return new(pos, first, 3, O.cmp, [R.BX.Plus(R.SI).Plus(s.NextByte(buff)), R.DX],   [ second ]  );
                case 0x51:
                    return new(pos, first, 3, O.cmp, [R.BX.Plus(R.DI).Minus(s.NextByte(buff)), R.DX],   [ second ]  );
                case 0x52:
                    return new(pos, first, 3, O.cmp, [R.BP.Plus(R.SI).Minus(s.NextByte(buff)), R.DX],   [ second ]  );
                case 0x53:
                    return new(pos, first, 3, O.cmp, [R.BP.Plus(R.DI).Plus(s.NextByte(buff)), R.DX],   [ second ]  );
                case 0x54:
                    return new(pos, first, 3, O.cmp, [R.SI.Plus(s.NextByte(buff)), R.DX],   [ second ]  );
                case 0x55:
                    return new(pos, first, 3, O.cmp, [R.DI.Minus(s.NextByte(buff)), R.DX],   [ second ]  );
                case 0x56:
                    return new(pos, first, 3, O.cmp, [R.BP.Plus(s.NextByte(buff)), R.DX],   [ second ]  );
                case 0x57:
                    return new(pos, first, 3, O.cmp, [R.BX.Plus(s.NextByte(buff)), R.DX],   [ second ]  );
                case 0x58:
                    return new(pos, first, 3, O.cmp, [R.BX.Plus(R.SI).Plus(s.NextByte(buff)), R.BX],   [ second ]  );
                case 0x59:
                    return new(pos, first, 3, O.cmp, [R.BX.Plus(R.DI).Plus(s.NextByte(buff)), R.BX],   [ second ]  );
                case 0x5A:
                    return new(pos, first, 3, O.cmp, [R.BP.Plus(R.SI).Minus(s.NextByte(buff)), R.BX],   [ second ]  );
                case 0x5B:
                    return new(pos, first, 3, O.cmp, [R.BP.Plus(R.DI).Minus(s.NextByte(buff)), R.BX],   [ second ]  );
                case 0x5C:
                    return new(pos, first, 3, O.cmp, [R.SI.Plus(s.NextByte(buff)), R.BX],   [ second ]  );
                case 0x5D:
                    return new(pos, first, 3, O.cmp, [R.DI.Plus(s.NextByte(buff)), R.BX],   [ second ]  );
                case 0x5E:
                    return new(pos, first, 3, O.cmp, [R.BP.Plus(s.NextByte(buff)), R.BX],   [ second ]  );
                case 0x5F:
                    return new(pos, first, 3, O.cmp, [R.BX.Plus(s.NextByte(buff)), R.BX],   [ second ]  );
                case 0x60:
                    return new(pos, first, 3, O.cmp, [R.BX.Plus(R.SI).Plus(s.NextByte(buff)), R.SP],   [ second ]  );
                case 0x61:
                    return new(pos, first, 3, O.cmp, [R.BX.Plus(R.DI).Plus(s.NextByte(buff)), R.SP],   [ second ]  );
                case 0x62:
                    return new(pos, first, 3, O.cmp, [R.BP.Plus(R.SI).Plus(s.NextByte(buff)), R.SP],   [ second ]  );
                case 0x63:
                    return new(pos, first, 3, O.cmp, [R.BP.Plus(R.DI).Minus(s.NextByte(buff)), R.SP],   [ second ]  );
                case 0x64:
                    return new(pos, first, 3, O.cmp, [R.SI.Plus(s.NextByte(buff)), R.SP],   [ second ]  );
                case 0x65:
                    return new(pos, first, 3, O.cmp, [R.DI.Minus(s.NextByte(buff)), R.SP],   [ second ]  );
                case 0x66:
                    return new(pos, first, 3, O.cmp, [R.BP.Plus(s.NextByte(buff)), R.SP],   [ second ]  );
                case 0x67:
                    return new(pos, first, 3, O.cmp, [R.BX.Plus(s.NextByte(buff)), R.SP],   [ second ]  );
                case 0x68:
                    return new(pos, first, 3, O.cmp, [R.BX.Plus(R.SI).Plus(s.NextByte(buff)), R.BP],   [ second ]  );
                case 0x6A:
                    return new(pos, first, 3, O.cmp, [R.BP.Plus(R.SI).Plus(s.NextByte(buff)), R.BP],   [ second ]  );
                case 0x6B:
                    return new(pos, first, 3, O.cmp, [R.BP.Plus(R.DI).Plus(s.NextByte(buff)), R.BP],   [ second ]  );
                case 0x6C:
                    return new(pos, first, 3, O.cmp, [R.SI.Minus(s.NextByte(buff)), R.BP],   [ second ]  );
                case 0x6D:
                    return new(pos, first, 3, O.cmp, [R.DI.Minus(s.NextByte(buff)), R.BP],   [ second ]  );
                case 0x6E:
                    return new(pos, first, 3, O.cmp, [R.BP.Minus(s.NextByte(buff)), R.BP],   [ second ]  );
                case 0x6F:
                    return new(pos, first, 3, O.cmp, [R.BX.Plus(s.NextByte(buff)), R.BP],   [ second ]  );
                case 0x70:
                    return new(pos, first, 3, O.cmp, [R.BX.Plus(R.SI).Minus(s.NextByte(buff)), R.SI],   [ second ]  );
                case 0x71:
                    return new(pos, first, 3, O.cmp, [R.BX.Plus(R.DI).Plus(s.NextByte(buff)), R.SI],   [ second ]  );
                case 0x72:
                    return new(pos, first, 3, O.cmp, [R.BP.Plus(R.SI).Plus(s.NextByte(buff)), R.SI],   [ second ]  );
                case 0x73:
                    return new(pos, first, 3, O.cmp, [R.BP.Plus(R.DI).Plus(s.NextByte(buff)), R.SI],   [ second ]  );
                case 0x74:
                    return new(pos, first, 3, O.cmp, [R.SI.Plus(s.NextByte(buff)), R.SI],   [ second ]  );
                case 0x76:
                    return new(pos, first, 3, O.cmp, [R.BP.Plus(s.NextByte(buff)), R.SI],   [ second ]  );
                case 0x77:
                    return new(pos, first, 3, O.cmp, [R.BX.Plus(s.NextByte(buff)), R.SI],   [ second ]  );
                case 0x78:
                    return new(pos, first, 3, O.cmp, [R.BX.Plus(R.SI).Plus(s.NextByte(buff)), R.DI],   [ second ]  );
                case 0x79:
                    return new(pos, first, 3, O.cmp, [R.BX.Plus(R.DI).Plus(s.NextByte(buff)), R.DI],   [ second ]  );
                case 0x7A:
                    return new(pos, first, 3, O.cmp, [R.BP.Plus(R.SI).Plus(s.NextByte(buff)), R.DI],   [ second ]  );
                case 0x7B:
                    return new(pos, first, 3, O.cmp, [R.BP.Plus(R.DI).Plus(s.NextByte(buff)), R.DI],   [ second ]  );
                case 0x7C:
                    return new(pos, first, 3, O.cmp, [R.SI.Plus(s.NextByte(buff)), R.DI],   [ second ]  );
                case 0x7D:
                    return new(pos, first, 3, O.cmp, [R.DI.Minus(s.NextByte(buff)), R.DI],   [ second ]  );
                case 0x7E:
                    return new(pos, first, 3, O.cmp, [R.BP.Plus(s.NextByte(buff)), R.DI],   [ second ]  );
                case 0x7F:
                    return new(pos, first, 3, O.cmp, [R.BX.Plus(s.NextByte(buff)), R.DI],   [ second ]  );
                case 0x80:
                    return new(pos, first, 4, O.cmp, [R.BX.Plus(R.SI).Signed(s.NextShort(buff)), R.AX],   [ second ]  );
                case 0x81:
                    return new(pos, first, 4, O.cmp, [R.BX.Plus(R.DI).Signed(s.NextShort(buff)), R.AX],   [ second ]  );
                case 0x82:
                    return new(pos, first, 4, O.cmp, [R.BP.Plus(R.SI).Signed(s.NextShort(buff)), R.AX],   [ second ]  );
                case 0x83:
                    return new(pos, first, 4, O.cmp, [R.BP.Plus(R.DI).Signed(s.NextShort(buff)), R.AX],   [ second ]  );
                case 0x84:
                    return new(pos, first, 4, O.cmp, [R.SI.Signed(s.NextShort(buff)), R.AX],   [ second ]  );
                case 0x85:
                    return new(pos, first, 4, O.cmp, [R.DI.Signed(s.NextShort(buff)), R.AX],   [ second ]  );
                case 0x88:
                    return new(pos, first, 4, O.cmp, [R.BX.Plus(R.SI).Signed(s.NextShort(buff)), R.CX],   [ second ]  );
                case 0x89:
                    return new(pos, first, 4, O.cmp, [R.BX.Plus(R.DI).Signed(s.NextShort(buff)), R.CX],   [ second ]  );
                case 0x8A:
                    return new(pos, first, 4, O.cmp, [R.BP.Plus(R.SI).Signed(s.NextShort(buff)), R.CX],   [ second ]  );
                case 0x8B:
                    return new(pos, first, 4, O.cmp, [R.BP.Plus(R.DI).Signed(s.NextShort(buff)), R.CX],   [ second ]  );
                case 0x8C:
                    return new(pos, first, 4, O.cmp, [R.SI.Signed(s.NextShort(buff)), R.CX],   [ second ]  );
                case 0x8D:
                    return new(pos, first, 4, O.cmp, [R.DI.Signed(s.NextShort(buff)), R.CX],   [ second ]  );
                case 0x8E:
                    return new(pos, first, 4, O.cmp, [R.BP.Signed(s.NextShort(buff)), R.CX],   [ second ]  );
                case 0x8F:
                    return new(pos, first, 4, O.cmp, [R.BX.Signed(s.NextShort(buff)), R.CX],   [ second ]  );
                case 0x90:
                    return new(pos, first, 4, O.cmp, [R.BX.Plus(R.SI).Signed(s.NextShort(buff)), R.DX],   [ second ]  );
                case 0x91:
                    return new(pos, first, 4, O.cmp, [R.BX.Plus(R.DI).Signed(s.NextShort(buff)), R.DX],   [ second ]  );
                case 0x92:
                    return new(pos, first, 4, O.cmp, [R.BP.Plus(R.SI).Signed(s.NextShort(buff)), R.DX],   [ second ]  );
                case 0x93:
                    return new(pos, first, 4, O.cmp, [R.BP.Plus(R.DI).Signed(s.NextShort(buff)), R.DX],   [ second ]  );
                case 0x95:
                    return new(pos, first, 4, O.cmp, [R.DI.Signed(s.NextShort(buff)), R.DX],   [ second ]  );
                case 0x96:
                    return new(pos, first, 4, O.cmp, [R.BP.Signed(s.NextShort(buff)), R.DX],   [ second ]  );
                case 0x97:
                    return new(pos, first, 4, O.cmp, [R.BX.Signed(s.NextShort(buff)), R.DX],   [ second ]  );
                case 0x98:
                    return new(pos, first, 4, O.cmp, [R.BX.Plus(R.SI).Signed(s.NextShort(buff)), R.BX],   [ second ]  );
                case 0x99:
                    return new(pos, first, 4, O.cmp, [R.BX.Plus(R.DI).Signed(s.NextShort(buff)), R.BX],   [ second ]  );
                case 0x9A:
                    return new(pos, first, 4, O.cmp, [R.BP.Plus(R.SI).Signed(s.NextShort(buff)), R.BX],   [ second ]  );
                case 0x9B:
                    return new(pos, first, 4, O.cmp, [R.BP.Plus(R.DI).Signed(s.NextShort(buff)), R.BX],   [ second ]  );
                case 0x9C:
                    return new(pos, first, 4, O.cmp, [R.SI.Signed(s.NextShort(buff)), R.BX],   [ second ]  );
                case 0x9D:
                    return new(pos, first, 4, O.cmp, [R.DI.Signed(s.NextShort(buff)), R.BX],   [ second ]  );
                case 0x9E:
                    return new(pos, first, 4, O.cmp, [R.BP.Signed(s.NextShort(buff)), R.BX],   [ second ]  );
                case 0x9F:
                    return new(pos, first, 4, O.cmp, [R.BX.Signed(s.NextShort(buff)), R.BX],   [ second ]  );
                case 0xA0:
                    return new(pos, first, 4, O.cmp, [R.BX.Plus(R.SI).Signed(s.NextShort(buff)), R.SP],   [ second ]  );
                case 0xA1:
                    return new(pos, first, 4, O.cmp, [R.BX.Plus(R.DI).Signed(s.NextShort(buff)), R.SP],   [ second ]  );
                case 0xA2:
                    return new(pos, first, 4, O.cmp, [R.BP.Plus(R.SI).Signed(s.NextShort(buff)), R.SP],   [ second ]  );
                case 0xA3:
                    return new(pos, first, 4, O.cmp, [R.BP.Plus(R.DI).Signed(s.NextShort(buff)), R.SP],   [ second ]  );
                case 0xA4:
                    return new(pos, first, 4, O.cmp, [R.SI.Signed(s.NextShort(buff)), R.SP],   [ second ]  );
                case 0xA6:
                    return new(pos, first, 4, O.cmp, [R.BP.Signed(s.NextShort(buff)), R.SP],   [ second ]  );
                case 0xA8:
                    return new(pos, first, 4, O.cmp, [R.BX.Plus(R.SI).Signed(s.NextShort(buff)), R.BP],   [ second ]  );
                case 0xA9:
                    return new(pos, first, 4, O.cmp, [R.BX.Plus(R.DI).Signed(s.NextShort(buff)), R.BP],   [ second ]  );
                case 0xAA:
                    return new(pos, first, 4, O.cmp, [R.BP.Plus(R.SI).Signed(s.NextShort(buff)), R.BP],   [ second ]  );
                case 0xAB:
                    return new(pos, first, 4, O.cmp, [R.BP.Plus(R.DI).Signed(s.NextShort(buff)), R.BP],   [ second ]  );
                case 0xAC:
                    return new(pos, first, 4, O.cmp, [R.SI.Signed(s.NextShort(buff)), R.BP],   [ second ]  );
                case 0xAE:
                    return new(pos, first, 4, O.cmp, [R.BP.Signed(s.NextShort(buff)), R.BP],   [ second ]  );
                case 0xAF:
                    return new(pos, first, 4, O.cmp, [R.BX.Signed(s.NextShort(buff)), R.BP],   [ second ]  );
                case 0xB0:
                    return new(pos, first, 4, O.cmp, [R.BX.Plus(R.SI).Signed(s.NextShort(buff)), R.SI],   [ second ]  );
                case 0xB1:
                    return new(pos, first, 4, O.cmp, [R.BX.Plus(R.DI).Signed(s.NextShort(buff)), R.SI],   [ second ]  );
                case 0xB2:
                    return new(pos, first, 4, O.cmp, [R.BP.Plus(R.SI).Signed(s.NextShort(buff)), R.SI],   [ second ]  );
                case 0xB3:
                    return new(pos, first, 4, O.cmp, [R.BP.Plus(R.DI).Signed(s.NextShort(buff)), R.SI],   [ second ]  );
                case 0xB4:
                    return new(pos, first, 4, O.cmp, [R.SI.Signed(s.NextShort(buff)), R.SI],   [ second ]  );
                case 0xB5:
                    return new(pos, first, 4, O.cmp, [R.DI.Signed(s.NextShort(buff)), R.SI],   [ second ]  );
                case 0xB6:
                    return new(pos, first, 4, O.cmp, [R.BP.Signed(s.NextShort(buff)), R.SI],   [ second ]  );
                case 0xB7:
                    return new(pos, first, 4, O.cmp, [R.BX.Signed(s.NextShort(buff)), R.SI],   [ second ]  );
                case 0xB8:
                    return new(pos, first, 4, O.cmp, [R.BX.Plus(R.SI).Signed(s.NextShort(buff)), R.DI],   [ second ]  );
                case 0xB9:
                    return new(pos, first, 4, O.cmp, [R.BX.Plus(R.DI).Signed(s.NextShort(buff)), R.DI],   [ second ]  );
                case 0xBA:
                    return new(pos, first, 4, O.cmp, [R.BP.Plus(R.SI).Signed(s.NextShort(buff)), R.DI],   [ second ]  );
                case 0xBB:
                    return new(pos, first, 4, O.cmp, [R.BP.Plus(R.DI).Signed(s.NextShort(buff)), R.DI],   [ second ]  );
                case 0xBC:
                    return new(pos, first, 4, O.cmp, [R.SI.Signed(s.NextShort(buff)), R.DI],   [ second ]  );
                case 0xBD:
                    return new(pos, first, 4, O.cmp, [R.DI.Signed(s.NextShort(buff)), R.DI],   [ second ]  );
                case 0xBE:
                    return new(pos, first, 4, O.cmp, [R.BP.Signed(s.NextShort(buff)), R.DI],   [ second ]  );
                case 0xBF:
                    return new(pos, first, 4, O.cmp, [R.BX.Signed(s.NextShort(buff)), R.DI],   [ second ]  );
                case 0xC0:
                    return new(pos, first, 2, O.cmp, [R.AX, R.AX],  [second]  );
                case 0xC1:
                    return new(pos, first, 2, O.cmp, [R.CX, R.AX],  [second]  );
                case 0xC2:
                    return new(pos, first, 2, O.cmp, [R.DX, R.AX],  [second]  );
                case 0xC3:
                    return new(pos, first, 2, O.cmp, [R.BX, R.AX],  [second]  );
                case 0xC4:
                    return new(pos, first, 2, O.cmp, [R.SP, R.AX],  [second]  );
                case 0xC5:
                    return new(pos, first, 2, O.cmp, [R.BP, R.AX],  [second]  );
                case 0xC6:
                    return new(pos, first, 2, O.cmp, [R.SI, R.AX],  [second]  );
                case 0xC7:
                    return new(pos, first, 2, O.cmp, [R.DI, R.AX],  [second]  );
                case 0xC8:
                    return new(pos, first, 2, O.cmp, [R.AX, R.CX],  [second]  );
                case 0xCA:
                    return new(pos, first, 2, O.cmp, [R.DX, R.CX],  [second]  );
                case 0xCB:
                    return new(pos, first, 2, O.cmp, [R.BX, R.CX],  [second]  );
                case 0xCC:
                    return new(pos, first, 2, O.cmp, [R.SP, R.CX],  [second]  );
                case 0xCD:
                    return new(pos, first, 2, O.cmp, [R.BP, R.CX],  [second]  );
                case 0xCE:
                    return new(pos, first, 2, O.cmp, [R.SI, R.CX],  [second]  );
                case 0xCF:
                    return new(pos, first, 2, O.cmp, [R.DI, R.CX],  [second]  );
                case 0xD0:
                    return new(pos, first, 2, O.cmp, [R.AX, R.DX],  [second]  );
                case 0xD1:
                    return new(pos, first, 2, O.cmp, [R.CX, R.DX],  [second]  );
                case 0xD3:
                    return new(pos, first, 2, O.cmp, [R.BX, R.DX],  [second]  );
                case 0xD4:
                    return new(pos, first, 2, O.cmp, [R.SP, R.DX],  [second]  );
                case 0xD5:
                    return new(pos, first, 2, O.cmp, [R.BP, R.DX],  [second]  );
                case 0xD6:
                    return new(pos, first, 2, O.cmp, [R.SI, R.DX],  [second]  );
                case 0xD7:
                    return new(pos, first, 2, O.cmp, [R.DI, R.DX],  [second]  );
                case 0xD8:
                    return new(pos, first, 2, O.cmp, [R.AX, R.BX],  [second]  );
                case 0xD9:
                    return new(pos, first, 2, O.cmp, [R.CX, R.BX],  [second]  );
                case 0xDA:
                    return new(pos, first, 2, O.cmp, [R.DX, R.BX],  [second]  );
                case 0xDB:
                    return new(pos, first, 2, O.cmp, [R.BX, R.BX],  [second]  );
                case 0xDC:
                    return new(pos, first, 2, O.cmp, [R.SP, R.BX],  [second]  );
                case 0xDD:
                    return new(pos, first, 2, O.cmp, [R.BP, R.BX],  [second]  );
                case 0xDE:
                    return new(pos, first, 2, O.cmp, [R.SI, R.BX],  [second]  );
                case 0xDF:
                    return new(pos, first, 2, O.cmp, [R.DI, R.BX],  [second]  );
                case 0xE0:
                    return new(pos, first, 2, O.cmp, [R.AX, R.SP],  [second]  );
                case 0xE1:
                    return new(pos, first, 2, O.cmp, [R.CX, R.SP],  [second]  );
                case 0xE2:
                    return new(pos, first, 2, O.cmp, [R.DX, R.SP],  [second]  );
                case 0xE3:
                    return new(pos, first, 2, O.cmp, [R.BX, R.SP],  [second]  );
                case 0xE4:
                    return new(pos, first, 2, O.cmp, [R.SP, R.SP],  [second]  );
                case 0xE7:
                    return new(pos, first, 2, O.cmp, [R.DI, R.SP],  [second]  );
                case 0xE8:
                    return new(pos, first, 2, O.cmp, [R.AX, R.BP],  [second]  );
                case 0xE9:
                    return new(pos, first, 2, O.cmp, [R.CX, R.BP],  [second]  );
                case 0xEA:
                    return new(pos, first, 2, O.cmp, [R.DX, R.BP],  [second]  );
                case 0xEC:
                    return new(pos, first, 2, O.cmp, [R.SP, R.BP],  [second]  );
                case 0xED:
                    return new(pos, first, 2, O.cmp, [R.BP, R.BP],  [second]  );
                case 0xEE:
                    return new(pos, first, 2, O.cmp, [R.SI, R.BP],  [second]  );
                case 0xF0:
                    return new(pos, first, 2, O.cmp, [R.AX, R.SI],  [second]  );
                case 0xF1:
                    return new(pos, first, 2, O.cmp, [R.CX, R.SI],  [second]  );
                case 0xF2:
                    return new(pos, first, 2, O.cmp, [R.DX, R.SI],  [second]  );
                case 0xF3:
                    return new(pos, first, 2, O.cmp, [R.BX, R.SI],  [second]  );
                case 0xF4:
                    return new(pos, first, 2, O.cmp, [R.SP, R.SI],  [second]  );
                case 0xF5:
                    return new(pos, first, 2, O.cmp, [R.BP, R.SI],  [second]  );
                case 0xF6:
                    return new(pos, first, 2, O.cmp, [R.SI, R.SI],  [second]  );
                case 0xF7:
                    return new(pos, first, 2, O.cmp, [R.DI, R.SI],  [second]  );
                case 0xF9:
                    return new(pos, first, 2, O.cmp, [R.CX, R.DI],  [second]  );
                case 0xFA:
                    return new(pos, first, 2, O.cmp, [R.DX, R.DI],  [second]  );
                case 0xFB:
                    return new(pos, first, 2, O.cmp, [R.BX, R.DI],  [second]  );
                case 0xFC:
                    return new(pos, first, 2, O.cmp, [R.SP, R.DI],  [second]  );
                case 0xFD:
                    return new(pos, first, 2, O.cmp, [R.BP, R.DI],  [second]  );
                case 0xFE:
                    return new(pos, first, 2, O.cmp, [R.SI, R.DI],  [second]  );
                case 0xFF:
                    return new(pos, first, 2, O.cmp, [R.DI, R.DI],  [second]  );
            }
            return null;
        }
    }
}
