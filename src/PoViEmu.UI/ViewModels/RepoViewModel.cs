using CommunityToolkit.Mvvm.ComponentModel;

namespace PoViEmu.UI.ViewModels
{
    public partial class RepoViewModel : ViewModelBase
    {
        [ObservableProperty] private string _title = string.Empty;

        public void Init()
        {
        }
    }
}