// ReSharper disable InconsistentNaming

using System.IO;
using PoViEmu.Core.Machine.Core;
using PoViEmu.Core.Machine.Decoding;
using O = PoViEmu.Core.Machine.Ops.OpCode;
using R = PoViEmu.Core.Machine.Ops.Register;

namespace PoViEmu.Expert
{
    internal static class Intel16xA1
    {
        internal static Instruction? Parse(Stream s, byte[] buff, long pos, byte first)
        {
            var movA = s.NextShort(buff);
            return new(pos, first, 3, O.mov, [R.AX, movA.Box()]);
        }
    }
}
