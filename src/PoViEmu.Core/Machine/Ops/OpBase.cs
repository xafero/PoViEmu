// ReSharper disable InconsistentNaming

namespace PoViEmu.Core.Machine.Ops
{
    public enum OpBase : byte
    {
        Unknown = 0,

        aaa,

        aas,
        
        aam,
        
        adc,
        
        add,
        
        and,
        
        arpl,

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
        
        fadd,
        
        faddp,
        
        fsubp,
        
        fdiv,
        
        fdivr,
        
        fdivrp,
        
        fild,
        
        fcomi,
        
        fist,
        
        fld,
        
        fisttp,
        
        fnsave,
        
        fisub,
        
        fnstcw,
        
        fnstsw,
        
        fldenv,
        
        fxch,

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
        
        imul,

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
        
        rcr,

        rep,

        repne,
        
        rol,
        
        ror,

        sahf,
        
        sar,
        
        scasb,

        scasw,
        
        shl,

        stosb,

        stosw,
        
        sub,
        
        sbb,
        
        test,

        wait,
        
        xchg,
        
        xlatb,
        
        xor
    }
}