using PoViEmu.Base;
using PoViEmu.UI.Models;
using System.Linq;
using MachineStateI86 = PoViEmu.I186.CPU.MachineState;

namespace PoViEmu.UI.ViewModels
{
    public class UnassIntViewModel : UnassViewModel
    {
        public void Read(ushort segment, ushort offset, byte[] bytes, int lineSize = 16)
        {
            Lines.Clear();
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
            var bytes = state.ReadMemory(seg, off, 512);
            Read(seg, off, bytes.ToArray());
        }
    }
}