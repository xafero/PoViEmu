using PoViEmu.UI.Models;
using System.Linq;
using DefS = PoViEmu.SH3.CPU.Impl.Defaults;
using MachineStateSH3 = PoViEmu.SH3.CPU.MachineState;

namespace PoViEmu.UI.ViewModels
{
    public class UnassHitViewModel : UnassViewModel
    {
        public void Read(uint offset, byte[] bytes, int count = 25)
        {
            Lines.Clear();

            var cpuFs = DefS.CpuFactory;
            var m = new MachineStateSH3();
            var cpuRs = cpuFs.CreateReader(m);

            var i = 0;
            while (i <= count)
            {
                var item = cpuRs.NextInstruction();
                var txt = item.ToString().Split("    ", 2).Last().Trim();
                var hex = item.Bytes;
                var off = $"{offset:X8}";
                Lines.Add(new BytesLine(off, hex, txt));
                offset = (ushort)(offset + (hex.Length / 2));
                i++;
            }
        }

        public void Read(MachineStateSH3 state)
        {
            var off = state.PC;
            var bytes = state.ReadMemory(off, 128);
            Read(off, bytes.ToArray());
        }
    }
}