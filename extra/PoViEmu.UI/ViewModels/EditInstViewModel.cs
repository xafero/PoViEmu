using CommunityToolkit.Mvvm.ComponentModel;
using System;
using PoViEmu.UI.Routes;

namespace PoViEmu.UI.ViewModels
{
    public partial class EditInstViewModel : CreateInstViewModel, IRoutable
    {
        [ObservableProperty] 
        private Guid _instanceId;
    }
}