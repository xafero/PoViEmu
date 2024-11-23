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

namespace PoViEmu.Core.Alledge
{
    public record Mu16Operand(B16 Seg, B16? Base, B16? Idx, short? Disp)
        : MemOperand(Seg, Base, Idx, Disp)
    {
    }
}