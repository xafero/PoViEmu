using PoViEmu.UI.Models;
using System.Reflection;
using System.Linq;
using PoViEmu.Base;
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

        public void Read(ushort seg, ICodeReader<InstrI> reader, FakeState m, int count = 25)
        {
            Lines.Clear();

            var i = 0;
            while (i <= count)
            {
                var item = reader.NextInstruction();
                m.IP = (ushort)(m.IP + item.Bytes.Length / 2);
                var parts = TextHelper.SplitOn(item.ToString(), 3);
                var addr = $"{seg:X4}:{parts[0]}";
                var hex = parts[1];
                var txt = parts[2].RemoveSpaces();
                Lines.Add(new BytesLine(addr, hex, txt));
                i++;
            }
        }

        public void Read(StateI state)
        {
            var seg = state.CS;
            var off = state.IP;

            var m = new FakeState { CS = seg, IP = off, Wrapped = state };
            var reader = (ICodeReader<InstrI>)_constr.Invoke([m]);
            Read(seg, reader, m);
        }
    }
}