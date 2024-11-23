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
            var oSign = Off < 0 ? "" : "+";
            var oIdx = HasIdx ? $"+{Idx}" : "";
            var bse = HasBase ? $"{Base}" : "";
            var of = HasBase ? $"{Off}" : $"{Off:x4}";
            return $"{type} [{Seg}:{bse}{oIdx}{oSign}{of}]";
        }

        public bool HasIdx => Idx != B16.None;
        public bool HasBase => Base != B16.None;
    }

    public abstract record MemOperand(B16 Seg, B16? Base, B16? Idx, short? Off)
        : BaseOperand;
}