namespace PoViEmu.X86Decoding.Ops
{
    public enum Modifier
    {
        Unknown = 0,

        @byte,
        dword,
        far,
        qword,
        @short,
        to,
        tword,
        word
    }
}