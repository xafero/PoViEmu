#!/bin/sh

for it in *_x86.com
do
  objdump -D -Mintel,i8086 -b binary -m i386 -z "${it}" > "${it%.*}.d.txt"
done

for it in *_sh3.com
do
  sh-elf-objdump -D -b binary -m sh3 -z "${it}" > "${it%.*}.d.txt"
done


