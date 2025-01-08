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

        public static byte[] WriteUInt8(byte value)
        {
            var buffer = new byte[1];
            WriteUInt8(value, buffer);
            return buffer;
        }

        public static void WriteUInt8(byte value, byte[] buffer, int offset = 0)
            => WriteInt8((sbyte)value, buffer, offset);

        public static byte[] WriteUInt16(ushort value, EndianMode mode = default)
        {
            var buffer = new byte[2];
            WriteUInt16(value, buffer, mode);
            return buffer;
        }

        public static void WriteUInt16(ushort value, byte[] buffer, EndianMode mode = default, long offset = 0)
            => WriteInt16((short)value, buffer, mode, offset);

        public static byte[] WriteUInt32(uint value, EndianMode mode = default)
        {
            var buffer = new byte[4];
            WriteUInt32(value, buffer, mode);
            return buffer;
        }

        public static void WriteUInt32(uint value, byte[] buffer, EndianMode mode = default, long offset = 0)
            => WriteInt32((int)value, buffer, mode, offset);

        public static byte[] WriteInt8(sbyte value)
        {
            var buffer = new byte[1];
            WriteInt8(value, buffer);
            return buffer;
        }

        public static void WriteInt8(sbyte value, byte[] buffer, int offset = 0)
        {
            buffer[offset] = (byte)value;
        }

        public static byte[] WriteInt16(short value, EndianMode mode = default)
        {
            var buffer = new byte[2];
            WriteInt16(value, buffer, mode);
            return buffer;
        }

        public static void WriteInt16(short value, byte[] buffer, EndianMode mode = default, long offset = 0)
        {
            if (mode == EndianMode.BigEndian)
            {
                buffer[offset] = (byte)((value >> 8) & 0xFF);
                buffer[offset + 1] = (byte)(value & 0xFF);
            }
            else
            {
                buffer[offset] = (byte)(value & 0xFF);
                buffer[offset + 1] = (byte)((value >> 8) & 0xFF);
            }
        }

        public static byte[] WriteInt32(int value, EndianMode mode = default)
        {
            var buffer = new byte[4];
            WriteInt32(value, buffer, mode);
            return buffer;
        }

        public static void WriteInt32(int value, byte[] buffer, EndianMode mode = default, long offset = 0)
        {
            if (mode == EndianMode.BigEndian)
            {
                buffer[offset] = (byte)((value >> 24) & 0xFF);
                buffer[offset + 1] = (byte)((value >> 16) & 0xFF);
                buffer[offset + 2] = (byte)((value >> 8) & 0xFF);
                buffer[offset + 3] = (byte)(value & 0xFF);
            }
            else
            {
                buffer[offset] = (byte)(value & 0xFF);
                buffer[offset + 1] = (byte)((value >> 8) & 0xFF);
                buffer[offset + 2] = (byte)((value >> 16) & 0xFF);
                buffer[offset + 3] = (byte)((value >> 24) & 0xFF);
            }
        }
    }
}