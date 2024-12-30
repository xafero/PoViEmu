// ReSharper disable UnusedMember.Global

using System.Linq;

namespace PoViEmu.Core.Risc
{
    public sealed class Instruction
    {
        public Instruction(byte[] bytes, Mnemonic code, params BaseOperand[] args)
        {
            Bytes = bytes;
            Code = code;
            Args = args;
        }

        public byte[] Bytes { get; init; }
        public Mnemonic Code { get; init; }
        public BaseOperand[] Args { get; set; }

        public int Length => Bytes.Length;

        public override string ToString()
        {
            var codeTxt = Code.Name();
            var arg = string.Join(",", Args.Select(a => a.ToString()));
            const string space = "    ";
            return $"{codeTxt}{space}{arg}".Trim();
        }
    }
}