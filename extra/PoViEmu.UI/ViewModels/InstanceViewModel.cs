using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using PoViEmu.Inventory.Config;
using PoViEmu.UI.Tools;

namespace PoViEmu.UI.ViewModels
{
    public partial class InstanceViewModel : ViewModelBase, IRoutable
    {
        [ObservableProperty] private IList<OneEntity> _instances;
    }
}