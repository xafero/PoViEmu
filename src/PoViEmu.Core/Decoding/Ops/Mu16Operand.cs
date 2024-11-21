using PoViEmu.Core.Hardware;
using PoViEmu.Core.Hardware.AckNow;

namespace PoViEmu.Core.Decoding.Ops
{
    public record Mu16Operand(
        B16Register Seg,
        B16Register? Base,
        B16Register? Idx,
        short? Off
    )
        : MemOperand<ushort>(Seg, Base, Idx, Off)
    {
        public override ushort this[MachineState m]
        {
            get => m.U16[this.SegA(m), this.OffA(m)];
            set => m.U16[this.SegA(m), this.OffA(m)] = value;
        }

        public override string ToString()
        {
            var oSign = Off < 0 ? "" : "+";
            var oIdx = HasIdx ? $"+{Idx}" : "";
            var bse = HasBase ? $"{Base}" : "";
            var of = HasBase ? $"{Off}" : $"{Off:x4}";
            return $"U16 [{Seg}:{bse}{oIdx}{oSign}{of}]";
        }

        public bool HasIdx => Idx != B16Register.None;
        public bool HasBase => Base != B16Register.None;
    }
}