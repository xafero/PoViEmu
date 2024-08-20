using System;
using System.IO;
using System.Linq;
using System.Threading;
using Iced.Intel;
using Newtonsoft.Json;

// ReSharper disable HeuristicUnreachableCode

namespace PoViEmu.Tasty
{
    internal static class Program
    {
        private static NasmFormatter GetFormatter()
        {
            var options = new FormatterOptions
            {
                BinaryPrefix = "0b",
                BinarySuffix = null,
                HexPrefix = "0x",
                HexSuffix = null,
                SmallHexNumbersInDecimal = false,
                SpaceAfterOperandSeparator = true,
                UppercaseMnemonics = true
            };
            NasmFormatter formatter = new(options);
            return formatter;
        }

        private static Decoder GetDecoder(out MemoryStream stream)
        {
            stream = new MemoryStream();
            CodeReader reader = new StreamCodeReader(stream);
            var decoder = Decoder.Create(16, reader, DecoderOptions.NoInvalidCheck);
            return decoder;
        }

        private static void Main(string[] args)
        {
            var info = new VersionInfo();

            // var x = JsonConvert.SerializeObject(new { x = info }, Formatting.Indented);
            // Console.WriteLine(x);

            var threadName = $"{info.Product}Emulation";

            void Loop(object? _)
            {
                var fmt = GetFormatter();

                Instruction instr;
                var decoder = GetDecoder(out var mem);
                var rawBytes = Convert.FromHexString("2EFF2E050000F000F0FFFFFFFFFFFFFF2B0000C4B41600C4AF0B00C4300100C02B0000C4");
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