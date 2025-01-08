using B32 = PoViEmu.SH3.ISA.ShRegister;

namespace PoViEmu.SH3.ISA.Ops.Mems
{
    public abstract record MemOperand<T>(B32 Seg, B32? Base, B32? Idx, short? Disp)
        : MemOperand(Seg, Base, Idx, Disp)
    {
        public abstract T this[IMachineState m] { get; set; }

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

        public bool HasIdx => Idx != null && Idx != B32.Unknown;
        public bool HasBase => Base != null && Base != B32.Unknown;
        public bool HasDisp => Disp != null && Disp != 0;
    }

    public abstract record MemOperand(B32 Seg, B32? Base, B32? Idx, short? Disp)
        : BaseOperand;
}