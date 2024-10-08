using System;
using System.Collections.Generic;
using Iced.Intel;
using Newtonsoft.Json.Linq;
using PoViEmu.Core.Decoding;

namespace PoViEmu.Core.Hardware.AckNow
{
    public static class MachExt
    {
        public static void Set(this MachineState m, EmsRegister reg, ushort val)
        {
            switch (reg)
            {
                case EmsRegister.Bank0: m.Bank0 = val; break;
                case EmsRegister.Bank1: m.Bank1 = val; break;
                case EmsRegister.Bank2: m.Bank2 = val; break;
                case EmsRegister.Bank3: m.Bank3 = val; break;
                case EmsRegister.Bank4: m.Bank4 = val; break;
                case EmsRegister.Bank5: m.Bank5 = val; break;
                case EmsRegister.Bank6: m.Bank6 = val; break;
                case EmsRegister.Frame0: m.Frame0 = val; break;
                case EmsRegister.Frame1: m.Frame1 = val; break;
                case EmsRegister.Frame2: m.Frame2 = val; break;
                case EmsRegister.Frame3: m.Frame3 = val; break;
                case EmsRegister.Frame4: m.Frame4 = val; break;
                case EmsRegister.Frame5: m.Frame5 = val; break;
                case EmsRegister.Frame6: m.Frame6 = val; break;
                case EmsRegister.Frame7: m.Frame7 = val; break;
                case EmsRegister.Frame8: m.Frame8 = val; break;
                case EmsRegister.Frame9: m.Frame9 = val; break;
                case EmsRegister.Frame10: m.Frame10 = val; break;
                case EmsRegister.Frame11: m.Frame11 = val; break;
                default: throw new InvalidOperationException($"{reg} = {val:X4}");
            }
        }

        public static void Set(this MachineState m, FlagRegister reg, bool val)
        {
            switch (reg)
            {
                case FlagRegister.TF: m.TF = val; break;
                case FlagRegister.DF: m.DF = val; break;
                case FlagRegister.IF: m.IF = val; break;
                case FlagRegister.OF: m.OF = val; break;
                case FlagRegister.SF: m.SF = val; break;
                case FlagRegister.ZF: m.ZF = val; break;
                case FlagRegister.AF: m.AF = val; break;
                case FlagRegister.PF: m.PF = val; break;
                case FlagRegister.CF: m.CF = val; break;
                default: throw new InvalidOperationException($"{reg} = {val}");
            }
        }

        public static void Set(this MachineState m, B8Register reg, byte val)
        {
            switch (reg)
            {
                case B8Register.AL: m.AL = val; break;
                case B8Register.BL: m.BL = val; break;
                case B8Register.CL: m.CL = val; break;
                case B8Register.DL: m.DL = val; break;
                case B8Register.AH: m.AH = val; break;
                case B8Register.BH: m.BH = val; break;
                case B8Register.CH: m.CH = val; break;
                case B8Register.DH: m.DH = val; break;
                default: throw new InvalidOperationException($"{reg} = {val:X2}");
            }
        }

        public static void Set(this MachineState m, B16Register reg, ushort val)
        {
            switch (reg)
            {
                case B16Register.AX: m.AX = val; break;
                case B16Register.BX: m.BX = val; break;
                case B16Register.CX: m.CX = val; break;
                case B16Register.DX: m.DX = val; break;
                case B16Register.DI: m.DI = val; break;
                case B16Register.SI: m.SI = val; break;
                case B16Register.BP: m.BP = val; break;
                case B16Register.IP: m.IP = val; break;
                case B16Register.SP: m.SP = val; break;
                case B16Register.CS: m.CS = val; break;
                case B16Register.DS: m.DS = val; break;
                case B16Register.ES: m.ES = val; break;
                case B16Register.SS: m.SS = val; break;
                default: throw new InvalidOperationException($"{reg} = {val:X4}");
            }
        }
        
        public static ushort Get(this MachineState m, EmsRegister reg)
        {
            switch (reg)
            {
                case EmsRegister.Bank0: return m.Bank0;
                case EmsRegister.Bank1: return m.Bank1;
                case EmsRegister.Bank2: return m.Bank2;
                case EmsRegister.Bank3: return m.Bank3;
                case EmsRegister.Bank4: return m.Bank4;
                case EmsRegister.Bank5: return m.Bank5;
                case EmsRegister.Bank6: return m.Bank6;
                case EmsRegister.Frame0: return m.Frame0;
                case EmsRegister.Frame1: return m.Frame1;
                case EmsRegister.Frame2: return m.Frame2;
                case EmsRegister.Frame3: return m.Frame3;
                case EmsRegister.Frame4: return m.Frame4;
                case EmsRegister.Frame5: return m.Frame5;
                case EmsRegister.Frame6: return m.Frame6;
                case EmsRegister.Frame7: return m.Frame7;
                case EmsRegister.Frame8: return m.Frame8;
                case EmsRegister.Frame9: return m.Frame9;
                case EmsRegister.Frame10: return m.Frame10;
                case EmsRegister.Frame11: return m.Frame11;
                default: throw new InvalidOperationException($"{reg}");
            }
        }
        
        public static bool Get(this MachineState m, FlagRegister reg)
        {
            switch (reg)
            {
                case FlagRegister.TF: return m.TF;
                case FlagRegister.DF: return m.DF;
                case FlagRegister.IF: return m.IF;
                case FlagRegister.OF: return m.OF;
                case FlagRegister.SF: return m.SF;
                case FlagRegister.ZF: return m.ZF;
                case FlagRegister.AF: return m.AF;
                case FlagRegister.PF: return m.PF;
                case FlagRegister.CF: return m.CF;
                default: throw new InvalidOperationException($"{reg}");
            }
        }
        
