// ReSharper disable InconsistentNaming

namespace PoViEmu.Core.Machine.Ops
{
    public enum OpCode
    {
        Unknown = 0,
        
        aaa,
        aad,
        aam,
        aas,
        adc,
        add,
        and,
        arpl,
        bound,
        call,
        cbw,
        clc,
        cld,
        cli,
        cmc,
        cmp,
        cmpsb,
        cmpsw,
        cs,
        cwd,
        daa,
        das,
        dec,
        div,
        ds,
        enter,
        es,
        fadd,
        faddp,
        fcmovb,
        fcmovnbe,
        fcom,
        fcomi,
        fcomip,
        fdiv,
        fdivr,
        fdivrp,
        fnstenv,
        fsubr,
        ficom,
        fnop,
        fild,
        fist,
        fiadd,
        fimul,
        fisttp,
        fisub,
        fld,
        fldenv,
        fmul,
        fnsave,
        fnstcw,
        fnstsw,
        fs,
        fsubp,
        fst,
        fstp,
        fxch,
        gs,
        hlt,
        idiv,
        imul,
        @in,
        inc,
        insb,
        insw,
        @int,
        int1,
        int3,
        into,
        iret,
        ja,
        jc,
        jcxz,
        jg,
        jl,
        jmp,
        jna,
        jnc,
        jng,
        jnl,
        jno,
        jns,
        jnz,
        jo,
        jpe,
        jpo,
        js,
        jz,
        lahf,
        lds,
        lea,
        les,
        leave,
        @lock,
        lodsb,
        lodsw,
        loop,
        loope,
        loopne,
        mov,
        movsb,
        movsw,
        mul,
        neg,
        nop,
        not,
        o32,
        or,
        @out,
        outsb,
        outsw,
        pop,
        popa,
        popf,
        push,
        pusha,
        pushf,
        rcl,
        rcr,
        rep,
        repne,
        ret,
        retf,
        rol,
        ror,
        sahf,
        salc,
        sar,
        sbb,
        scasb,
        scasw,
        shl,
        ss,
        stc,
        std,
        sti,
        stosb,
        stosw,
        sub,
        test,
        wait,
        xchg,
        xlatb,
        xor,
        fchs,
        fabs,
        ftst,
        fxam,
        fld1,
        fldz,
        fldln2,
        fldlg2,
        fldpi,
        fldl2e,
        fldl2t,
        fpatan,
        fptan,
        fyl2x,
        f2xm1,
        fxtract,
        fprem1,
        fdecstp,
        fincstp,
        fprem,
        fyl2xp1,
        fsqrt,
        fsincos,
        frndint,
        fscale,
        fsin,
        fcos,
        fucompp,
        fneni,
        fndisi,
        fnclex,
        fninit,
        fsetpm,
        fcompp,
        fcomp,
        fsub,
        fldcw,
        frstor,
        fucomip,
        fcmove,
        fcmovbe,
        fcmovu,
        fcmovnb,
        fcmovne,
        fcmovnu,
        fmulp,
        fsubrp,
        fdivp,
        ffreep,
        fucomi,
        ffree,
        fucom,
        fucomp,
        shr,
        xabort
    }
}