using System;
using Iced.Intel;

// ReSharper disable InconsistentNaming

namespace PoViEmu.Core.Hardware
{
    public sealed class NC3022
    {
        public void Execute(Instruction instruct)
        {
            var m = instruct.Mnemonic;
            switch (m)
            {
                // case Mnemonic.Aas: break;

                // case Mnemonic.Xor: break;

                default: throw new ArgumentException($"{m} ?!");
            }
        }
    }
}