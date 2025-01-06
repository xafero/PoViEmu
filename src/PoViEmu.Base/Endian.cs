namespace PoViEmu.Base
{
    public static class Endian
    {
        public static byte ReadUInt8(byte[] buffer, int offset = 0)
            => buffer[offset];

        public static sbyte ReadInt8(byte[] buffer, int offset = 0)
            => (sbyte)ReadUInt8(buffer, offset);

        private static int ReadRawInt16(byte[] buffer, int offset = 0, EndianMode mode = default)
            => mode == EndianMode.BigEndian
                ? (buffer[offset] << 8) | buffer[offset + 1]
                : (buffer[offset + 1] << 8) | buffer[offset];

        public static short ReadInt16(byte[] buffer, int offset = 0, EndianMode mode = default)
            => (short)ReadRawInt16(buffer, offset, mode);

        public static ushort ReadUInt16(byte[] buffer, int offset = 0, EndianMode mode = default)
            => (ushort)ReadRawInt16(buffer, offset, mode);

        public static int ReadInt32(byte[] buffer, int offset = 0, EndianMode mode = default)
            => mode == EndianMode.BigEndian
                ? (buffer[offset] << 24) | (buffer[offset + 1] << 16) |
                  (buffer[offset + 2] << 8) | buffer[offset + 3]
                : (buffer[offset + 3] << 24) | (buffer[offset + 2] << 16) |
                  (buffer[offset + 1] << 8) | buffer[offset];

        public static uint ReadUInt32(byte[] buffer, int offset = 0, EndianMode mode = default)
            => (uint)ReadInt32(buffer, offset, mode);
    }
}