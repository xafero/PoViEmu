using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using PoViEmu.Common;
using PoViEmu.Core.Inventory;

namespace PoViEmu.UI.ViewModels
{
    public partial class RepoViewModel : ViewModelBase
    {
        [ObservableProperty] private ObservableCollection<string> _addIn = new();
        [ObservableProperty] private ObservableCollection<string> _system = new();
        [ObservableProperty] private ObservableCollection<string> _bios = new();

        public async Task Init()
        {
            var root = PathHelper.CurrentDir;
            var repo = AppRepo.Instance;
            await repo.Load(root);

            var a1 = repo.SearchAddIn("chess").First();
            var x1 = await repo.GetCached(root, a1);
            AddIn.Add(JsonHelper.ToJson(x1));

            var a2 = repo.SearchSystem(a1.Model).First();
            var x2 = await repo.GetCached(root, a2);
            System.Add(JsonHelper.ToJson(x2));

            var a3 = repo.SearchBios(a2.Model).First();
            var x3 = await repo.GetCached(root, a3);
            Bios.Add(JsonHelper.ToJson(x3));
        }
    }
}