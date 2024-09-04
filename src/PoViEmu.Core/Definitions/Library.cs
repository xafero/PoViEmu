using System.Collections.Generic;
using B = ByteSizeLib.ByteSize;

namespace PoViEmu.Core.Definitions
{
    public static class Library
    {
        public static List<DeviceDef> GetAllDevices()
        {
            const string soc = "NC3022";
            // const string hit = "SH3_7709";
            const string rs = "RS232";
            const string ird = "IrDA";
            // const string usb = "USB1";
            ScreenSize size = new(160, 160);

            var list = new List<DeviceDef>
            {
                new("PV-250X", 1999, "Z481", soc,
                    B.FromMebiBytes(2), size, rs, "Europe"),
                new("PV-450X", 1999, "Z481", soc,
                    B.FromMebiBytes(4), size, rs, "Europe"),

                new("PV-S250", 2000, "Z486", soc,
                    B.FromMebiBytes(2), size, rs, "Europe"),
                new("PV-S450", 2000, "Z486", soc,
                    B.FromMebiBytes(4), size, rs, "Europe"),

                new("PV-750+", 2000, "Z488", soc,
                    B.FromMebiBytes(2), size, ird, "Europe"),

                new("PV-S460", 2001, "G500", soc,
                    B.FromMebiBytes(4), size, rs, "Europe"),
                new("PV-S660", 2001, "G500", soc,
                    B.FromMebiBytes(6), size, rs, "Europe"),

                new("PV-S400+", 2002, "G501", soc,
                    B.FromMebiBytes(4), size, rs, "USA"),
                new("PV-S600+", 2002, "G501", soc,
                    B.FromMebiBytes(6), size, rs, "USA")
                
                /*, new("PV-S1600", 2003, "EDV20", hit,
                    B.FromMebiBytes(12), size, usb, "World") */
            };

            return list;
        }
    }
}