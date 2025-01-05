namespace PoViEmu.Base.CPU
{
    public static class ValTool
    {
        public static int ToNum(this bool value) => value ? 1 : 0;

        public static bool ToBool(this byte value) => value == 1;
    }
}