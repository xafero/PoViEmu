using CommunityToolkit.Mvvm.ComponentModel;

namespace PoViEmu.UI.ViewModels
{
    public partial class TemplViewModel : ViewModelBase
    {
        [ObservableProperty] private string _debug;
    }
}