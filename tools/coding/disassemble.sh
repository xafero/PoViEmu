#!/bin/sh

for it in *.bin
do
  ndisasm -b 16 -p intel "${it}" > "${it%.*}.n.txt"
  objdump -D -Mintel,i8086 -b binary -m i386 "${it}" > "${it%.*}.o.txt"
done

