import argparse
from core_sys import create_dir
from core_find import run_finding


def run_app():
    parser = argparse.ArgumentParser(description="A tool for extracting CASIO PV resources.")
    parser.add_argument("-i", "--input", type=str, dest="inpdir", help="path to the source directory")
    parser.add_argument("-o", "--output", type=str, dest="outdir", help="path to the target directory")
    args = parser.parse_args()

    if args.inpdir is None or args.outdir is None:
        print("Failed to parse options! Try help.")
        return

    dir_src = args.inpdir
    dir_dst = create_dir(args.outdir)

    print(f"Input folder  : {dir_src}")
    print(f"Output folder : {dir_dst}")
    run_finding(dir_src, dir_dst)

    print("Done.")


if __name__ == "__main__":
    run_app()
