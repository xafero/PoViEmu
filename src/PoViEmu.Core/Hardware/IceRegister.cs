using Iced.Intel;

namespace PoViEmu.Core.Hardware
{
    public record IceRegister(Register R16, Register H8, Register L8);
}