using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PoViEmu.Core.Machine.Core;

namespace PoViEmu.Core.Machine
{
    public static class Intel16Util
    {
        public static Instruction[] Disassemble(this Stream stream)
        {
            var buffer = new byte[1];
            var instr = Intel16.Disassemble(stream, buffer).ToArray();
            return instr;
        }

        public static string ToText(this IEnumerable<Instruction> instr)
        {
            var text = string.Join(Environment.NewLine, instr.Select(i => i.ToString()));
            return text;
        }
    }
}