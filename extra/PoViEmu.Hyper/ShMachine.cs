using System;
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

        protected override void ClockOnTick(object? sender, TickEventArgs e)
        {
            var src = (SysClock)sender!;
            Console.WriteLine($" {_cpu} {DateTime.Now:u} => {src.TickHz} f, {src.TickMs} ms, {e.Cycles} c");

            object ni = null;
            _cpu.Execute((SH3.ISA.Decoding.XInstruction)ni, _state);
        }
    }
}