using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PoViEmu.Core.Machine.Core;

namespace PoViEmu.Expert
{
    public static class XIntel16Util
    {
        public static IEnumerable<Instruction> Disassemble(this Stream stream, int skip = 0)
        {
            var buffer = new byte[6];
            if (skip >= 1)
                stream.Seek(skip, SeekOrigin.Current);
            var startPos = stream.Position;
            var instr = XIntel16.Disassemble(stream, buffer, startPos);
            return instr;
        }
    }
}