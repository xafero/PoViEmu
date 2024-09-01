using System.IO;
using System.Text.Json.Serialization;
using PoViEmu.Core.Inventory;

namespace PoViEmu.UI.Models
{
    public record SystemPlusItem(SystemItem Item)
    {
        [JsonIgnore] public bool Exists => File.Exists(AppRepo.Instance.GetFilePath(Item).file);
    }
}