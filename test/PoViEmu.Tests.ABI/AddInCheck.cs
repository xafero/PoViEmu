using System.IO;
using PoViEmu.Base;
using PoViEmu.I186.ABI;
using PoViEmu.Tests.Base;

namespace PoViEmu.Tests.ABI
{
    public static class AddInCheck
    {
        public static void DoShouldRead(string dir, string fileName)
        {
            var file = Path.Combine(dir, $"{fileName}.bin");
            using var stream = File.OpenRead(file);
            var addIn = AddInReader.Read(stream);

            var actual = JsonHelper.ToJson(addIn);
            var jFile = Path.Combine(dir, $"{fileName}.json");
            var expected = TextHelper.ToText(jFile);
            TestTool.Equal(expected, actual);
        }
    }
}