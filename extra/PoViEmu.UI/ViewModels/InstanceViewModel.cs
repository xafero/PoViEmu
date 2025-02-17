using CommunityToolkit.Mvvm.ComponentModel;

namespace PoViEmu.UI.ViewModels
{
    public partial class InstanceViewModel : ViewModelBase
    {
        [ObservableProperty] private string _templateName;

        [ObservableProperty] private string _instanceName;

        [ObservableProperty] private string _instanceNotes;
    }
}