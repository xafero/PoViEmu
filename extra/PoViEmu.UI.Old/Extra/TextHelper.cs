using System.Linq;

namespace PoViEmu.UI.Extra
{
    public static class TextHelper
    {
        public static string TryAsText(this byte[] bytes)
        {
            var chars = bytes.Select(b =>
            {
                var letter = (char)b;
                return char.IsLetterOrDigit(letter) && letter <= 175 ? letter : '.';
            });
            var txt = new string(chars.ToArray());
            return txt;
        }
    }
}