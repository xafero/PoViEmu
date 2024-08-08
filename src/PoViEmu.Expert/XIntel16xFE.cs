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

namespace PoViEmu.Expert
{
    internal static class Intel16xFE
    {
        internal static Instruction? Parse(Stream s, byte[] buff, long pos, byte first)
        {
            var second = s.NextByte();
            switch (second)
            {
                case 0x00:
                    return new(pos, first, 2, O.inc, [M.@byte.On(R.BX.Plus(R.SI))]);
                case 0x01:
                    return new(pos, first, 2, O.inc, [M.@byte.On(R.BX.Plus(R.DI))]);
                case 0x02:
                    return new(pos, first, 2, O.inc, [M.@byte.On(R.BP.Plus(R.SI))]);
                case 0x03:
                    return new(pos, first, 2, O.inc, [M.@byte.On(R.BP.Plus(R.DI))]);
                case 0x05:
                    return new(pos, first, 2, O.inc, [M.@byte.On(R.DI.Box())]);
                case 0x06:
                    break;
                case 0x07:
                    return new(pos, first, 2, O.inc, [M.@byte.On(R.BX.Box())]);
                case 0x08:
                    return new(pos, first, 2, O.dec, [M.@byte.On(R.BX.Plus(R.SI))]);
                case 0x09:
                    return new(pos, first, 2, O.dec, [M.@byte.On(R.BX.Plus(R.DI))]);
                case 0x0A:
                    return new(pos, first, 2, O.dec, [M.@byte.On(R.BP.Plus(R.SI))]);
                case 0x0B:
                    return new(pos, first, 2, O.dec, [M.@byte.On(R.BP.Plus(R.DI))]);
                case 0x0C:
                    return new(pos, first, 2, O.dec, [M.@byte.On(R.SI.Box())]);
                case 0x0D:
                    return new(pos, first, 2, O.dec, [M.@byte.On(R.DI.Box())]);
                case 0x0E:
                    break;
                case 0x0F:
                    return new(pos, first, 2, O.dec, [M.@byte.On(R.BX.Box())]);
                case 0x40:
                    break;
                case 0x41:
                    break;
                case 0x42:
                    break;
                case 0x43:
                    break;
                case 0x44:
                    break;
                case 0x45:
                    break;
                case 0x46:
                    break;
                case 0x47:
                    break;
                case 0x48:
                    break;
                case 0x49:
                    break;
                case 0x4A:
                    break;
                case 0x4B:
                    break;
                case 0x4C:
                    break;
                case 0x4D:
                    break;
                case 0x4E:
                    break;
                case 0x4F:
                    break;
                case 0x80:
                    break;
                case 0x81:
                    break;
                case 0x82:
                    break;
                case 0x84:
                    break;
                case 0x85:
                    break;
                case 0x86:
                    break;
                case 0x87:
                    break;
                case 0x88:
                    break;
                case 0x8A:
                    break;
                case 0x8B:
                    break;
                case 0x8C:
                    break;
                case 0x8D:
                    break;
                case 0x8E:
                    break;
                case 0xC0:
                    return new(pos, first, 2, O.inc, [R.AL]);
                case 0xC1:
                    return new(pos, first, 2, O.inc, [R.CL]);
                case 0xC2:
                    return new(pos, first, 2, O.inc, [R.DL]);
                case 0xC4:
                    return new(pos, first, 2, O.inc, [R.AH]);
                case 0xC5:
                    return new(pos, first, 2, O.inc, [R.CH]);
                case 0xC6:
                    return new(pos, first, 2, O.inc, [R.DH]);
                case 0xC7:
                    return new(pos, first, 2, O.inc, [R.BH]);
                case 0xC8:
                    return new(pos, first, 2, O.dec, [R.AL]);
                case 0xC9:
                    return new(pos, first, 2, O.dec, [R.CL]);
                case 0xCA:
                    return new(pos, first, 2, O.dec, [R.DL]);
                case 0xCC:
                    return new(pos, first, 2, O.dec, [R.AH]);
                case 0xCD:
                    return new(pos, first, 2, O.dec, [R.CH]);
                case 0xCE:
                    return new(pos, first, 2, O.dec, [R.DH]);
                case 0xCF:
                    return new(pos, first, 2, O.dec, [R.BH]);
            }
            return null;
        }
    }
}
