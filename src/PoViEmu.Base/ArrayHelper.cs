namespace PoViEmu.Base
{
    public static class ArrayHelper
    {
        private static void CheckBounds(byte[] array, long address)
        {
            if (address >= array.LongLength)
            {
                throw new ArrayException(address, array.LongLength);
            }
        }

        public static byte CheckGet(this byte[] array, long address)
        {
            CheckBounds(array, address);
            return array[address];
        }

        public static void CheckSet(this byte[] array, long address, byte value)
        {
            CheckBounds(array, address);
            array[address] = value;
        }
    }
}