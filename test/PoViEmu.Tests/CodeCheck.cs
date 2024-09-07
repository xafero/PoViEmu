using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PoViEmu.Core.Decoding;
using PoViEmu.Core.Diffs;
using PoViEmu.Core.Hardware;
using Xunit;

namespace PoViEmu.Tests
{
    public static class CodeCheck
    {
        public static void DoShouldRead(string dir, string fileName)
        {
            var file = Path.Combine(dir, $"{fileName}.tml");
            var state = IniStateTool.ReadFile(file);

            var cpu = new NC3022();
            var changes = new List<string>();

            foreach (var (seg, i) in state.ToInstructions())
            {
                var before = state.ToAllToString();
                try
                {
                    cpu.Execute(ref state, i.Parsed);
                }
                catch (Exception ex)
                {
                    var debug = $"{seg:x4}:{i:x4}".Trim();
                    throw new InvalidOperationException(debug, ex);
                }
                var after = state.ToAllToString();

                var diff = DiffUtil.Check(before, after).ToStr();
                changes.Add(string.Join(" ", diff));
            }

            var oFile = Path.Combine(dir, $"{fileName}.txt");
            File.WriteAllLines(oFile, changes, Encoding.UTF8);

            var iFile = Path.Combine(dir, $"{fileName}.dtx");
            var expected = File.ReadAllLines(iFile, Encoding.UTF8);
            Assert.Equal(expected, actual: changes);
        }
    }
}