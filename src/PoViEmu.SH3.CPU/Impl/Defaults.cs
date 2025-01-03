using PoViEmu.Base.CPU;
using PoViEmu.SH3.ISA.Decoding;

namespace PoViEmu.SH3.CPU.Impl
{
    public static class Defaults
    {
        public static ICpuFactory<XInstruction, MachineState> CpuFactory { get; } = new CpuFactory();

        public static IValFormatter ValFormatter { get; } = new ValFormatter();
    }
}