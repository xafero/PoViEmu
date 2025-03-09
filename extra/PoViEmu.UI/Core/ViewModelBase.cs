using CommunityToolkit.Mvvm.ComponentModel;

namespace PoViEmu.UI.Core
{
    public interface IViewModelBase
    {
    }

    public abstract class ViewModelBase : ObservableObject, IViewModelBase
    {
    }

    public abstract class ValidModelBase : ObservableValidator, IViewModelBase
    {
        public void ForceCheck()
        {
            ValidateAllProperties();
        }
    }
}