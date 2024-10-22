namespace PoViEmu.Core.Hardware
{
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
}