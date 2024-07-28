using System;
using System.IO;
using System.Linq;

namespace PoViEmu.Core.Machine
{
    public static class Intel16Util
    {
        public static string Disassemble(this Stream stream)
        {
            var buffer = new byte[1];
            var instr = Intel16.Disassemble(stream, buffer).ToArray();

            var text = string.Join(Environment.NewLine, instr.Select(i => i.ToString()));
            return text;
        }
    }
}