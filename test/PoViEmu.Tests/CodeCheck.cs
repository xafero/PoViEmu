using System;
using System.IO;
using PoViEmu.Core.Decoding;
using PoViEmu.Core.Hardware;

namespace PoViEmu.Tests
{
    public static class CodeCheck
    {
        public static void DoShouldRead(string dir, string fileName)
        {
            var file = Path.Combine(dir, $"{fileName}.tml");
            var state = IniStateTool.ReadFile(file);

            var cpu = new NC3022();
            foreach (var (seg, i) in state.ToInstructions())
            {
                try
                {
                    cpu.Execute(state, i.Parsed);
                }
                catch (Exception ex)
                {
                    var debug = $"{seg:x4}:{i:x4}".Trim();
                    throw new InvalidOperationException(debug, ex);
                }
            }
        }
    }
}