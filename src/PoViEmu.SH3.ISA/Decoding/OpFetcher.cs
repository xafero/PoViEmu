using System.Collections.Generic;
using PoViEmu.SH3.ISA.Ops;

namespace PoViEmu.SH3.ISA.Decoding
{
    public static class OpFetcher
    {
        public static IEnumerable<BaseOperand> GetOps(this Instruction ins)
        {
            var ops = ins.Args;
            return ops;
        }
    }
}