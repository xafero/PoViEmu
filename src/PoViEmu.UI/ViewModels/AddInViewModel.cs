using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using PoViEmu.Common;
using PoViEmu.Core;
using PoViEmu.Core.Images;
using PoViEmu.UI.Tools;

// ReSharper disable ConvertConstructorToMemberInitializers

namespace PoViEmu.UI.ViewModels
{
    public partial class AddInViewModel : ViewModelBase
    {
        [ObservableProperty] private ObservableCollection<AddInInfoPlus<Bitmap>> _addIns = new();
        [ObservableProperty] private string _folder;

        public AddInViewModel()
        {
            var home = EnvHelper.GetHomeDir()!;
            _folder = Path.Combine(home, "Desktop", "Apps_cp1o", "Extra"); // "User_Bin"
        }

        public void Load()
        {
            AddIns.Clear();
            const SearchOption o = SearchOption.AllDirectories;
            var files = Directory.EnumerateFiles(Folder, "*.bin", o);
            var tmp = new List<AddInInfoPlus<Bitmap>>();
            foreach (var file in files)
            {
                var bytes = File.ReadAllBytes(file);
                var raw = AddInReader.Read(bytes);
                var info = raw.LoadImages(bytes);
                tmp.Add(info);
            }
            foreach (var item in tmp.OrderBy(i => $"{i.Info.Name}:{i.Info.Version}"))
                AddIns.Add(item);
        }
    }
}