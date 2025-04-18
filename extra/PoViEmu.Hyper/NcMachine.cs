using System;
using PoViEmu.I186.CPU;
using DefI = PoViEmu.I186.CPU.Impl.Defaults;

namespace PoViEmu.Hyper
{
    public sealed class NcMachine : BaseMachine<NC3022, MachineState>
    {
        public NcMachine() : base(new NC3022(), new MachineState())
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
            _cpu.Execute((I186.ISA.Decoding.XInstruction)ni, _state);
        }

        public void DoIt(byte[] bytes)
        {
            var cpuFi = DefI.CpuFactory;
            var cpuI = cpuFi.CreateCpu(bytes, out var m1);
            var cpuRi = cpuFi.CreateReader(m1);
            var i1 = cpuRi.NextInstruction();
            cpuI.Execute(i1, m1);
        }
    }
}