// ReSharper disable InconsistentNaming

using System.IO;
using PoViEmu.Core.Machine.Core;
using PoViEmu.Core.Machine.Decoding;
using O = PoViEmu.Core.Machine.Ops.OpCode;
using M = PoViEmu.Core.Machine.Ops.Modifier;

namespace PoViEmu.Expert
{
    internal static class Intel16xEB
    {
        internal static Instruction? Parse(Stream s, byte[] buff, long pos, byte first)
        {
            var second = s.NextByte(buff);
            return new(pos, first, 2, O.jmp, [M.@short.On(second.Skip())]);
        }
    }
}
