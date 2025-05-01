using System.Linq;
using PoViEmu.Base;
using PoViEmu.UI.Dbg.Models;
using PoViEmu.UI.Dbg.ViewModels;
using MachineStateSH3 = PoViEmu.SH3.CPU.MachineState;
using MachineStateI86 = PoViEmu.I186.CPU.MachineState;

namespace PoViEmu.UI.Dbg.Core
{
    public static class MemoryTool
    {
        private static void Read(RunDbgViewModel rvm, ushort segment, ushort offset, byte[] bytes, int lineSize = 16)
        {
            rvm.Lines.Clear();
            foreach (var oneArray in bytes.SplitEvery(lineSize))
            {
                var txt = oneArray.DecodeChars();
                var off = $"{segment:X4}:{offset:X4}";
                rvm.Lines.Add(new BytesLine(off, oneArray, txt));
                offset = (ushort)(offset + oneArray.Length);
            }
        }

        public static void Read(this RunDbgViewModel rvm, MachineStateI86 state)
        {
            var seg = state.DS;
            var off = state.SI;
            var bytes = state.ReadMemory(seg, off, 512);
            Read(rvm, seg, off, bytes.ToArray());
        }

        private static void Read(RunDbgViewModel rvm, uint offset, byte[] bytes, int lineSize = 16)
        {
            rvm.Lines.Clear();
            foreach (var oneArray in bytes.SplitEvery(lineSize))
            {
                var txt = oneArray.DecodeChars();
                var off = $"{offset:X8}";
                rvm.Lines.Add(new BytesLine(off, oneArray, txt));
                offset = (ushort)(offset + oneArray.Length);
            }
        }

        public static void Read(this RunDbgViewModel rvm, MachineStateSH3 state)
        {
            var off = state.R15;
            var bytes = state.ReadMemory(off, 512);
            Read(rvm, off, bytes.ToArray());
        }
    }
}