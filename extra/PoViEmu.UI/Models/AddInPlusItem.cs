using System.IO;
using System.Text.Json.Serialization;
using Avalonia.Media.Imaging;
using PoViEmu.Inventory.Upper;
using PoViEmu.Inventory.Utils;
using PoViEmu.UI.Tools;

namespace PoViEmu.UI.Models
{
    public record AddInPlusItem(AddInItem Item)
    {
        [JsonIgnore] public Bitmap MenuIcon => Item.Entry.MenuIcon.Png.LoadImage();
        [JsonIgnore] public bool Exists => File.Exists(AppRepo.Instance.GetFilePath(Item).file);
    }
}