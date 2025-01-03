using PoViEmu.Base.CPU;
using PoViEmu.I186.ISA.Decoding;

namespace PoViEmu.I186.CPU.Impl
{
    internal sealed class CpuFactory : ICpuFactory<XInstruction, MachineState>
    {
        public ICpu<XInstruction, MachineState> CreateCpu(byte[] bytes, out MachineState state)
        {
            var c = new NC3022();
            var m = new MachineState();
            m.InitForCom(loadSeg: 0x0750, cxInit: 0x002C, axInit: 0xFFFF);
            m.WriteMemory(m.CS, m.IP, bytes);
            state = m;
            return c;
        }

        public ICodeReader<XInstruction> CreateReader(MachineState state)
        {
            return new StateCodeReader(state);
        }
    }
}