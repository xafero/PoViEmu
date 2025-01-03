using System.Collections.Generic;
using HexIO;

namespace PoViEmu.Base.ABI.Hex
{
    public static class IntelHexExt
    {
        public static IEnumerable<IntelHexRecord> ReadRecords(this IntelHexStreamReader reader)
        {
            while (!reader.EndOfStream)
            {
                var record = reader.ReadHexRecord();
                yield return record;
            }
        }

        public static void ReadRecord(IntelHexRecord record, HexState state, DataHandler onData)
        {
            switch (record.RecordType)
            {
                case IntelHexRecordType.Data:
                    {
                        var nextAddress = record.Offset + state.BaseAddress;
                        for (var i = 0; i < record.RecordLength; i++)
                        {
                            var current = nextAddress + i;
                            var value = record.Data[i];
                            onData(current, value);
                        }
                        break;
                    }
                case IntelHexRecordType.EndOfFile:
                    {
                        state.EndOfFile = true;
                        break;
                    }
                case IntelHexRecordType.ExtendedSegmentAddress:
                    {
                        state.BaseAddress = (record.Data[0] << 8 | record.Data[1]) << 4;
                        break;
                    }
                case IntelHexRecordType.ExtendedLinearAddress:
                    {
                        state.BaseAddress = (record.Data[0] << 8 | record.Data[1]) << 16;
                        break;
                    }
                case IntelHexRecordType.StartSegmentAddress:
                    {
                        state.CS = (ushort)(record.Data[0] << 8 + record.Data[1]);
                        state.IP = (ushort)(record.Data[2] << 8 + record.Data[3]);
                        break;
                    }
                case IntelHexRecordType.StartLinearAddress:
                    state.EIP = (uint)(record.Data[0] << 24) +
                                (uint)(record.Data[1] << 16) +
                                (uint)(record.Data[2] << 8) +
                                record.Data[3];
                    break;
            }
        }
    }
}