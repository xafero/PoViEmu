using CommunityToolkit.Mvvm.ComponentModel;

namespace PoViEmu.UI.ViewModels
{
    public partial class HandheldViewModel : ViewModelBase
    {
        [ObservableProperty] private string _title = string.Empty;
    }
}