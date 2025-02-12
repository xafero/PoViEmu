using System;
using PoViEmu.Inventory.Infos;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable InconsistentNaming

namespace PoViEmu.Inventory.Upper
{
    public record TemplEntry(
        string P,
        DateTime Changed,
        ModelKind Kind,
        string Name,
        Display Display,
        ModelId Internal,
        string Label,
        Storage Storage,
        string[] Ports,
        string CPU,
        string Market
    ) : IRelUrl
    {
        public string BuildUrl(string @base) => $"{@base}/{P}";
    }
}