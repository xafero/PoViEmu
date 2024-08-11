// ReSharper disable InconsistentNaming

using System.IO;
using PoViEmu.Core.Machine.Core;
using O = PoViEmu.Core.Machine.Ops.OpCode;
using R = PoViEmu.Core.Machine.Ops.Register;

namespace PoViEmu.Expert
{
    internal static class Intel16x0D
    {
        internal static Instruction? Parse(Stream s, byte[] buff, long pos, byte first)
        {
            var orA = s.NextShort(buff);
            return new(pos, first, 3, O.or, [R.AX, orA]);
        }
    }
}
