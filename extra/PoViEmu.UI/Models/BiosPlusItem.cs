using System.IO;
using System.Text.Json.Serialization;
using PoViEmu.Core.Inventory;
using PoViEmu.Inventory;

namespace PoViEmu.UI.Models
{
    public record BiosPlusItem(BiosItem Item)
    {
        [JsonIgnore] public bool Exists => File.Exists(AppRepo.Instance.GetFilePath(Item).file);
    }
}