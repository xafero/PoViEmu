using System;
using System.Linq;

namespace PoViEmu.Core.Hardware
{
    public static class MachParser
    {
        public static (string? key, string? arg) SplitPropName(string? name)
        {
            var parts = name?.Split('|') ?? [];
            var key = parts.FirstOrDefault();
            var arg = parts.Length >= 2 ? parts.Skip(1).First() : null;
            return (key, arg);
        }

        public static object GetByString(this MachineState m, string? name)
        {
            var (key, arg) = SplitPropName(name);
            return key switch
            {
                nameof(MachineState.AX) => m.AX,
                nameof(MachineState.BX) => m.BX,
                nameof(MachineState.CX) => m.CX,
                nameof(MachineState.DX) => m.DX,
                nameof(MachineState.AH) => m.AH,
                nameof(MachineState.BH) => m.BH,
                nameof(MachineState.CH) => m.CH,
                nameof(MachineState.DH) => m.DH,
                nameof(MachineState.AL) => m.AL,
                nameof(MachineState.BL) => m.BL,
                nameof(MachineState.CL) => m.CL,
                nameof(MachineState.DL) => m.DL,
                nameof(MachineState.SI) => m.SI,
                nameof(MachineState.DI) => m.DI,
                nameof(MachineState.DS) => m.DS,
                nameof(MachineState.ES) => m.ES,
                nameof(MachineState.SS) => m.SS,
                nameof(MachineState.SP) => m.SP,
                nameof(MachineState.BP) => m.BP,
                nameof(MachineState.CS) => m.CS,
                nameof(MachineState.IP) => m.IP,
                nameof(MachineState.F) => m.F,
                nameof(MachineState.TF) => m.TF,
                nameof(MachineState.DF) => m.DF,
                nameof(MachineState.IF) => m.IF,
                nameof(MachineState.OF) => m.OF,
                nameof(MachineState.SF) => m.SF,
                nameof(MachineState.ZF) => m.ZF,
                nameof(MachineState.AF) => m.AF,
                nameof(MachineState.PF) => m.PF,
                nameof(MachineState.CF) => m.CF,
                nameof(MachineState.Bk0) => m.Bk0,
                nameof(MachineState.Bk1) => m.Bk1,
                nameof(MachineState.Bk2) => m.Bk2,
                nameof(MachineState.Bk3) => m.Bk3,
                nameof(MachineState.Bk4) => m.Bk4,
                nameof(MachineState.Bk5) => m.Bk5,
                nameof(MachineState.Bk6) => m.Bk6,
                nameof(MachineState.Fr0) => m.Fr0,
                nameof(MachineState.Fr1) => m.Fr1,
                nameof(MachineState.Fr2) => m.Fr2,
                nameof(MachineState.Fr3) => m.Fr3,
                nameof(MachineState.Fr4) => m.Fr4,
                nameof(MachineState.Fr5) => m.Fr5,
                nameof(MachineState.Fr6) => m.Fr6,
                nameof(MachineState.Fr7) => m.Fr7,
                nameof(MachineState.Fr8) => m.Fr8,
                nameof(MachineState.Fr9) => m.Fr9,
                nameof(MachineState.Fr10) => m.Fr10,
                nameof(MachineState.Fr11) => m.Fr11,
                nameof(MachineState.U8) => m.GetU8(arg),
                nameof(MachineState.U16) => m.GetU16(arg),
                _ => throw new InvalidOperationException(name)
            };
        }

