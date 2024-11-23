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
using PoViEmu.Core.Hardware.AckNow;

namespace PoViEmu.Core.Decoding.Ops.Regs
{
    public record Reg16Operand(B16 Reg) : RegOperand<B16>;
}