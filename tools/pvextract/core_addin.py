from datetime import datetime
from ctypes import Structure, c_char, c_ushort, c_uint, sizeof
from io import BytesIO


class AddInHeader(Structure):
    _pack_ = 1

    _fields_ = [
        # Signature of the Add-In {0x00, 0xFF, 'C','A','S','I','O', 0x03}
        ("Signature", c_char * 8),
        # Model identifier {'Z','4','8','6'}
        ("Model", c_char * 4),
        # Version of the header {'0','1','0','0'}
        ("HeaderVersion", c_char * 4),
        # Status of the Add-In
        ("Status", c_ushort),
        # Mode and sub-mode of the Add-In
        ("Mode", c_ushort),
        # Name of the Add-In
        ("Name", c_char * 16),
        # Length of the Add-In data
        ("Length", c_uint),
        # Date of compilation (YYYYMMDD), e.g. "20020219" stands for Feb. 19th, 2002
        ("CompileDate", c_char * 8),
        # Time of compilation (HHMM), e.g. "1246" is 46 minutes past 12 o'clock
        ("CompileTime", c_char * 4),
        # Version of the Add-In, "0120" stands for version 1.20
        ("Version", c_char * 4),
        # Date of library (YYYYMMDD), e.g. "20000215" stands for Feb. 15th, 2000
        ("LibraryDate", c_char * 8),
        # Time of library compilation (HHMM), e.g. "0940" is 40 minutes past 9 o'clock
        ("LibraryTime", c_char * 4),
        # Version of the library, e.g. "0100" stands for version 1.00
        ("LibraryVersion", c_char * 4),
        # Offset to the Add-In icon data
        ("OffsetIcon", c_uint),
        # Offset to the Add-In list icon data
        ("OffsetLIcon", c_uint),
        # Comment to the Add-In
        ("UserComment", c_char * 64),
    ]


def load_addin_header(file_name, file_arr):
    try:
        with (open(file_name, "rb") if file_arr is None else BytesIO(file_arr)) as f:
            header_size = sizeof(AddInHeader)
            data = f.read(header_size)
            raw = AddInHeader.from_buffer_copy(data)
            return to_addin_info(raw)
    except ValueError:
        return None
    except UnicodeDecodeError:
        return None


def to_addin_info(header):
    res = {'model': header.Model.decode(),
           'status': header.Status,
           'mode': header.Mode,
           'name': parse_utf(header.Name),
           'length': header.Length,
           'compiled': parse_datetime(header.CompileDate.decode(), header.CompileTime.decode()),
           'version': parse_version(header.Version.decode())}
    return res


def parse_version(ver):
    major = ver[0].lstrip('0') + ver[1]
    minor = ver[2] + ver[3].rstrip('0')
    return major + "." + minor


def parse_datetime(c_date, c_time):
    format_str = "%Y%m%d%H%M"
    return datetime.strptime(c_date + c_time, format_str).isoformat()


def parse_utf(my_bytes):
    try:
        txt = my_bytes.decode()
    except UnicodeDecodeError:
        txt = ''
    return (txt.replace(' ', '')
            .replace('\'', '')
            .replace('!', '')
            .replace('.', '-')
            .replace('#', 'Hash'))
