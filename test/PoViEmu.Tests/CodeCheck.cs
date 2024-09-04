using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PoViEmu.Core.Decoding;

namespace PoViEmu.Tests
{
    public static class CodeCheck
    {
        public static void DoShouldRead(string dir, string fileName)
        {
            var file = Path.Combine(dir, $"{fileName}.raw");
            var bytes = File.ReadAllBytes(file);

            var state = new MachineState
            {
                Memory = { [0] = new Dictionary<ushort, List<byte>> { { 0, bytes.ToList() } } }
            };
            var debug = state.ToCodeString();
            throw new InvalidOperationException(debug);
        }
    }
}