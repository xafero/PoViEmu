using System;
using PoViEmu.Base.CPU;
using PoViEmu.I186.CPU;
using PoViEmu.I186.ISA.Decoding;
using DefI = PoViEmu.I186.CPU.Impl.Defaults;

namespace PoViEmu.Hyper
{
    public sealed class NcMachine : BaseMachine<NC3022, MachineState>
    {
        private ICodeReader<XInstruction> _reader;

        public NcMachine()
        {
            Init();
        }

        private void Init()
        {
            Clock.Cycles = 2;
            Clock.TickHz = 1;

            var factory = DefI.CpuFactory;
            byte[] bytes = [];
            Cpu = (NC3022)factory.CreateCpu(bytes, out var state);
            State = state;
            _reader = factory.CreateReader(State);
        }

        protected override void ClockOnTick(object? sender, TickEventArgs e)
        {
            var src = (SysClock)sender!;
            Console.WriteLine($" {Cpu} {DateTime.Now:u} => {src.TickHz} f, {src.TickMs} ms, {e.Cycles} c");

            if (Cpu is { } cpu && State is { } state)
            {
                var ni = _reader.NextInstruction();
                cpu.Execute(ni, state);
            }
        }
    }
}