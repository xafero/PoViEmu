using System;
using System.IO;

namespace PoViEmu.Core.Data
{
    public sealed class MemoInfo
    {
        public string FileName { get; set; }
        public long Size { get; set; }
        public DateTime LastWrite { get; set; }

        public void SetFile(string file)
        {
            FileName = Path.GetFileNameWithoutExtension(file);
            var info = new FileInfo(file);
            Size = info.Length;
            LastWrite = info.LastWriteTime;
        }
    }
}