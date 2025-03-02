namespace PoViEmu.Base
{
    public static class CollHelper
    {
        public static bool IsContained(this byte[] first, byte[] second)
        {
            for (var i = 0; i < first.Length && i < second.Length; i++)
            {
                var one = first[i];
                var two = second[i];
                if (one != two)
                    return false;
            }
            return true;
        }
    }
}