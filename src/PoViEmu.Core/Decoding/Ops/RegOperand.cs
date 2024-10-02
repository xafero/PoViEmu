using Iced.Intel;

namespace PoViEmu.Core.Decoding.Ops
{
    public abstract record RegOperand(Register Reg) : BaseOperand;
}