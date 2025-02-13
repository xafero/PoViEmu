using System.Collections.Generic;
using PoViEmu.Compression;

namespace PoViEmu.Inventory.Infos
{
    public static class ModelExt
    {
        public record ChipArray(byte[] Raw, Chip Obj);

        public static IDictionary<string, ChipArray> GetFiles(this ModelTree mt)
        {
            var dict = new Dictionary<string, ChipArray>();
            foreach (var level in mt.Groups)
            foreach (var item in level.Value.Chips)
                if (item.Value is { File: { } file } chip)
                {
                    var cmp = new Compressed(CompressAlgo.Brotli, file.Brotli);
                    var raw = cmp.Decompress();
                    chip.File.Brotli = [];
                    dict.Add(file.Name.ToLowerInvariant(), new(raw, chip));
                }
            return dict;
        }
    }
}