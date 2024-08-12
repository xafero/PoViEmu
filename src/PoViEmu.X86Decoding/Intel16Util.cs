using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
using PoViEmu.Core.Machine.Core;
using PoViEmu.X86Decoding;

namespace PoViEmu.Expert
{
    public static class Intel16Util
    {
        public static IEnumerable<Instruction> Disassemble(this Stream stream, int skip = 0, bool err = true)
        {
            var buffer = new byte[6];
            if (skip >= 1)
                stream.Seek(skip, SeekOrigin.Current);
            var startPos = stream.Position;
            var instr = Intel16.Disassemble(stream, buffer, startPos, err: err);
            return instr;
        }

        public static string ToText(this IEnumerable<Instruction> instr)
        {
            var text = string.Join(Environment.NewLine, instr.Select(i => i.ToString()));
            return text;
        }
    }
}