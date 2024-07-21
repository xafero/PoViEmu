namespace PoViEmu.Common
{
    public static class TypeHelper
    {
        public static string GetTypeName(this object obj)
        {
            return obj?.GetType().Name;
        }
    }
}