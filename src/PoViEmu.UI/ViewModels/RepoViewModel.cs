using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using PoViEmu.Common;
using PoViEmu.Core.Inventory;
using PoViEmu.UI.Models;

namespace PoViEmu.UI.ViewModels
{
    public partial class RepoViewModel : ViewModelBase
    {
        [ObservableProperty] private ObservableCollection<AddInPlusItem> _addIn = new();
        [ObservableProperty] private ObservableCollection<SystemItem> _system = new();
        [ObservableProperty] private ObservableCollection<BiosItem> _bios = new();

        public async Task Init()
        {
            var root = PathHelper.CurrentDir;
            var repo = AppRepo.Instance;
            await repo.Load(root);

            foreach (var item in repo.SearchAddIn(null).Select(i => new AddInPlusItem(i)))
                AddIn.Add(item);

            foreach (var item in repo.SearchSystem(null))
                System.Add(item);

            foreach (var item in repo.SearchBios(null))
                Bios.Add(item);
        }
    }
}