using System;
using PoViEmu.Base;
using PoViEmu.Base.CPU;
using PoViEmu.SH3.CPU;
using PoViEmu.SH3.ISA;
using PoViEmu.SH3.ISA.Decoding;
using DefS = PoViEmu.SH3.CPU.Impl.Defaults;

namespace PoViEmu.Hyper
{
    public sealed class ShMachine : BaseMachine<SH7291, MachineState>
    {
        private ICodeReader<XInstruction> _reader;
        private byte[] _boot;

        public ShMachine(byte[] boot)
        {
            _boot = boot;
            Init();
        }

        private void Init()
        {
            Clock.Cycles = 16;
            Clock.TickHz = 4;

            var factory = DefS.CpuFactory;
            var bytes = _boot;
            Cpu = (SH7291)factory.CreateCpu(bytes, out var state);
            State = state;
            _reader = factory.CreateReader(State);
        }

        protected override void ClockOnTick(object? sender, TickEventArgs e)
        {
            var src = (SysClock)sender!;
            // Console.WriteLine($" {Cpu} {DateTime.Now:u} => {src.TickHz} f, {src.TickMs} ms, {e.Cycles} c");

            if (Cpu is not { } cpu || State is not { } state || cpu.Halted)
                return;
            var cycles = (int)Math.Round(e.Cycles);
            for (var i = 0; !cpu.Halted && i < cycles; i++)
            {
                try
                {
                    var current = _reader.NextInstruction();
                    cpu.Execute(current, state);

                    var dos = cpu.GetDosEmu();
                    if (dos.StdOut.ToString().TrimNull() is { } txt)
                    {
                        Console.WriteLine($" * (S) '{current}' {state[ShRegister.R1]} '{txt}'");
                        dos.StdOut.GetStringBuilder().Clear();
                    }
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