        public static byte Get(this MachineState m, B8Register reg)
        {
            switch (reg)
            {
                case B8Register.AL: return m.AL;
                case B8Register.BL: return m.BL;
                case B8Register.CL: return m.CL;
                case B8Register.DL: return m.DL;
                case B8Register.AH: return m.AH;
                case B8Register.BH: return m.BH;
                case B8Register.CH: return m.CH;
                case B8Register.DH: return m.DH;
                default: throw new InvalidOperationException($"{reg}");
            }
        }
        
        public static ushort Get(this MachineState m, B16Register reg)
        {
            switch (reg)
            {
                case B16Register.AX: return m.AX;
                case B16Register.BX: return m.BX;
                case B16Register.CX: return m.CX;
                case B16Register.DX: return m.DX;
                case B16Register.DI: return m.DI;
                case B16Register.SI: return m.SI;
                case B16Register.BP: return m.BP;
                case B16Register.IP: return m.IP;
                case B16Register.SP: return m.SP;
                case B16Register.CS: return m.CS;
                case B16Register.DS: return m.DS;
                case B16Register.ES: return m.ES;
                case B16Register.SS: return m.SS;
                default: throw new InvalidOperationException($"{reg}");
            }
        }

        public static IDictionary<string, object> ToDict(this MachineState m)
        {
            var dict = new Dictionary<string, object>();
            foreach (var reg in Enum.GetValues<B8Register>())
            {
                if (reg == default) continue;
                dict[$"{reg}"] = m.Get(reg).AsHex();
            }
            foreach (var reg in Enum.GetValues<B16Register>())
            {
                if (reg == default) continue;
                dict[$"{reg}"] = m.Get(reg).AsHex();
            }
            foreach (var reg in Enum.GetValues<FlagRegister>())
            {
                if (reg == default) continue;
                dict[$"{reg}"] = m.Get(reg).AsInt();
            }
            foreach (var reg in Enum.GetValues<EmsRegister>())
            {
                if (reg == default) continue;
                dict[$"{reg}"] = m.Get(reg).AsHex();
            }
            return dict;
        }

        public static MachineState FromDict(this IDictionary<string, object> d)
        {
            var state = new MachineState();
            foreach (var pair in d)
            {
                var key = pair.Key;
                if (Enum.TryParse<B8Register>(key, ignoreCase: true, out var b8))
                    state.Set(b8, ToByte(pair.Value));
                else if (Enum.TryParse<B16Register>(key, ignoreCase: true, out var b16))
                    state.Set(b16, ToUShort(pair.Value));
                else if (Enum.TryParse<FlagRegister>(key, ignoreCase: true, out var fl))
                    state.Set(fl, ToBool(pair.Value));
                else if (Enum.TryParse<EmsRegister>(key, ignoreCase: true, out var em))
                    state.Set(em, ToUShort(pair.Value));
                else
                    throw new InvalidOperationException($"{pair.Key} = {pair.Value}");
            }
            return state;
        }

        private static bool ToBool(object raw)
        {
            if (raw is int i)
            {
                return i.AsBool();
            }
            throw new InvalidOperationException($"{raw} ?!");
        }

        private static ushort ToUShort(object raw)
        {
            if (raw is string txt && txt.StartsWith("0x"))
            {
                return (ushort)Convert.ToInt32(txt, 16);
            }
            throw new InvalidOperationException($"{raw} ?!");
        }

        private static byte ToByte(object raw)
        {
            if (raw is string txt && txt.StartsWith("0x"))
            {
                return (byte)Convert.ToInt32(txt, 16);
            }
            throw new InvalidOperationException($"{raw} ?!");
        }
        
        public static object? AsMine(this Register register)
        {
            switch (register)
            {
                case Register.AL: return B8Register.AL;
                case Register.BL: return B8Register.BL;
                case Register.CL: return B8Register.CL;
                case Register.DL: return B8Register.DL;
                case Register.AH: return B8Register.AH;
                case Register.BH: return B8Register.BH;
                case Register.CH: return B8Register.CH;
                case Register.DH: return B8Register.DH;
                case Register.SPL: return B16Register.SP;
                case Register.BPL: return B16Register.BP;
                case Register.SIL: return B16Register.SI;
                case Register.DIL: return B16Register.DI;
                case Register.AX: return B16Register.AX;
                case Register.BX: return B16Register.BX;
                case Register.CX: return B16Register.CX;
                case Register.DX: return B16Register.DX;
                case Register.SP: return B16Register.SP;
                case Register.BP: return B16Register.BP;
                case Register.SI: return B16Register.SI;
                case Register.DI: return B16Register.DI;
                case Register.EAX: return B16Register.AX;
                case Register.EBX: return B16Register.BX;
                case Register.ECX: return B16Register.CX;
                case Register.EDX: return B16Register.DX;
                case Register.ESP: return B16Register.SP;
                case Register.EBP: return B16Register.BP;
                case Register.ESI: return B16Register.SI;
                case Register.EDI: return B16Register.DI;
                case Register.RAX: return B16Register.AX;
                case Register.RBX: return B16Register.BX;
                case Register.RCX: return B16Register.CX;
                case Register.RDX: return B16Register.DX;
                case Register.RSP: return B16Register.SP;
                case Register.RBP: return B16Register.BP;
                case Register.RSI: return B16Register.SI;
                case Register.RDI: return B16Register.DI;
                case Register.EIP: return B16Register.IP;
                case Register.RIP: return B16Register.IP;
                case Register.CS: return B16Register.CS;
                case Register.DS: return B16Register.DS;
                case Register.ES: return B16Register.ES;
                case Register.SS: return B16Register.SS;
            }
            return null;
        }
    }
}