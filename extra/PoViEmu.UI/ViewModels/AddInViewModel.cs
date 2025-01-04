using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using PoViEmu.Core.Inventory;
using PoViEmu.I186.ABI;
using PoViEmu.Inventory.Utils;
using PoViEmu.UI.Tools;

// ReSharper disable ConvertConstructorToMemberInitializers

namespace PoViEmu.UI.ViewModels
{
    public partial class AddInViewModel : ViewModelBase
    {
        [ObservableProperty] private ObservableCollection<AddInInfoPlus<Bitmap>> _addIns = [];

        public void Load()
        {
            AddIns.Clear();
            const SearchOption o = SearchOption.AllDirectories;
            var folder = AppConst.Instance.DataRoot;
            var files = Directory.EnumerateFiles(folder, "*.bin", o);
            var tmp = new List<AddInInfoPlus<Bitmap>>();
            foreach (var file in files)
            {
                try
                {
                    var bytes = File.ReadAllBytes(file);
                    var raw = AddInReader.Read(bytes);
                    var info = raw.LoadImages(bytes);
                    tmp.Add(info);
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            foreach (var item in tmp.OrderBy(i => $"{i.Info.Name}:{i.Info.Version}"))
                AddIns.Add(item);
        }
    }
}