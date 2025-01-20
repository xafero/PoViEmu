namespace PoViEmu.Base
{
    public static class NullHelper
    {
        public static string NullStr(this uint? value)
        {
            return value == null ? "\u2205" : $"{value:x}";
        }
    }
}