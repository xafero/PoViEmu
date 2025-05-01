using System.Linq;
using PoViEmu.Base;
using PoViEmu.UI.Dbg.Models;
using PoViEmu.UI.Dbg.ViewModels;
using MachineStateSH3 = PoViEmu.SH3.CPU.MachineState;
using MachineStateI86 = PoViEmu.I186.CPU.MachineState;

namespace PoViEmu.UI.Dbg.Core
{
    public static class StackTool
    {
        private static void Read(RunDbgViewModel rvm, ushort segment, ushort offset, byte[] bytes, int lineSize = 2)
        {
            rvm.StaLines.Clear();
            foreach (var oneArray in bytes.SplitEvery(lineSize))
            {
                var hex = oneArray.ToHex(false).ToLower();
                var off = $"{segment:X4}:{offset:X4}";
                rvm.StaLines.Add(new BytesLine(off, null, Beta: hex));
                offset = (ushort)(offset + oneArray.Length);
            }
        }

        public static void Read(RunDbgViewModel rvm, MachineStateI86 state)
        {
            var seg = state.SS;
            var off = state.SP;
            var bytes = state.ReadMemory(seg, off, 128);
            Read(rvm, seg, off, bytes.ToArray());
        }

        private static void Read(RunDbgViewModel rvm, uint offset, byte[] bytes, int lineSize = 4)
        {
            rvm.StaLines.Clear();
            foreach (var oneArray in bytes.SplitEvery(lineSize))
            {
                var hex = oneArray.ToHex(false).ToLower();
                var off = $"{offset:X8}";
                rvm.StaLines.Add(new BytesLine(off, null, Beta: hex));
                offset = (ushort)(offset + oneArray.Length);
            }
        }

        public static void Read(RunDbgViewModel rvm, MachineStateSH3 state)
        {
            var off = state.R15;
            var bytes = state.ReadMemory(off, 128);
            Read(rvm, off, bytes.ToArray());
        }
    }
}