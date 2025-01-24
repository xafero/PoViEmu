using System;
using B32 = PoViEmu.SH3.ISA.ShRegister;
using AM = PoViEmu.SH3.ISA.Ops.Places.AddressingMode;
using PoViEmu.SH3.ISA.Core;

// ReSharper disable IntroduceOptionalParameters.Global
// ReSharper disable IdentifierTypo

namespace PoViEmu.SH3.ISA.Ops.Places
{
    /// <summary>
    /// A register-relative address (usually program counter)
    /// </summary>
    public record AddressOperand(B32 Base, int? Disp, B32? Idx, AM Mode, bool Align)
        : BaseOperand, IAddressOperand
    {
        public AddressOperand(B32 Base, int Disp, bool Align = false) : this(Base, Disp, null, AM.Relative, Align)
        {
        }

        public AddressOperand(B32 Base, B32 Idx) : this(Base, null, Idx, AM.Indexed, false)
        {
        }

        public AddressOperand(B32 Base) : this(Base, null, null, AM.RegIndirect, false)
        {
        }

        public byte ByteSize => 0;

        public override string ToString()
        {
            return Mode switch
            {
                AM.RegIndirect => $"@{Base.Name()}",
                AM.Relative => $"0x{Disp:x8}",
                AM.Indexed => $"{Idx!.Value.Name()}",
                _ => throw new InvalidOperationException($"{Mode} ?!")
            };
        }

        public uint CalcAddr(IMachineState state)
        {
            var newAddr = this.OffA(state);
            return newAddr;
        }
    }
}