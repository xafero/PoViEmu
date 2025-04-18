using System;
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

        protected override void ClockOnTick(object? sender, TickEventArgs e)
        {
            var src = (SysClock)sender!;
            Console.WriteLine($" {_cpu} {DateTime.Now:u} => {src.TickHz} f, {src.TickMs} ms, {e.Cycles} c");

            object ni = null;
            _cpu.Execute((I186.ISA.Decoding.XInstruction)ni, _state);
        }
    }
}