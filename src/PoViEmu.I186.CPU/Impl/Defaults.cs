using PoViEmu.Base.CPU;
using PoViEmu.I186.ISA.Decoding;

namespace PoViEmu.I186.CPU.Impl
{
    public static class Defaults
    {
        public static ICpuFactory<XInstruction, MachineState> CpuFactory { get; } = new CpuFactory();

        public static IValFormatter ValFormatter { get; } = new ValFormatter();
    }
}