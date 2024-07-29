namespace PoViEmu.Core.Machine.Args
{
    public class BytePlusArg : ConstantArg
    {
        public BytePlusArg(byte value) : base(value)
        {
        }

        public override string ToString()
        {
            return $"byte +0x{Value:x}";
        }
    }
}