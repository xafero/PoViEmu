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
                    default:
                        yield break;
                }
            }
        }
    }
}