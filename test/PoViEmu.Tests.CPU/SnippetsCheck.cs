using PoViEmu.Base;
using System.Linq;
using Xunit;
using PoViEmu.Base.CPU.Diff;
using ExeT2 = PoViEmu.SH3.CPU.ExecTool;
using DefT2 = PoViEmu.SH3.CPU.Impl.Defaults;

namespace PoViEmu.Tests.CPU
{
    public static class SnippetsCheck
    {
        public static (string[] changes, byte? ret, string[] actual)
            DoShouldExec(byte[] bytes, string code, string[] input)
        {
            var fmt = DefT2.ValFormatter;
            var first = true;
            var (@out, ret, diff) = ExeT2.Execute(bytes, act: s =>
                {
                    for (var i = 0; i < input.Length; i += 2)
                        s[input[i]] = input[i + 1];
                    s.WriteMemory(s.PC, bytes);
                },
                beforeExec: (x, _) =>
                {
                    if (!first) return;
                    Assert.Equal(code, x.ToString().Split("  ", 3).Last().RemoveSpaces());
                    first = false;
                });
            var actual = @out.ToLines();
            var changes = diff.ToChangeLines(fmt, ignoreIP: true);
            return (changes, ret, actual);
        }
    }
}