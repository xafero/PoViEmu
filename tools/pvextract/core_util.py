from core_sys import check_ext


def is_ignore(file_ext):
    return (check_ext(file_ext, '.prc')
            or check_ext(file_ext, '.txt')
            or check_ext(file_ext, '.lzh')
            or check_ext(file_ext, '.adt')
            or check_ext(file_ext, '.pdf')
            or check_ext(file_ext, '.xls')
            or check_ext(file_ext, '.doc')
            or check_ext(file_ext, '.ods')
            or check_ext(file_ext, '.sdd')
            or check_ext(file_ext, '.bat')
            or check_ext(file_ext, '.res')
            or check_ext(file_ext, '.html')
            or check_ext(file_ext, '.sig')
            or check_ext(file_ext, '.eml')
            or check_ext(file_ext, '.jpg')
            or check_ext(file_ext, '.bmp')
            or check_ext(file_ext, '.png')
            or check_ext(file_ext, '.gif')
            or check_ext(file_ext, '.ima')
            or check_ext(file_ext, '.pvf')
            or check_ext(file_ext, '.pas')
            or check_ext(file_ext, '.obj')
            or check_ext(file_ext, '.json')
            or check_ext(file_ext, '.ini')
            or check_ext(file_ext, '.00')
            or check_ext(file_ext, '.1')
            or check_ext(file_ext, '.2')
            or check_ext(file_ext, '.plg')
            or check_ext(file_ext, '.dbg')
            or check_ext(file_ext, '.cpj')
            or check_ext(file_ext, '.lin')
            or check_ext(file_ext, '.map')
            or check_ext(file_ext, '.bas')
            or check_ext(file_ext, '.obp')
            or check_ext(file_ext, '.mo')
            or check_ext(file_ext, '.lnk')
            or check_ext(file_ext, '.msg')
            or check_ext(file_ext, '.inc')
            or check_ext(file_ext, '.abs')
            or check_ext(file_ext, '.cpp')
            or check_ext(file_ext, '.hpp')
            or check_ext(file_ext, '.c')
            or check_ext(file_ext, '.h')
            or check_ext(file_ext, '.fr')
            or check_ext(file_ext, '.es')
            or check_ext(file_ext, '.it')
            or check_ext(file_ext, '.de')
            or check_ext(file_ext, '.cab')
            or check_ext(file_ext, '.lst')
            or check_ext(file_ext, '.lng')
            or check_ext(file_ext, '.rtf')
            or check_ext(file_ext, '.cnt')
            or check_ext(file_ext, '.app')
            or check_ext(file_ext, '.htm')
            or check_ext(file_ext, '.bak')
            or check_ext(file_ext, '.bdsproj')
            or check_ext(file_ext, '.off')
            or check_ext(file_ext, '.mak')
            or check_ext(file_ext, '.mem')
            or check_ext(file_ext, '.sub')
            or check_ext(file_ext, '.def')
            or check_ext(file_ext, '.dlr')
            or check_ext(file_ext, '.dlw')
            or check_ext(file_ext, '.dll')
            or check_ext(file_ext, '.dlp')
            or check_ext(file_ext, '.dlm')
            or check_ext(file_ext, '.hlp')
            or check_ext(file_ext, '.log')
            or check_ext(file_ext, '.isu')
            or check_ext(file_ext, '.lib')
            or check_ext(file_ext, '.img')
            or len(file_ext) == 0)


def is_bin(file_ext):
    return check_ext(file_ext, '.bin')


def is_hex(file_ext):
    return check_ext(file_ext, '.hex')


def is_diff(file_ext):
    return check_ext(file_ext, '.diff')


def is_dat(file_ext):
    return check_ext(file_ext, '.dat')


def is_twf(file_ext):
    return check_ext(file_ext, '.twf')


def is_pva(file_ext):
    return check_ext(file_ext, '.pva')


def is_interesting(file_path):
    return is_bin(file_path) or is_hex(file_path) or is_diff(file_path) or is_dat(file_path) or is_pva(file_path) or is_twf(file_path)


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
