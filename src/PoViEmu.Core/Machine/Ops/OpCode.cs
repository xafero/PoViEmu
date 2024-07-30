// ReSharper disable InconsistentNaming

namespace PoViEmu.Core.Machine.Ops
{
    public enum OpCode : byte
    {
        Unknown = 0,

        a32 = 0x67,

        aaa = 0x37,

        aam = 0xD4,

        aas = 0x3F,

        add = 0x80,

        add_ax = 0x03,

        adc = 0x81,

        and = 0x23,

        cbw = 0x98,

        clc = 0xF8,

        cld = 0xFC,

        cli = 0xFA,

        cmc = 0xF5,

        cmp = 0x3B,

        cmp_ax = 0x3D,

        cmp_b = 0x83,

        cmpsb = 0xA6,

        cmpsw = 0xA7,

        cs = 0x2E,

        cwd = 0x99,

        daa = 0x27,

        das = 0x2F,

        dec_dx = 0x4A,

        dec_si = 0x4E,

        dec_sp = 0x4C,

        dec_bp = 0x4D,

        dec_ax = 0x48,

        dec_bx = 0x4B,

        dec_cx = 0x49,

        dec_di = 0x4F,

        ds = 0x3E,

        es = 0x26,

        fs = 0x64,

        fstp = 0xDD,

        gs = 0x65,

        hlt = 0xF4,

        in_al_dx = 0xEC,

        in_ax_dx = 0xED,

        inc_ax = 0x40,

        inc_bp = 0x45,

        inc_bx = 0x43,

        inc_cx = 0x41,

        inc_di = 0x47,

        inc_dx = 0x42,

        inc_si = 0x46,

        inc_sp = 0x44,

        insb = 0x6C,

        insw = 0x6D,

        int1 = 0xF1,

        int3 = 0xCC,

        into = 0xCE,

        iret = 0xCF,

        jng = 0x7E,

        jmp_short = 0xEB,

        jl = 0x7C,

        jnl = 0x7D,

        jno = 0x71,

        jnz = 0x75,

        jz = 0x74,

        lahf = 0x9F,

        leave = 0xC9,

        @lock = 0xF0,

        lodsb = 0xAC,

        lodsw = 0xAD,

        loop = 0xE2,

        mov_ah = 0xB4,

        mov_al = 0x8A,

        mov = 0x8B,

        mov_bx = 0xBB,

        mov_si = 0xBE,

        mov_cx = 0xB9,

        mov_dx = 0xBA,

        mov_ax_s = 0xB8,

        mov_cl = 0xB1,

        movsb = 0xA4,

        movsw = 0xA5,

        mul = 0xF7,

        nop = 0x90,

        o32 = 0x66,

        out_dx_al = 0xEE,

        out_dx_ax = 0xEF,

        outsb = 0x6E,

        outsw = 0x6F,

        or = 0x0B,

        or_bl = 0x0a,

        pop_dx = 0x5A,

        pop_es = 0x07,

        pop_si = 0x5E,

        pop_cx = 0x59,

        pop_di = 0x5F,

        pop_ds = 0x1F,

        pop_sp = 0x5C,

        pop_ss = 0x17,

        pop_ax = 0x58,

        pop_bp = 0x5D,

        pop_bx = 0x5B,

        popa = 0x61,

        popf = 0x9D,

        push_ax = 0x50,

        push_cx = 0x51,

        push_dx = 0x52,

        push_bx = 0x53,

        push_bp = 0x55,

        push_si = 0x56,

        push_sp = 0x54,

        push_ss = 0x16,

        push_cs = 0x0E,

        push_di = 0x57,

        push_ds = 0x1E,

        push_es = 0x06,

        pusha = 0x60,

        pushf = 0x9C,

        rep = 0xF3,

        repne = 0xF2,

        ret = 0xC3,

        retf = 0xCB,

        sahf = 0x9E,

        salc = 0xD6,

        scasb = 0xAE,

        scasw = 0xAF,

        shl = 0xD3,

        shl_one = 0xD1,

        ss = 0x36,

        stc = 0xF9,

        std = 0xFD,

        sti = 0xFB,

        stosb = 0xAA,

        stosw = 0xAB,

        sub_ax = 0x2B,
        
        sub_al = 0x2C,

        test = 0x85,

        wait = 0x9B,

        xchg_ax_di = 0x97,

        xchg_ax_dx = 0x92,

        xchg_ax_si = 0x96,

        xchg_ax_bp = 0x95,

        xchg_ax_bx = 0x93,

        xchg_ax_cx = 0x91,

        xchg_ax_sp = 0x94,

        xlatb = 0xD7,

        xor = 0x33,

        xor_al = 0x34
    }
}