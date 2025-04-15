using PoViEmu.I186.CPU;

namespace PoViEmu.Hyper
{
    public sealed class NcMachine : IVMachine
    {
        private readonly NC3022 _cpu;
        private readonly MachineState _state;

        public NcMachine()
        {
            _cpu = new NC3022();
            _state = new MachineState();
        }
    }
}