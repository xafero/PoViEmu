using System;
using System.IO;
using System.Linq;
using System.Text;
using PoViEmu.Base.CPU;

// ReSharper disable InconsistentNaming

namespace PoViEmu.I186.CPU.Soft
{
    public sealed class DOSInterrupts : IInterruptHandler, IDosEmu
    {
        public const byte MainIntNo = 0x21;
        
        /// <summary>
        /// Standard output
        /// </summary>
        public StringWriter StdOut { get; } = new();

        /// <summary>
        /// Last exit/return code
        /// </summary>
        public byte? ReturnCode { get; private set; }

        public void Handle(byte num, MachineState m)
        {
            switch (num)
            {
                case MainIntNo:
                    switch (m.AH)
                    {
                        // Write string to standard output
                        case 0x09:
                            const byte terminated = (byte)'$';
                            var chars = m.ReadMemory(m.DS, m.DX, 0xFF)
                                .TakeWhile(j => j != terminated).ToArray();
                            var txt = Encoding.ASCII.GetString(chars);
                            StdOut.Write(txt);
                            m.AL = 0x24;
                            return;

                        // Write character to standard output
                        case 0x02:
                            var oneChar = m.DL;
                            txt = Encoding.ASCII.GetString(new[] { oneChar });
                            StdOut.Write(txt);
                            m.AL = (byte)txt.FirstOrDefault();
                            return;

                        // Terminate with return code
                        case 0x4C:
                            ReturnCode = m.AL;
                            return;
                    }
                    break;
            }
            throw new InvalidOperationException($"Missing DOS interrupt 0x{num:X2}!");
        }
    }
}