using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace PoViEmu.Common
{
    public static class SysInfo
    {
        public static string GetEntryDir<T>()
        {
            return GetEntryDir(typeof(T).Assembly);
        }

        public static string GetEntryDir(Assembly? assembly = null)
        {
            var ass = assembly ?? Assembly.GetEntryAssembly();
            var full = Path.GetFullPath(ass?.Location ?? string.Empty);
            full = Path.GetDirectoryName(full) ?? string.Empty;
            return full;
        }

        public static SystemInfo GetInfo()
        {
            return new()
            {
                Framework = RuntimeInformation.FrameworkDescription,
                OSDesc = RuntimeInformation.OSDescription,
                OSArch = RuntimeInformation.OSArchitecture.ToString(),
                OSVer = Environment.OSVersion.ToString(),
                HostName = Environment.MachineName,
                UserName = $"{Environment.UserDomainName}\\{Environment.UserName}",
                ProcPath = Environment.ProcessPath,
                CurrentDir = Environment.CurrentDirectory,
                ProcId = Environment.ProcessId
            };
        }
    }

    public record SystemInfo
    {
        public string? Framework { get; init; }
        public string? OSDesc { get; init; }
        public string? OSArch { get; init; }
        public string? OSVer { get; init; }
        public string? HostName { get; init; }
        public string? UserName { get; init; }
        public string? ProcPath { get; init; }
        public string? CurrentDir { get; init; }
        public int? ProcId { get; init; }
    }
}