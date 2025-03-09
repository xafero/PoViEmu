using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using PoViEmu.UI.Core;
using PoViEmu.UI.Routes;

namespace PoViEmu.UI.ViewModels
{
    public partial class CreateInstViewModel : ValidModelBase, IRoutable
    {
        [ObservableProperty] 
        private string _templateName;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Name is required")]
        [MinLength(1, ErrorMessage = "Name should be longer than zero")]
        [MaxLength(40, ErrorMessage = "Name should not be too long")]
        private string _instanceName;

        [ObservableProperty] 
        [NotifyDataErrorInfo] 
        [MaxLength(200, ErrorMessage = "Notes should not be too long")]
        private string _instanceNotes;
    }
}