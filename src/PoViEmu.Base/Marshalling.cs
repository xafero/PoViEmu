using System.Runtime.InteropServices;

namespace PoViEmu.Base
{
    public static class Marshalling
    {
        public static T Read<T>(byte[] bytes)
        {
            T result;
            var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            try
            {
                var pointer = handle.AddrOfPinnedObject();
                result = Marshal.PtrToStructure<T>(pointer)!;
            }
            finally
            {
                handle.Free();
            }
            return result;
        }
    }
}