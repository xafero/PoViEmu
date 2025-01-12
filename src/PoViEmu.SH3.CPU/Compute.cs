using PoViEmu.Base.CPU;
using PoViEmu.SH3.CPU.Soft;
using Fl = PoViEmu.SH3.ISA.Flagged;
using static PoViEmu.SH3.ISA.ShRegister;
using MU16 = PoViEmu.SH3.ISA.Ops.Mems.Mu16Operand;
using MU32 = PoViEmu.SH3.ISA.Ops.Mems.Mu32Operand;
using MU8 = PoViEmu.SH3.ISA.Ops.Mems.Mu8Operand;
using NO = PoViEmu.SH3.ISA.Ops.Jumps.NearOperand;
using R = PoViEmu.SH3.ISA.Ops.Regs.Reg32Operand;

// ReSharper disable InconsistentNaming

namespace PoViEmu.SH3.CPU
{
    internal static class Jumping
    {
        public static void BranchIfFalse(this MachineState s, int d, ref uint nextIP)
        {
            long disp;
            if ((d & 0x80) == 0)
                disp = 0x000000FF & d;
            else
                disp = 0xFFFFFF00 | d;
            if (s.T == false)
                nextIP = (uint)(s.PC + (disp << 1) + 4);
            else
                nextIP = s.PC + 2;
        }

        public static void BranchIfFalseDelay(this MachineState s, int d, ref uint nextIP)
        {
            int disp;
            var temp = s.PC;
            if ((d & 0x80) == 0)
                disp = 0x000000FF & d;
            else
                disp = (int)(0xFFFFFF00 | d);
            if (s.T == false)
            {
                nextIP = (uint)(s.PC + (disp << 1) + 4);
                DelaySlot(temp + 2);
            }
            else
                nextIP = s.PC + 2;
        }

        private static void DelaySlot(uint temp)
        {
            // TODO delay slot ?!
        }

        public static void Branch(this MachineState s, int d, ref uint nextIP)
        {
            int disp;
            if ((d & 0x800) == 0)
                disp = 0x00000FFF & d;
            else
                disp = (int)(0xFFFFF000 | d);
            var temp = nextIP;
            nextIP = (uint)(s.PC + (disp << 1) + 4);
            DelaySlot(temp + 2);
        }

        public static void BranchFar(this MachineState s, R m, ref uint nextIP)
        {
            var temp = nextIP;
            nextIP = s.PC + s[m];
            DelaySlot(temp + 2);
        }

        public static void BranchSubroutine(this MachineState s, int d, ref uint nextIP)
        {
            long disp;
            if ((d & 0x800) == 0)
                disp = 0x00000FFF & d;
            else
                disp = 0xFFFFF000 | d;
            s.PR = nextIP;
            nextIP = (uint)(s.PC + (disp << 1) + 4);
            DelaySlot(s.PR + 2);
        }

        public static void BranchSubroutineFar(this MachineState s, R m, ref uint nextIP)
        {
            s.PR = s.PC;
            nextIP = s.PC + s[m];
            DelaySlot(s.PR + 2);
        }

        public static void BranchIfTrue(this MachineState s, int d, ref uint nextIP)
        {
            int disp;
            if ((d & 0x80) == 0)
                disp = 0x000000FF & d;
            else
                disp = (int)(0xFFFFFF00 | d);
            if (s.T)
                nextIP = (uint)(s.PC + (disp << 1) + 4);
            else
                nextIP = s.PC + 2;
        }

        public static void BranchIfTrueDelay(this MachineState s, int d, ref uint nextIP)
        {
            int disp;
            var temp = s.PC;
            if ((d & 0x80) == 0)
                disp = 0x000000FF & d;
            else
                disp = (int)(0xFFFFFF00 | d);
            if (s.T)
            {
                nextIP = (uint)(s.PC + (disp << 1) + 4);
                DelaySlot(temp + 2);
            }
            else
            {
                nextIP = s.PC + 2;
            }
        }

