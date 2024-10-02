using Iced.Intel;

namespace PoViEmu.Core.Decoding.Ops
{
    public record Reg8Operand(Register Reg) : RegOperand(Reg);
}