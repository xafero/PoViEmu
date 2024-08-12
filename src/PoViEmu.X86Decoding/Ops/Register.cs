namespace PoViEmu.X86Decoding.Ops
{
    public enum Register
    {
        Unknown = 0,

        AX,
        BX,
        CX,
        DX,

        BP,
        SI,
        DI,
        SP,

        CS,
        DS,
        ES,
        FS,
        GS,
        SS,

        AL,
        BL,
        CL,
        DL,

        AH,
        BH,
        CH,
        DH,

        St0,
        St1,
        St2,
        St3,
        St4,
        St5,
        St6,
        St7,

        Segr0,
        Segr1,
        Segr2,
        Segr3,
        Segr4,
        Segr5,
        Segr6,
        Segr7
    }
}