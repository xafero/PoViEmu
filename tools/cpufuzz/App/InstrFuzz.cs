using System;
using System.IO;
using PoViEmu.Core.Machine;
using PoViEmu.Tests;
using PoViEmu.CpuFuzzer.Core;

namespace PoViEmu.CpuFuzzer.App
{
    public static class InstrFuzz
    {
        private static bool IsIgnored(string expected)
        {
            return expected.Contains("  db ") || expected.Contains("  64") || expected.Contains("  67")
                   || expected.Contains("  26") || expected.Contains("  36") || expected.Contains("  F3")
                   || expected.Contains("  65") || expected.Contains("  F2") || expected.Contains("  0F")
                   || expected.Contains("  2E") || expected.Contains("  9B") || expected.Contains("  F0")
                   || expected.Contains("  3E") || expected.Contains("  66");
        }

        public static void Start()
        {
            const int byteCount = 9;
            const int tryCount = 6;

            var bytes = new byte[byteCount];
            var rnd = Random.Shared;

            for (var i = 0; i < tryCount; i++)
            {
                rnd.NextBytes(bytes);

                var expected = NasmTool.DisassembleNasm(bytes);
                Console.Write($" [{i}]");

                if (IsIgnored(expected))
                {
                    i -= 1;
                    continue;
                }
                Console.WriteLine();

                var actual = new MemoryStream(bytes).Disassemble().ToText();
                var diffs = DiffTool.DiffThis(expected, actual);
                if (diffs.Count == 0)
                {
                    Console.WriteLine("    => OK !");
                    Console.WriteLine();
                    i -= 1;
                    continue;
                }

                foreach (var diff in diffs)
                    Console.WriteLine($"    {diff}");
                Console.WriteLine();
                break;
            }
        }
    }
}