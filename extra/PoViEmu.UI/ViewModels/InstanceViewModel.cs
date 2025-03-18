using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using PoViEmu.Inventory.Config;
using PoViEmu.UI.Core;
using PoViEmu.UI.Routes;

namespace PoViEmu.UI.ViewModels
{
    public partial class InstanceViewModel : ViewModelBase, INavigable
    {
        [ObservableProperty] private string? _debug;

        [ObservableProperty] private IList<OneEntity>? _instances;

        [ObservableProperty] private bool _showNextBtn;

        [ObservableProperty] private OneEntity? _selected;

        public void OnBack()
        {
            Debug = null;
            ShowNextBtn = false;
            Selected = null;
        }
    }
}