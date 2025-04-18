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

        protected override void ClockOnTick(object? sender, TickEventArgs e)
        {
            var src = (SysClock)sender!;
            Console.WriteLine($" {_cpu} {DateTime.Now:u} => {src.TickHz} f, {src.TickMs} ms, {e.Cycles} c");

            object ni = null;
            _cpu.Execute((I186.ISA.Decoding.XInstruction)ni, _state);
        }

        public void DoIt(byte[] bytes)
        {
            ICpu cpu;
            INotifyPropertyChanged m;
            Func<string, object> fetchProp;
            Action<IInstruction> execThis;
            Func<IInstruction> readNext;

            var cpuFi = DefI.CpuFactory;
            var cpuI = cpuFi.CreateCpu(bytes, out var m1);
            var cpuRi = cpuFi.CreateReader(m1);
            fetchProp = nn => m1[nn];
            execThis = ni => cpuI.Execute((I186.ISA.Decoding.XInstruction)ni, m1);
            readNext = () => cpuRi.NextInstruction();
            cpu = cpuI;
            m = m1;
        }
    }
}