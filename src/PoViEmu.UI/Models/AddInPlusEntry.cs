using System.Text.Json.Serialization;
using Avalonia.Media.Imaging;
using PoViEmu.Core.Inventory;
using PoViEmu.UI.Tools;

namespace PoViEmu.UI.Models
{
    public record AddInPlusItem(AddInItem Item)
    {
        [JsonIgnore] public Bitmap MenuIcon => Item.Entry.MenuIcon.Png.LoadImage();
        [JsonIgnore] public Bitmap ListIcon => Item.Entry.ListIcon.Png.LoadImage();
    }
}