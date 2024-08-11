// ReSharper disable InconsistentNaming

using System.IO;
using PoViEmu.Core.Machine.Core;
using PoViEmu.Core.Machine.Decoding;
using O = PoViEmu.Core.Machine.Ops.OpCode;

namespace PoViEmu.Expert
{
    internal static class Intel16x7B
    {
        internal static Instruction? Parse(Stream s, byte[] buff, long pos, byte first)
        {
            var second = s.NextByte(buff);
            return new(pos, first, 2, O.jpo, [second.Skip()]);
        }
    }
}
