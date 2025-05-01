using System;

// ReSharper disable InconsistentNaming

namespace PoViEmu.Hyper
{
    public record VMConfig(
        Guid Id,
        CpuKind Kind,
        int Cycles,
        double Hertz
    );
}