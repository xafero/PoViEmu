using System.Collections.Generic;

namespace PoViEmu.Core.Decoding
{
    public interface IMemBlob
    {
        IEnumerable<byte> GetBytes();
    }
}