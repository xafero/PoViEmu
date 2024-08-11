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
    internal static class Intel16x8C
    {
        internal static Instruction? Parse(Stream s, byte[] buff, long pos, byte first)
        {
            var second = s.NextByte(buff);
            switch (second)
            {
                case 0x00:
                    return new(pos, first, 2, O.mov, [R.BX.Plus(R.SI), R.ES],  [second]  );
                case 0x01:
                    return new(pos, first, 2, O.mov, [R.BX.Plus(R.DI), R.ES],  [second]  );
                case 0x02:
                    return new(pos, first, 2, O.mov, [R.BP.Plus(R.SI), R.ES],  [second]  );
                case 0x03:
                    return new(pos, first, 2, O.mov, [R.BP.Plus(R.DI), R.ES],  [second]  );
                case 0x05:
                    return new(pos, first, 2, O.mov, [R.DI.Box(), R.ES],  [second]  );
                case 0x06:
                    return new(pos, first, 4, O.mov, [s.NextShort(buff).Box(), R.ES],   [ second ]  );
                case 0x07:
                    return new(pos, first, 2, O.mov, [R.BX.Box(), R.ES],  [second]  );
                case 0x08:
                    return new(pos, first, 2, O.mov, [R.BX.Plus(R.SI), R.CS],  [second]  );
                case 0x09:
                    return new(pos, first, 2, O.mov, [R.BX.Plus(R.DI), R.CS],  [second]  );
                case 0x0A:
                    return new(pos, first, 2, O.mov, [R.BP.Plus(R.SI), R.CS],  [second]  );
                case 0x0B:
                    return new(pos, first, 2, O.mov, [R.BP.Plus(R.DI), R.CS],  [second]  );
                case 0x0C:
                    return new(pos, first, 2, O.mov, [R.SI.Box(), R.CS],  [second]  );
                case 0x0E:
                    return new(pos, first, 4, O.mov, [s.NextShort(buff).Box(), R.CS],   [ second ]  );
                case 0x0F:
                    return new(pos, first, 2, O.mov, [R.BX.Box(), R.CS],  [second]  );
                case 0x11:
                    return new(pos, first, 2, O.mov, [R.BX.Plus(R.DI), R.SS],  [second]  );
                case 0x12:
                    return new(pos, first, 2, O.mov, [R.BP.Plus(R.SI), R.SS],  [second]  );
                case 0x13:
                    return new(pos, first, 2, O.mov, [R.BP.Plus(R.DI), R.SS],  [second]  );
                case 0x14:
                    return new(pos, first, 2, O.mov, [R.SI.Box(), R.SS],  [second]  );
                case 0x15:
                    return new(pos, first, 2, O.mov, [R.DI.Box(), R.SS],  [second]  );
                case 0x16:
                    return new(pos, first, 4, O.mov, [s.NextShort(buff).Box(), R.SS],   [ second ]  );
                case 0x17:
                    return new(pos, first, 2, O.mov, [R.BX.Box(), R.SS],  [second]  );
                case 0x18:
                    return new(pos, first, 2, O.mov, [R.BX.Plus(R.SI), R.DS],  [second]  );
                case 0x19:
                    return new(pos, first, 2, O.mov, [R.BX.Plus(R.DI), R.DS],  [second]  );
                case 0x1A:
                    return new(pos, first, 2, O.mov, [R.BP.Plus(R.SI), R.DS],  [second]  );
                case 0x1B:
                    return new(pos, first, 2, O.mov, [R.BP.Plus(R.DI), R.DS],  [second]  );
                case 0x1C:
                    return new(pos, first, 2, O.mov, [R.SI.Box(), R.DS],  [second]  );
                case 0x1D:
                    return new(pos, first, 2, O.mov, [R.DI.Box(), R.DS],  [second]  );
                case 0x1E:
                    return new(pos, first, 4, O.mov, [s.NextShort(buff).Box(), R.DS],   [ second ]  );
                case 0x1F:
                    return new(pos, first, 2, O.mov, [R.BX.Box(), R.DS],  [second]  );
                case 0x20:
                    return new(pos, first, 2, O.mov, [R.BX.Plus(R.SI), R.FS],  [second]  );
                case 0x21:
                    return new(pos, first, 2, O.mov, [R.BX.Plus(R.DI), R.FS],  [second]  );
                case 0x22:
                    return new(pos, first, 2, O.mov, [R.BP.Plus(R.SI), R.FS],  [second]  );
                case 0x23:
                    return new(pos, first, 2, O.mov, [R.BP.Plus(R.DI), R.FS],  [second]  );
                case 0x24:
                    return new(pos, first, 2, O.mov, [R.SI.Box(), R.FS],  [second]  );
                case 0x25:
                    return new(pos, first, 2, O.mov, [R.DI.Box(), R.FS],  [second]  );
                case 0x26:
                    return new(pos, first, 4, O.mov, [s.NextShort(buff).Box(), R.FS],   [ second ]  );
                case 0x27:
                    return new(pos, first, 2, O.mov, [R.BX.Box(), R.FS],  [second]  );
                case 0x28:
                    return new(pos, first, 2, O.mov, [R.BX.Plus(R.SI), R.GS],  [second]  );
                case 0x29:
                    return new(pos, first, 2, O.mov, [R.BX.Plus(R.DI), R.GS],  [second]  );
                case 0x2B:
                    return new(pos, first, 2, O.mov, [R.BP.Plus(R.DI), R.GS],  [second]  );
                case 0x2C:
                    return new(pos, first, 2, O.mov, [R.SI.Box(), R.GS],  [second]  );
                case 0x2D:
                    return new(pos, first, 2, O.mov, [R.DI.Box(), R.GS],  [second]  );
                case 0x2E:
                    return new(pos, first, 4, O.mov, [s.NextShort(buff).Box(), R.GS],   [ second ]  );
                case 0x2F:
                    return new(pos, first, 2, O.mov, [R.BX.Box(), R.GS],  [second]  );
                case 0x30:
                    return new(pos, first, 2, O.mov, [R.BX.Plus(R.SI), R.Segr6],  [second]  );
                case 0x31:
                    return new(pos, first, 2, O.mov, [R.BX.Plus(R.DI), R.Segr6],  [second]  );
                case 0x32:
                    return new(pos, first, 2, O.mov, [R.BP.Plus(R.SI), R.Segr6],  [second]  );
                case 0x33:
                    return new(pos, first, 2, O.mov, [R.BP.Plus(R.DI), R.Segr6],  [second]  );
                case 0x34:
                    return new(pos, first, 2, O.mov, [R.SI.Box(), R.Segr6],  [second]  );
                case 0x35:
                    return new(pos, first, 2, O.mov, [R.DI.Box(), R.Segr6],  [second]  );
                case 0x36:
                    return new(pos, first, 4, O.mov, [s.NextShort(buff).Box(), R.Segr6],   [ second ]  );
                case 0x37:
                    return new(pos, first, 2, O.mov, [R.BX.Box(), R.Segr6],  [second]  );
                case 0x38:
                    return new(pos, first, 2, O.mov, [R.BX.Plus(R.SI), R.Segr7],  [second]  );
                case 0x39:
                    return new(pos, first, 2, O.mov, [R.BX.Plus(R.DI), R.Segr7],  [second]  );
                case 0x3A:
                    return new(pos, first, 2, O.mov, [R.BP.Plus(R.SI), R.Segr7],  [second]  );
                case 0x3B:
                    return new(pos, first, 2, O.mov, [R.BP.Plus(R.DI), R.Segr7],  [second]  );
                case 0x3C:
                    return new(pos, first, 2, O.mov, [R.SI.Box(), R.Segr7],  [second]  );
                case 0x3D:
                    return new(pos, first, 2, O.mov, [R.DI.Box(), R.Segr7],  [second]  );
                case 0x3E:
                    return new(pos, first, 4, O.mov, [s.NextShort(buff).Box(), R.Segr7],   [ second ]  );
                case 0x40:
                    return new(pos, first, 3, O.mov, [R.BX.Plus(R.SI).Signed(s.NextByte(buff)), R.ES],   [ second ]  );
                case 0x41:
                    return new(pos, first, 3, O.mov, [R.BX.Plus(R.DI).Signed(s.NextByte(buff)), R.ES],   [ second ]  );
                case 0x42:
                    return new(pos, first, 3, O.mov, [R.BP.Plus(R.SI).Signed(s.NextByte(buff)), R.ES],   [ second ]  );
                case 0x43:
                    return new(pos, first, 3, O.mov, [R.BP.Plus(R.DI).Signed(s.NextByte(buff)), R.ES],   [ second ]  );
                case 0x44:
                    return new(pos, first, 3, O.mov, [R.SI.Signed(s.NextByte(buff)), R.ES],   [ second ]  );
                case 0x45:
                    return new(pos, first, 3, O.mov, [R.DI.Signed(s.NextByte(buff)), R.ES],   [ second ]  );
                case 0x46:
                    return new(pos, first, 3, O.mov, [R.BP.Signed(s.NextByte(buff)), R.ES],   [ second ]  );
                case 0x47:
                    return new(pos, first, 3, O.mov, [R.BX.Minus(s.NextByte(buff)), R.ES],   [ second ]  );
                case 0x48:
                    return new(pos, first, 3, O.mov, [R.BX.Plus(R.SI).Minus(s.NextByte(buff)), R.CS],   [ second ]  );
                case 0x49:
                    return new(pos, first, 3, O.mov, [R.BX.Plus(R.DI).Signed(s.NextByte(buff)), R.CS],   [ second ]  );
                case 0x4B:
                    return new(pos, first, 3, O.mov, [R.BP.Plus(R.DI).Signed(s.NextByte(buff)), R.CS],   [ second ]  );
                case 0x4C:
                    return new(pos, first, 3, O.mov, [R.SI.Signed(s.NextByte(buff)), R.CS],   [ second ]  );
                case 0x4D:
                    return new(pos, first, 3, O.mov, [R.DI.Signed(s.NextByte(buff)), R.CS],   [ second ]  );
                case 0x4E:
                    return new(pos, first, 3, O.mov, [R.BP.Signed(s.NextByte(buff)), R.CS],   [ second ]  );
                case 0x4F:
                    return new(pos, first, 3, O.mov, [R.BX.Signed(s.NextByte(buff)), R.CS],   [ second ]  );
                case 0x50:
                    return new(pos, first, 3, O.mov, [R.BX.Plus(R.SI).Minus(s.NextByte(buff)), R.SS],   [ second ]  );
                case 0x51:
                    return new(pos, first, 3, O.mov, [R.BX.Plus(R.DI).Minus(s.NextByte(buff)), R.SS],   [ second ]  );
                case 0x52:
                    return new(pos, first, 3, O.mov, [R.BP.Plus(R.SI).Signed(s.NextByte(buff)), R.SS],   [ second ]  );
                case 0x53:
                    return new(pos, first, 3, O.mov, [R.BP.Plus(R.DI).Signed(s.NextByte(buff)), R.SS],   [ second ]  );
                case 0x54:
                    return new(pos, first, 3, O.mov, [R.SI.Signed(s.NextByte(buff)), R.SS],   [ second ]  );
                case 0x55:
                    return new(pos, first, 3, O.mov, [R.DI.Signed(s.NextByte(buff)), R.SS],   [ second ]  );
                case 0x56:
                    return new(pos, first, 3, O.mov, [R.BP.Signed(s.NextByte(buff)), R.SS],   [ second ]  );
                case 0x57:
                    return new(pos, first, 3, O.mov, [R.BX.Signed(s.NextByte(buff)), R.SS],   [ second ]  );
                case 0x59:
                    return new(pos, first, 3, O.mov, [R.BX.Plus(R.DI).Minus(s.NextByte(buff)), R.DS],   [ second ]  );
                case 0x5B:
                    return new(pos, first, 3, O.mov, [R.BP.Plus(R.DI).Signed(s.NextByte(buff)), R.DS],   [ second ]  );
                case 0x5C:
                    return new(pos, first, 3, O.mov, [R.SI.Signed(s.NextByte(buff)), R.DS],   [ second ]  );
                case 0x5D:
                    return new(pos, first, 3, O.mov, [R.DI.Signed(s.NextByte(buff)), R.DS],   [ second ]  );
                case 0x5F:
                    return new(pos, first, 3, O.mov, [R.BX.Signed(s.NextByte(buff)), R.DS],   [ second ]  );
                case 0x61:
                    return new(pos, first, 3, O.mov, [R.BX.Plus(R.DI).Signed(s.NextByte(buff)), R.FS],   [ second ]  );
                case 0x62:
                    return new(pos, first, 3, O.mov, [R.BP.Plus(R.SI).Signed(s.NextByte(buff)), R.FS],   [ second ]  );
                case 0x64:
                    return new(pos, first, 3, O.mov, [R.SI.Minus(s.NextByte(buff)), R.FS],   [ second ]  );
                case 0x65:
                    return new(pos, first, 3, O.mov, [R.DI.Signed(s.NextByte(buff)), R.FS],   [ second ]  );
                case 0x66:
                    return new(pos, first, 3, O.mov, [R.BP.Minus(s.NextByte(buff)), R.FS],   [ second ]  );
                case 0x68:
                    return new(pos, first, 3, O.mov, [R.BX.Plus(R.SI).Signed(s.NextByte(buff)), R.GS],   [ second ]  );
                case 0x69:
                    return new(pos, first, 3, O.mov, [R.BX.Plus(R.DI).Minus(s.NextByte(buff)), R.GS],   [ second ]  );
                case 0x6A:
                    return new(pos, first, 3, O.mov, [R.BP.Plus(R.SI).Signed(s.NextByte(buff)), R.GS],   [ second ]  );
                case 0x6B:
                    return new(pos, first, 3, O.mov, [R.BP.Plus(R.DI).Signed(s.NextByte(buff)), R.GS],   [ second ]  );
                case 0x6C:
                    return new(pos, first, 3, O.mov, [R.SI.Minus(s.NextByte(buff)), R.GS],   [ second ]  );
                case 0x6D:
                    return new(pos, first, 3, O.mov, [R.DI.Minus(s.NextByte(buff)), R.GS],   [ second ]  );
                case 0x6E:
                    return new(pos, first, 3, O.mov, [R.BP.Signed(s.NextByte(buff)), R.GS],   [ second ]  );
                case 0x6F:
                    return new(pos, first, 3, O.mov, [R.BX.Minus(s.NextByte(buff)), R.GS],   [ second ]  );
                case 0x70:
                    return new(pos, first, 3, O.mov, [R.BX.Plus(R.SI).Signed(s.NextByte(buff)), R.Segr6],   [ second ]  );
                case 0x71:
                    return new(pos, first, 3, O.mov, [R.BX.Plus(R.DI).Minus(s.NextByte(buff)), R.Segr6],   [ second ]  );
                case 0x72:
                    return new(pos, first, 3, O.mov, [R.BP.Plus(R.SI).Minus(s.NextByte(buff)), R.Segr6],   [ second ]  );
                case 0x73:
                    return new(pos, first, 3, O.mov, [R.BP.Plus(R.DI).Signed(s.NextByte(buff)), R.Segr6],   [ second ]  );
                case 0x74:
                    return new(pos, first, 3, O.mov, [R.SI.Signed(s.NextByte(buff)), R.Segr6],   [ second ]  );
                case 0x75:
                    return new(pos, first, 3, O.mov, [R.DI.Signed(s.NextByte(buff)), R.Segr6],   [ second ]  );
                case 0x76:
                    return new(pos, first, 3, O.mov, [R.BP.Signed(s.NextByte(buff)), R.Segr6],   [ second ]  );
                case 0x77:
                    return new(pos, first, 3, O.mov, [R.BX.Signed(s.NextByte(buff)), R.Segr6],   [ second ]  );
                case 0x78:
                    return new(pos, first, 3, O.mov, [R.BX.Plus(R.SI).Signed(s.NextByte(buff)), R.Segr7],   [ second ]  );
                case 0x79:
                    return new(pos, first, 3, O.mov, [R.BX.Plus(R.DI).Signed(s.NextByte(buff)), R.Segr7],   [ second ]  );
                case 0x7A:
                    return new(pos, first, 3, O.mov, [R.BP.Plus(R.SI).Signed(s.NextByte(buff)), R.Segr7],   [ second ]  );
                case 0x7B:
                    return new(pos, first, 3, O.mov, [R.BP.Plus(R.DI).Signed(s.NextByte(buff)), R.Segr7],   [ second ]  );
                case 0x7D:
                    return new(pos, first, 3, O.mov, [R.DI.Signed(s.NextByte(buff)), R.Segr7],   [ second ]  );
                case 0x7E:
                    return new(pos, first, 3, O.mov, [R.BP.Signed(s.NextByte(buff)), R.Segr7],   [ second ]  );
                case 0x81:
                    return new(pos, first, 4, O.mov, [R.BX.Plus(R.DI).Signed(s.NextShort(buff)), R.ES],   [ second ]  );
                case 0x82:
                    return new(pos, first, 4, O.mov, [R.BP.Plus(R.SI).Signed(s.NextShort(buff)), R.ES],   [ second ]  );
                case 0x83:
                    return new(pos, first, 4, O.mov, [R.BP.Plus(R.DI).Signed(s.NextShort(buff)), R.ES],   [ second ]  );
                case 0x84:
                    return new(pos, first, 4, O.mov, [R.SI.Signed(s.NextShort(buff)), R.ES],   [ second ]  );
                case 0x85:
                    return new(pos, first, 4, O.mov, [R.DI.Signed(s.NextShort(buff)), R.ES],   [ second ]  );
                case 0x86:
                    return new(pos, first, 4, O.mov, [R.BP.Signed(s.NextShort(buff)), R.ES],   [ second ]  );
                case 0x87:
                    return new(pos, first, 4, O.mov, [R.BX.Signed(s.NextShort(buff)), R.ES],   [ second ]  );
                case 0x88:
                    return new(pos, first, 4, O.mov, [R.BX.Plus(R.SI).Signed(s.NextShort(buff)), R.CS],   [ second ]  );
                case 0x89:
                    return new(pos, first, 4, O.mov, [R.BX.Plus(R.DI).Signed(s.NextShort(buff)), R.CS],   [ second ]  );
                case 0x8A:
                    return new(pos, first, 4, O.mov, [R.BP.Plus(R.SI).Signed(s.NextShort(buff)), R.CS],   [ second ]  );
                case 0x8B:
                    return new(pos, first, 4, O.mov, [R.BP.Plus(R.DI).Signed(s.NextShort(buff)), R.CS],   [ second ]  );
                case 0x8C:
                    return new(pos, first, 4, O.mov, [R.SI.Signed(s.NextShort(buff)), R.CS],   [ second ]  );
                case 0x8D:
                    return new(pos, first, 4, O.mov, [R.DI.Signed(s.NextShort(buff)), R.CS],   [ second ]  );
                case 0x8E:
                    return new(pos, first, 4, O.mov, [R.BP.Signed(s.NextShort(buff)), R.CS],   [ second ]  );
                case 0x91:
                    return new(pos, first, 4, O.mov, [R.BX.Plus(R.DI).Signed(s.NextShort(buff)), R.SS],   [ second ]  );
                case 0x93:
                    return new(pos, first, 4, O.mov, [R.BP.Plus(R.DI).Signed(s.NextShort(buff)), R.SS],   [ second ]  );
                case 0x94:
                    return new(pos, first, 4, O.mov, [R.SI.Signed(s.NextShort(buff)), R.SS],   [ second ]  );
                case 0x96:
                    return new(pos, first, 4, O.mov, [R.BP.Signed(s.NextShort(buff)), R.SS],   [ second ]  );
                case 0x97:
                    return new(pos, first, 4, O.mov, [R.BX.Signed(s.NextShort(buff)), R.SS],   [ second ]  );
                case 0x98:
                    return new(pos, first, 4, O.mov, [R.BX.Plus(R.SI).Signed(s.NextShort(buff)), R.DS],   [ second ]  );
                case 0x99:
                    return new(pos, first, 4, O.mov, [R.BX.Plus(R.DI).Signed(s.NextShort(buff)), R.DS],   [ second ]  );
                case 0x9A:
                    return new(pos, first, 4, O.mov, [R.BP.Plus(R.SI).Signed(s.NextShort(buff)), R.DS],   [ second ]  );
                case 0x9C:
                    return new(pos, first, 4, O.mov, [R.SI.Signed(s.NextShort(buff)), R.DS],   [ second ]  );
                case 0x9D:
                    return new(pos, first, 4, O.mov, [R.DI.Signed(s.NextShort(buff)), R.DS],   [ second ]  );
                case 0x9E:
                    return new(pos, first, 4, O.mov, [R.BP.Signed(s.NextShort(buff)), R.DS],   [ second ]  );
                case 0x9F:
                    return new(pos, first, 4, O.mov, [R.BX.Signed(s.NextShort(buff)), R.DS],   [ second ]  );
                case 0xA0:
                    return new(pos, first, 4, O.mov, [R.BX.Plus(R.SI).Signed(s.NextShort(buff)), R.FS],   [ second ]  );
                case 0xA1:
                    return new(pos, first, 4, O.mov, [R.BX.Plus(R.DI).Signed(s.NextShort(buff)), R.FS],   [ second ]  );
                case 0xA2:
                    return new(pos, first, 4, O.mov, [R.BP.Plus(R.SI).Signed(s.NextShort(buff)), R.FS],   [ second ]  );
                case 0xA3:
                    return new(pos, first, 4, O.mov, [R.BP.Plus(R.DI).Signed(s.NextShort(buff)), R.FS],   [ second ]  );
                case 0xA4:
                    return new(pos, first, 4, O.mov, [R.SI.Signed(s.NextShort(buff)), R.FS],   [ second ]  );
                case 0xA5:
                    return new(pos, first, 4, O.mov, [R.DI.Signed(s.NextShort(buff)), R.FS],   [ second ]  );
                case 0xA6:
                    return new(pos, first, 4, O.mov, [R.BP.Signed(s.NextShort(buff)), R.FS],   [ second ]  );
                case 0xA7:
                    return new(pos, first, 4, O.mov, [R.BX.Signed(s.NextShort(buff)), R.FS],   [ second ]  );
                case 0xA8:
                    return new(pos, first, 4, O.mov, [R.BX.Plus(R.SI).Signed(s.NextShort(buff)), R.GS],   [ second ]  );
                case 0xA9:
                    return new(pos, first, 4, O.mov, [R.BX.Plus(R.DI).Signed(s.NextShort(buff)), R.GS],   [ second ]  );
                case 0xAA:
                    return new(pos, first, 4, O.mov, [R.BP.Plus(R.SI).Signed(s.NextShort(buff)), R.GS],   [ second ]  );
                case 0xAB:
                    return new(pos, first, 4, O.mov, [R.BP.Plus(R.DI).Signed(s.NextShort(buff)), R.GS],   [ second ]  );
                case 0xAC:
                    return new(pos, first, 4, O.mov, [R.SI.Signed(s.NextShort(buff)), R.GS],   [ second ]  );
                case 0xAD:
                    return new(pos, first, 4, O.mov, [R.DI.Signed(s.NextShort(buff)), R.GS],   [ second ]  );
                case 0xAE:
                    return new(pos, first, 4, O.mov, [R.BP.Signed(s.NextShort(buff)), R.GS],   [ second ]  );
                case 0xAF:
                    return new(pos, first, 4, O.mov, [R.BX.Signed(s.NextShort(buff)), R.GS],   [ second ]  );
                case 0xB0:
                    return new(pos, first, 4, O.mov, [R.BX.Plus(R.SI).Signed(s.NextShort(buff)), R.Segr6],   [ second ]  );
                case 0xB1:
                    return new(pos, first, 4, O.mov, [R.BX.Plus(R.DI).Signed(s.NextShort(buff)), R.Segr6],   [ second ]  );
                case 0xB2:
                    return new(pos, first, 4, O.mov, [R.BP.Plus(R.SI).Signed(s.NextShort(buff)), R.Segr6],   [ second ]  );
                case 0xB3:
                    return new(pos, first, 4, O.mov, [R.BP.Plus(R.DI).Signed(s.NextShort(buff)), R.Segr6],   [ second ]  );
                case 0xB4:
                    return new(pos, first, 4, O.mov, [R.SI.Signed(s.NextShort(buff)), R.Segr6],   [ second ]  );
                case 0xB5:
                    return new(pos, first, 4, O.mov, [R.DI.Signed(s.NextShort(buff)), R.Segr6],   [ second ]  );
                case 0xB6:
                    return new(pos, first, 4, O.mov, [R.BP.Signed(s.NextShort(buff)), R.Segr6],   [ second ]  );
                case 0xB7:
                    return new(pos, first, 4, O.mov, [R.BX.Signed(s.NextShort(buff)), R.Segr6],   [ second ]  );
                case 0xB8:
                    return new(pos, first, 4, O.mov, [R.BX.Plus(R.SI).Signed(s.NextShort(buff)), R.Segr7],   [ second ]  );
                case 0xB9:
                    return new(pos, first, 4, O.mov, [R.BX.Plus(R.DI).Signed(s.NextShort(buff)), R.Segr7],   [ second ]  );
                case 0xBA:
                    return new(pos, first, 4, O.mov, [R.BP.Plus(R.SI).Signed(s.NextShort(buff)), R.Segr7],   [ second ]  );
                case 0xBB:
                    return new(pos, first, 4, O.mov, [R.BP.Plus(R.DI).Signed(s.NextShort(buff)), R.Segr7],   [ second ]  );
                case 0xBC:
                    return new(pos, first, 4, O.mov, [R.SI.Signed(s.NextShort(buff)), R.Segr7],   [ second ]  );
                case 0xBE:
                    return new(pos, first, 4, O.mov, [R.BP.Signed(s.NextShort(buff)), R.Segr7],   [ second ]  );
                case 0xBF:
                    return new(pos, first, 4, O.mov, [R.BX.Signed(s.NextShort(buff)), R.Segr7],   [ second ]  );
                case 0xC0:
                    return new(pos, first, 2, O.mov, [R.AX, R.ES],  [second]  );
                case 0xC1:
                    return new(pos, first, 2, O.mov, [R.CX, R.ES],  [second]  );
                case 0xC2:
                    return new(pos, first, 2, O.mov, [R.DX, R.ES],  [second]  );
                case 0xC3:
                    return new(pos, first, 2, O.mov, [R.BX, R.ES],  [second]  );
                case 0xC4:
                    return new(pos, first, 2, O.mov, [R.SP, R.ES],  [second]  );
                case 0xC5:
                    return new(pos, first, 2, O.mov, [R.BP, R.ES],  [second]  );
                case 0xC6:
                    return new(pos, first, 2, O.mov, [R.SI, R.ES],  [second]  );
                case 0xC7:
                    return new(pos, first, 2, O.mov, [R.DI, R.ES],  [second]  );
                case 0xC8:
                    return new(pos, first, 2, O.mov, [R.AX, R.CS],  [second]  );
                case 0xC9:
                    return new(pos, first, 2, O.mov, [R.CX, R.CS],  [second]  );
                case 0xCA:
                    return new(pos, first, 2, O.mov, [R.DX, R.CS],  [second]  );
                case 0xCB:
                    return new(pos, first, 2, O.mov, [R.BX, R.CS],  [second]  );
                case 0xCC:
                    return new(pos, first, 2, O.mov, [R.SP, R.CS],  [second]  );
                case 0xCD:
                    return new(pos, first, 2, O.mov, [R.BP, R.CS],  [second]  );
                case 0xCE:
                    return new(pos, first, 2, O.mov, [R.SI, R.CS],  [second]  );
                case 0xCF:
                    return new(pos, first, 2, O.mov, [R.DI, R.CS],  [second]  );
                case 0xD0:
                    return new(pos, first, 2, O.mov, [R.AX, R.SS],  [second]  );
                case 0xD1:
                    return new(pos, first, 2, O.mov, [R.CX, R.SS],  [second]  );
                case 0xD2:
                    return new(pos, first, 2, O.mov, [R.DX, R.SS],  [second]  );
                case 0xD3:
                    return new(pos, first, 2, O.mov, [R.BX, R.SS],  [second]  );
                case 0xD4:
                    return new(pos, first, 2, O.mov, [R.SP, R.SS],  [second]  );
                case 0xD5:
                    return new(pos, first, 2, O.mov, [R.BP, R.SS],  [second]  );
                case 0xD6:
                    return new(pos, first, 2, O.mov, [R.SI, R.SS],  [second]  );
                case 0xD7:
                    return new(pos, first, 2, O.mov, [R.DI, R.SS],  [second]  );
                case 0xD8:
                    return new(pos, first, 2, O.mov, [R.AX, R.DS],  [second]  );
                case 0xD9:
                    return new(pos, first, 2, O.mov, [R.CX, R.DS],  [second]  );
                case 0xDA:
                    return new(pos, first, 2, O.mov, [R.DX, R.DS],  [second]  );
                case 0xDB:
                    return new(pos, first, 2, O.mov, [R.BX, R.DS],  [second]  );
                case 0xDC:
                    return new(pos, first, 2, O.mov, [R.SP, R.DS],  [second]  );
                case 0xDD:
                    return new(pos, first, 2, O.mov, [R.BP, R.DS],  [second]  );
                case 0xDE:
                    return new(pos, first, 2, O.mov, [R.SI, R.DS],  [second]  );
                case 0xDF:
                    return new(pos, first, 2, O.mov, [R.DI, R.DS],  [second]  );
                case 0xE0:
                    return new(pos, first, 2, O.mov, [R.AX, R.FS],  [second]  );
                case 0xE1:
                    return new(pos, first, 2, O.mov, [R.CX, R.FS],  [second]  );
                case 0xE2:
                    return new(pos, first, 2, O.mov, [R.DX, R.FS],  [second]  );
                case 0xE3:
                    return new(pos, first, 2, O.mov, [R.BX, R.FS],  [second]  );
                case 0xE4:
                    return new(pos, first, 2, O.mov, [R.SP, R.FS],  [second]  );
                case 0xE5:
                    return new(pos, first, 2, O.mov, [R.BP, R.FS],  [second]  );
                case 0xE6:
                    return new(pos, first, 2, O.mov, [R.SI, R.FS],  [second]  );
                case 0xE7:
                    return new(pos, first, 2, O.mov, [R.DI, R.FS],  [second]  );
                case 0xE8:
                    return new(pos, first, 2, O.mov, [R.AX, R.GS],  [second]  );
                case 0xE9:
                    return new(pos, first, 2, O.mov, [R.CX, R.GS],  [second]  );
                case 0xEA:
                    return new(pos, first, 2, O.mov, [R.DX, R.GS],  [second]  );
                case 0xEC:
                    return new(pos, first, 2, O.mov, [R.SP, R.GS],  [second]  );
                case 0xED:
                    return new(pos, first, 2, O.mov, [R.BP, R.GS],  [second]  );
                case 0xEF:
                    return new(pos, first, 2, O.mov, [R.DI, R.GS],  [second]  );
                case 0xF0:
                    return new(pos, first, 2, O.mov, [R.AX, R.Segr6],  [second]  );
                case 0xF1:
                    return new(pos, first, 2, O.mov, [R.CX, R.Segr6],  [second]  );
                case 0xF2:
                    return new(pos, first, 2, O.mov, [R.DX, R.Segr6],  [second]  );
                case 0xF3:
                    return new(pos, first, 2, O.mov, [R.BX, R.Segr6],  [second]  );
                case 0xF4:
                    return new(pos, first, 2, O.mov, [R.SP, R.Segr6],  [second]  );
                case 0xF5:
                    return new(pos, first, 2, O.mov, [R.BP, R.Segr6],  [second]  );
                case 0xF6:
                    return new(pos, first, 2, O.mov, [R.SI, R.Segr6],  [second]  );
                case 0xF7:
                    return new(pos, first, 2, O.mov, [R.DI, R.Segr6],  [second]  );
                case 0xF8:
                    return new(pos, first, 2, O.mov, [R.AX, R.Segr7],  [second]  );
                case 0xF9:
                    return new(pos, first, 2, O.mov, [R.CX, R.Segr7],  [second]  );
                case 0xFA:
                    return new(pos, first, 2, O.mov, [R.DX, R.Segr7],  [second]  );
                case 0xFB:
                    return new(pos, first, 2, O.mov, [R.BX, R.Segr7],  [second]  );
                case 0xFC:
                    return new(pos, first, 2, O.mov, [R.SP, R.Segr7],  [second]  );
                case 0xFD:
                    return new(pos, first, 2, O.mov, [R.BP, R.Segr7],  [second]  );
                case 0xFE:
                    return new(pos, first, 2, O.mov, [R.SI, R.Segr7],  [second]  );
                case 0xFF:
                    return new(pos, first, 2, O.mov, [R.DI, R.Segr7],  [second]  );
            }
            return null;
        }
    }
}
