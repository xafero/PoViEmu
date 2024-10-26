using ConsoleServer;
using Spectre.Console;

namespace PoViEmu.Telnet
{
    internal static class TelnetHelper
    {
        public static string GetSessionKey(this TelnetSession s)
        {
            return $"{s.ipEndPoint}";
        }

        public static void WritePrompt(this TelnetSession s, string prefix)
        {
            WriteOneLine(s, $"{prefix} ", newLine: false);
        }

        public static void WriteOneLine(this TelnetSession s, string text, bool newLine = true)
        {
            s.AnsiConsole.Markup(text);
            if (newLine)
                s.AnsiConsole.WriteLine();
        }
    }
}