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
        private byte[] _boot;

        public NcMachine(byte[] boot)
        {
            _boot = boot;
            Init();
        }

        private void Init()
        {
            Clock.Cycles = 2;
            Clock.TickHz = 1;

            var factory = DefI.CpuFactory;
            var bytes = _boot;
            Cpu = (NC3022)factory.CreateCpu(bytes, out var state);
            State = state;
            _reader = factory.CreateReader(State);
        }

        protected override void ClockOnTick(object? sender, TickEventArgs e)
        {
            var src = (SysClock)sender!;
            Console.WriteLine($" {Cpu} {DateTime.Now:u} => {src.TickHz} f, {src.TickMs} ms, {e.Cycles} c");

            if (Cpu is not { } cpu || State is not { } state || cpu.Halted)
                return;
            var cycles = (int)e.Cycles;
            for (var i = 0; !cpu.Halted && i < cycles; i++)
            {
                try
                {
                    var current = _reader.NextInstruction();
                    cpu.Execute(current, state);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($" {ex.GetType().Name}: {ex.Message}");
                    cpu.Halted = true;
                }
            }
        }
    }
}