// ReSharper disable InconsistentNaming

using System.IO;
using PoViEmu.Core.Machine.Core;
using PoViEmu.Core.Machine.Decoding;
using O = PoViEmu.Core.Machine.Ops.OpCode;

namespace PoViEmu.Expert
{
    internal static class Intel16x9A
    {
        internal static Instruction? Parse(Stream s, byte[] buff, long pos, byte first)
        {
            var callA = s.NextShort(buff);
            var callB = s.NextShort(buff);
            return new(pos, first, 5, O.call, [callA.ToMem(callB)]);
        }
    }
}
