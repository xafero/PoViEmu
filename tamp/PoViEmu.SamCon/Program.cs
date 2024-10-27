using System;
using PoViEmu.Core.Hardware;
using PoViEmu.Telnet;

namespace PoViEmu.SamCon
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var state = DebugCmds.State;
            var l = state.Collect();
            l.PropertyChanged += (s, e) =>
            {
                Console.WriteLine($" | {e.PropertyName} " +
                                  $"| {e.Old.Format()} " +
                                  $"| {e.New.Format()}");
            };

            var telnet = new TelnetRepl<CmdName>
            {
                HelloMessage = null,
                Prompt = "-",
                Handlers =
                {
                    [CmdName.Help] = DebugCmds.Help,
                    [CmdName.R] = DebugCmds.Register
                }
            };
            telnet.Start();

            Console.WriteLine($"Telnet is listening on {telnet.EndPoint}!");
            Console.WriteLine("Press ENTER to quit...");
            Console.ReadLine();

            telnet.Stop();
        }
    }
}