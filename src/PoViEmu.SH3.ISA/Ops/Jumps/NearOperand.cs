namespace PoViEmu.SH3.ISA.Ops.Jumps
{
    public record NearOperand(int Diff)
        : JumpOperand
    {
        public uint NextOff(uint nextIP)
        {
            var callDst = nextIP + Diff;
            var destIP = (uint)callDst;
            return destIP;
        }
        
        public override string ToString()
        {
            return $"0x{Diff:x8}";
        }
    }
}