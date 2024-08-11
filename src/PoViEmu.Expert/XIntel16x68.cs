// ReSharper disable InconsistentNaming

using System.IO;
using PoViEmu.Core.Machine.Core;
using PoViEmu.Core.Machine.Decoding;
using O = PoViEmu.Core.Machine.Ops.OpCode;
using M = PoViEmu.Core.Machine.Ops.Modifier;

namespace PoViEmu.Expert
{
    internal static class Intel16x68
    {
        internal static Instruction? Parse(Stream s, byte[] buff, long pos, byte first)
        {
            var posA = s.NextShort(buff);
            return new(pos, first, 3, O.push, [M.word.On(posA)]);
        }
    }
}
