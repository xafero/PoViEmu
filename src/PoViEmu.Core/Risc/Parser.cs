using O = PoViEmu.Core.Risc.OpCodes;
using T = PoViEmu.Core.Risc.InstTool;

namespace PoViEmu.Core.Risc
{
    public sealed class Parser
    {
        public static object Parse(IReader reader)
        {
            var first = reader.ReadNextByte();
            byte second;
            byte imm;
            byte dis;
            ushort dsp;
            byte dst;
            byte src;
            switch (first)
            {
                case 0b00000000:
                    second = reader.ReadNextByte();
                    switch (second)
                    {
                        case 0b00001000:
                            return T.Create(first, second, O.CLRT);
                        case 0b00001001:
                            return T.Create(first, second, O.NOP);
                        case 0b00001011:
                            return T.Create(first, second, O.RTS);
                        case 0b00011000:
                            return T.Create(first, second, O.SETT);
                        case 0b00011001:
                            return T.Create(first, second, O.DIV0U);
                        case 0b00011011:
                            return T.Create(first, second, O.SLEEP);
                        case 0b00101000:
                            return T.Create(first, second, O.CLRMAC);
                        case 0b00101011:
                            return T.Create(first, second, O.RTE);
                        case 0b00111000:
                            return T.Create(first, second, O.LDTLB);
                        case 0b01001000:
                            return T.Create(first, second, O.CLRS);
                        case 0b01011000:
                            return T.Create(first, second, O.SETS);
                    }
                    break;
                case 0b10000000:
                    second = reader.ReadNextByte();
                    break;
                case 0b10000001:
                    second = reader.ReadNextByte();
                    break;
                case 0b10000010:
                    second = reader.ReadNextByte();
                    imm = second;
                    return T.Create(first, second, O.SETRC, i: imm);
                case 0b10000100:
                    second = reader.ReadNextByte();
                    break;
                case 0b10000101:
                    second = reader.ReadNextByte();
                    break;
                case 0b10001000:
                    second = reader.ReadNextByte();
                    imm = second;
                    return T.Create(first, second, O.CMP, i: imm);
                case 0b10001001:
                    second = reader.ReadNextByte();
                    dis = second;
                    return T.Create(first, second, O.BT, d: dis);
                case 0b10001011:
                    second = reader.ReadNextByte();
                    dis = second;
                    return T.Create(first, second, O.BF, d: dis);
                case 0b10001101:
                    second = reader.ReadNextByte();
                    dis = second;
                    return T.Create(first, second, O.BT, d: dis);
                case 0b10001111:
                    second = reader.ReadNextByte();
                    dis = second;
                    return T.Create(first, second, O.BF, d: dis);
                case 0b11000000:
                    second = reader.ReadNextByte();
                    dis = second;
                    return T.Create(first, second, O.MOV, d: dis);
                case 0b11000001:
                    second = reader.ReadNextByte();
                    dis = second;
                    return T.Create(first, second, O.MOV, d: dis);
                case 0b11000010:
                    second = reader.ReadNextByte();
                    dis = second;
                    return T.Create(first, second, O.MOV, d: dis);
                case 0b11000011:
                    second = reader.ReadNextByte();
                    imm = second;
                    return T.Create(first, second, O.TRAPA, i: imm);
                case 0b11000100:
                    second = reader.ReadNextByte();
                    dis = second;
                    return T.Create(first, second, O.MOV, d: dis);
                case 0b11000101:
                    second = reader.ReadNextByte();
                    dis = second;
                    return T.Create(first, second, O.MOV, d: dis);
                case 0b11000110:
                    second = reader.ReadNextByte();
                    dis = second;
                    return T.Create(first, second, O.MOV, d: dis);
                case 0b11000111:
                    second = reader.ReadNextByte();
                    dis = second;
                    return T.Create(first, second, O.MOVA, d: dis);
                case 0b11001000:
                    second = reader.ReadNextByte();
                    imm = second;
                    return T.Create(first, second, O.TST, i: imm);
                case 0b11001001:
                    second = reader.ReadNextByte();
                    imm = second;
                    return T.Create(first, second, O.AND, i: imm);
                case 0b11001010:
                    second = reader.ReadNextByte();
                    imm = second;
                    return T.Create(first, second, O.XOR, i: imm);
                case 0b11001011:
                    second = reader.ReadNextByte();
                    imm = second;
                    return T.Create(first, second, O.OR, i: imm);
                case 0b11001100:
                    second = reader.ReadNextByte();
                    imm = second;
                    return T.Create(first, second, O.TST, i: imm);
                case 0b11001101:
                    second = reader.ReadNextByte();
                    imm = second;
                    return T.Create(first, second, O.AND, i: imm);
                case 0b11001111:
                    second = reader.ReadNextByte();
                    imm = second;
                    return T.Create(first, second, O.OR, i: imm);
            }

            var (high, low) = T.SplitByte(first);
            switch (high)
            {
                case 0b0000:
                    break;
                case 0b0001:
                    break;
                case 0b0010:
                    break;
                case 0b0011:
                    break;
                case 0b0100:
                    break;
                case 0b0101:
                    break;
                case 0b0110:
                    break;
                case 0b0111:
                    second = reader.ReadNextByte();
                    dst = low;
                    imm = second;
                    return T.Create(first, second, O.ADD, n: dst, i: imm);
                case 0b1001:
                    second = reader.ReadNextByte();
                    dst = low;
                    dis = second;
                    return T.Create(first, second, O.MOV, n: dst, d: dis);
                case 0b1010:
                    second = reader.ReadNextByte();
                    dsp = T.CombineBytes(low, second);
                    return T.Create(first, second, O.BRA, d: dsp);
                case 0b1011:
                    second = reader.ReadNextByte();
                    dsp = T.CombineBytes(low, second);
                    return T.Create(first, second, O.BSR, d: dsp);
                case 0b1101:
                    second = reader.ReadNextByte();
                    dst = low;
                    dis = second;
                    return T.Create(first, second, O.MOV, n: dst, d: dis);
                case 0b1110:
                    second = reader.ReadNextByte();
                    dst = low;
                    imm = second;
                    return T.Create(first, second, O.MOV, n: dst, i: imm);
            }
            return "???";
        }
    }
}