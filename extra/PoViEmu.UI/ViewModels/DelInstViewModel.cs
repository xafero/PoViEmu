using System;
using CommunityToolkit.Mvvm.ComponentModel;
using PoViEmu.UI.Core;
using PoViEmu.UI.Routes;

namespace PoViEmu.UI.ViewModels
{
    public partial class DelInstViewModel : ViewModelBase, IRoutable
    {
        [ObservableProperty] 
        private Guid _instanceId;

        [ObservableProperty]
        private string _instanceName;
    }
}