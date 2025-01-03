using Iced.Intel;

namespace PoViEmu.I186.ISA.Decoding
{
    public static class DecoderExt
    {
        private static readonly NasmFormatter Format = new(new FormatterOptions
        {
            UppercaseMnemonics = true
        });
        
        public static (string op, string arg) FormatStr(this Instruction instruct)
        {
            StringOutput output = new();
            Format.Format(instruct, output);
            var text = output.ToStringAndReset();
            var parts = text.Split(' ', 2);
            var op = instruct.IsInvalid ? "???" : parts[0];
            var arg = parts.Length == 2 ? parts[1] : string.Empty;
            return (op, arg);
        }
        
        public static bool IsInvalidFor16Bit(this Instruction parsed)
        {
            return parsed.IsInvalid || parsed.CodeSize != CodeSize.Code16;
        }
    }
}