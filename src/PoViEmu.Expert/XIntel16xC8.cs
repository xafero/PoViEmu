// ReSharper disable InconsistentNaming

using System.IO;
using PoViEmu.Core.Machine.Core;
using O = PoViEmu.Core.Machine.Ops.OpCode;

namespace PoViEmu.Expert
{
    internal static class Intel16xC8
    {
        internal static Instruction? Parse(Stream s, byte[] buff, long pos, byte first)
        {
            var enterA = s.NextShort(buff);
            var enterB = s.NextByte(buff);
            return new(pos, first, 4, O.enter, [enterA, enterB]);
        }
    }
}
