using CommunityToolkit.Mvvm.ComponentModel;

namespace PoViEmu.UI.Core
{
    public abstract class ValidModelBase : ObservableValidator, IViewModelBase
    {
        public void ForceCheck()
        {
            ValidateAllProperties();
        }
    }
}