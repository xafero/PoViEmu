namespace PoViEmu.Core.Hardware
{
    public interface IFlagged
    {
        bool CarryFlag { set; }
        bool AuxCarryFlag { set; }
        bool ParityFlag { set; }
        bool SignFlag { set; }
        bool ZeroFlag { set; }
        bool OverflowFlag { set; }
    }
}