using PoViEmu.UI.ViewModels;

namespace PoViEmu.UI.Core
{
    public interface IHasMain
    {
        MainViewModel Main { get; set; }
    }
}