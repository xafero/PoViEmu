using ConsoleServer;

namespace PoViEmu.Telnet
{
    internal delegate bool CommandDlgt(
        TelnetSession session, string cmd, string arg
    );
}