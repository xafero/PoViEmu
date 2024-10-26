using System;

namespace PoViEmu.Telnet
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var telnet = new TelnetDebugger();
            telnet.Start();

            Console.WriteLine("Press ENTER to quit...");
            Console.ReadLine();

            telnet.Stop();
        }
    }
}