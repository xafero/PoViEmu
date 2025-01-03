#!/bin/sh

echo "." > dump.txt

for it in *.com
do
  objdump -D -Mintel,i8086 -b binary -m i386 --no-addresses --no-show-raw-insn "${it}" >> dump.txt
done

cat dump.txt \
    | grep -v ".byte" \
    | grep -v ".data" \
    | grep -v "file format binary" \
    | grep -v "Disassembly of" \
    | sort | uniq >> dump.s.txt

mv dump.s.txt dump.txt

