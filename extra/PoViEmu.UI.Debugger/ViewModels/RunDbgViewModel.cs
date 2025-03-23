using PoViEmu.UI.Core;
using PoViEmu.UI.Routes;
using PoViEmu.UI.ViewModels;

namespace PoViEmu.UI.Dbg.ViewModels
{
    public partial class RunDbgViewModel : ViewModelBase, IRoutable
    {
        public RunInstViewModel? Base { get; set; }
    }
}