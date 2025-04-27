
.section .text
.global _start

_start:
    mov     #0, r1

loop:
    add     #1, r1
    mov     r1, r2

    mov     #0xFF, r10
    and     r10, r2

    add     #'0', r2
    mov     r2, r4
    mov     #0x02, r3
    trapa   #0x21

    bsr     delay
    nop

    bra     loop
    nop

delay:
    mov     #2, r6
delay_outer:
    mov     #2, r7
delay_inner:
    dt      r7
    bf      delay_inner

    dt      r6
    bf      delay_outer

    rts
    nop

