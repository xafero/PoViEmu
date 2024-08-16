namespace PoViEmu.Core.Meta
{
    public static class Devices
    {
        public static Device? Create(DeviceModel model)
        {
            switch (model)
            {
                case DeviceModel.S460:
                    return new Device(model, 4, 2, 1, 1);
                case DeviceModel.S660:
                    return new Device(model, 6, 4, 1, 1);
            }
            return null;
        }
    }
}