using CommunityToolkit.Mvvm.ComponentModel;

namespace PoViEmu.UI.ViewModels
{
    public partial class MinimalViewModel : ViewModelBase
    {
        [ObservableProperty] private string _title = string.Empty;
    }
}