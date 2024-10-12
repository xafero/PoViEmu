namespace PoViEmu.Common
{
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