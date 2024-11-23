using PoViEmu.Core.Hardware;
using PoViEmu.Core.Hardware.AckNow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iced.Intel;
using PoViEmu.Core.Decoding;
using PoViEmu.Core.Decoding.Ops;
using PoViEmu.Core.Hardware;
using PoViEmu.Core.Hardware.AckNow;
using R = Iced.Intel.Register;
using MS = Iced.Intel.MemorySize;
using OK = Iced.Intel.OpKind;
using B8 = PoViEmu.Core.Hardware.AckNow.B8Register;
using B16 = PoViEmu.Core.Hardware.AckNow.B16Register;
using PoViEmu.Core.Hardware.AckNow;

namespace PoViEmu.Core.Decoding.Ops
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