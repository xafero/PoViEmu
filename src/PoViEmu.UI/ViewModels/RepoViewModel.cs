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

            var a1 = repo.SearchAddIn("chess").First();
            AddIn.Add(new AddInPlusItem(a1));

            var a2 = repo.SearchSystem(a1.Model).First();
            System.Add(a2);

            var a3 = repo.SearchBios(a2.Model).First();
            Bios.Add(a3);
        }
    }
}