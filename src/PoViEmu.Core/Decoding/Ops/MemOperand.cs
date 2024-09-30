using Iced.Intel;

namespace PoViEmu.Core.Decoding.Ops
{
    public record MemOperand(Register Base, short Displ) : BaseOperand;
}