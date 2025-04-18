using PoViEmu.SH3.CPU;

namespace PoViEmu.Hyper
{
    public sealed class ShMachine : BaseMachine<SH7291, MachineState>
    {
        public ShMachine() : base(new SH7291(), new MachineState())
        {
            _clock.Cycles = 2;
            _clock.TickHz = 1;
        }

        public void Execute()
        {
            object ni = null;
            _cpu.Execute((SH3.ISA.Decoding.XInstruction)ni, _state);
        }
    }
}