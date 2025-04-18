using System;
using PoViEmu.SH3.CPU;
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
            Func<string, object> fetchProp = nn => m2[nn];
            Action<IInstruction> execThis = ni => cpuS.Execute((SH3.ISA.Decoding.XInstruction)ni, m2);
            Func<IInstruction> readNext = () => cpuRs.NextInstruction();
        }
    }
}