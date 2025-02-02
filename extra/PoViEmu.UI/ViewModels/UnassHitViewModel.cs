using PoViEmu.UI.Models;
using System.Reflection;
using System.Linq;
using PoViEmu.Base;
using PoViEmu.Base.CPU;
using InstrS = PoViEmu.SH3.ISA.Decoding.XInstruction;
using DefS = PoViEmu.SH3.CPU.Impl.Defaults;
using StateS = PoViEmu.SH3.CPU.MachineState;

namespace PoViEmu.UI.ViewModels
{
    public class UnassHitViewModel : UnassViewModel
    {
        private readonly ConstructorInfo _constr;

        public UnassHitViewModel()
        {
            var cpuFi = DefS.CpuFactory;
            var cpuRi = cpuFi.CreateReader(new StateS());
            _constr = cpuRi.GetType().GetConstructors().Single();
        }

        public void Read(ICodeReader<InstrS> reader, FakeState m, int count = 25)
        {
            Lines.Clear();

            var i = 0;
            while (i <= count)
            {
                var item = reader.NextInstruction();
                m.PC = (ushort)(m.PC + item.Bytes.Length / 2);
                var parts = TextHelper.SplitOn(item.ToString(), 3);
                var addr = $"{parts[0]}";
                var hex = parts[1];
                var txt = parts[2].RemoveSpaces();
                Lines.Add(new BytesLine(addr, hex, txt));
                i++;
            }
        }

        public void Read(StateS state)
        {
            var off = state.PC;

            var m = new FakeState { PC = off, dPC = null, Wrapped = state };
            var reader = (ICodeReader<InstrS>)_constr.Invoke([m]);
            Read(reader, m);
        }
    }
}