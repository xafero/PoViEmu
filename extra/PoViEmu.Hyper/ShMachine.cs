using System;
using PoViEmu.SH3.CPU;
using DefS = PoViEmu.SH3.CPU.Impl.Defaults;

namespace PoViEmu.Hyper
{
    public sealed class ShMachine : BaseMachine<SH7291, MachineState>
    {
        public ShMachine() : base(new SH7291(), new MachineState())
        {
            _clock.Cycles = 2;
            _clock.TickHz = 1;
            Init();
        }

        private void Init()
        {
            throw new NotImplementedException();
        }

        protected override void ClockOnTick(object? sender, TickEventArgs e)
        {
            var src = (SysClock)sender!;
            Console.WriteLine($" {_cpu} {DateTime.Now:u} => {src.TickHz} f, {src.TickMs} ms, {e.Cycles} c");

            object ni = null;
            _cpu.Execute((SH3.ISA.Decoding.XInstruction)ni, _state);
        }

        public void DoIt(byte[] bytes)
        {
            var cpuFs = DefS.CpuFactory;
            var cpuS = cpuFs.CreateCpu(bytes, out var m2);
            var cpuRs = cpuFs.CreateReader(m2);
            var i1 = cpuRs.NextInstruction();
            cpuS.Execute(i1, m2);
        }
    }
}