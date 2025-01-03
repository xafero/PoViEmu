using System.IO;
using System.Text;
using PoViEmu.Base;
using PoViEmu.Base.CPU.Diff;
using PoViEmu.I186.CPU.Impl;
using PoViEmu.Tests.Base;
using Xunit;
using static PoViEmu.I186.CPU.ExecTool;

namespace PoViEmu.Tests.CPU
{
    public static class CompatCheck
    {
        public static void DoShouldRead(string dir, string fileName)
        {
            var fmt = Defaults.ValFormatter;

            var comFile = Path.Combine(dir, $"{fileName}.com");
            var txtFile = Path.Combine(dir, $"{fileName}.txt");

            var comBytes = File.ReadAllBytes(comFile);
            var expected = TextHelper.ReadUtf8Lines(txtFile);

            var (@out, ret, diff) = Execute(comBytes);
            var actual = @out.ToLines();
            var changes = diff.ToChangeLines(fmt, ignoreIP: false);

            var obj = JsonHelper.ToJson(new { Out = actual, Log = changes });
            var ojf = Path.Combine(dir, $"{fileName}.d.json");
            File.WriteAllText(ojf, obj, Encoding.UTF8);

            TestTool.Equal(expected, actual);
            Assert.Equal("0", $"{ret}");
        }
    }
}