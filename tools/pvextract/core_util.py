from core_sys import check_ext


def is_ignore(file_ext):
    return (check_ext(file_ext, '.pva')
            or check_ext(file_ext, '.twf')
            or check_ext(file_ext, '.prc')
            or check_ext(file_ext, '.txt')
            or check_ext(file_ext, '.lzh')
            or check_ext(file_ext, '.adt')
            or check_ext(file_ext, '.pdf')
            or check_ext(file_ext, '.xls')
            or check_ext(file_ext, '.doc')
            or check_ext(file_ext, '.sdd')
            or check_ext(file_ext, '.bat')
            or check_ext(file_ext, '.res')
            or check_ext(file_ext, '.html')
            or check_ext(file_ext, '.sig')
            or check_ext(file_ext, '.jpg')
            or check_ext(file_ext, '.png')
            or check_ext(file_ext, '.gif')
            or check_ext(file_ext, '.ima')
            or check_ext(file_ext, '.pvf')
            or check_ext(file_ext, '.c')
            or check_ext(file_ext, '.h')
            or check_ext(file_ext, '.lib')
            or check_ext(file_ext, '.img'))


def is_bin(file_ext):
    return check_ext(file_ext, '.bin')


def is_hex(file_ext):
    return check_ext(file_ext, '.hex')


def is_diff(file_ext):
    return check_ext(file_ext, '.diff')


def is_dat(file_ext):
    return check_ext(file_ext, '.dat')


def is_interesting(file_path):
    return is_bin(file_path) or is_hex(file_path) or is_diff(file_path) or is_dat(file_path)


def is_tgz(file_ext):
    return (check_ext(file_ext, '.tar.gz') or check_ext(file_ext, '.tgz')
            or check_ext(file_ext, '.tar') or check_ext(file_ext, '.gz'))


def is_zip(file_ext):
    return check_ext(file_ext, '.zip')


def is_rar(file_ext):
    return check_ext(file_ext, '.rar')


def is_7z(file_ext):
    return check_ext(file_ext, '.7z')


def is_exe(file_ext):
    return check_ext(file_ext, '.exe')


def is_iso(file_ext):
    return check_ext(file_ext, '.iso')
