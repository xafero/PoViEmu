
bits 16

org 0x100

start:
    xor bx, bx

.loop:
    inc bx
    mov ax, bx
    add al, '0'
    mov dl, al
    mov ah, 0x02
    int 0x21
    call delay
    jmp .loop

delay:
    mov cx, 0x2
.delay_outer:
    push cx
    mov cx, 0x2
.delay_inner:
    loop .delay_inner
    pop cx
    loop .delay_outer
    ret


