namespace PoViEmu.Core.Risc
{
    public enum Mnemonic
    {
        INVALID = 0,

        Clrt,
        
        Nop,
        
        Rts,
        
        Sett,
        
        Div0u,
        
        Sleep,
        
        Clrmac,
        
        Rte,
        
        Ldtlb,
        
        Clrs,
        
        Sets,
        
        MovB99,
        
        xxx012,
        xxx013,
        
        MovB,
        
        MovW393,
        
        CmpEq,
        
        Bt,
        
        Bf,
        
        BtS,
        
        BfS,
        
        MovB9922,
        
        xxx022,
        xxx023,
        
        Trapa,
        
        MovB2,
        
        MovW23,
        
        xxx027,
        
        Mova,
        
        Tst,
        
        And,
        
        Xor,
        
        Or,
        
        TstB,
        
        AndB,
        
        OrB,
        
        Stc20292,
        
        Bsrf,
        
        Sts22,
        
        Stc,
        
        Sts23,
        
        Stc236737,
        
        Braf,
        
        Movt,
        
        Sts292,
        
        Stc1273,
        
        Stc28282,
        
        Stc2828,
        
        Stc23202,
        
        xxx049,
        
        Stc29202,
        
        xxx051,
        
        Stc2,
        
        Pref,
        
        xxx054,
        Stc3,
        
        xxx056,
        
        Stc898,
        
        xxx058,
        
        Stc833,
        
        xxx060,
        
        Stc383,
        
        Stc39393,
        
        Stc29829,
        
        Stc292092,
        
        xxx065,
        xxx066,
        xxx067,
        
        MulL,
        
        MovB34,
        
        xxx070,
        xxx071,
        
        MacL,
        
        MovL289384,
        
        xxx074,
        xxx075,
        xxx076,
        
        MovB929,
        
        xxx078,
        xxx079,
        
        Div0s,
        
        Tst2,
        
        Xor2,
        
        Or2,
        
        CmpStr,
        
        Xtrct,
        
        MuluW,
        
        MulsW,
        
        CmpHs,
        
        CmpGe,
        
        Div1,
        
        DmuluL,
        
        CmpHi,
        
        CmpGt,
        
        Sub,
        
        Subc,
        
        Subv,
        
        DmulsL,
        
        Addc,
        
        Addv,
        
        LdsL81,
        
        LdcL282,
        
        Lds,
        
        Ldc2249,
        
        xxx107,
        
        LdsL83,
        
        LdcL,
        
        Lds33,
        
        Ldc922,
        
        LdsL729,
        
        LdcL292311,
        
        Lds383,
        
        Ldc22342,
        
        LdcL2290,
        
        Ldc93932,
        
        LdcL293421,
        
        Ldc9223,
        
        LdcL99,
        
        Ldc9,
        
        xxx122,
        
        LdcL294,
        
        xxx124,
        
        Ldc12,
        
        xxx126,
        
        LdcL74,
        
        xxx128,
        
        Ldc2389,
        
        LdcL2,
        
        xxx131,
        
        Ldc2,
        
        LdcL3,
        
        xxx134,
        
        Ldc,
        
        LdcL4,
        
        xxx137,
        
        Ldc3,
        
        LdcL54,
        
        xxx140,
        
        Ldc4,
        
        LdcL92,
        
        Ldc5,
        
        LdcL933,
        
        Ldc99,
        
        LdcL2234,
        
        Ldc83,
        
        LdcL2394,
        
        Ldc92,
        
        Shll,
        
        Shlr,
        
        StsL233,
        
        StcL18129,
        
        Rotl,
        
        Rotr,
        
        Shll2,
        
        Shlr2,
        
        Jsr,
        
        Dt,
        
        CmpPz,
        
        StsL2722111,
        
        StcL4,
        
        CmpPl,
        
        Shll8,
        
        Shlr8,
        
        TasB,
        
        Shal,
        
        Shar,
        
        StsL282821119,
        
        StcL171712,
        
        Rotcl,
        
        Rotcr,
        
        Shll16,
        
        Shlr16,
        
        Jmp,
        
        StcL1171,
        
        StcL15129,
        
        StcL22611112,
        
        xxx179,
        
        StcL211001,
        
        StcL1111,
        
        xxx182,
        
        StcL3893,
        
        xxx184,
        xxx185,
        
        StcL38332,
        
        xxx187,
        xxx188,
        
        StcL3832111,
        
        xxx190,
        xxx191,
        
        StcL262722,
        
        xxx193,
        
        StcL2822,
        
        StcL28282,
        
        StcL11029,
        
        StcL221138,
        
        Shad,
        
        Shld,
        
        MacW,
        
        MovL393,
        
        MovB383,
        
        MovW37784,
        
        xxx204,
        
        Mov2,
        
        MovB98933,
        
        MovW3828,
        
        xxx208,

        Not,
        
        SwapB,
        
        SwapW,
        
        Negc,
        
        Neg,
        
        ExtuB,
        
        ExtuW,
        
        ExtsB,
        
        ExtsW,

        Add,

        MovW349,
        
        Bra,
        
        Bsr,
        
        MovL28283,
        
        Mov,
        
        LdsL,
        
        MovL,
        MovW,
        StcL,
        Sts,
        StsL
    }
}