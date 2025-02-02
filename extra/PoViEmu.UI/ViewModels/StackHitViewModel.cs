using PoViEmu.UI.Models;
using System.Linq;
using PoViEmu.Base;
using MachineStateSH3 = PoViEmu.SH3.CPU.MachineState;

namespace PoViEmu.UI.ViewModels
{
    public class StackHitViewModel : StackViewModel
    {
        public void Read(uint offset, byte[] bytes, int lineSize = 4)
        {
            Lines.Clear();
            foreach (var oneArray in bytes.SplitEvery(lineSize))
            {
                var hex = oneArray.ToHex(false, true);
                var off = $"{offset:X8}";
                Lines.Add(new BytesLine(off, hex));
                offset = (ushort)(offset + oneArray.Length);
            }
        }

        public void Read(MachineStateSH3 state)
        {
            var off = state.R15;
            var bytes = state.ReadMemory(off, 128);
            Read(off, bytes.ToArray());
        }
    }
}
