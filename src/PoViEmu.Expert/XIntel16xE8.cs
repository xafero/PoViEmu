// ReSharper disable InconsistentNaming

using System.IO;
using PoViEmu.Core.Machine.Core;
using PoViEmu.Core.Machine.Decoding;
using O = PoViEmu.Core.Machine.Ops.OpCode;

namespace PoViEmu.Expert
{
    internal static class Intel16xE8
    {
        internal static Instruction? Parse(Stream s, byte[] buff, long pos, byte first)
        {
            var callA = s.NextShort(buff).Skip();
            return new(pos, first, 3, O.call, [callA]);
        }
    }
}
