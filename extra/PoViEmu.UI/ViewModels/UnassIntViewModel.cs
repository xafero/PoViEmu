using PoViEmu.Base;
using PoViEmu.UI.Models;
using System.Linq;
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
using MachineStateI86 = PoViEmu.I186.CPU.MachineState;

namespace PoViEmu.UI.ViewModels
{
    public class UnassIntViewModel : UnassViewModel
    {
        public void Read(ushort segment, ushort offset, byte[] bytes, int lineSize = 16)
        {
            Lines.Clear();
            
            var cpuFi = DefI.CpuFactory;
            var m = new MachineStateI86();
            var cpuRi = cpuFi.CreateReader(m);
            
            foreach (var oneArray in bytes.SplitEvery(lineSize))
            {
                var txt = oneArray.DecodeChars();
                var hex = oneArray.ToHex(false, true);
                var off = $"{segment:X4}:{offset:X4}";
                Lines.Add(new BytesLine(off, hex, txt));
                offset = (ushort)(offset + oneArray.Length);
            }
        }

        public void Read(MachineStateI86 state)
        {
            var seg = state.DS;
            var off = state.SI;
            var bytes = state.ReadMemory(seg, off, 128);
            Read(seg, off, bytes.ToArray());
        }
    }
}