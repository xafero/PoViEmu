using System;
using PoViEmu.I186.CPU;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.IO;
using System.Xml.Linq;
using PoViEmu.Base;
using PoViEmu.Base.CPU;
using PoViEmu.Base.ISA;
using DefI = PoViEmu.I186.CPU.Impl.Defaults;
using DefS = PoViEmu.SH3.CPU.Impl.Defaults;
using static PoViEmu.Base.FileHelper;

namespace PoViEmu.Hyper
{
    public sealed class NcMachine : BaseMachine<NC3022, MachineState>
    {
        public NcMachine() : base(new NC3022(), new MachineState())
        {
            _clock.Cycles = 2;
            _clock.TickHz = 1;
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
            Func<string, object> fetchProp = nn => m1[nn];
            Action<IInstruction> execThis = ni => cpuI.Execute((I186.ISA.Decoding.XInstruction)ni, m1);
            Func<IInstruction> readNext = () => cpuRi.NextInstruction();
        }
    }
}