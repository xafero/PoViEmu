using System;
using CommunityToolkit.Mvvm.ComponentModel;
using PoViEmu.UI.Core;
using PoViEmu.UI.Routes;

namespace PoViEmu.UI.ViewModels
{
    public partial class RunInstViewModel : ViewModelBase, IRoutable, IHasMain
    {
        [ObservableProperty] private Guid _instanceId;

        [ObservableProperty] private bool _viewIsMinimal;

        [ObservableProperty] private MainViewModel _main;
    }
}