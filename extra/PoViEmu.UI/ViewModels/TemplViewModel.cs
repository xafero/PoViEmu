using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using PoViEmu.Inventory.Upper;

namespace PoViEmu.UI.ViewModels
{
    public partial class TemplViewModel : ViewModelBase
    {
        [ObservableProperty] private string _debug;

        [ObservableProperty] private IList<TemplEntry> _templates;

        [ObservableProperty] private bool _showNextBtn;

        [ObservableProperty] private TemplEntry _selected;
    }
}