using System;
using PoViEmu.Core.Machine.Core;
using O = PoViEmu.Core.Machine.Ops.OpCode;
using R = PoViEmu.Core.Machine.Ops.Register;
using A = PoViEmu.Core.Machine.Ops.OpArg;

namespace PoViEmu.Core.Machine.Decoding
{
    public static class DecodeExt
    {
        public static O ToOp<T>(this T val)
        {
            return Enum.TryParse<O>(val?.ToString(), out var res)
                ? res
                : throw new InvalidOperationException($"{val} ?!");
        }

        public static R ToReg(this object val)
        {
            return Enum.TryParse<R>(val.ToString(), out var reg)
                ? reg
                : default;
        }

        public static A ToReg(this Rgo val, Rga mode, OpBit bit)
        {
            var is16Bit = bit == OpBit.b16;
            switch (mode)
            {
                case Rga.R2M:
                    switch (val)
                    {
                        case Rgo.ax: return R.bx.Plus(R.si, 0);
                        case Rgo.bx: return R.bp.Plus(R.di, 0);
                        case Rgo.cx: return R.bx.Plus(R.di, 0);
                        case Rgo.dx: return R.bp.Plus(R.si, 0);
                        case Rgo.di: return R.bx.Box(0);
                        case Rgo.si: return default;
                        case Rgo.sp: return R.si.Box(0);
                        case Rgo.bp: return R.di.Box(0);
                        default: throw new ArgumentOutOfRangeException(nameof(val), val, null);
                    }
                case Rga.R2R:
                    switch (val)
                    {
                        case Rgo.ax: return is16Bit ? R.ax : R.al;
                        case Rgo.bx: return is16Bit ? R.bx : R.bl;
                        case Rgo.cx: return is16Bit ? R.cx : R.cl;
                        case Rgo.dx: return is16Bit ? R.dx : R.dl;
                        case Rgo.di: return is16Bit ? R.di : R.bh;
                        case Rgo.si: return is16Bit ? R.si : R.dh;
                        case Rgo.sp: return is16Bit ? R.sp : R.ah;
                        case Rgo.bp: return is16Bit ? R.bp : R.ch;
                        default: throw new ArgumentOutOfRangeException(nameof(val), val, null);
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }
    }
}