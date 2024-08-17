namespace PoViEmu.Core.Meta
{
    public sealed class Device
    {
        public Device(DeviceModel model, int memoryMb, int userMb, int addInMb, int osMb)
        {
            // TODO: segmented memory space --> a maximum of 16 addins --> 64 kb x 16 = 1 MB 
            
            Model = model;
            Memory = memoryMb * 1024 * 1024;
            UserMemory = userMb * 1024 * 1024;
            AddInMemory = addInMb * 1024 * 1024;
            SysMemory = osMb * 1024 * 1024;
        }

        public DeviceModel Model { get; }
        public int Memory { get; }
        public int UserMemory { get; }
        public int AddInMemory { get; }
        public int SysMemory { get; }
    }
}