using PoViEmu.UI.Models;
using System.Linq;
using DefI = PoViEmu.I186.CPU.Impl.Defaults;
using MachineStateI86 = PoViEmu.I186.CPU.MachineState;

namespace PoViEmu.UI.ViewModels
{
    public class UnassIntViewModel : UnassViewModel
    {
        public void Read(ushort seg, ushort offset, byte[] bytes, int count = 25)
        {
            Lines.Clear();

            var cpuFi = DefI.CpuFactory;
            var m = new MachineStateI86();
            var cpuRi = cpuFi.CreateReader(m);

            var i = 0;
            while (i <= count)
            {
                var item = cpuRi.NextInstruction();
                var txt = item.ToString();
                var hex = item.Bytes;
                var off = $"{seg:X4}:{offset:X4}";
                Lines.Add(new BytesLine(off, hex, txt));
                offset = (ushort)(offset + (hex.Length / 2));
                i++;
            }
        }

        public void Read(MachineStateI86 state)
        {
            var seg = state.CS;
            var off = state.IP;
            var bytes = state.ReadMemory(seg, off, 128);
            Read(seg, off, bytes.ToArray());
        }
    }
}