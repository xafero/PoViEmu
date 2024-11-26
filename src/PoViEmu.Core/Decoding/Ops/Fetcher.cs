using System.Collections.Generic;
using Iced.Intel;
using PoViEmu.Core.Hardware;
using R = Iced.Intel.Register;

namespace PoViEmu.Core.Decoding.Ops
{
    public static class Fetcher
    {
        public static IEnumerable<XInstruction> Prefetch(this MachineState m)
        {
            foreach (var instruct in m.ToInstructions(m.CS, m.IP))
            {
                yield return instruct;
            }
        }

        private static bool Is8Bit(R reg)
        {
            switch (reg)
            {
                case R.AL:
                case R.BL:
                case R.CL:
                case R.DL:
                case R.AH:
                case R.BH:
                case R.CH:
                case R.DH:
                    return true;
            }
            return false;
        }

        private static bool Is16Bit(R reg)
        {
            switch (reg)
            {
                case R.AX:
                case R.BX:
                case R.CX:
                case R.DX:
                case R.BP:
                case R.SP:
                case R.DI:
                case R.SI:
                case R.CS:
                case R.DS:
                case R.ES:
                case R.SS:
                    return true;
            }
            return false;
        }

        public static bool IsInvalidFor16Bit(this Instruction parsed)
        {
            return parsed.IsInvalid || parsed.CodeSize != CodeSize.Code16;
        }
    }
}