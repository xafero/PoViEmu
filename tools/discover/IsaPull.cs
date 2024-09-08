using System;
using System.IO;
using System.Linq;
using ByteSizeLib;
using PoViEmu.Core;
using PoViEmu.Core.Addins;
using PoViEmu.Core.Hardware;

namespace Discover
{
    internal static class IsaPull
    {
        internal static void Run(Options opt)
        {
            var folder = Path.GetFullPath(opt.InputDir);

            const SearchOption o = SearchOption.AllDirectories;
            var files = Directory.EnumerateFiles(folder, "*.*", o);

            foreach (var file in files)
            {
                if (IsIgnoredExt(file) || IsIgnoredDir(file))
                    continue;
                var bytes = File.ReadAllBytes(file);
                if (bytes.GetMimeType() is not { } mt)
                    continue;
                var size = bytes.Length;
                Console.WriteLine($" * [{mt}] {file} ({ByteSize.FromBytes(size)})");

                var obj = mt.Load(bytes);
                if (obj is AddInInfo ai)
                {
                    Console.WriteLine($"    {ai.GetName()} v{ai.Version} (compiled at {ai.Compiled:u} for {ai.Model})");
                    
                }
                break;
            }
        }

        private static readonly string[] Ext =
        [
            "", ".sh", ".png", ".md", ".7z", ".sample", ".cs", ".csproj", ".py", ".yml",
            ".txt", ".json", ".props", ".bat", ".dll", ".exe", ".cache", ".pyc", ".info",
            ".targets", ".axaml", ".xml", ".manifest", ".ico", ".pack", ".pdb", ".h",
            ".doc", ".fr", ".it", ".es", ".de", ".pdf", ".bmt", ".bmp", ".c", ".a86",
            ".map", ".hex", ".dmp", ".10", ".ps1", ".tml", ".dtx", ".fish", ".obj",
            ".csh", ".rev", ".idx", ".lib", ".1", ".2", ".editorconfig", ".copycomplete",
            ".pth", ".so", ".nupkg", ".gitignore", ".sln", ".zip", ".ttf", ".cmd",
            ".cfg", ".desktop", ".name", ".notice", ".license", ".about", ".rst",
            ".tmpl", ".pyi", ".apache", ".gz", ".dylib", ".nuspec", ".user", ".tests",
            ".pem", ".testcase", ".mo", ".po", ".js", ".bsd", ".typed"
        ];

        private static bool IsIgnoredExt(string file)
            => Ext.Contains(Path.GetExtension(file).ToLowerInvariant());

        private static bool IsIgnoredDir(string file)
        {
            var s = Path.DirectorySeparatorChar;
            return file.Contains($"{s}.venv{s}");
        }
    }
}