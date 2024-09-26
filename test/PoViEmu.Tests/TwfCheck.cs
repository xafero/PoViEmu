using System.IO;
using PoViEmu.Common;
using PoViEmu.Core.Data;

namespace PoViEmu.Tests
{
    public static class TwfCheck
    {
        public static void DoShouldRead(string dir, string fileName)
        {
            var file = Path.Combine(dir, $"{fileName}.twf");
            using var stream = File.OpenRead(file);
            var addIn = TwfReader.Read(stream);
            addIn.SetFile(file);

            var actual = JsonHelper.ToJson(addIn);
            var jFile = Path.Combine(dir, $"{fileName}.json");
            var expected = TextHelper.ToText(jFile);
            TestTool.Equal(expected, actual);
        }
    }
}