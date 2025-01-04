using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using PoViEmu.Inventory.Upper;
using PoViEmu.UI.Models;

namespace PoViEmu.UI.ViewModels
{
    public partial class RepoViewModel : ViewModelBase
    {
        [ObservableProperty] private ObservableCollection<AddInPlusItem> _addIn = [];
        [ObservableProperty] private ObservableCollection<SystemPlusItem> _system = [];
        [ObservableProperty] private ObservableCollection<BiosPlusItem> _bios = [];

        public async Task Init()
        {
            var repo = AppRepo.Instance;
            await repo.Load();

            foreach (var item in repo.SearchAddIn(null).Select(i => new AddInPlusItem(i)))
                AddIn.Add(item);

            foreach (var item in repo.SearchSystem(null).Select(i => new SystemPlusItem(i)))
                System.Add(item);

            foreach (var item in repo.SearchBios(null).Select(i => new BiosPlusItem(i)))
                Bios.Add(item);
        }
    }
}