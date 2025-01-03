using System;
using System.Linq;
using PoViEmu.SH3.ISA.Decoding;
using PoViEmu.SH3.ISA.Ops;

namespace PoViEmu.SH3.CPU.Errors
{
    public sealed class UnhandledOpcodeException : Exception
    {
        public UnhandledOpcodeException(Instruction instruct, BaseOperand[] ops, string extra)
            : base($"({instruct.Mnemonic}) {instruct}\n" +
                   $"[{string.Join(" ; ", ops.Select(o => o.ToString()))}]\n" +
                   $"{extra}")
        {
        }
    }
}