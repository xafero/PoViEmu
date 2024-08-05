using System;
using System.IO;
using System.Linq;
using PoViEmu.Core.Machine;
using Xunit;

namespace PoViEmu.Tests
{
    public class InstrTest
    {
        [Theory]
        [InlineData(0x00, 0xFE)]
        [InlineData(0x01, 0xFE)]
        [InlineData(0x02, 0xFE)]
        [InlineData(0x03, 0xFE)]
        [InlineData(0x06)]
        [InlineData(0x07)]
        [InlineData(0x08, 0xFE)]
        [InlineData(0x09, 0xFE)]
        [InlineData(0x0A, 0xFE)]
        [InlineData(0x0B, 0xFE)]
        [InlineData(0x0E)]
        [InlineData(0x10, 0xFE)]
        [InlineData(0x11, 0xFE)]
        [InlineData(0x13, 0xFE)]
        [InlineData(0x16)]
        [InlineData(0x17)]
        [InlineData(0x18, 0xFE)]
        [InlineData(0x19, 0xFE)]
        [InlineData(0x1A, 0xFE)]
        [InlineData(0x1B, 0xFE)]
        [InlineData(0x1E)]
        [InlineData(0x1F)]
        [InlineData(0x20, 0xFE)]
        [InlineData(0x21, 0xFE)]
        [InlineData(0x22, 0xFE)]
        [InlineData(0x23, 0xFE)]
        [InlineData(0x27)]
        [InlineData(0x28, 0xFE)]
        [InlineData(0x29, 0xFE)]
        [InlineData(0x2B, 0xFE)]
        [InlineData(0x2F)]
        [InlineData(0x30, 0xFE)]
        [InlineData(0x31, 0xFE)]
        [InlineData(0x32, 0xFE)]
        [InlineData(0x33, 0xFE)]
        [InlineData(0x37)]
        [InlineData(0x38, 0xFE)]
        [InlineData(0x3F)]
        [InlineData(0x40)]
        [InlineData(0x41)]
        [InlineData(0x42)]
        [InlineData(0x43)]
        [InlineData(0x44)]
        [InlineData(0x45)]
        [InlineData(0x46)]
        [InlineData(0x47)]
        public void ShouldCheck(params int[] rawBytes)
        {
            var bytes = rawBytes.Select(b => (byte)b).ToArray();
            var expected = NasmTool.DisassembleNasm(bytes);
            var instr = new MemoryStream(bytes).Disassemble().ToArray();
            var mis = instr.Where(i => i.Bytes.Length != i.Size).ToArray();
            if (mis.Length >= 1)
                throw new InvalidOperationException($"{string.Join("|",
                    mis.Select(m => (m.Size, m.Bytes.Length)))} {mis.ToText()}");
            var actual = instr.ToText();
            TestTool.Compare(expected, actual);
        }
    }
}