using PoViEmu.UI.Models;
using System.Linq;
using PoViEmu.Base;
using MachineStateI86 = PoViEmu.I186.CPU.MachineState;

namespace PoViEmu.UI.ViewModels
{
    public class StackIntViewModel : StackViewModel
    {
        public void Read(ushort segment, ushort offset, byte[] bytes, int lineSize = 2)
        {
            Lines.Clear();
            foreach (var oneArray in bytes.SplitEvery(lineSize))
            {
                var hex = oneArray.ToHex(false, true);
                var off = $"{segment:X4}:{offset:X4}";
                Lines.Add(new BytesLine(off, hex));
                offset = (ushort)(offset + oneArray.Length);
            }
        }

        public void Read(MachineStateI86 state)
        {
            var seg = state.SS;
            var off = state.SP;
            var bytes = state.ReadMemory(seg, off, 128);
            Read(seg, off, bytes.ToArray());
        }
    }
}