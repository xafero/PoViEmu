
// ReSharper disable InconsistentNaming

namespace PoViEmu.Hyper
{
    public record VMConfig(
        CpuKind Kind,
        int Cycles,
        double Hertz
    );
}