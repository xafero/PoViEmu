// ReSharper disable InconsistentNaming

using System.IO;
using PoViEmu.Core.Machine.Core;
using PoViEmu.Core.Machine.Decoding;
using O = PoViEmu.Core.Machine.Ops.OpCode;

namespace PoViEmu.Expert
{
    internal static class Intel16xEA
    {
        internal static Instruction? Parse(Stream s, byte[] buff, long pos, byte first)
        {
            var jmpA = s.NextShort(buff);
            var jmpB = s.NextShort(buff);
            return new(pos, first, 5, O.jmp, [jmpA.ToMem(jmpB)]);
        }
    }
}
