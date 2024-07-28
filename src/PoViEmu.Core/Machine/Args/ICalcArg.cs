using PoViEmu.Core.Machine.Core;

namespace PoViEmu.Core.Machine.Args
{
    public interface ICalcArg
    {
        Instruction Parent { set; }
    }
}