        public static void SetByString(this MachineState m, string name, object value)
        {
            var (key, arg) = SplitPropName(name);
            switch (key?.ToLowerInvariant())
            {
                case "ax":
                    m.AX = ToVal<ushort>(value);
                    break;
                case "bx":
                    m.BX = ToVal<ushort>(value);
                    break;
                case "cx":
                    m.CX = ToVal<ushort>(value);
                    break;
                case "dx":
                    m.DX = ToVal<ushort>(value);
                    break;
                case "ah":
                    m.AH = ToVal<byte>(value);
                    break;
                case "bh":
                    m.BH = ToVal<byte>(value);
                    break;
                case "ch":
                    m.CH = ToVal<byte>(value);
                    break;
                case "dh":
                    m.DH = ToVal<byte>(value);
                    break;
                case "al":
                    m.AL = ToVal<byte>(value);
                    break;
                case "bl":
                    m.BL = ToVal<byte>(value);
                    break;
                case "cl":
                    m.CL = ToVal<byte>(value);
                    break;
                case "dl":
                    m.DL = ToVal<byte>(value);
                    break;
                case "si":
                    m.SI = ToVal<ushort>(value);
                    break;
                case "di":
                    m.DI = ToVal<ushort>(value);
                    break;
                case "ds":
                    m.DS = ToVal<ushort>(value);
                    break;
                case "es":
                    m.ES = ToVal<ushort>(value);
                    break;
                case "ss":
                    m.SS = ToVal<ushort>(value);
                    break;
                case "sp":
                    m.SP = ToVal<ushort>(value);
                    break;
                case "bp":
                    m.BP = ToVal<ushort>(value);
                    break;
                case "cs":
                    m.CS = ToVal<ushort>(value);
                    break;
                case "ip":
                    m.IP = ToVal<ushort>(value);
                    break;
                case "f":
                    m.F = ToVal<Flagged>(value);
                    break;
                case "tf":
                    m.TF = ToVal<bool>(value);
                    break;
                case "df":
                    m.DF = ToVal<bool>(value);
                    break;
                case "if":
                    m.IF = ToVal<bool>(value);
                    break;
                case "of":
                    m.OF = ToVal<bool>(value);
                    break;
                case "sf":
                    m.SF = ToVal<bool>(value);
                    break;
                case "zf":
                    m.ZF = ToVal<bool>(value);
                    break;
                case "af":
                    m.AF = ToVal<bool>(value);
                    break;
                case "pf":
                    m.PF = ToVal<bool>(value);
                    break;
                case "cf":
                    m.CF = ToVal<bool>(value);
                    break;
                case "bk0":
                    m.Bk0 = ToVal<ushort>(value);
                    break;
                case "bk1":
                    m.Bk1 = ToVal<ushort>(value);
                    break;
                case "bk2":
                    m.Bk2 = ToVal<ushort>(value);
                    break;
                case "bk3":
                    m.Bk3 = ToVal<ushort>(value);
                    break;
                case "bk4":
                    m.Bk4 = ToVal<ushort>(value);
                    break;
                case "bk5":
                    m.Bk5 = ToVal<ushort>(value);
                    break;
                case "bk6":
                    m.Bk6 = ToVal<ushort>(value);
                    break;
                case "fr0":
                    m.Fr0 = ToVal<ushort>(value);
                    break;
                case "fr1":
                    m.Fr1 = ToVal<ushort>(value);
                    break;
                case "fr2":
                    m.Fr2 = ToVal<ushort>(value);
                    break;
                case "fr3":
                    m.Fr3 = ToVal<ushort>(value);
                    break;
                case "fr4":
                    m.Fr4 = ToVal<ushort>(value);
                    break;
                case "fr5":
                    m.Fr5 = ToVal<ushort>(value);
                    break;
                case "fr6":
                    m.Fr6 = ToVal<ushort>(value);
                    break;
                case "fr7":
                    m.Fr7 = ToVal<ushort>(value);
                    break;
                case "fr8":
                    m.Fr8 = ToVal<ushort>(value);
                    break;
                case "fr9":
                    m.Fr9 = ToVal<ushort>(value);
                    break;
                case "fr10":
                    m.Fr10 = ToVal<ushort>(value);
                    break;
                case "fr11":
                    m.Fr11 = ToVal<ushort>(value);
                    break;

                // TODO
                // case "u8":    m.GetU8(arg)   =   ToVal<ushort>(value); break;
                // case "u16":   m.GetU16(arg)   =   ToVal<ushort>(value); break;

                default: throw new InvalidOperationException(name);
            }
        }

        public static T ToVal<T>(object value)
        {
            var type = typeof(T).Name;
            object res;
            switch (type)
            {
                case nameof(Flagged):
                    res = value switch
                    {
                        Flagged f => f,
                        ushort fu => fu,
                        _ => Convert.ToUInt16($"{value}", 16)
                    };
                    break;
                case nameof(UInt16):
                    res = value is ushort u ? u : Convert.ToUInt16($"{value}", 16);
                    break;
                case nameof(Byte):
                    res = value is byte y ? y : Convert.ToByte($"{value}", 16);
                    break;
                case nameof(Boolean):
                    res = value is bool b ? b : Convert.ToByte($"{value}") == 1;
                    break;
                default:
                    throw new InvalidOperationException($"{type} | {value}");
            }
            return (T)res;
        }

        public static ChangeList Collect(this MachineState m)
        {
            var list = new ChangeList(m);
            return list;
        }
    }
}