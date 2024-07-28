using System;
using System.Diagnostics;
using System.IO;

namespace PoViEmu.Tests
{
    public static class NasmTool
    {
        public static string DisassembleNasm(byte[] bytes)
        {
            string text;
            var tmpFile = Path.GetTempFileName();
            try
            {
                File.WriteAllBytes(tmpFile, bytes);
                string[] pArgs = ["-b", "16", "-p", "intel", tmpFile];
                var info = new ProcessStartInfo("ndisasm", pArgs)
                {
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };
                using var proc = Process.Start(info);
                proc!.WaitForExit(TimeSpan.FromSeconds(5));
                text = proc.StandardOutput.ReadToEnd();
                text += proc.StandardError.ReadToEnd();
            }
            finally
            {
                File.Delete(tmpFile);
            }
            text = text.Trim();
            return text;
        }
    }
}