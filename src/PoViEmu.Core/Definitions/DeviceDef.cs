using ByteSizeLib;

namespace PoViEmu.Core.Definitions
{
    public record DeviceDef(
        string Name,
        int Year,
        string Model,
        string Chip,
        ByteSize Mem,
        ScreenSize Size,
        string Port,
        string Market
    );
}