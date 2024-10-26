using ConsoleServer;

namespace PoViEmu.Telnet
{
    public delegate bool CommandDlgt(
        TelnetSession session, string cmd, string arg
    );
}