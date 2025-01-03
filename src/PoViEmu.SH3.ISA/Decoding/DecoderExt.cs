namespace PoViEmu.SH3.ISA.Decoding
{
    public static class DecoderExt
    {
        public static (string op, string arg) FormatStr(this Instruction instruct)
        {
            var text = instruct.ToString();
            var parts = text.Split(' ', 2);
            var op = instruct.IsInvalid ? "???" : parts[0];
            var arg = parts.Length == 2 ? parts[1] : string.Empty;
            return (op, arg);
        }

        public static bool IsSimplyInvalid(this Instruction parsed)
        {
            return parsed.IsInvalid || parsed.Length != 2;
        }
    }
}