namespace PoViEmu.SH3.ISA.Core
{
    public static class NamingTools
    {
        public static string Name(this Mnemonic val)
        {
            if (val == Mnemonic.Invalid)
                return "???";
            var txt = val.ToString();
            if (txt.EndsWith('L'))
                txt = $"{txt[..^1]}.L";
            if (txt.EndsWith('W'))
                txt = $"{txt[..^1]}.W";
            if (txt.EndsWith('B'))
                txt = $"{txt[..^1]}.B";
            if (txt.EndsWith('S'))
                txt = $"{txt[..^1]}.S";
            txt = txt.ToLower();
            if (txt.StartsWith("cmp"))
                txt = $"{txt[..3]}/{txt[3..]}";
            return txt;
        }
        
        public static string Name(this ShRegister val)
        {
            if (val == ShRegister.Unknown)
                return "??";
            var txt = val.ToString().ToLower();
            return txt;
        }
    }
}