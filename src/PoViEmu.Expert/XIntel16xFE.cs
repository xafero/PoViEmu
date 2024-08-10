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
            var second = s.NextByte(buff);
            switch (second)
            {
                case 0x00:
                    return new(pos, first, 2, O.inc, [M.@byte.On(R.BX.Plus(R.SI))], [second]);
                case 0x01:
                    return new(pos, first, 2, O.inc, [M.@byte.On(R.BX.Plus(R.DI))], [second]);
                case 0x02:
                    return new(pos, first, 2, O.inc, [M.@byte.On(R.BP.Plus(R.SI))], [second]);
                case 0x03:
                    return new(pos, first, 2, O.inc, [M.@byte.On(R.BP.Plus(R.DI))], [second]);
                case 0x05:
                    return new(pos, first, 2, O.inc, [M.@byte.On(R.DI.Box())], [second]);
                case 0x06:
                case 0x0E:
                case 0x40:
                case 0x41:
                case 0x42:
                case 0x43:
                case 0x44:
                case 0x45:
                case 0x46:
                case 0x47:
                case 0x48:
                case 0x49:
                case 0x4A:
                case 0x4B:
                case 0x4C:
                case 0x4D:
                case 0x4E:
                case 0x4F:
                case 0x80:
                case 0x81:
                case 0x82:
                case 0x84:
                case 0x85:
                case 0x86:
                case 0x87:
                case 0x88:
                case 0x8A:
                case 0x8B:
                case 0x8C:
                case 0x8D:
                case 0x8E:
                    break;
                case 0x07:
                    return new(pos, first, 2, O.inc, [M.@byte.On(R.BX.Box())], [second]);
                case 0x08:
                    return new(pos, first, 2, O.dec, [M.@byte.On(R.BX.Plus(R.SI))], [second]);
                case 0x09:
                    return new(pos, first, 2, O.dec, [M.@byte.On(R.BX.Plus(R.DI))], [second]);
                case 0x0A:
                    return new(pos, first, 2, O.dec, [M.@byte.On(R.BP.Plus(R.SI))], [second]);
                case 0x0B:
                    return new(pos, first, 2, O.dec, [M.@byte.On(R.BP.Plus(R.DI))], [second]);
                case 0x0C:
                    return new(pos, first, 2, O.dec, [M.@byte.On(R.SI.Box())], [second]);
                case 0x0D:
                    return new(pos, first, 2, O.dec, [M.@byte.On(R.DI.Box())], [second]);
                case 0x0F:
                    return new(pos, first, 2, O.dec, [M.@byte.On(R.BX.Box())], [second]);
                case 0xC0:
                    return new(pos, first, 2, O.inc, [R.AL], [second]);
                case 0xC1:
                    return new(pos, first, 2, O.inc, [R.CL], [second]);
                case 0xC2:
                    return new(pos, first, 2, O.inc, [R.DL], [second]);
                case 0xC4:
                    return new(pos, first, 2, O.inc, [R.AH], [second]);
                case 0xC5:
                    return new(pos, first, 2, O.inc, [R.CH], [second]);
                case 0xC6:
                    return new(pos, first, 2, O.inc, [R.DH], [second]);
                case 0xC7:
                    return new(pos, first, 2, O.inc, [R.BH], [second]);
                case 0xC8:
                    return new(pos, first, 2, O.dec, [R.AL], [second]);
                case 0xC9:
                    return new(pos, first, 2, O.dec, [R.CL], [second]);
                case 0xCA:
                    return new(pos, first, 2, O.dec, [R.DL], [second]);
                case 0xCC:
                    return new(pos, first, 2, O.dec, [R.AH], [second]);
                case 0xCD:
                    return new(pos, first, 2, O.dec, [R.CH], [second]);
                case 0xCE:
                    return new(pos, first, 2, O.dec, [R.DH], [second]);
                case 0xCF:
                    return new(pos, first, 2, O.dec, [R.BH], [second]);
            }
            return null;
        }
    }
}
