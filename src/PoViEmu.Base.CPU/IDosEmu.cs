using System.IO;

namespace PoViEmu.Base.CPU
{
    public interface IDosEmu
    {
        StringWriter StdOut { get; }

        byte? ReturnCode { get; }
    }
}