        public static void Jump(this MachineState s, R m, ref uint nextIP)
        {
            var temp = s.PC;
            nextIP = s[m] + 4;
            DelaySlot(temp + 2);
        }

        public static void JumpSubroutine(this MachineState s, MU32 mem, ref uint nextIP)
        {
            s.PR = s.PC;
            nextIP = mem[s] + 4;
            DelaySlot(s.PR + 2);
        }

        public static void ReturnFromEx(this MachineState s, ref uint nextIP)
        {
            var temp = s.PC;
            nextIP = s.SPC;
            s.SR = (Fl)s.SSR;
            DelaySlot(temp + 2);
        }

        public static void ReturnSubroutine(this MachineState s, ref uint nextIP)
        {
            var temp = s.PC;
            nextIP = s.PR + 4;
            DelaySlot(temp + 2);
        }
    }

    internal static class Special
    {
        public static void Sleep(this MachineState _, SH7291 cpu, ref uint nextIP)
        {
            nextIP -= 2;
            // TODO Sleep Mode
            cpu.Halted = true;
        }

        public static void Trapa(this MachineState s, SH7291 cpu, sbyte i)
        {
            /*
            // TODO
            //
            long immT;
            immT=(0x000000FF & i);
            TRA=immT<<2;
            s.SSR = (uint)s.SR;
            s.SPC = s.PC;
            s.MD = true;
            s.BL = true;
            s.RB = true;
            s.EXPEVT=0x00000160;
            s.PC=s.VBR+0x00000100;
            //
            */

            cpu.ExecuteInterrupt((byte)i, s);
            if ((cpu.InterruptTable[0x21] as DOSInterrupts)?.ReturnCode is not null)
                cpu.Halted = true;
        }
    }

    internal static class Compute
    {
        public static void Add(this MachineState s, R m, R n)
        {
            s[n] += s[m];
        }

        public static void Add(this MachineState s, sbyte i, R n)
        {
            if ((i & 0x80) == 0)
                s[n] = (uint)(s[n] + (0x000000FF & i));
            else
                s[n] += 0xFFFFFF00 | (byte)i;
        }

        public static void Addc(this MachineState s, R m, R n)
        {
            var tmp1 = s[n] + s[m];
            var tmp0 = s[n];
            s[n] = (uint)(tmp1 + s.T.ToNum());
            s.T = tmp0 > tmp1 || tmp1 > s[n];
        }

        public static void Addv(this MachineState s, R m, R n)
        {
            var dest = (int)s[n] >= 0 ? 0 : 1;
            var src = (int)s[m] >= 0 ? 0 : 1;
            src += dest;
            s[n] += s[m];
            var ans = (int)s[n] >= 0 ? 0 : 1;
            ans += dest;
            if (src is 0 or 2)
                s.T = ans == 1;
            else
                s.T = false;
        }

        public static void And(this MachineState s, R m, R n)
        {
            s[n] &= s[m];
        }

        public static void And(this MachineState s, byte i, R n)
        {
            s[n] = (uint)(s[n] & (0x000000FF & i));
        }

        public static void Andb(this MachineState s, byte i, MU8 mem)
        {
            var temp = (int)mem[s];
            temp = (temp & (0x000000FF & i));
            mem[s] = (byte)temp;
        }

        public static void Clrmac(this MachineState s)
        {
            s.MACH = 0;
            s.MACL = 0;
        }

        public static void Clrs(this MachineState s)
        {
            s.S = false;
        }

        public static void Clrt(this MachineState s)
        {
            s.T = false;
        }

        public static void CmpEq(this MachineState s, R m, R n)
        {
            s.T = s[n] == s[m];
        }

        public static void CmpGe(this MachineState s, R m, R n)
        {
            s.T = (int)s[n] >= s[m];
        }

        public static void CmpGt(this MachineState s, R m, R n)
        {
            s.T = (int)s[n] > s[m];
        }

        public static void CmpHi(this MachineState s, R m, R n)
        {
            s.T = (uint)s[n] > s[m];
        }

        public static void CmpHs(this MachineState s, R m, R n)
        {
            s.T = (uint)s[n] >= s[m];
        }

