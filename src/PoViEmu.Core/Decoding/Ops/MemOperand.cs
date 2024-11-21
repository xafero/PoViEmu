using PoViEmu.Core.Hardware;
using PoViEmu.Core.Hardware.AckNow;

namespace PoViEmu.Core.Decoding.Ops
{
    public abstract record MemOperand<T>(
        B16Register Seg,
        B16Register? Base,
        B16Register? Idx,
        short? Off
    )
        : MemOperand(Seg, Base, Idx, Off)
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

        public bool HasIdx => Idx != B16Register.None;
        public bool HasBase => Base != B16Register.None;
    }

    public abstract record MemOperand(
        B16Register Seg,
        B16Register? Base,
        B16Register? Idx,
        short? Off
    )
        : BaseOperand;
}