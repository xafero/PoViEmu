// ReSharper disable InconsistentNaming

using System.IO;
using PoViEmu.Core.Machine.Core;
using O = PoViEmu.Core.Machine.Ops.OpCode;

namespace PoViEmu.Expert
{
    internal static class Intel16xC2
    {
        internal static Instruction? Parse(Stream s, byte[] buff, long pos, byte first)
        {
            var retA = s.NextShort(buff);
            return new(pos, first, 3, O.ret, [retA]);
        }
    }
}
