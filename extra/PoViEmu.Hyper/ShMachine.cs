using PoViEmu.SH3.CPU;

namespace PoViEmu.Hyper
{
    public sealed class ShMachine : IVMachine
    {
        private readonly SH7291 _cpu;
        private readonly MachineState _state;

        public ShMachine()
        {
            _cpu = new SH7291();
            _state = new MachineState();
        }

        public void Execute()
        {
            object ni = null;
            _cpu.Execute((SH3.ISA.Decoding.XInstruction)ni, _state);
        }
    }
}