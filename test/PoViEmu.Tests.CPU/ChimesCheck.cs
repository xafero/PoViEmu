using PoViEmu.Base;
using System.IO;
using System.Text;
using PoViEmu.Base.CPU.Diff;
using ExeT1 = PoViEmu.I186.CPU.ExecTool;
using DefT1 = PoViEmu.I186.CPU.Impl.Defaults;
using ExeT2 = PoViEmu.SH3.CPU.ExecTool;
using DefT2 = PoViEmu.SH3.CPU.Impl.Defaults;

namespace PoViEmu.Tests.CPU
{
    public static class ChimesCheck
    {
        public static void DoShouldRead(string dir, string fileName)
        {
            var com1File = Path.Combine(dir, $"{fileName}_x86.com");
            var com2File = Path.Combine(dir, $"{fileName}_sh3.com");
            var txtFile = Path.Combine(dir, $"{fileName}.txt");

            var com1Bytes = File.ReadAllBytes(com1File);
            var com2Bytes = File.ReadAllBytes(com2File);
            var expected = TextHelper.ReadUtf8Lines(txtFile);

            var (actual1, ret1) = Exec1(com1Bytes, dir, com1File);
            var (actual2, ret2) = Exec2(com2Bytes, dir, com2File);

            // TODO TestTool.Equal(expected, actual1);
            // TODO Assert.Equal("0", $"{ret1}");

            // TODO TestTool.Equal(expected, actual2);
            // TODO Assert.Equal("0", $"{ret2}");
        }

        private static (string[] t, byte? r) Exec1(byte[] comBytes, string dir, string fName)
        {
            var fmt = DefT1.ValFormatter;
            var (@out, ret, diff) = ExeT1.Execute(comBytes);
            var actual = @out.ToLines();
            var changes = diff.ToChangeLines(fmt, ignoreIP: false);
            WriteDebug(dir, fName, actual, changes);
            return (actual, ret);
        }

        private static (string[] t, byte? r) Exec2(byte[] comBytes, string dir, string fName)
        {
            var fmt = DefT2.ValFormatter;
            var (@out, ret, diff) = ExeT2.Execute(comBytes);
            var actual = @out.ToLines();
            var changes = diff.ToChangeLines(fmt, ignoreIP: false);
            WriteDebug(dir, fName, actual, changes);
            return (actual, ret);
        }

        private static void WriteDebug(string dir, string fileName, object actual, object changes)
        {
            var shortName = Path.GetFileNameWithoutExtension(fileName);
            var obj = JsonHelper.ToJson(new { Out = actual, Log = changes });
            var ojf = Path.Combine(dir, $"{shortName}.d.json");
            File.WriteAllText(ojf, obj, Encoding.UTF8);
        }
    }
}