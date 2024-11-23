using PoViEmu.Core.Hardware;
using B16 = PoViEmu.Core.Hardware.AckNow.B16Register;

namespace PoViEmu.Core.Decoding.Ops.Mems
{
    public abstract record MemOperand<T>(B16 Seg, B16? Base, B16? Idx, short? Disp)
        : MemOperand(Seg, Base, Idx, Disp)
    {
        public abstract T this[MachineState m] { get; set; }

        public sealed override string ToString()
        {
            var type = typeof(T).Name;
            var oBase = HasBase ? $"{Base}+" : "";
            var oIdx = HasIdx ? $"{Idx}+" : "";
            var onlyD = !HasBase && !HasIdx && HasDisp;
            var oDisp = onlyD ? $"{Disp:x4}" : HasDisp ? $"{Disp}" : "";
            var txt = $"{type} [{Seg}:{oBase}{oIdx}{oDisp}]";
            txt = txt.Replace("+-", "-").Replace("+]", "]");
            return txt;
        }

        public bool HasIdx => Idx != null && Idx != B16.None;
        public bool HasBase => Base != null && Base != B16.None;
        public bool HasDisp => Disp != null && Disp != 0;
    }

    public abstract record MemOperand(B16 Seg, B16? Base, B16? Idx, short? Disp)
        : BaseOperand;
}