using System.IO;
using PoViEmu.Core;

namespace PoViEmu.Tests
{
    public static class FirmwareCheck
    {
        public static void DoShouldRead(string dir, string fileName)
        {
            var file = Path.Combine(dir, $"{fileName}.hex");
            using var stream = File.OpenRead(file);
            var binary = IntelHexReader.Extract(stream);

            var actual = binary;
            var bFile = Path.Combine(dir, $"{fileName}.bin");
            File.WriteAllBytes($"{bFile}.bin", actual);
            var expected = File.ReadAllBytes(bFile);
            TestTool.Equal(expected, actual);
        }
    }
}