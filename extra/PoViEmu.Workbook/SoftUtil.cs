using System;
using PoViEmu.Base.CPU;
using PoViEmu.I186.CPU;
using PoViEmu.I186.CPU.Soft;
using PoViEmu.SH3.CPU;
using PoViEmu.SH3.CPU.Soft;
using WareTool = PoViEmu.SH3.CPU.Soft.WareTool;

namespace PoViEmu.Workbook.CPU
{
    internal static class SoftUtil
    {
        internal static bool GetDosOut(ICpu c, out string stdTxt, out string retNum)
        {
            stdTxt = retNum = null;
            IDosEmu dos = c is NC3022 nc ? nc.GetDOS()
                : c is SH7291 sc ? sc.GetDOS()
                : throw new InvalidOperationException("No DOS!");

            var stdOut = dos.StdOut;
            var stdBld = stdOut.GetStringBuilder();
            if (stdBld.Length >= 1)
            {
                stdTxt = $"{stdOut}";
                stdBld.Clear();
            }
            if (dos.ReturnCode is { } retCode)
            {
                retNum = $"{retCode:X4}";
            }
            return stdTxt != null || retNum != null;
        }
    }
}