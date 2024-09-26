using System;
using System.IO;

namespace PoViEmu.Core.Data
{
    public sealed class TwfInfo
    {
        public string AppInfo { get; set; }
        public TwfModel Device { get; set; }
        public string ProgName { get; set; }
        
        public long Size { get; set; }
        public DateTime? Created { get; set; }
        public string FileName { get; set; }

        public void SetFile(string file)
        {
            FileName = Path.GetFileNameWithoutExtension(file);
            var info = new FileInfo(file);
            Size = info.Length;
            Created = info.CreationTime;
        }
    }
}