// ReSharper disable InconsistentNaming

namespace PoViEmu.Core.Machine.Ops
{
    public enum OpCode : byte
    {
        Unknown = 0,

        aaa = 0x37,

        aad = 0xD5,

        aam = 0xD4,

        aas = 0x3F,

        add = 0x80,

        add_ax = 0x03,

        add_cl = 0x02,

        add_dh = 0x00,

        adc = 0x81,

        adc_al = 0x14,

        adc_ch = 0x10,

        adc_ah = 0x12,

        adc_sp = 0x11,

        and = 0x23,

        and_ah = 0x22,
        
        and_al = 0x24,

        and_dx = 0x21,

        and_dl = 0x20,

        arpl_bp = 0x63,

        cbw = 0x98,

        clc = 0xF8,

        cld = 0xFC,

        cli = 0xFA,

        cmc = 0xF5,

        cmp = 0x3B,

        cmp_ax = 0x3D,

        cmp_cx = 0x39,

        cmp_al = 0x3C,
        
        cmp_bl = 0x3A,

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

        fcomip = 0xDF,

        ficom = 0xDE,

        fmul = 0xD8,

        fnstenv = 0xD9,

        fs = 0x64,

        fstp = 0xDD,

        gs = 0x65,

        hlt = 0xF4,

        in_al = 0xE4,

        in_al_dx = 0xEC,

        in_ax_dx = 0xED,
        
        in_ax = 0xE5,

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

        @int = 0xCD,

        int1 = 0xF1,

        int3 = 0xCC,

        into = 0xCE,

        iret = 0xCF,

        jcxz = 0xE3,

        jc = 0x72,

        jg = 0x7F,

        jna = 0x76,
        
        jng = 0x7E,

        jmp_short = 0xEB,

        jmp_far = 0xFF,

        ja = 0x77,

        jl = 0x7C,

        jnl = 0x7D,

        jno = 0x71,

        jpo = 0x7B,

        jnz = 0x75,

        jo = 0x70,

        js = 0x78,

        jz = 0x74,

        lahf = 0x9F,

        leave = 0xC9,

        @lock = 0xF0,

        lodsb = 0xAC,

        lodsw = 0xAD,

        loop = 0xE2,

        loope = 0xE1,

        loopne = 0xE0,

        mov_ah = 0xB4,

        mov_bh = 0xB7,

        mov_al = 0x8A,

        mov_al_b = 0xB0,

        mov = 0x8B,

        mov_ax = 0x89,

        mov_bx = 0xBB,

        mov_bl = 0xB3,

        mov_si = 0xBE,

        mov_ch = 0xB5,

        mov_cx = 0xB9,

        mov_dh = 0xB6,

        mov_cl_x = 0x88,

        mov_cs = 0x8E,

        mov_dl = 0xB2,

        mov_dx = 0xBA,

        mov_ax_s = 0xB8,

        mov_cl = 0xB1,

        movsb = 0xA4,

        movsw = 0xA5,

        mul = 0xF7,

        nop = 0x90,

        o32 = 0x66,

        out_ax = 0xE7,

        out_dx_al = 0xEE,

        out_dx_ax = 0xEF,

        outsb = 0x6E,

        outsw = 0x6F,

        or = 0x0B,

        or_bh = 0x08,
        
        or_bx = 0x09,
        
        or_bl = 0x0a,

        or_al = 0x0C,

        out_al = 0xE6,

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

        push_cs = 0x0E,

        push_di = 0x57,

        push_ds = 0x1E,

        push_es = 0x06,

        push_ss = 0x16,

        push = 0x6A,

        pusha = 0x60,

        pushf = 0x9C,

        rcl = 0xD0,

        rep = 0xF3,

        repne = 0xF2,

        ret = 0xC3,

        retf = 0xCB,

        sahf = 0x9E,

        salc = 0xD6,

        sbb_al = 0x18,
        
        sbb_cl = 0x1A,
        
        sbb_bx = 0x19,

        scasb = 0xAE,

        scasw = 0xAF,

        shl = 0xD3,

        shl_one = 0xD1,

        shl_bp = 0xD2,

        ss = 0x36,

        stc = 0xF9,

        std = 0xFD,

        sti = 0xFB,

        stosb = 0xAA,

        stosw = 0xAB,

        sub_ax = 0x2B,

        sub_al = 0x2C,
        
        sub_ah = 0x2A,

        sub_dh = 0x28,

        test = 0x85,

        test_al = 0xA8,

        test_dl = 0x84,

        wait = 0x9B,

        xchg_dh_al = 0x86,

        xchg_ax_di = 0x97,

        xchg_ax_dx = 0x92,

        xchg_ax_si = 0x96,

        xchg_ax_bp = 0x95,

        xchg_ax_bx = 0x93,

        xchg_ax_cx = 0x91,

        xchg_ax_sp = 0x94,

        xchg_bp_si = 0x87,
        
        xlatb = 0xD7,

        xor = 0x33,

        xor_al = 0x34,

        xor_bp = 0x30,
        
        xor_ch = 0x32
    }
}