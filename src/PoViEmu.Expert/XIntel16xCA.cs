﻿// ReSharper disable InconsistentNaming

using System.IO;
using PoViEmu.Core.Machine.Core;
using O = PoViEmu.Core.Machine.Ops.OpCode;

namespace PoViEmu.Expert
{
    internal static class Intel16xCA
    {
        internal static Instruction? Parse(Stream s, byte[] buff, long pos, byte first)
        {
            var retfA = s.NextShort(buff);
            return new(pos, first, 3, O.retf, [retfA]);
        }
    }
}
