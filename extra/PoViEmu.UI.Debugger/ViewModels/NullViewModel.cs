using CommunityToolkit.Mvvm.ComponentModel;
using PoViEmu.UI.Core;
using PoViEmu.UI.Routes;

namespace PoViEmu.UI.Dbg.ViewModels
{
    public partial class NullViewModel : ViewModelBase, IRoutable
    {
        [ObservableProperty] private string? _modelName;
    }
}