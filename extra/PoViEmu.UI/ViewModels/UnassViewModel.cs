using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using PoViEmu.UI.Models;

namespace PoViEmu.UI.ViewModels
{
    public partial class UnassViewModel : ViewModelBase
    {
        [ObservableProperty] 
        private ObservableCollection<AssemblyLine> _lines;

        public UnassViewModel()
        {
            Lines = new ObservableCollection<AssemblyLine>();
        }
    }
}