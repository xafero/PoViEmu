// ReSharper disable InconsistentNaming

namespace PoViEmu.Core.Machine.Ops
{
    public enum OpBase : byte
    {
        Unknown = 0,

        aaa,

        aas,
        
        add,
        
        and,

        cbw,

        clc,

        cld,

        cli,

        cmc,
        
        cmp,

        cmpsb,

        cmpsw,

        cwd,

        daa,

        das,
        
        dec,

        hlt,
        
        idiv,
        
        @in,
        
        inc,

        insb,
        
        insw,
        
        iret,
        
        jmp,

        lahf,
        
        leave,

        @lock,

        mov,
        
        movsb,

        movsw,
        
        mul,

        nop,
        
        not,
        
        @out,
        
        outsb,
        
        outsw,
        
        pop,

        popa,
        
        popf,

        push,

        pusha,
        
        pushf,
        
        or,

        rep,

        repne,

        sahf,
        
        sar,
        
        scasb,

        scasw,
        
        shl,

        stosb,

        stosw,
        
        sub,
        
        test,

        wait,
        
        xchg,
        
        xlatb,
        
        xor
    }
}