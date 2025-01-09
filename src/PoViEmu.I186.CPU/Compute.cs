namespace PoViEmu.I186.CPU
{
    internal static class Compute
    {
        public static void CompareByte(this MachineState m, byte src1, byte src2)
        {
            var res = src1 - src2;
            ModifyStatus(m, res);
            SetIndexes(m, 1);
        }

        public static void CompareWord(this MachineState m, ushort src1, ushort src2)
        {
            var res = src1 - src2;
            ModifyStatus(m, res);
            SetIndexes(m, 2);
        }

        private static void SetIndexes(this MachineState m, int size)
        {
            if (m.DF == false)
            {
                m.SI = (ushort)(m.SI + size);
                m.DI = (ushort)(m.DI + size);
            }
            else
            {
                m.SI = (ushort)(m.SI - size);
                m.DI = (ushort)(m.DI - size);
            }
        }

        private static void ModifyStatus(MachineState m, int res)
        {
            m.ZF = res == 0;
            m.SF = res < 0;
        }
    }
}