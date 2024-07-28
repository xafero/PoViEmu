// ReSharper disable InconsistentNaming

namespace PoViEmu.Core.Machine.Ops
{
    public enum OpBase : byte
    {
        Unknown = 0,

        aaa,

        aas,
        
        add,

        cbw,

        clc,

        cld,

        cli,

        cmc,

        cmpsb,

        cmpsw,

        cwd,

        daa,

        das,
        
        dec,

        hlt,
        
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

        nop,
        
        @out,
        
        outsb,
        
        outsw,
        
        pop,

        popa,
        
        popf,

        push,

        pusha,
        
        pushf,

        rep,

        repne,

        sahf,

        scasb,

        scasw,

        stosb,

        stosw,

        wait,
        
        xchg,
        
        xlatb
    }
}