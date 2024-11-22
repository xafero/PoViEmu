using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iced.Intel;
using PoViEmu.Core.Decoding;
using PoViEmu.Core.Decoding.Ops;
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

namespace PoViEmu.SamCon
{
    public abstract record BaseOperand
    {
    }

    public abstract record RegOperand : BaseOperand
    {
    }

    public abstract record NumOperand : BaseOperand
    {
    }

    public abstract record JumpOperand : BaseOperand
    {
    }

    public abstract record MemOperand(B16 Seg, B16? Base, B16? Idx, short? Disp)
        : BaseOperand
    {
    }

    public record Mu16Operand(B16 Seg, B16? Base, B16? Idx, short? Disp)
        : MemOperand(Seg, Base, Idx, Disp)
    {
    }

    public record Mi16Operand(B16 Seg, B16? Base, B16? Idx, short? Disp)
        : MemOperand(Seg, Base, Idx, Disp)
    {
    }

    public record Mi8Operand(B16 Seg, B16? Base, B16? Idx, short? Disp)
        : MemOperand(Seg, Base, Idx, Disp)
    {
    }

    public record Mu8Operand(B16 Seg, B16? Base, B16? Idx, short? Disp)
        : MemOperand(Seg, Base, Idx, Disp)
    {
    }

    public record NearOperand(ushort Dst) : JumpOperand
    {
    }

    public record FarOperand(ushort Sel, ushort Dst) : JumpOperand
    {
    }

    public record U8Operand(byte Val) : NumOperand
    {
    }

    public record U16Operand(ushort Val) : NumOperand
    {
    }

    public record I16Operand(short Val) : NumOperand
    {
    }

    public record Reg8Operand(B8 Reg) : RegOperand
    {
    }

    public record Reg16Operand(B16 Reg) : RegOperand
    {
    }
}