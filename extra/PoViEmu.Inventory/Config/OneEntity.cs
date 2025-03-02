using System;

namespace PoViEmu.Inventory.Config
{
    public class OneEntity
    {
        public required Guid Id { get; set; }

        public required string Name { get; set; }

        public required string Notes { get; set; }

        public required string Template { get; set; }
    }
}