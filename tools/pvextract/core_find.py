import glob
import argparse
import json
import os
import io
import libarchive
from rarfile import RarFile
from zipfile import ZipFile
from tarfile import TarFile, ReadError as TarError
from os.path import splitext, exists, join, basename, isfile
from core_util import *
from core_addin import load_addin_header
from core_sys import create_dir, copy_maybe


def process_one_file(filename, input_dir, output_dir, file_obj=None):
    v_file_ext = splitext(filename)[1]
    v_file_name = filename.replace(input_dir, '')
    if is_ignore(v_file_ext):
        return
    if is_bin(v_file_ext):
        addin = load_addin_header(filename, file_obj)
        if addin is None:
            return
        addin_name = addin['name']
        if len(addin_name) == 0:
            return
        addin_model = addin['model']
        addin_ver = addin['version']
        dst_name = addin_name + "_" + addin_ver + ".bin"
        dst_dir = create_dir(join(output_dir, "addins", addin_model))
        dst_file = join(dst_dir, dst_name)
        if exists(dst_file):
            return
        copy_maybe(filename, dst_file, file_obj)
        print(f" * {v_file_name} --> {dst_file}")
        return
    if is_diff(v_file_ext):
        dst_dir = create_dir(join(output_dir, "diffs"))
        dst_file = join(dst_dir, basename(filename))
        if exists(dst_file):
            return
        copy_maybe(filename, dst_file, file_obj)
        print(f" * {v_file_name} --> {dst_file}")
        return
    if is_hex(v_file_ext):
        dst_dir = create_dir(join(output_dir, "hexs"))
        dst_file = join(dst_dir, basename(filename))
        if exists(dst_file):
            return
        copy_maybe(filename, dst_file, file_obj)
        print(f" * {v_file_name} --> {dst_file}")
        return
    if is_dat(v_file_ext):
        dst_dir = create_dir(join(output_dir, "dats"))
        dst_file = join(dst_dir, basename(filename))
        if exists(dst_file):
            return
        copy_maybe(filename, dst_file, file_obj)
        print(f" * {v_file_name} --> {dst_file}")
        return
    if is_zip(v_file_ext):
        try:
            with ZipFile(filename) as zf:
                for z_item in zf.namelist():
                    if is_interesting(z_item):
                        with zf.open(z_item, 'r') as zfs:
                            process_one_file(z_item, '', output_dir, zfs.read())
        except:
            pass
        return
    if is_7z(v_file_ext) or is_iso(v_file_ext) or is_exe(v_file_ext):
        try:
            with libarchive.file_reader(filename) as lf:
                for l_entry in lf:
                    l_item = l_entry.path
                    if is_interesting(l_item):
                        with io.BytesIO() as l_mem:
                            for l_block in l_entry.get_blocks():
                                l_mem.write(l_block)
                            l_mem.seek(0)
                            process_one_file(l_item, '', output_dir, l_mem.read())
        except libarchive.exception.ArchiveError:
            pass
        return
    if is_rar(v_file_ext):
        try:
            with RarFile(filename) as rf:
                for r_item in rf.namelist():
                    if is_interesting(r_item):
                        with rf.open(r_item, 'r') as rfs:
                            process_one_file(r_item, '', output_dir, rfs.read())
        except:
            pass
        return
    if is_tgz(v_file_ext):
        try:
            with TarFile.open(filename) as tf:
                for t_member in tf.getmembers():
                    t_item = t_member.path
                    if is_interesting(t_item):
                        with tf.extractfile(t_member) as tfs:
                            process_one_file(t_item, '', output_dir, tfs.read())
        except TarError:
            pass
        return
    print(f" * {v_file_name} --> {v_file_ext}")


def run_finding(input_dir, output_dir):
    for filename in glob.iglob(join(input_dir, "**", "*"), recursive=True):
        if not isfile(filename):
            continue
        process_one_file(filename, input_dir, output_dir)
