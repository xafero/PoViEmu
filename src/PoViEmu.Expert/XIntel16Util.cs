using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PoViEmu.Core.Machine.Core;

namespace PoViEmu.Expert
{
    public static class XIntel16Util
    {
        public static Instruction[] Disassemble(this Stream stream)
        {
            var buffer = new byte[1];
            var instr = XIntel16.Disassemble(stream, buffer).ToArray();
            return instr;
        }
    }
}