namespace PoViEmu.SH3.ISA.Ops.Places
{
    /// <summary>
    /// A register-relative address (usually program counter)
    /// </summary>
    public record AddressOperand(ShRegister Base, int Disp) : BaseOperand
    {
        public override string ToString()
        {
            var val = Disp;
            return $"0x{val:x8}";
        }

        public uint CalcAddress(IMachineState s)
        {
            return (uint)(s[Base] + Disp);
        }
    }
}