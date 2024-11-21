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

        public override string ToString()
        {
            var off = Idx != null ? $"{Idx}" : $"{Off:X4}";
            var type = GetType().Name;
            return $"{type} [{Seg}:{off}]";
        }
    }

    public abstract record MemOperand(
        B16Register Seg,
        B16Register? Base,
        B16Register? Idx,
        short? Off
    )
        : BaseOperand;
}