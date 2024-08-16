using PoViEmu.Common;
using PoViEmu.Core.Meta;

namespace PoViEmu.Core
{
    public class Xtra
    {
        public static void Y()
        {
            BytesHelper.Allocate(5);

            var a1 = Devices.Create(DeviceModel.S460);
            var a2 = Devices.Create(DeviceModel.S660);

            Processor cpu = new NC3022();
            Video gfx = new GenericLCD();
            Battery bat = new SimpleBattery();
            Sound snd = new BeepSound();
            Keyboard kb = new GeneralKeys();
            Port port = new SerialPort();
        }

        public static void X()
        {
            var segment1 = new MemorySegment { [0x0000] = 124, [0xFFFF] = 98 };
            var segment2 = new MemorySegment { [0x0000] = 124, [0xFFFF] = 98 };
            var segment3 = new MemorySegment { [0x0000] = 124, [0xFFFF] = 98 };

            var space = new MemorySpace { [0xC0000] = segment1, [0xD0000] = segment2, [0xE0000] = segment3 };
        }
    }
}