using PoViEmu.UI.Models;
using System.Linq;
using System.Reflection;
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

        public void Read(uint offset, byte[] bytes, int count = 25)
        {
            Lines.Clear();

            var m = new FakeState { PC = offset, dPC = null };
            var reader = (ICodeReader<InstrS>)_constr.Invoke([m]);

            var i = 0;
            while (i <= count)
            {
                var item = reader.NextInstruction();
                var txt = item.ToString().Split("    ", 2).Last().Trim();
                var hex = item.Bytes;
                var off = $"{offset:X8}";
                Lines.Add(new BytesLine(off, hex, txt));
                offset = (ushort)(offset + (hex.Length / 2));
                i++;
            }
        }

        public void Read(StateS state)
        {
            var off = state.PC;
            var bytes = state.ReadMemory(off, 128);
            Read(off, bytes.ToArray());
        }
    }
}