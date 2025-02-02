using System.Linq;
using PoViEmu.Base;
using PoViEmu.UI.Models;
using MachineStateSH3 = PoViEmu.SH3.CPU.MachineState;
using StateSH3 = PoViEmu.SH3.CPU.MachineState;

namespace PoViEmu.UI.ViewModels
{
    public class MemHitViewModel : MemViewModel
    {
        public void Read(uint offset, byte[] bytes, int lineSize = 16)
        {
            Lines.Clear();
            foreach (var oneArray in bytes.SplitEvery(lineSize))
            {
                var txt = oneArray.DecodeChars();
                var hex = oneArray.ToHex(false, true);
                var off = $"{offset:X8}";
                Lines.Add(new BytesLine(off, hex, txt));
                offset = (ushort)(offset + oneArray.Length);
            }
        }

        public void Read(MachineStateSH3 state)
        {
            var off = state.R15;
            var bytes = state.ReadMemory(off, 512);
            Read(off, bytes.ToArray());
        }
    }
}