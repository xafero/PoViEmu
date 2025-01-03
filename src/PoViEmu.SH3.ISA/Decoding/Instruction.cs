// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming

using System.Linq;
using PoViEmu.SH3.ISA.Core;
using PoViEmu.SH3.ISA.Ops;

namespace PoViEmu.SH3.ISA.Decoding
{
    public sealed class Instruction
    {
        public Instruction(byte[] bytes, Mnemonic code, params BaseOperand[] args)
        {
            Bytes = bytes;
            Mnemonic = code;
            Args = args;
            IsInvalid = false;
        }

        public bool IsInvalid { get; set; }
        public uint NextIP32 { get; set;}
        public uint IP32 { get; set;}

        public byte[] Bytes { get; }
        public Mnemonic Mnemonic { get; }
        public BaseOperand[] Args { get; }

        public int Length => Bytes.Length;

        public override string ToString()
        {
            var codeTxt = Mnemonic.Name();
            var arg = string.Join(",", Args.Select(a => a.ToString()));
            const string space = "    ";
            return $"{codeTxt}{space}{arg}".Trim();
        }
    }
}