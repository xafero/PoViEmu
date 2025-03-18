using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace PoViEmu.UI.ViewModels
{
    public partial class EditInstViewModel : CreateInstViewModel
    {
        [ObservableProperty] 
        private Guid _instanceId;
    }
}