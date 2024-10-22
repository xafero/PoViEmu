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
}