using System.IO;
using System.Text.Json.Serialization;
using PoViEmu.Inventory.Upper;
using PoViEmu.Inventory.Utils;

namespace PoViEmu.UI.Models
{
    public record SystemPlusItem(SystemItem Item)
    {
        [JsonIgnore] public bool Exists => File.Exists(FileRepo.GetFilePath(Item).file);
    }
}