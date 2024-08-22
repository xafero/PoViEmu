using System;
using System.Linq;
using System.Threading;
using Iced.Intel;
using PoViEmu.Core;

// ReSharper disable HeuristicUnreachableCode

namespace PoViEmu.Tasty
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            dynamic info = 42; // new VersionInfo();

            // var x = JsonConvert.SerializeObject(new { x = info }, Formatting.Indented);
            // Console.WriteLine(x);

            var threadName = $"{info.Product}Emulation";

            void Loop(object? _)
            {
                var fmt = IntelDecoder.GetFormatter();

                Instruction instr;
                var decoder = IntelDecoder.GetDecoder(out var mem);
                var rawBytes = Convert
                    .FromHexString("2EFF2E050000F000F0FFFFFFFFFFFFFF2B0000C4B41600C4AF0B00C4300100C02B0000C4")
                    .Concat(new byte[] { 52, 31, 45, 98, 221, 148 }).ToArray();
                mem.Write(rawBytes);
                mem.Position = 0L;

                var lastPos = 0L;
                while (mem.Position < mem.Length)
                {
                    lastPos = mem.Position;
                    decoder.Decode(out instr);

                    var bytes = rawBytes[(int)lastPos..(int)(lastPos + instr.Length)];

                    var strOut = new StringOutput();
                    fmt.Format(instr, strOut);
                    var strTxt = strOut.ToString();

                    if (instr.IsInvalid) strTxt = strTxt.Replace("(BAD)", "???");

                    var byteStr = Convert.ToHexString(bytes);
                    Console.WriteLine($" {instr.IP16:X4} ( {byteStr} ) {strTxt}");
                }
            }

            var thread = new Thread(Loop)
            {
                Name = threadName, Priority = ThreadPriority.AboveNormal, IsBackground = false
            };
            thread.Start();
        }
    }
}