        public static void CmpPl(this MachineState s, R n)
        {
            s.T = (int)s[n] > 0;
        }

        public static void CmpPz(this MachineState s, R n)
        {
            s.T = (int)s[n] >= 0;
        }

        public static void CmpStr(this MachineState s, R m, R n)
        {
            var temp = s[n] ^ s[m];
            var HH = (int)((temp & 0xFF000000) >> 12);
            var HL = (int)((temp & 0x00FF0000) >> 8);
            var LH = (int)((temp & 0x0000FF00) >> 4);
            var LL = (int)(temp & 0x000000FF);
            HH = HH & HL & LH & LL;
            s.T = HH == 0;
        }

        public static void CmpEq(this MachineState s, sbyte i, R n)
        {
            int imm;
            if ((i & 0x80) == 0)
                imm = 0x000000FF & i;
            else
                imm = (int)(0xFFFFFF00 | (byte)i);
            s.T = s[n] == imm;
        }

        public static void Div0s(this MachineState s, R m, R n)
        {
            s.Q = (s[n] & 0x80000000) != 0;
            s.M = (s[m] & 0x80000000) != 0;
            s.T = s.M != s.Q;
        }

        public static void Div0u(this MachineState s)
        {
            s.M = s.Q = s.T = false;
        }

        public static void Div1(this MachineState s, R m, R n)
        {
            uint tmp0;
            byte tmp1;
            var old_q = (byte)s.Q.ToNum();
            s.Q = (0x80000000 & s[n]) != 0;
            s[n] <<= 1;
            s[n] |= (uint)s.T.ToNum();
            switch (old_q)
            {
                case 0:
                    switch (s.M)
                    {
                        case false:
                            tmp0 = s[n];
                            s[n] -= s[m];
                            tmp1 = (byte)(s[n] > tmp0 ? 1 : 0);
                            switch (s.Q)
                            {
                                case false:
                                    s.Q = tmp1.ToBool();
                                    break;
                                case true:
                                    s.Q = tmp1 == 0;
                                    break;
                            }
                            break;
                        case true:
                            tmp0 = s[n];
                            s[n] += s[m];
                            tmp1 = (byte)(s[n] < tmp0 ? 1 : 0);
                            switch (s.Q)
                            {
                                case false:
                                    s.Q = tmp1 == 0;
                                    break;
                                case true:
                                    s.Q = tmp1.ToBool();
                                    break;
                            }
                            break;
                    }
                    break;
                case 1:
                    switch (s.M)
                    {
                        case false:
                            tmp0 = s[n];
                            s[n] += s[m];
                            tmp1 = (byte)(s[n] < tmp0 ? 1 : 0);
                            switch (s.Q)
                            {
                                case false:
                                    s.Q = tmp1.ToBool();
                                    break;
                                case true:
                                    s.Q = tmp1 == 0;
                                    break;
                            }
                            break;
                        case true:
                            tmp0 = s[n];
                            s[n] -= s[m];
                            tmp1 = (byte)(s[n] > tmp0 ? 1 : 0);
                            switch (s.Q)
                            {
                                case false:
                                    s.Q = tmp1 == 0;
                                    break;
                                case true:
                                    s.Q = tmp1.ToBool();
                                    break;
                            }
                            break;
                    }
                    break;
            }
            s.T = s.Q == s.M;
        }

