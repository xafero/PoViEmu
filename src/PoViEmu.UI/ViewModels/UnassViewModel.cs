using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using PoViEmu.Common;
using PoViEmu.Core.Decoding;
using PoViEmu.UI.Models;

// using PoViEmu.X86Decoding;

namespace PoViEmu.UI.ViewModels
{
    public partial class UnassViewModel : ViewModelBase
    {
        [ObservableProperty] private ObservableCollection<AssemblyLine> _assembly = new();

        public void Init()
        {
            var stuffDir = SysInfo.GetEntryDir<UnassViewModel>().GetChild("Stuff");
            var file = stuffDir.GetChild("sample.bin");

            using var stream = File.OpenRead(file);
            stream.Seek(1524, SeekOrigin.Begin);

            using var reader = new MemCodeReader(stream);
            var lines = reader.Decode(0x0).Select(t => t.ToString());
            var objects = lines.Select(l =>
            {
                var parts = TextHelper.SplitOn(l);
                return new AssemblyLine(parts[0], parts[1], parts[2]);
            });
            Assembly.Clear();
            Array.ForEach(objects.Skip(4).Take(60).ToArray(), o => Assembly.Add(o));
        }
    }
}