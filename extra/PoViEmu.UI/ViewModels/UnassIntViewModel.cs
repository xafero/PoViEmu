using PoViEmu.UI.Models;
using System.Linq;
using System.Reflection;
using PoViEmu.Base.CPU;
using InstrI = PoViEmu.I186.ISA.Decoding.XInstruction;
using DefI = PoViEmu.I186.CPU.Impl.Defaults;
using StateI = PoViEmu.I186.CPU.MachineState;

namespace PoViEmu.UI.ViewModels
{
    public class UnassIntViewModel : UnassViewModel
    {
        private readonly ConstructorInfo _constr;

        public UnassIntViewModel()
        {
            var cpuFi = DefI.CpuFactory;
            var cpuRi = cpuFi.CreateReader(new StateI());
            _constr = cpuRi.GetType().GetConstructors().Single();
        }

        public void Read(ushort seg, ushort offset, byte[] bytes, int count = 25)
        {
            Lines.Clear();

            var m = new FakeState { CS = seg, IP = offset };
            var reader = (ICodeReader<InstrI>)_constr.Invoke([m]);

            var i = 0;
            while (i <= count)
            {
                var item = reader.NextInstruction();
                var txt = item.ToString().Split("    ", 2).Last().Trim();
                var hex = item.Bytes;
                var off = $"{seg:X4}:{offset:X4}";
                Lines.Add(new BytesLine(off, hex, txt));
                offset = (ushort)(offset + (hex.Length / 2));
                i++;
            }
        }

        public void Read(StateI state)
        {
            var seg = state.CS;
            var off = state.IP;
            var bytes = state.ReadMemory(seg, off, 128);
            Read(seg, off, bytes.ToArray());
        }
    }
}