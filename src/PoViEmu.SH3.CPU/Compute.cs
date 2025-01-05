using static PoViEmu.SH3.CPU.MachExt;
using RS = PoViEmu.SH3.ISA.Ops.SourceReg;
using RD = PoViEmu.SH3.ISA.Ops.DestReg;

namespace PoViEmu.SH3.CPU
{
    internal static class Compute
    {
        public static void MACL(this MachineState s, RS m, RD n)
        {
            uint RnL, RnH, RmL, RmH, Res0, Res1, Res2;
            uint temp0, temp1, temp2, temp3;
            long tempm, tempn, fnLmL;

            tempn = ReadLong(s, s[n]);
            s[n] += 4;
            tempm = ReadLong(s, s[m]);
            s[m] += 4;

            fnLmL = (tempn ^ tempm) < 0 ? -1 : 0;

            if (tempn < 0) tempn = -tempn;
            if (tempm < 0) tempm = -tempm;

            temp1 = (uint)tempn;
            temp2 = (uint)tempm;

            RnL = temp1 & 0x0000FFFF;
            RnH = (temp1 >> 16) & 0x0000FFFF;
            RmL = temp2 & 0x0000FFFF;
            RmH = (temp2 >> 16) & 0x0000FFFF;

            temp0 = RmL * RnL;
            temp1 = RmH * RnL;
            temp2 = RmL * RnH;
            temp3 = RmH * RnH;

            Res2 = 0;
            Res1 = temp1 + temp2;
            if (Res1 < temp1)
                Res2 += 0x00010000;

            temp1 = (Res1 << 16) & 0xFFFF0000;
            Res0 = temp0 + temp1;
            if (Res0 < temp0)
                Res2++;

            Res2 += ((Res1 >> 16) & 0x0000FFFF) + temp3;

            if (fnLmL < 0)
            {
                Res2 = ~Res2;
                if (Res0 == 0)
                    Res2++;
                else
                    Res0 = ~Res0 + 1;
            }

            if (s.S)
            {
                Res0 += s.MACL;
                if (s.MACL > Res0)
                    Res2++;
                Res2 += s.MACH & 0x0000FFFF;

                if ((Res2 & 0xFFFF8000) == 0xFFFF8000)
                {
                    if ((long)Res2 < 0)
                    {
                        Res2 = 0x00008000;
                        Res0 = 0x00000000;
                    }
                    else if (Res2 > 0x00007FFF)
                    {
                        Res2 = 0x00007FFF;
                        Res0 = 0xFFFFFFFF;
                    }
                }

                s.MACH = Res2;
                s.MACL = Res0;
            }
            else
            {
                Res0 += s.MACL;
                if (s.MACL > Res0)
                    Res2++;
                Res2 += s.MACH;

                s.MACH = Res2;
                s.MACL = Res0;
            }
        }

        public static void MACW(this MachineState s, RS m, RD n)
        {
            long tempm, tempn, dest, src, ans;
            ulong templ;

            tempn = ReadWord(s, s[n]);
            s[n] += 2;
            tempm = ReadWord(s, s[m]);
            s[m] += 2;

            templ = s.MACL;

            tempm = (short)tempn * (long)(short)tempm;

            dest = s.MACL >= 0 ? 0 : 1;

            if (tempm >= 0)
            {
                src = 0;
                tempn = 0;
            }
            else
            {
                src = 1;
                tempn = 0xFFFFFFFF;
            }

            src += dest;

            s.MACL = (uint)(s.MACL + tempm);

            ans = s.MACL >= 0 ? 0 : 1;
            ans += dest;

            if (s.S)
            {
                if (ans == 1)
                {
                    if (src == 0 || src == 2)
                        s.MACH |= 0x00000001;
                    if (src == 0)
                        s.MACL = 0x7FFFFFFF;
                    if (src == 2)
                        s.MACL = 0x80000000;
                }
            }
            else
            {
                s.MACH = (uint)(s.MACH + tempn);

                if (templ > s.MACL)
                    s.MACH++;

                if ((s.MACH & 0x00000200) == 0)
                    s.MACH &= 0x000003FF;
                else
                    s.MACH |= 0xFFFFFC00;
            }
        }
    }
}