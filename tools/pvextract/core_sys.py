import shutil
from os import makedirs
from os.path import isdir


def create_dir(path):
    if not isdir(path):
        makedirs(path)
    return path


def copy_maybe(src_file, dst_file, src_obj):
    if src_obj is None:
        shutil.copy(src_file, dst_file)
        return
    with open(dst_file, "wb") as binary_file:
        binary_file.write(src_obj)


def check_ext(file_txt_raw, ext):
    file_txt = file_txt_raw.lower()
    if isinstance(file_txt, bytes):
        ext = ext.encode()
    return file_txt == ext or file_txt.endswith(ext)
