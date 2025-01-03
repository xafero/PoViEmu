using PoViEmu.Base;
using PoViEmu.Base.ISA;

namespace PoViEmu.SH3.ISA.Decoding
{
    public record XInstruction(string Bytes, Instruction Parsed) : IInstruction
    {
        public override string ToString() => ToString(string.Empty);

        public string ToString(string prefix)
        {
            var (op, arg) = Parsed.FormatStr();
            var bits = Bytes.ToUpper().AddSpaceTo(16);
            var opa = op.AddSpaceTo(9);
            return $"{prefix}{Parsed.IP32:X8}   {bits}  {opa} {arg}";
        }
    }
}