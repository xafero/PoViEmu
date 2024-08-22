using System.IO;
using PoViEmu.Common;
using PoViEmu.Core;
using PoViEmu.Core.Dumps;

namespace PoViEmu.Tests
{
    public static class DumpCheck
    {
        public static void DoShouldRead(string dir, string fileName, string ext = ".bin")
        {
            var file = Path.Combine(dir, $"{fileName}{ext}");
            using var stream = File.OpenRead(file);
            var dump = DumpReader.Read(stream);
            dump.LoadOsAddIns(stream);

            var actual = JsonHelper.ToJson(dump);
            var jFile = Path.Combine(dir, $"{fileName}.json");
            var expected = TextHelper.ToText(jFile);
            TestTool.Equal(expected, actual);
        }
    }
}