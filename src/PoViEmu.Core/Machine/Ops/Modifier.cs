// ReSharper disable InconsistentNaming

namespace PoViEmu.Core.Machine.Ops
{
    public enum Modifier
    {
        Unknown = 0,

        to,
        
        @byte,
        
        @short,
        
        far,
        
        dword,
        
        tword,
        
        qword,
        
        word,
        
        // TODO
        byteP,
        
        byteM
    }
}