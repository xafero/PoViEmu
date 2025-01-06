namespace PoViEmu.Base.ISA
{
    public interface IFlatMemAccess<T>
    {
        T Get(uint off);

        void Set(uint off, T value);
    }
}