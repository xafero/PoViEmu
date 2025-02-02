using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using PoViEmu.UI.Models;

namespace PoViEmu.UI.ViewModels
{
    public partial class StackViewModel : ViewModelBase
    {
        [ObservableProperty] private ObservableCollection<StackLine> _lines = new();

        public void Init()
        {
            Lines.Clear();

            Lines.Add(new StackLine("SS", 0x0000, 0x002B));
            Lines.Add(new StackLine("SS", 0x0002, 0xC400));
            Lines.Add(new StackLine("SS", 0x0004, 0x16B4));
            Lines.Add(new StackLine("SS", 0x0006, 0xC400));
            Lines.Add(new StackLine("SS", 0x0008, 0x0BAF));
        }
    }
}