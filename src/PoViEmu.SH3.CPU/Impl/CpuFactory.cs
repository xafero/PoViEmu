using PoViEmu.Base.CPU;
using PoViEmu.SH3.ISA.Decoding;

namespace PoViEmu.SH3.CPU.Impl
{
    internal sealed class CpuFactory : ICpuFactory<XInstruction, MachineState>
    {
        public ICpu<XInstruction, MachineState> CreateCpu(byte[] bytes, out MachineState state)
        {
            var c = new SH7291();
            var m = new MachineState();
            m.InitForCom();
            m.WriteMemory(m.PC, bytes);
            state = m;
            return c;
        }

        public ICodeReader<XInstruction> CreateReader(MachineState state)
        {
            return new StateCodeReader(state);
        }
    }
}