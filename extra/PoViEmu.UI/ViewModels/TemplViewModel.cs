using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using PoViEmu.Inventory.Upper;
using PoViEmu.UI.Core;
using PoViEmu.UI.Routes;

namespace PoViEmu.UI.ViewModels
{
    public partial class TemplViewModel : ViewModelBase, INavigable
    {
        [ObservableProperty] private string? _debug;

        [ObservableProperty] private IList<TemplEntry>? _templates;

        [ObservableProperty] private bool _showNextBtn;

        [ObservableProperty] private TemplEntry? _selected;

        public void OnBack()
        {
            Debug = null;
            ShowNextBtn = false;
            Selected = null;
        }
    }
}