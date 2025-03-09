using System;
using CommunityToolkit.Mvvm.ComponentModel;
using PoViEmu.UI.Core;
using PoViEmu.UI.Routes;

namespace PoViEmu.UI.ViewModels
{
    public partial class RunInstViewModel : ViewModelBase, IRoutable
    {
        [ObservableProperty] private Guid _instanceId;
    }
}