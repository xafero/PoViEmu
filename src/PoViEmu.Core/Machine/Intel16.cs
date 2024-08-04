// ReSharper disable InconsistentNaming

using System.Collections.Generic;
using System.IO;
using PoViEmu.Core.Machine.Args;
using PoViEmu.Core.Machine.Core;
using PoViEmu.Core.Machine.Ops;
using System;
using System.Collections.Generic;
using System.IO;
using PoViEmu.Core.Machine.Args;
using PoViEmu.Core.Machine.Core;
using PoViEmu.Core.Machine.Decoding;
using PoViEmu.Core.Machine.Ops;
using OpTool = PoViEmu.Common.EnumHelper<PoViEmu.Core.Machine.Decoding.Opa>;
using OfaTool = PoViEmu.Common.EnumHelper<PoViEmu.Core.Machine.Decoding.Opa1A>;
using OfbTool = PoViEmu.Common.EnumHelper<PoViEmu.Core.Machine.Decoding.Opa1B>;
using OfcTool = PoViEmu.Common.EnumHelper<PoViEmu.Core.Machine.Decoding.Opa1C>;
using RgTool = PoViEmu.Common.EnumHelper<PoViEmu.Core.Machine.Decoding.Rgo>;
using SgTool = PoViEmu.Common.EnumHelper<PoViEmu.Core.Machine.Decoding.Sgo>;
using FgTool = PoViEmu.Common.EnumHelper<PoViEmu.Core.Machine.Decoding.Fgo>;
using RaTool = PoViEmu.Common.EnumHelper<PoViEmu.Core.Machine.Decoding.Rga>;
using OdTool = PoViEmu.Common.EnumHelper<PoViEmu.Core.Machine.Decoding.OpDir>;
using ObTool = PoViEmu.Common.EnumHelper<PoViEmu.Core.Machine.Decoding.OpBit>;
using O = PoViEmu.Core.Machine.Ops.OpCode;
using R = PoViEmu.Core.Machine.Ops.Register;
using M = PoViEmu.Core.Machine.Ops.Modifier;

namespace PoViEmu.Core.Machine
{
    public static class Intel16
    {
        public static IEnumerable<Instruction> Disassemble(Stream stream, byte[] buffer)
        {
            while (stream.ReadBytesPos(buffer) is { } pos)
            {
                var first = buffer[0];
                switch (first)
                {
                    case 0x06:
                        yield return new(pos, first, 1, O.push, args: [R.es]);
                        continue;
                    case 0x07:
                        yield return new(pos, first, 1, O.pop, args: [R.es]);
                        continue;
                    case 0x0E:
                        yield return new(pos, first, 1, O.push, args: [R.cs]);
                        continue;
                    case 0x16:
                        yield return new(pos, first, 1, O.push, args: [R.ss]);
                        continue;
                    case 0x17:
                        yield return new(pos, first, 1, O.pop, args: [R.ss]);
                        continue;
                    case 0x1E:
                        yield return new(pos, first, 1, O.push, args: [R.ds]);
                        continue;
                    case 0x1F:
                        yield return new(pos, first, 1, O.pop, args: [R.ds]);
                        continue;
                    case 0x27:
                        yield return new(pos, first, 1, O.daa);
                        continue;
                    case 0x2F:
                        yield return new(pos, first, 1, O.das);
                        continue;
                    case 0x37:
                        yield return new(pos, first, 1, O.aaa);
                        continue;
                    case 0x3F:
                        yield return new(pos, first, 1, O.aas);
                        continue;
                    default:
                        throw new InstructionError(pos, first);
                }
            }
        }
    }
}