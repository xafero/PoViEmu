namespace PoViEmu.Base.ISA
{
    public interface IMemAccess<T>
    {
        T Get(ushort seg, ushort off);

        void Set(ushort seg, ushort off, T value);
    }
}