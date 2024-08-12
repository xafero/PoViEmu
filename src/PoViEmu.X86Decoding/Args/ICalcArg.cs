using PoViEmu.X86Decoding.Core;

namespace PoViEmu.X86Decoding.Args
{
    public interface ICalcArg
    {
        Instruction? Parent { set; }
    }
}