        public static void DmulsL(this MachineState s, R m, R n)
        {
            var tempn = (int)s[n];
            var tempm = (int)s[m];
            if (tempn < 0) tempn = -tempn;
            if (tempm < 0) tempm = -tempm;
            var fnLmL = (int)(s[n] ^ s[m]) < 0 ? -1 : 0;
            var temp1 = (uint)tempn;
            var temp2 = (uint)tempm;
            var RnL = temp1 & 0x0000FFFF;
            var RnH = (temp1 >> 16) & 0x0000FFFF;
            var RmL = temp2 & 0x0000FFFF;
            var RmH = (temp2 >> 16) & 0x0000FFFF;
            var temp0 = RmL * RnL;
            temp1 = RmH * RnL;
            temp2 = RmL * RnH;
            var temp3 = RmH * RnH;
            uint Res2 = 0;
            var Res1 = temp1 + temp2;
            if (Res1 < temp1) Res2 += 0x00010000;
            temp1 = (Res1 << 16) & 0xFFFF0000;
            var Res0 = temp0 + temp1;
            if (Res0 < temp0) Res2++;
            Res2 = Res2 + ((Res1 >> 16) & 0x0000FFFF) + temp3;
            if (fnLmL < 0)
            {
                Res2 = ~Res2;
                if (Res0 == 0)
                    Res2++;
                else
                    Res0 = (~Res0) + 1;
            }
            s.MACH = Res2;
            s.MACL = Res0;
        }

        public static void DmuluL(this MachineState s, R m, R n)
        {
            var RnL = s[n] & 0x0000FFFF;
            var RnH = (s[n] >> 16) & 0x0000FFFF;
            var RmL = s[m] & 0x0000FFFF;
            var RmH = (s[m] >> 16) & 0x0000FFFF;
            var temp0 = RmL * RnL;
            var temp1 = RmH * RnL;
            var temp2 = RmL * RnH;
            var temp3 = RmH * RnH;
            uint Res2 = 0;
            var Res1 = temp1 + temp2;
            if (Res1 < temp1) Res2 += 0x00010000;
            temp1 = (Res1 << 16) & 0xFFFF0000;
            var Res0 = temp0 + temp1;
            if (Res0 < temp0) Res2++;
            Res2 = Res2 + ((Res1 >> 16) & 0x0000FFFF) + temp3;
            s.MACH = Res2;
            s.MACL = Res0;
        }

        public static void Dt(this MachineState s, R n)
        {
            s[n]--;
            s.T = s[n] == 0;
        }

        public static void Extsb(this MachineState s, R m, R n)
        {
            s[n] = s[m];
            if ((s[m] & 0x00000080) == 0)
                s[n] &= 0x000000FF;
            else
                s[n] |= 0xFFFFFF00;
        }

        public static void Extsw(this MachineState s, R m, R n)
        {
            s[n] = s[m];
            if ((s[m] & 0x00008000) == 0)
                s[n] &= 0x0000FFFF;
            else
                s[n] |= 0xFFFF0000;
        }

        public static void Extub(this MachineState s, R m, R n)
        {
            s[n] = s[m];
            s[n] &= 0x000000FF;
        }

        public static void Extuw(this MachineState s, R m, R n)
        {
            s[n] = s[m];
            s[n] &= 0x0000FFFF;
        }

        public static void Stc(this MachineState s, R m, R n)
        {
            s[n] = s[m];
        }

        public static void Ldc(this MachineState s, R m, R n)
        {
            switch (n.Reg)
            {
                case SR: s.SR = (Fl)(s[m] & 0x0FFF0FFF); break;
                case SSR: s.SSR = s[m] & 0x700003F3; break;
                default: s[n] = s[m]; break;
            }
        }

        public static void StcL(this MachineState s, R m, MU32 mem)
        {
            mem[s] = s[m];
        }

        public static void LdcL(this MachineState s, MU32 mem, R n)
        {
            switch (n.Reg)
            {
                case SR:
                    s.SR = (Fl)( mem[s]  & 0x0FFF0FFF);
                    break;
                case SSR:
                    s.SSR =  mem[s]  & 0x700003F3;
                    break;
                default:
                    s[n] =   mem[s];
                    break;
            }
        }

        public static void Lds(this MachineState s, R m, R n)
        {
            switch (n.Reg)
            {
                case MACH:
                    s.MACH = s[m];
                    if ((s.MACH & 0x00000200) == 0)
                        s.MACH &= 0x000003FF;
                    else
                        s.MACH |= 0xFFFFFC00;
                    break;
                default:
                    s[n] = s[m];
                    break;
            }
        }

