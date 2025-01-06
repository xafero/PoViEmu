using PoViEmu.SH3.ISA.Core;
using PoViEmu.SH3.ISA.Decoding;

namespace PoViEmu.Tests.ISA.Util
{
    internal sealed class MyParser : ICodeParser
    {
        public string Parse(byte[] bytes)
        {
            var instr = Parser.Parse(new ArrayReader(bytes));
            var text = instr.ToString();
            return text;
        }
    }
}