using System;
using System.IO;
using System.Linq;
using PoViEmu.X86Decoding;

namespace PoViEmu.Tests
{
    public abstract class InstrTestBase
    {
        protected void ShouldCheckThis(params int[] rawBytes)
        {
            var bytes = rawBytes.Select(b => (byte)b).ToArray();
            var expected = NasmTool.DisassembleNasm(bytes);
            var instr = new MemoryStream(bytes).Disassemble().ToArray();
            var mis = instr.Where(i => i.Bytes.Length != i.Size).ToArray();

            var actual = instr.ToText();
            TestTool.Compare(expected, actual);

            if (mis.Length >= 1)
                throw new InvalidOperationException($"{string.Join("|",
                    mis.Select(m => (m.Size, m.Bytes.Length)))} {mis.ToText()}");
        }
    }
}