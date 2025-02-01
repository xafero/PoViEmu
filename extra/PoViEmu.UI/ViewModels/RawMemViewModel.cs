using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using PoViEmu.UI.Models;

namespace PoViEmu.UI.ViewModels
{
    public partial class RawMemViewModel : ViewModelBase
    {
        [ObservableProperty] 
        private ObservableCollection<BytesLine> _lines;

        public RawMemViewModel()
        {
            Lines = new ObservableCollection<BytesLine>();
        }
    }
}