        public static void LdsL(this MachineState s, MU32 mem, R n)
        {
            switch (n.Reg)
            {
                case MACH:
                    s.MACH = (uint)( mem[s] );
                    if ((s.MACH & 0x00000200) == 0)
                        s.MACH &= 0x000003FF;
                    else
                        s.MACH |= 0xFFFFFC00;
                    break;
                default:
                    s[n] = (uint)( mem[s] );
                    break;
            }
        }

        public static void Ldtlb(this MachineState s)
        {
            // TODO ?!
            // TLB_tag=PTEH;
            // TLB_data=PTEL;
        }

        public static void Macl(this MachineState s, R m, R n)
        {
            var tempn = (int)( s[n] );
            var tempm = (int)( s[m] );

            var fnLmL = (tempn ^ tempm) < 0 ? -1 : 0;

            if (tempn < 0) tempn = -tempn;
            if (tempm < 0) tempm = -tempm;

            var temp1 = (uint)tempn;
            var temp2 = (uint)tempm;

            var RnL = temp1 & 0x0000FFFF;
            var RnH = (temp1 >> 16) & 0x0000FFFF;
            var RmL = temp2 & 0x0000FFFF;
            var RmH = (temp2 >> 16) & 0x0000FFFF;

            var temp0 = RmL * RnL;
            temp1 = RmH * RnL;
            temp2 = RmL * RnH;
            var temp3 = RmH * RnH;

            uint Res2 = 0;
            var Res1 = temp1 + temp2;
            if (Res1 < temp1)
                Res2 += 0x00010000;

            temp1 = (Res1 << 16) & 0xFFFF0000;
            var Res0 = temp0 + temp1;
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

        public static void Macw(this MachineState s, R m, R n)
        {
            int src;

            var tempn = (uint)( s[n] );
            int tempm = (int)( s[m] );

            var templ = s.MACL;

            tempm = (int)((short)tempn * (long)(short)tempm);

            var dest = (int)s.MACL >= 0 ? 0 : 1;

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

            src = (int)(src + dest);

            s.MACL = (uint)(s.MACL + tempm);

            long ans = s.MACL >= 0 ? 0 : 1;
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

        public static void Mov(this MachineState s, R m, R n)
        {
            s[n] = s[m];
        }

        public static void Mov(this MachineState s, R r, MU8 mem)
        {
            mem[s] = (byte)s[r];
        }

        public static void Mov(this MachineState s, R r, MU16 mem)
        {
            mem[s] = (ushort)s[r];
        }

        public static void Mov(this MachineState s, R r, MU32 mem)
        {
            mem[s] = s[r];
        }

        public static void Mov(this MachineState s, MU8 mem, R n)
        {
            s[n] = mem[s];
        }

        public static void Mov(this MachineState s, MU16 mem, R n)
        {
            s[n] = mem[s];
        }

        public static void Mov(this MachineState s, MU32 mem, R n)
        {
            s[n] = mem[s];
        }

        public static void Mova(this MachineState s, int d, R r)
        {
            var disp = (0x000000FF & d);
            s[r] = (uint)((s.PC & 0xFFFFFFFC) + (disp << 2));
        }

        public static void Movt(this MachineState s, R n)
        {
            s[n] = (0x00000001 & (uint)s.SR);
        }

        public static void MulL(this MachineState s, R m, R n)
        {
            s.MACL = s[n] * s[m];
        }

        public static void MulsW(this MachineState s, R m, R n)
        {
            s.MACL = (uint)((short)s[n] * (short)s[m]);
        }

        public static void MuluW(this MachineState s, R m, R n)
        {
            s.MACL = (uint)((ulong)(ushort)s[n] * (ulong)(ushort)s[m]);
        }

        public static void Neg(this MachineState s, R m, R n)
        {
            s[n] = 0 - s[m];
        }

        public static void Negc(this MachineState s, R m, R n)
        {
            long temp = 0 - s[m];
            s[n] = (uint)(temp - s.T.ToNum());
            s.T = 0 < temp || temp < s[n];
        }

        public static void Not(this MachineState s, R m, R n)
        {
            s[n] = ~s[m];
        }

        public static void Or(this MachineState s, R m, R n)
        {
            s[n] |= s[m];
        }

        public static void Or(this MachineState s, byte i, R n)
        {
            s[n] = (uint)(s[n] | (0x000000FF & (long)i));
        }

        public static void Or(this MachineState s, byte i, MU8 mem)
        {
            var temp = mem[s];
            temp = (byte)(temp | (0x000000FF & i));
            mem[s] = temp;
        }

        public static void Rotcl(this MachineState s, R n)
        {
            long tempYx = (s[n] & 0x80000000) == 0 ? 0 : 1;
            s[n] <<= 1;
            if (s.T)
                s[n] |= 0x00000001;
            else
                s[n] &= 0xFFFFFFFE;
            s.T = tempYx == 1;
        }

        public static void Rotcr(this MachineState s, R n)
        {
            var temp = (s[n] & 0x00000001) == 0 ? 0 : 1;
            s[n] >>= 1;
            if (s.T)
                s[n] |= 0x80000000;
            else
                s[n] &= 0x7FFFFFFF;
            s.T = temp == 1;
        }

        public static void Rotl(this MachineState s, R n)
        {
            s.T = (s[n] & 0x80000000) != 0;
            s[n] <<= 1;
            if (s.T)
                s[n] |= 0x00000001;
            else
                s[n] &= 0xFFFFFFFE;
        }

        public static void Rotr(this MachineState s, R n)
        {
            s.T = (s[n] & 0x00000001) != 0;
            s[n] >>= 1;
            if (s.T)
                s[n] |= 0x80000000;
            else
                s[n] &= 0x7FFFFFFF;
        }

        public static void Sett(this MachineState s)
        {
            s.T = true;
        }

        public static void Sets(this MachineState s)
        {
            s.S = true;
        }

        public static void Shad(this MachineState s, R m, R n)
        {
            var sgn = (int)(s[m] & 0x80000000);
            var cnt = (int)(s[m] & 0x0000001F);
            if (sgn == 0)
                s[n] <<= (int)cnt;
            else
                s[n] = (uint)(((int)s[n]) >> ((~cnt + 1) & 0x1F));
        }

        public static void Shal(this MachineState s, R n)
        {
            s.T = (s[n] & 0x80000000) != 0;
            s[n] <<= 1;
        }

        public static void Shar(this MachineState s, R n)
        {
            s.T = (s[n] & 0x00000001) != 0;
            var temp = (s[n] & 0x80000000) == 0 ? 0 : 1;
            s[n] >>= 1;
            if (temp == 1)
                s[n] |= 0x80000000;
            else
                s[n] &= 0x7FFFFFFF;
        }

        public static void Shld(this MachineState s, R m, R n)
        {
            var sgnX = (int)(s[m] & 0x80000000);
            var cntX = (int)(s[m] & 0x0000001F);
            if (sgnX == 0)
                s[n] <<= (int)cntX;
            else
                s[n] >>= (int)((~cntX + 1) & 0x1F);
        }

        public static void Shll(this MachineState s, R n)
        {
            s.T = (s[n] & 0x80000000) != 0;
            s[n] <<= 1;
        }

        public static void Shlln(this MachineState s, int v, R n)
        {
            s[n] <<= v;
        }

        public static void Shlr(this MachineState s, R n)
        {
            s.T = (s[n] & 0x00000001) != 0;
            s[n] >>= 1;
            s[n] &= 0x7FFFFFFF;
        }

        public static void Shlrn(this MachineState s, int v, R n)
        {
            s[n] >>= v;
            switch (v)
            {
                case 2: s[n] &= 0x3FFFFFFF; break;
                case 8: s[n] &= 0x00FFFFFF; break;
                case 16: s[n] &= 0x0000FFFF; break;
            }
        }

        public static void Sts(this MachineState s, R m, R n)
        {
            switch (m.Reg)
            {
                case MACH:
                    s[n] = s.MACH;
                    if ((s[n] & 0x00000200) == 0)
                        s[n] &= 0x000003FF;
                    else
                        s[n] |= 0xFFFFFC00;
                    break;
                default:
                    s[n] = s[m];
                    break;
            }
        }

        public static void StsL(this MachineState s, R m, MU32 mem)
        {
            switch (m.Reg)
            {
                case MACH:
                    if ((s.MACH & 0x00000200) == 0)
                        mem[s] = s.MACH & 0x000003FF;
                    else
                        mem[s] = s.MACH | 0xFFFFFC00;
                    break;
                default:
                    mem[s] = s[m];
                    break;
            }
        }

        public static void Sub(this MachineState s, R m, R n)
        {
            s[n] -= s[m];
        }

        public static void Subc(this MachineState s, R m, R n)
        {
            var tmp1 = s[n] - s[m];
            var tmp0 = s[n];
            s[n] = (uint)(tmp1 - (ulong)s.T.ToNum());
            s.T = tmp0 < tmp1 || (tmp1 < s[n]);
        }

        public static void Subv(this MachineState s, R m, R n)
        {
            var dest = (int)s[n] >= 0 ? 0 : 1;
            var src = (int)s[m] >= 0 ? 0 : 1;
            src += dest;
            s[n] -= s[m];
            var ans = (int)s[n] >= 0 ? 0 : 1;
            ans += dest;
            if (src == 1)
                s.T = ans == 1;
            else
                s.T = false;
        }

        public static void Swapb(this MachineState s, R m, R n)
        {
            var temp0 = s[m] & 0xffff0000;
            var temp1 = (s[m] & 0x000000ff) << 8;
            s[n] = (s[m] & 0x0000ff00) >> 8;
            s[n] = (uint)(s[n] | temp1 | temp0);
        }

        public static void Swapw(this MachineState s, R m, R n)
        {
            var tempSw = (s[m] >> 16) & 0x0000FFFF;
            s[n] = s[m] << 16;
            s[n] = (uint)(s[n] | tempSw);
        }

        public static void Tasb(this MachineState s, MU8 mem)
        {
            var temp = (int)(mem[s]);
            s.T = temp == 0;
            temp |= 0x00000080;
            mem[s] = (byte)temp;
        }

        public static void Tst(this MachineState s, R m, R n)
        {
            s.T = (s[n] & s[m]) == 0;
        }

        public static void Tst(this MachineState s, byte i, R n)
        {
            var temp = s[R0] & (0x000000FF & (long)i);
            s.T = temp == 0;
        }

        public static void Tstb(this MachineState s, byte i, MU8 mem)
        {
            var temp = (int)(mem[s]);
            temp &= (0x000000FF & (int)i);
            s.T = temp == 0;
        }

        public static void Xor(this MachineState s, R m, R n)
        {
            s[n] ^= s[m];
        }

        public static void Xor(this MachineState s, byte i, R n)
        {
            s[R0] = (uint)(s[R0] ^ (0x000000FF & (long)i));
        }

        public static void Xorb(this MachineState s, byte i, MU8 mem)
        {
            var temp = (int)(mem[s]);
            temp ^= (0x000000FF & (int)i);
            mem[s] = (byte)temp;
        }

        public static void Xtrct(this MachineState s, R m, R n)
        {
            var temp = (s[m] << 16) & 0xFFFF0000;
            s[n] = (s[n] >> 16) & 0x0000FFFF;
            s[n] = (uint)(s[n] | temp);
        }

        public static void Macl(this MachineState s, MU32 a, MU32 b)
        {
            // TODO
        }

        public static void Macw(this MachineState s, MU16 a, MU16 b)
        {
            // TODO
        }

        public static void Mov(this MachineState s, sbyte i, R n)
        {
            s[n] = (uint)i;
        }

        public static void MovW(this MachineState s, NO m, R n)
        {
            s[n] = (uint)m.Diff;
        }

        public static void MovW(this MachineState s, R r, MU16 mem)
        {
            mem[s] = (ushort)s[r];
        }

        public static void MovL(this MachineState s, NO m, R n)
        {
            s[n] = (uint)m.Diff;
        }
    }
}