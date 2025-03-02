using CommunityToolkit.Mvvm.ComponentModel;
using PoViEmu.UI.Tools;

namespace PoViEmu.UI.ViewModels
{
    public partial class CreateInstViewModel : ViewModelBase, IRoutable
    {
        [ObservableProperty] private string _templateName;

        [ObservableProperty] private string _instanceName;

        [ObservableProperty] private string _instanceNotes;
    }
}