using System.IO;
using System.Text;
using IniFile;

namespace PoViEmu.Common
{
    public class IniTool
    {
        public static Ini LoadIni(string file)
        {
            var enc = Encoding.UTF8;
            var text = File.ReadAllText(file, enc);
            text = text.TrimEnd((char)26).Trim();
            var settings = new IniLoadSettings
            {
                Encoding = enc, CaseSensitive = false
            };
            return Ini.Load(text, settings);
        }
    }
}