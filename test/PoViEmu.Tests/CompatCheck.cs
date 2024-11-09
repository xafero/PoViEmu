using System.IO;
using PoViEmu.Common;
using System.Text;
using static PoViEmu.Core.Compat.StateTool;

namespace PoViEmu.Tests
{
    public static class CompatCheck
    {
        public static void DoShouldRead(string dir, string fileName)
        {
            var comFile = Path.Combine(dir, $"{fileName}.com");
            var txtFile = Path.Combine(dir, $"{fileName}.txt");

            var comBytes = File.ReadAllBytes(comFile);
            var expected = TextHelper.ReadUtf8Lines(txtFile);

            var (@out, ret, diff) = Execute(comBytes);
            var actual = @out.ToLines();

            // TODO
            var obj = JsonHelper.ToJson(new { Return = ret, Diff = diff });
            File.WriteAllText($"{fileName}.json", obj, Encoding.UTF8);

            TestTool.Equal(expected, actual);
        }
    }
}