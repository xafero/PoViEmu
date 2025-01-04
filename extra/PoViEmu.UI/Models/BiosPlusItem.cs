using System.IO;
using System.Text.Json.Serialization;
using PoViEmu.Inventory.Upper;

namespace PoViEmu.UI.Models
{
    public record BiosPlusItem(BiosItem Item)
    {
        [JsonIgnore] public bool Exists => File.Exists(AppRepo.Instance.GetFilePath(Item).file);
    }
}