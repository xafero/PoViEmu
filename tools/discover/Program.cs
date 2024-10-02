using System;
using CommandLine;
using PoViEmu.Common;
using PoViEmu.Core.Hardware;
using PoViEmu.Core.Hardware.AckNow;

namespace Discover
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var m = new MachineState();

            m.Set(B16Register.AX, 0x1234);
            m.Set(B16Register.BX, 0x2345);
            m.Set(B16Register.CX, 0x3829);
            m.Set(B16Register.DX, 0x3934);
            m.Set(B16Register.SP, 0x2935);
            m.Set(B16Register.IP, 0x4451);
            m.Set(B16Register.BP, 0x2923);
            m.Set(B16Register.SI, 0x2398);
            m.Set(B16Register.DI, 0x9323);
            m.Set(B16Register.CS, 0x2322);
            m.Set(B16Register.DS, 0x2499);
            m.Set(B16Register.SS, 0x3811);
            m.Set(B16Register.ES, 0x1389);
            
            m.Set(B8Register.AH, 0x05);
            m.Set(B8Register.AL, 0x06);
            m.Set(B8Register.BH, 0x07);
            m.Set(B8Register.BL, 0x08);
            m.Set(B8Register.CH, 0x09);
            m.Set(B8Register.CL, 0x10);
            m.Set(B8Register.DH, 0x11);
            m.Set(B8Register.DL, 0x12);
            
            m.Set(EmsRegister.Bank0, 0x10);
            m.Set(EmsRegister.Bank1, 0x20);
            m.Set(EmsRegister.Bank2, 0x30);
            m.Set(EmsRegister.Bank3, 0x40);
            m.Set(EmsRegister.Bank4, 0x50);
            m.Set(EmsRegister.Bank5, 0x60);
            m.Set(EmsRegister.Bank6, 0x70);
            m.Set(EmsRegister.Frame0, 0x80);
            m.Set(EmsRegister.Frame1, 0x90);
            m.Set(EmsRegister.Frame2, 0x100);
            m.Set(EmsRegister.Frame3, 0x110);
            m.Set(EmsRegister.Frame4, 0x120);
            m.Set(EmsRegister.Frame5, 0x130);
            m.Set(EmsRegister.Frame6, 0x140);
            m.Set(EmsRegister.Frame7, 0x150);
            m.Set(EmsRegister.Frame8, 0x160);
            m.Set(EmsRegister.Frame9, 0x170);
            m.Set(EmsRegister.Frame10, 0x180);
            m.Set(EmsRegister.Frame11, 0x190);
            
            m.Set(FlagRegister.TF, true);
            m.Set(FlagRegister.DF, true);
            m.Set(FlagRegister.IF, true);
            m.Set(FlagRegister.OF, true);
            m.Set(FlagRegister.SF, true);
            m.Set(FlagRegister.ZF, true);
            m.Set(FlagRegister.AF, true);
            m.Set(FlagRegister.PF, true);
            m.Set(FlagRegister.CF, true);

            Console.WriteLine(JsonHelper.ToJson(m));
        }

        private static void Main2(string[] args)
        {
            var parser = Parser.Default;
            parser.ParseArguments<Options>(args).WithParsed(o =>
            {
                if (o.HexMatch)
                {
                    HexMatch.Run(o);
                    return;
                }
                if (o.HexLine)
                {
                    HexLine.Run(o);
                    return;
                }
                if (o.GetModels)
                {
                    ModPull.Run(o);
                    return;
                }
                if (o.LookBins)
                {
                    LookBins.Run(o);
                    return;
                }
                if (o.ReadBins)
                {
                    ReadBins.Run(o);
                }
            });
        }
    }
}