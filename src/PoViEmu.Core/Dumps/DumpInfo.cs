using System;
using System.Collections.Generic;
using PoViEmu.Common;
using PoViEmu.Core.Addins;

namespace PoViEmu.Core.Dumps
{
    public sealed class DumpInfo
    {
        internal readonly DumpHeader _real;

        public DumpInfo(DumpHeader header)
            => _real = header;

        public string Signature
            => TextHelper.CleanUp(new string(_real.Signature));

        public DumpModel Model
            => TextHelper.ToEnum<DumpModel>(_real.Model, default);

        /// <summary>
        /// Values: M, N, h, m, o
        /// </summary>
        public char Flag { get; set; }

        public DateTime TimeStamp { get; set; }
        public Version Version { get; set; }
        public DateTime DeviceStamp { get; set; }
        public DumpModel DeviceModel { get; set; }

        public IDictionary<FileAddress, AddInInfo> AddIns { get; set; }
    }
}