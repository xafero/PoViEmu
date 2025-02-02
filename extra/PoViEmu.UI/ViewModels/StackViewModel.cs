using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using PoViEmu.UI.Models;

namespace PoViEmu.UI.ViewModels
{
    public abstract partial class StackViewModel : ViewModelBase
    {
        [ObservableProperty] private ObservableCollection<BytesLine> _lines = new();
    }
}