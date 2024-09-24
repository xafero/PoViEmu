using PoViEmu.Core.Modules;
using System.IO;
using PoViEmu.Common;

namespace PoViEmu.Tests
{
    public static class PvaCheck
    {
        public static void DoShouldRead(string dir, string fileName)
        {
            var file = Path.Combine(dir, $"{fileName}.pva");
            using var stream = File.OpenRead(file);
            var addIn = PvaReader.Read(stream);

            var actual = JsonHelper.ToJson(addIn);
            var jFile = Path.Combine(dir, $"{fileName}.json");
            var expected = TextHelper.ToText(jFile);
            TestTool.Equal(expected, actual);
        }
    }
}