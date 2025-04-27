#!/bin/sh

nasm -f bin op_x86.asm -o op_x86.com
objdump -D -Mintel,i8086 -b binary -m i386 -z op_x86.com > op_x86.d.txt

sh-elf-as --isa=sh3 -o op_sh3.obj op_sh3.asm
sh-elf-objcopy -O binary op_sh3.obj op_sh3.com
sh-elf-objdump -D -b binary -m sh3 -z op_sh3.com > op_sh3.d.txt

