namespace PoViEmu.Core.Machine.Args
{
    public class BytePlusArg : ConstantArg
    {
        public BytePlusArg(byte value) : base(value)
        {
        }

        public bool Signed { get; set; }

        public override string ToString()
        {
            object val = Signed ? (sbyte)Value : Value;
            return $"byte +0x{val:x}";
        }
    }
}