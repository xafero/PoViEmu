using System;
using Iced.Intel;
using PoViEmu.Core.Decoding;

// ReSharper disable InconsistentNaming

namespace PoViEmu.Core.Hardware
{
    public sealed class NC3022
    {
        public void Execute(MachineState s, Instruction i)
        {
            var m = i.Mnemonic;
            switch (m)
            {
                case Mnemonic.Push:
                    if (i is { OpCount: 1, Op0Kind: OpKind.Register })
                        switch (i.Op0Register)
                        {
                            case Register.CX: s.PushStack(s.CX); return;
                            case Register.DX: s.PushStack(s.DX); return;
                            case Register.SI: s.PushStack(s.SI); return;
                            case Register.DI: s.PushStack(s.DI); return;
                            default: throw new InvalidOperationException($"{i} ?");
                        }
                    break;

                
                
                
                
            }
            throw new ArgumentException($"{m} ({i}) ?!");
        }
    }
}