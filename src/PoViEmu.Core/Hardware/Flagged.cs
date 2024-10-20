using System;

namespace PoViEmu.Core.Hardware
{
    public interface IMachineState
    {
        ushort AX { get; set; }
        ushort BX { get; set; }
        ushort CX { get; set; }
        ushort DX { get; set; }

        byte AH { get; set; }
        byte BH { get; set; }
        byte CH { get; set; }
        byte DH { get; set; }

        byte AL { get; set; }
        byte BL { get; set; }
        byte CL { get; set; }
        byte DL { get; set; }

        ushort BP { get; set; }
        ushort SP { get; set; }
        ushort IP { get; set; }

        ushort DI { get; set; }
        ushort SI { get; set; }
        ushort F { get; set; }

        ushort CS { get; set; }
        ushort DS { get; set; }
        ushort ES { get; set; }
        ushort SS { get; set; }
    }

    public interface IEmsMachState : IMachineState
    {
        ushort Bank0 { get; set; }
        ushort Bank1 { get; set; }
        ushort Bank2 { get; set; }
        ushort Bank3 { get; set; }
        ushort Bank4 { get; set; }
        ushort Bank5 { get; set; }
        ushort Bank6 { get; set; }

        ushort Frame0 { get; set; }
        ushort Frame1 { get; set; }
        ushort Frame2 { get; set; }
        ushort Frame3 { get; set; }
        ushort Frame4 { get; set; }
        ushort Frame5 { get; set; }
        ushort Frame6 { get; set; }
        ushort Frame7 { get; set; }
        ushort Frame8 { get; set; }
        ushort Frame9 { get; set; }
        ushort Frame10 { get; set; }
        ushort Frame11 { get; set; }
    }

    /// <summary>
    /// Intel 8086 flags
    /// ( ____ODITSZ_A_P_C )
    /// </summary>
    [Flags]
    public enum Flagged : ushort
    {
        Carry = 1 << 0,
        Parity = 1 << 2,
        Auxiliary = 1 << 4,
        Zero = 1 << 6,
        Sign = 1 << 7,
        Trap = 1 << 8,
        Interrupt = 1 << 9,
        Direction = 1 << 10,
        Overflow = 1 << 11
    }
}