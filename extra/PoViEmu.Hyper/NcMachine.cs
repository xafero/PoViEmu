using PoViEmu.I186.CPU;

namespace PoViEmu.Hyper
{
    public sealed class NcMachine : BaseMachine<NC3022, MachineState>
    {
        public NcMachine() : base(new NC3022(), new MachineState())
        {
            _clock.Cycles = 2;
            _clock.TickHz = 1;
        }

        public void Execute()
        {
            object ni = null;
            _cpu.Execute((I186.ISA.Decoding.XInstruction)ni, _state);
        }
    }
}