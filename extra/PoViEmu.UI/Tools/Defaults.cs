﻿using MachineStateSH3 = PoViEmu.SH3.CPU.MachineState;
using MachineStateI86 = PoViEmu.I186.CPU.MachineState;

namespace PoViEmu.UI.Tools
{
    internal static class Defaults
    {
        static Defaults()
        {
            var stateI86 = new MachineStateI86
            {
                Bk0 = 0x10, Bk1 = 0x11, Bk2 = 0x12, Bk3 = 0x13,
                Bk4 = 0x14, Bk5 = 0x15, Bk6 = 0x16, Fr0 = 0x20,
                Fr1 = 0x21, Fr2 = 0x22, Fr3 = 0x23, Fr4 = 0x24,
                Fr5 = 0x25, Fr6 = 0x26, Fr7 = 0x27, Fr8 = 0x28,
                Fr9 = 0x29, Fr10 = 0x30, Fr11 = 0x31, AX = 0x3201,
                BX = 0x3302, CX = 0x3403, DX = 0x3504,
                CF = true, OF = true, DF = true, SF = true,
                AF = true, ES = 0x36, DS = 0x3237, SP = 0x38,
                BP = 0x39, CS = 0x40, DI = 0x41, SI = 0x42,
                IF = true, IP = 0x43, SS = 0x44, TF = true,
                PF = true, ZF = true
            };
            var bI = GetBytes();
            stateI86.WriteMemory(stateI86.DS, bI.offset, bI.bytes);
            stateI86.WriteMemory(stateI86.CS, stateI86.IP, GetX86Com());
            StateI86 = stateI86;

            var stateSh3 = new MachineStateSH3
            {
                I0 = true, I1 = true, I2 = true, I3 = true, M = true,
                Q = true, T = true, R0 = 0x10, R1 = 0x11, R2 = 0x12,
                R3 = 0x13, R4 = 0x14, R5 = 0x15, R6 = 0x16, R7 = 0x17,
                R8 = 0x18, R9 = 0x19, R10 = 0x20, R11 = 0x21, R12 = 0x22,
                R13 = 0x23, R14 = 0x24, R15 = 0x25, R0_b = 0x30, R1_b = 0x31,
                R2_b = 0x32, R3_b = 0x33, R4_b = 0x34, R5_b = 0x35, R6_b = 0x36,
                R7_b = 0x37, S = true, MD = true, PC = 0x38, PR = 0x39,
                BL = true, SSR = 0x40, dPC = 0x41, GBR = 0x42, RB = true,
                SPC = 0x43, VBR = 0x44, MACH = 0x45, MACL = 0x46
            };
            var bS = GetBytes();
            stateSh3.WriteMemory(bS.offset, bS.bytes);
            stateSh3.WriteMemory(stateSh3.PC, GetSh3Com());
            StateSh3 = stateSh3;
        }

        public static MachineStateSH3 StateSh3 { get; }

        public static MachineStateI86 StateI86 { get; }

        private static (ushort offset, byte[] bytes) GetBytes()
        {
            ushort offset = 0xD000;
            byte[] allBytes =
            [
                0x00, 0xFF, 0x43, 0x41, 0x53, 0x49, 0x4F, 0x03, 0x5A, 0x34, 0x38, 0x36, 0x30, 0x31, 0x30, 0x30, 0x01,
                0x01, 0xFF, 0x08, 0x61, 0x73, 0x73, 0x74, 0x65, 0x73, 0x74, 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0x54, 0x09, 0x00, 0x00, 0x32, 0x30, 0x32, 0x34, 0x30, 0x38, 0x31, 0x33, 0x32, 0x31, 0x31,
                0x31, 0x30, 0x31, 0x30, 0x31, 0x32, 0x30, 0x30, 0x30, 0x30, 0x32, 0x31, 0x35, 0x30, 0x39, 0x34, 0x30,
                0x30, 0x31, 0x30, 0x30, 0x50, 0x08, 0x00, 0x00, 0x00, 0x09, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xB8, 0x00, 0x19, 0x8E, 0xC0, 0xB8, 0x16, 0x80, 0x8E, 0xD8, 0x33, 0xF6, 0xBF, 0x00, 0x00, 0xB9,
                0xA4, 0x04, 0xFC, 0xF3, 0xA4, 0x06, 0x1F, 0xBF, 0xA4, 0x04, 0xB9, 0xA4, 0x04, 0x2B, 0xCF, 0x32, 0xC0,
                0xF3, 0xAA, 0xBB, 0x00, 0x08, 0xB8, 0x00, 0x08, 0x2B, 0xC3, 0x76, 0x1C, 0x8B, 0xC8, 0xC1, 0xE1, 0x03,
                0x3D, 0x00, 0x10, 0x76, 0x06, 0xB8, 0x00, 0x10, 0xB9, 0x00, 0x80, 0x8E, 0xC3, 0x03, 0xD8, 0x33, 0xFF,
                0x33, 0xC0, 0xF3, 0xAB, 0xEB, 0xDD, 0xC7, 0x06, 0x00, 0x00, 0x00, 0x00, 0xEA, 0x04, 0x00, 0x60, 0x80,
                0xCB, 0xCB, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x00, 0x00, 0x00, 0x00, 0x32,
                0x13, 0x3F, 0x13, 0x55, 0x01, 0x00, 0x80, 0x00, 0x00, 0x00, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
                0x20, 0x20, 0x28, 0x28, 0x28, 0x28, 0x28, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
                0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x88, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90,
                0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0xC4, 0xC4, 0xC4, 0xC4, 0xC4, 0xC4, 0xC4, 0xC4, 0xC4, 0xC4,
                0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0xC1, 0xC1, 0xC1, 0xC1, 0xC1, 0xC1, 0x81, 0x81, 0x81, 0x81,
                0x81, 0x81, 0x81, 0x81, 0x81, 0x81, 0x81, 0x81, 0x81, 0x81, 0x81, 0x81, 0x81, 0x81, 0x81, 0x81, 0x90,
                0x90, 0x90, 0x90, 0x90, 0x90, 0xC2, 0xC2, 0xC2, 0xC2, 0xC2, 0xC2, 0x82, 0x82, 0x82, 0x82, 0x82, 0x82,
                0x82, 0x82, 0x82, 0x82, 0x82, 0x82, 0x82, 0x82, 0x82, 0x82, 0x82, 0x82, 0x82, 0x82, 0x90, 0x90, 0x90,
                0x90, 0x20, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x31, 0x39, 0x30,
                0x30, 0x31, 0x32, 0x33, 0x31, 0x32, 0x31, 0x30, 0x30, 0x30, 0x31, 0x30, 0x31, 0x55, 0x8B, 0xEC, 0x83,
                0xEC, 0x22, 0x51, 0x52, 0xC7, 0x46, 0xFE, 0xD5, 0x7E, 0xC7, 0x46, 0xFC, 0xD4, 0x7E, 0xC7, 0x46, 0xFA,
                0xD3, 0x7E, 0x8B, 0x46, 0xFE, 0x03, 0x46, 0xFC, 0x89, 0x46, 0xF8, 0x8B, 0x46, 0xFE, 0x2B, 0x46, 0xFC,
                0x89, 0x46, 0xF8, 0x8B, 0x46, 0xFE, 0xF7, 0x66, 0xFC, 0x89, 0x46, 0xF8, 0x8B, 0x46, 0xFE, 0x99, 0xF7,
                0x7E, 0xFC, 0x89, 0x46, 0xF8, 0xFF, 0x4E, 0xF8, 0x8B, 0x46, 0xFE, 0x99, 0xF7, 0x7E, 0xFC, 0x89, 0x56,
                0xF8, 0xFF, 0x46, 0xF8, 0x8B, 0x46, 0xFE, 0x3B, 0x46, 0xF8, 0x75, 0x05, 0xB8, 0x01, 0x00, 0xEB, 0x02,
                0x33, 0xC0, 0x89, 0x46, 0xF6, 0x8B, 0x46, 0xFE, 0x3B, 0x46, 0xF8, 0x74, 0x05, 0xB8, 0x01, 0x00, 0xEB,
                0x02, 0x33, 0xC0, 0x89, 0x46, 0xF6, 0x8B, 0x46, 0xF8, 0x3B, 0x46, 0xFE, 0x7D, 0x05, 0xB8, 0x01, 0x00,
                0xEB, 0x02, 0x33, 0xC0, 0x89, 0x46, 0xF6, 0x8B, 0x46, 0xFE, 0x3B, 0x46, 0xF8, 0x7D, 0x05, 0xB8, 0x01,
                0x00, 0xEB, 0x02, 0x33, 0xC0, 0x89, 0x46, 0xF6, 0x8B, 0x46, 0xFE, 0x3B, 0x46, 0xF8
            ];
            return (offset, allBytes);
        }

        private static byte[] GetSh3Com() =>
        [
             0x2F, 0xE6, 0xE3, 0x4A, 0x2F, 0xD6, 0xE2, 0x12, 0x2F, 0xC6, 0x2F, 0xB6, 0x4F, 0x22, 0x4F, 0x12, 0x7F, 0xE8, 0x61, 0xF3, 0x2F, 0x32, 0x71, 0x08, 0x1F, 0x21, 0xD2, 0x4D, 0xD3, 0x4D, 0x43, 0x0B, 0xE0, 0x10, 0x53, 0xF1, 0x64, 0xF2, 0x34, 0x38, 0xD3, 0x4B, 0x43, 0x0B, 0x00, 0x09, 0xE2, 0x1A, 0x2F, 0x22, 0xE3, 0x1D, 0x1F, 0x31, 0x53, 0xF1, 0x64, 0xF2, 0x34, 0x3C, 0xD3, 0x46, 0x43, 0x0B, 0x00, 0x09, 0xE2, 0x1B, 0x2F, 0x22, 0xEC, 0x02, 0x1F, 0xC1, 0x64, 0xF2, 0x53, 0xF1, 0x24, 0x3F, 0xD3, 0x41, 0x43, 0x0B, 0x04, 0x1A, 0xE2, 0x34, 0xD3, 0x3F, 0x2F, 0x22, 0x64, 0xF2, 0x74, 0x01, 0x43, 0x0B, 0x2F, 0x42, 0x64, 0xF2, 0x74, 0xFF, 0xD3, 0x3B, 0x43, 0x0B, 0x2F, 0x42, 0xE2, 0x38, 0x2F, 0x22, 0xE3, 0x39, 0x1F, 0x31, 0x53, 0xF1, 0x64, 0xF2, 0x24, 0x39, 0xD3, 0x36, 0x43, 0x0B, 0x00, 0x09, 0x92, 0x61, 0xD3, 0x34, 0x2F, 0x22, 0x64, 0xF2, 0x43, 0x0B, 0x64, 0x47, 0xE2, 0x3C, 0x2F, 0x22, 0xE3, 0x1B, 0xD2, 0x31, 0x1F, 0x31, 0x61, 0xF2, 0x42, 0x0B, 0x50, 0xF1, 0x64, 0x03, 0xD3, 0x2D, 0x43, 0x0B, 0x74, 0x30, 0xE2, 0x75, 0xD3, 0x2D, 0xEB, 0x03, 0x2F, 0x22, 0x1F, 0xB1, 0x61, 0xF2, 0x43, 0x0B, 0x50, 0xF1, 0x64, 0x03, 0xD3, 0x27, 0x43, 0x0B, 0x74, 0x10, 0xE2, 0x07, 0x2F, 0x22, 0x1F, 0xB1, 0x53, 0xF1, 0x64, 0xF2, 0x44, 0x3C, 0xD3, 0x23, 0x43, 0x0B, 0x00, 0x09, 0x92, 0x3B, 0x2F, 0x22, 0x1F, 0xC1, 0x53, 0xF1, 0x64, 0xF2, 0x63, 0x3B, 0x44, 0x3C, 0xD3, 0x1E, 0x43, 0x0B, 0x00, 0x09, 0x9E, 0x32, 0xED, 0x3F, 0x2F, 0xE2, 0x1F, 0xD1, 0x53, 0xF1, 0x62, 0xF2, 0x32, 0x32, 0x89, 0x01, 0xA0, 0x01, 0xE4, 0x59, 0xE4, 0x4E, 0xD2, 0x17, 0x42, 0x0B, 0x00, 0x09, 0x53, 0xF1, 0x62, 0xF2, 0x32, 0x36, 0x8B, 0x01, 0xA0, 0x01, 0xE4, 0x59, 0xE4, 0x4E, 0xD2, 0x12, 0x42, 0x0B, 0x00, 0x09, 0x53, 0xF1, 0x62, 0xF2, 0x32, 0x36, 0x89, 0x01, 0xA0, 0x01, 0xE4, 0x59, 0xE4, 0x4E, 0xD2, 0x0D, 0x42, 0x0B, 0x00, 0x09, 0x53, 0xF1, 0x62, 0xF2, 0x32, 0x32, 0x8B, 0x01, 0xA0, 0x01, 0xE4, 0x59, 0xE4, 0x4E, 0xD2, 0x08, 0x42, 0x0B, 0x00, 0x09, 0x53, 0xF1, 0x62, 0xF2, 0x32, 0x30, 0x8B, 0x0F, 0xA0, 0x0F, 0xE4, 0x59, 0x00, 0xAD, 0x00, 0xD4, 0x00, 0xAB, 0x00, 0x00, 0x00, 0x00, 0x08, 0x20, 0x00, 0x00, 0x07, 0x7C, 0x00, 0x00, 0x04, 0x18, 0x00, 0x00, 0x06, 0xC4, 0x00, 0x00, 0x05, 0x54, 0xE4, 0x4E, 0xD2, 0x3C, 0x42, 0x0B, 0x00, 0x09, 0x53, 0xF1, 0x62, 0xF2, 0x32, 0x30, 0x89, 0x01, 0xA0, 0x01, 0xE4, 0x59, 0xE4, 0x4E, 0xD2, 0x37, 0x42, 0x0B, 0x00, 0x09, 0x2F, 0xE2, 0x1F, 0xD1, 0x53, 0xF1, 0x64, 0xF2, 0x92, 0x61, 0x24, 0x3A, 0xD3, 0x32, 0x43, 0x0B, 0x34, 0x2C, 0x2F, 0xE2, 0x1F, 0xD1, 0x53, 0xF1, 0x64, 0xF2, 0x24, 0x3B, 0x92, 0x58, 0xD3, 0x2E, 0x43, 0x0B, 0x34, 0x2C, 0x62, 0xF2, 0xEE, 0x0A, 0x32, 0xE3, 0x8B, 0x02, 0x53, 0xF1, 0xA0, 0x01, 0xE4, 0x59, 0xE4, 0x4E, 0xD3, 0x28, 0x43, 0x0B, 0x00, 0x09, 0x92, 0x4A, 0x63, 0xF2, 0x33, 0x27, 0x89, 0x03, 0xE1, 0x0E, 0x53, 0xF1, 0x33, 0x12, 0x89, 0x01, 0xA0, 0x01, 0xE4, 0x59, 0xE4, 0x4E, 0xD2, 0x21, 0x42, 0x0B, 0x00, 0x09, 0x53, 0xF1, 0x62, 0xF2, 0x32, 0x36, 0x89, 0x01, 0xA0, 0x01, 0xE4, 0x59, 0xE4, 0x4E, 0xD2, 0x1C, 0x42, 0x0B, 0x00, 0x09, 0x53, 0xF1, 0x62, 0xF2, 0x32, 0x32, 0x8B, 0x01, 0xA0, 0x01, 0xE4, 0x31, 0xE4, 0x32, 0xD3, 0x17, 0x43, 0x0B, 0x00, 0x09, 0x92, 0x29, 0x2F, 0x22, 0x61, 0x23, 0x60, 0xF2, 0x30, 0x10, 0x8B, 0x01, 0xA0, 0x01, 0xE4, 0x53, 0xE4, 0x46, 0xD3, 0x11, 0x43, 0x0B, 0x00, 0x09, 0xA0, 0x06, 0x2F, 0xB2, 0x64, 0xF2, 0xD3, 0x0E, 0x74, 0xFF, 0x2F, 0x42, 0x43, 0x0B, 0x74, 0x31, 0x62, 0xF2, 0x42, 0x15, 0x89, 0xF6, 0xE1, 0x04, 0xED, 0x0B, 0x2F, 0x12, 0x64, 0xF2, 0xD3, 0x08, 0x74, 0x02, 0x2F, 0x42, 0x43, 0x0B, 0x74, 0x41, 0x62, 0xF2, 0x32, 0xD3, 0x8B, 0xF6, 0x91, 0x07, 0xED, 0x00, 0x2F, 0x12, 0xA0, 0x17, 0x1F, 0xD1, 0xFD, 0xB2, 0xFD, 0xAF, 0x16, 0x90, 0x00, 0x91, 0x12, 0x34, 0x00, 0x00, 0x04, 0x18, 0x61, 0xF2, 0xD2, 0x24, 0x42, 0x0B, 0x60, 0xE3, 0x53, 0xF1, 0x62, 0x33, 0x43, 0x08, 0x33, 0x2C, 0x43, 0x00, 0x30, 0x3C, 0x1F, 0x01, 0x60, 0xE3, 0xD3, 0x1F, 0x43, 0x0B, 0x61, 0xF2, 0x2F, 0x02, 0x61, 0xF2, 0x21, 0x18, 0x8B, 0xEC, 0x54, 0xF1, 0xD3, 0x1C, 0x44, 0x19, 0x43, 0x0B, 0x74, 0x30, 0xE2, 0x04, 0xEE, 0x08, 0x2F, 0x22, 0xA0, 0x07, 0x2F, 0xD2, 0x64, 0xF2, 0xD3, 0x17, 0x43, 0x0B, 0x74, 0x30, 0x62, 0xF2, 0x72, 0x03, 0x2F, 0x22, 0x63, 0xF2, 0x33, 0xE3, 0x8B, 0xF5, 0xE4, 0x40, 0x1F, 0xD1, 0x1F, 0xE1, 0x53, 0xF1, 0x34, 0x38, 0xD3, 0x10, 0x43, 0x0B, 0x00, 0x09, 0xE4, 0x45, 0x1F, 0xD1, 0x1F, 0xC1, 0x53, 0xF1, 0x34, 0x38, 0xD3, 0x0C, 0x43, 0x0B, 0x00, 0x09, 0xE2, 0x33, 0x1F, 0x23, 0xE4, 0x46, 0x53, 0xF1, 0x34, 0x38, 0xD3, 0x08, 0x43, 0x0B, 0x00, 0x09, 0xB0, 0x0F, 0x00, 0x09, 0x7F, 0x18, 0x4F, 0x16, 0x4F, 0x26, 0x6B, 0xF6, 0x6C, 0xF6, 0x6D, 0xF6, 0x00, 0x0B, 0x6E, 0xF6, 0x00, 0x00, 0x00, 0x00, 0x05, 0xFC, 0x00, 0x00, 0x04, 0xA0, 0x00, 0x00, 0x04, 0x18, 0xE3, 0x4C, 0xE4, 0x00, 0xC3, 0x21, 0x00, 0x09, 0x00, 0x0B, 0x00, 0x09, 0xE3, 0x02, 0xE4, 0x0D, 0xC3, 0x21, 0xE4, 0x0A, 0xC3, 0x21, 0x00, 0x09, 0x00, 0x0B, 0x00, 0x09, 0x7F, 0xFC, 0x2F, 0x41, 0x64, 0xF1, 0xE3, 0x02, 0xC3, 0x21, 0x00, 0x09, 0x00, 0x0B, 0x7F, 0x04, 0x00, 0x0B, 0x00, 0x09, 0x4F, 0x22, 0xB0, 0x18, 0x00, 0x09, 0xB0, 0x1A, 0x00, 0x09, 0xA0, 0x1A, 0x4F, 0x26, 0x2F, 0xE6, 0xD5, 0x12, 0x6E, 0x52, 0xD0, 0x12, 0x63, 0xE3, 0xD1, 0x12, 0x33, 0x4C, 0x62, 0x02, 0x31, 0x2C, 0x33, 0x16, 0x8B, 0x02, 0xE0, 0xFF, 0x00, 0x0B, 0x6E, 0xF6, 0x60, 0xE3, 0x62, 0x52, 0x32, 0x4C, 0x25, 0x22, 0x00, 0x0B, 0x6E, 0xF6, 0xD3, 0x0B, 0xD2, 0x08, 0x00, 0x0B, 0x22, 0x32, 0x00, 0x0B, 0x00, 0x09, 0xE1, 0x00, 0xD0, 0x08, 0x4F, 0x22, 0x20, 0x12, 0xD3, 0x08, 0x43, 0x0B, 0xE4, 0x01, 0xD3, 0x07, 0xE2, 0x00, 0x4F, 0x26, 0x00, 0x0B, 0x23, 0x22, 0x00, 0x00, 0x00, 0x00, 0x08, 0x30, 0x00, 0x00, 0x00, 0x54, 0x00, 0x00, 0x08, 0x38, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x20, 0x08, 0x2F, 0x26, 0x89, 0x4B, 0x2F, 0x36, 0xE2, 0x00, 0x21, 0x27, 0x33, 0x3A, 0x31, 0x2A, 0x23, 0x07, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x31, 0x2E, 0x60, 0x13, 0x63, 0xF6, 0x00, 0x0B, 0x62, 0xF6, 0xD1, 0x03, 0xD2, 0x03, 0xE0, 0x00, 0x21, 0x22, 0x00, 0x0B, 0x62, 0xF6, 0x00, 0x09, 0x00, 0x00, 0x08, 0x34, 0x00, 0x00, 0x04, 0x4E, 0x20, 0x08, 0x2F, 0x26, 0x89, 0x45, 0xE2, 0x00, 0x00, 0x19, 0x41, 0x24, 0x32, 0x04, 0x41, 0x24, 0x32, 0x04, 0x41, 0x24, 0x32, 0x04, 0x41, 0x24, 0x32, 0x04, 0x41, 0x24, 0x32, 0x04, 0x41, 0x24, 0x32, 0x04, 0x41, 0x24, 0x32, 0x04, 0x41, 0x24, 0x32, 0x04, 0x41, 0x24, 0x32, 0x04, 0x41, 0x24, 0x32, 0x04, 0x41, 0x24, 0x32, 0x04, 0x41, 0x24, 0x32, 0x04, 0x41, 0x24, 0x32, 0x04, 0x41, 0x24, 0x32, 0x04, 0x41, 0x24, 0x32, 0x04, 0x41, 0x24, 0x32, 0x04, 0x41, 0x24, 0x32, 0x04, 0x41, 0x24, 0x32, 0x04, 0x41, 0x24, 0x32, 0x04, 0x41, 0x24, 0x32, 0x04, 0x41, 0x24, 0x32, 0x04, 0x41, 0x24, 0x32, 0x04, 0x41, 0x24, 0x32, 0x04, 0x41, 0x24, 0x32, 0x04, 0x41, 0x24, 0x32, 0x04, 0x41, 0x24, 0x32, 0x04, 0x41, 0x24, 0x32, 0x04, 0x41, 0x24, 0x32, 0x04, 0x41, 0x24, 0x32, 0x04, 0x41, 0x24, 0x32, 0x04, 0x41, 0x24, 0x32, 0x04, 0x41, 0x24, 0x32, 0x04, 0x41, 0x24, 0x60, 0x13, 0x00, 0x0B, 0x62, 0xF6, 0xD2, 0x03, 0xD1, 0x03, 0xE0, 0x00, 0x22, 0x12, 0x00, 0x0B, 0x62, 0xF6, 0x00, 0x09, 0x00, 0x00, 0x08, 0x34, 0x00, 0x00, 0x04, 0x4E, 0x20, 0x08, 0x2F, 0x26, 0x89, 0x55, 0x2F, 0x36, 0xE2, 0x00, 0x2F, 0x46, 0x21, 0x27, 0x04, 0x29, 0x33, 0x3A, 0x31, 0x2A, 0x23, 0x07, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x41, 0x24, 0x33, 0x04, 0x23, 0x27, 0x02, 0x29, 0x22, 0x4A, 0x42, 0x25, 0x8B, 0x02, 0x23, 0x07, 0x43, 0x21, 0x33, 0x04, 0x33, 0x4C, 0x60, 0x33, 0x64, 0xF6, 0x63, 0xF6, 0x00, 0x0B, 0x62, 0xF6, 0xD1, 0x03, 0xD2, 0x03, 0xE0, 0x00, 0x21, 0x22, 0x00, 0x0B, 0x62, 0xF6, 0x00, 0x09, 0x00, 0x00, 0x08, 0x34, 0x00, 0x00, 0x04, 0x4E, 0x20, 0x08, 0x89, 0x4D, 0x2F, 0x36, 0xE3, 0x00, 0x2F, 0x46, 0x64, 0x03, 0x00, 0x19, 0x41, 0x24, 0x33, 0x44, 0x41, 0x24, 0x33, 0x44, 0x41, 0x24, 0x33, 0x44, 0x41, 0x24, 0x33, 0x44, 0x41, 0x24, 0x33, 0x44, 0x41, 0x24, 0x33, 0x44, 0x41, 0x24, 0x33, 0x44, 0x41, 0x24, 0x33, 0x44, 0x41, 0x24, 0x33, 0x44, 0x41, 0x24, 0x33, 0x44, 0x41, 0x24, 0x33, 0x44, 0x41, 0x24, 0x33, 0x44, 0x41, 0x24, 0x33, 0x44, 0x41, 0x24, 0x33, 0x44, 0x41, 0x24, 0x33, 0x44, 0x41, 0x24, 0x33, 0x44, 0x41, 0x24, 0x33, 0x44, 0x41, 0x24, 0x33, 0x44, 0x41, 0x24, 0x33, 0x44, 0x41, 0x24, 0x33, 0x44, 0x41, 0x24, 0x33, 0x44, 0x41, 0x24, 0x33, 0x44, 0x41, 0x24, 0x33, 0x44, 0x41, 0x24, 0x33, 0x44, 0x41, 0x24, 0x33, 0x44, 0x41, 0x24, 0x33, 0x44, 0x41, 0x24, 0x33, 0x44, 0x41, 0x24, 0x33, 0x44, 0x41, 0x24, 0x33, 0x44, 0x41, 0x24, 0x33, 0x44, 0x41, 0x24, 0x33, 0x44, 0x41, 0x24, 0x33, 0x44, 0x8B, 0x03, 0x60, 0x33, 0x64, 0xF6, 0x00, 0x0B, 0x63, 0xF6, 0x30, 0x3C, 0x64, 0xF6, 0x00, 0x0B, 0x63, 0xF6, 0x2F, 0x26, 0xD1, 0x03, 0xD2, 0x03, 0xE0, 0x00, 0x21, 0x22, 0x00, 0x0B, 0x62, 0xF6, 0x00, 0x09, 0x00, 0x00, 0x08, 0x34, 0x00, 0x00, 0x04, 0x4E, 0x2F, 0x36, 0xD3, 0x05, 0x03, 0x3E, 0x70, 0xFC, 0x43, 0x2B, 0x00, 0x2E, 0x2F, 0x36, 0xD3, 0x02, 0x03, 0x3E, 0x70, 0xFC, 0x43, 0x2B, 0x03, 0x2E, 0x00, 0x00, 0x07, 0xDC, 0x00, 0x09, 0x53, 0x2E, 0x11, 0x0F, 0x50, 0x2D, 0x11, 0x3E, 0x53, 0x2C, 0x11, 0x0D, 0x50, 0x2B, 0x11, 0x3C, 0x53, 0x2A, 0x11, 0x0B, 0x50, 0x29, 0x11, 0x3A, 0x53, 0x28, 0x11, 0x09, 0x50, 0x27, 0x11, 0x38, 0x53, 0x26, 0x11, 0x07, 0x50, 0x25, 0x11, 0x36, 0x53, 0x24, 0x11, 0x05, 0x50, 0x23, 0x11, 0x34, 0x53, 0x22, 0x11, 0x03, 0x50, 0x21, 0x11, 0x32, 0x63, 0x22, 0x11, 0x01, 0x21, 0x32, 0x00, 0x0B, 0x63, 0xF6, 0x00, 0x00, 0x07, 0xD8, 0x00, 0x00, 0x07, 0xD6, 0x00, 0x00, 0x07, 0xD2, 0x00, 0x00, 0x07, 0xCE, 0x00, 0x00, 0x07, 0xCA, 0x00, 0x00, 0x07, 0xC6, 0x00, 0x00, 0x07, 0xC2, 0x00, 0x00, 0x07, 0xBE, 0x00, 0x00, 0x07, 0xBA, 0x00, 0x00, 0x07, 0xB6, 0x00, 0x00, 0x07, 0xB2, 0x00, 0x00, 0x07, 0xAE, 0x00, 0x00, 0x07, 0xAA, 0x00, 0x00, 0x07, 0xA6, 0x00, 0x00, 0x07, 0xA2, 0x00, 0x00, 0x07, 0x9E, 0x00, 0x00, 0x07, 0x9A, 0x00, 0x00, 0x00, 0x2B, 0x00, 0x00, 0x00, 0x23, 0x00, 0x00, 0x00, 0x4B, 0x00, 0x00, 0x00, 0x64
        ];

        private static byte[] GetX86Com() =>
        [
            0x55, 0x8B, 0xEC, 0x83, 0xEC, 0x0C, 0x51, 0x52, 0x56, 0x57, 0xC7, 0x46, 0xFE, 0x4A, 0x00, 0xC7, 0x46, 0xFC, 0x12, 0x00, 0x8D, 0x7E, 0xF4, 0xBE, 0x00, 0x00, 0xB9, 0x08, 0x00, 0x1E, 0x07, 0xFC, 0xF3, 0xA4, 0x8B, 0x46, 0xFE, 0x2B, 0x46, 0xFC, 0xE8, 0xAD, 0x02, 0xC7, 0x46, 0xFE, 0x1A, 0x00, 0xC7, 0x46, 0xFC, 0x1D, 0x00, 0x8B, 0x46, 0xFE, 0x03, 0x46, 0xFC, 0xE8, 0x9A, 0x02, 0xC7, 0x46, 0xFE, 0x1B, 0x00, 0xC7, 0x46, 0xFC, 0x02, 0x00, 0x8B, 0x46, 0xFE, 0xF7, 0x66, 0xFC, 0xE8, 0x87, 0x02, 0xC7, 0x46, 0xFE, 0x34, 0x00, 0xFF, 0x46, 0xFE, 0x8B, 0x46, 0xFE, 0xE8, 0x79, 0x02, 0xFF, 0x4E, 0xFE, 0x8B, 0x46, 0xFE, 0xE8, 0x70, 0x02, 0xC7, 0x46, 0xFE, 0x38, 0x00, 0xC7, 0x46, 0xFC, 0x39, 0x00, 0x8B, 0x46, 0xFE, 0x23, 0x46, 0xFC, 0xE8, 0x5D, 0x02, 0xC7, 0x46, 0xFE, 0xAD, 0x00, 0x8B, 0x46, 0xFE, 0xF7, 0xD0, 0xE8, 0x50, 0x02, 0xC7, 0x46, 0xFE, 0x3C, 0x00, 0xC7, 0x46, 0xFC, 0x1B, 0x00, 0x8B, 0x46, 0xFE, 0x33, 0xD2, 0xF7, 0x76, 0xFC, 0x8B, 0xC2, 0x05, 0x30, 0x00, 0xE8, 0x36, 0x02, 0xC7, 0x46, 0xFE, 0x75, 0x00, 0xC7, 0x46, 0xFC, 0x03, 0x00, 0x8B, 0x46, 0xFE, 0x33, 0xD2, 0xF7, 0x76, 0xFC, 0x05, 0x10, 0x00, 0xE8, 0x1E, 0x02, 0xC7, 0x46, 0xFE, 0x07, 0x00, 0xC7, 0x46, 0xFC, 0x03, 0x00, 0x8B, 0x46, 0xFE, 0x8B, 0x4E, 0xFC, 0xD3, 0xE0, 0xE8, 0x09, 0x02, 0xC7, 0x46, 0xFE, 0xD4, 0x00, 0xC7, 0x46, 0xFC, 0x02, 0x00, 0x8B, 0x46, 0xFE, 0x8B, 0x4E, 0xFC, 0xD3, 0xF8, 0xE8, 0xF4, 0x01, 0xC7, 0x46, 0xFE, 0xAB, 0x00, 0xC7, 0x46, 0xFC, 0x3F, 0x00, 0x8B, 0x46, 0xFE, 0x3B, 0x46, 0xFC, 0x73, 0x05, 0xB8, 0x59, 0x00, 0xEB, 0x03, 0xB8, 0x4E, 0x00, 0xE8, 0xD7, 0x01, 0x8B, 0x46, 0xFC, 0x3B, 0x46, 0xFE, 0x73, 0x05, 0xB8, 0x59, 0x00, 0xEB, 0x03, 0xB8, 0x4E, 0x00, 0xE8, 0xC4, 0x01, 0x8B, 0x46, 0xFC, 0x3B, 0x46, 0xFE, 0x72, 0x05, 0xB8, 0x59, 0x00, 0xEB, 0x03, 0xB8, 0x4E, 0x00, 0xE8, 0xB1, 0x01, 0x8B, 0x46, 0xFE, 0x3B, 0x46, 0xFC, 0x72, 0x05, 0xB8, 0x59, 0x00, 0xEB, 0x03, 0xB8, 0x4E, 0x00, 0xE8, 0x9E, 0x01, 0x8B, 0x46, 0xFE, 0x3B, 0x46, 0xFC, 0x75, 0x05, 0xB8, 0x59, 0x00, 0xEB, 0x03, 0xB8, 0x4E, 0x00, 0xE8, 0x8B, 0x01, 0x8B, 0x46, 0xFE, 0x3B, 0x46, 0xFC, 0x74, 0x05, 0xB8, 0x59, 0x00, 0xEB, 0x03, 0xB8, 0x4E, 0x00, 0xE8, 0x78, 0x01, 0xC7, 0x46, 0xFE, 0xAB, 0x00, 0xC7, 0x46, 0xFC, 0x3F, 0x00, 0x8B, 0x46, 0xFE, 0x33, 0x46, 0xFC, 0x2D, 0x4E, 0x02, 0xE8, 0x62, 0x01, 0xC7, 0x46, 0xFE, 0xAB, 0x00, 0xC7, 0x46, 0xFC, 0x3F, 0x00, 0x8B, 0x46, 0xFE, 0x0B, 0x46, 0xFC, 0x2D, 0x51, 0x02, 0xE8, 0x4C, 0x01, 0x83, 0x7E, 0xFE, 0x0A, 0x7C, 0x0B, 0x83, 0x7E, 0xFC, 0x00, 0x72, 0x05, 0xB8, 0x59, 0x00, 0xEB, 0x03, 0xB8, 0x4E, 0x00, 0xE8, 0x35, 0x01, 0x81, 0x7E, 0xFE, 0x90, 0x16, 0x7F, 0x06, 0x83, 0x7E, 0xFC, 0x0E, 0x73, 0x05, 0xB8, 0x59, 0x00, 0xEB, 0x03, 0xB8, 0x4E, 0x00, 0xE8, 0x1D, 0x01, 0x8B, 0x46, 0xFC, 0x3B, 0x46, 0xFE, 0x72, 0x05, 0xB8, 0x59, 0x00, 0xEB, 0x03, 0xB8, 0x4E, 0x00, 0xE8, 0x0A, 0x01, 0x8B, 0x46, 0xFE, 0x3B, 0x46, 0xFC, 0x72, 0x08, 0xB8, 0x31, 0x00, 0xE8, 0xFC, 0x00, 0xEB, 0x06, 0xB8, 0x32, 0x00, 0xE8, 0xF4, 0x00, 0xC7, 0x46, 0xFE, 0x91, 0x00, 0x8B, 0x46, 0xFE, 0x3D, 0x91, 0x00, 0x75, 0x08, 0xB8, 0x53, 0x00, 0xE8, 0xE1, 0x00, 0xEB, 0x06, 0xB8, 0x46, 0x00, 0xE8, 0xD9, 0x00, 0xC7, 0x46, 0xFE, 0x03, 0x00, 0xEB, 0x0C, 0xB8, 0x31, 0x00, 0xFF, 0x4E, 0xFE, 0x03, 0x46, 0xFE, 0xE8, 0xC6, 0x00, 0x83, 0x7E, 0xFE, 0x00, 0x7F, 0xEE, 0xC7, 0x46, 0xFE, 0x04, 0x00, 0xB8, 0x41, 0x00, 0x83, 0x46, 0xFE, 0x02, 0x03, 0x46, 0xFE, 0xE8, 0xAE, 0x00, 0x83, 0x7E, 0xFE, 0x0B, 0x7C, 0xED, 0xC7, 0x46, 0xFE, 0x34, 0x12, 0xC7, 0x46, 0xFC, 0x00, 0x00, 0xEB, 0x24, 0x8B, 0x46, 0xFC, 0xBB, 0x0A, 0x00, 0xF7, 0xE3, 0x8B, 0xC8, 0x8B, 0x46, 0xFE, 0xBB, 0x0A, 0x00, 0x99, 0xF7, 0xFB, 0x03, 0xCA, 0x89, 0x4E, 0xFC, 0x8B, 0x46, 0xFE, 0xBB, 0x0A, 0x00, 0x99, 0xF7, 0xFB, 0x89, 0x46, 0xFE, 0x83, 0x7E, 0xFE, 0x00, 0x75, 0xD6, 0x8B, 0x46, 0xFC, 0xB1, 0x08, 0xD3, 0xE8, 0x05, 0x30, 0x00, 0xE8, 0x65, 0x00, 0xC7, 0x46, 0xFE, 0x04, 0x00, 0xC7, 0x46, 0xFE, 0x00, 0x00, 0xEB, 0x0D, 0xB8, 0x30, 0x00, 0x03, 0x46, 0xFE, 0xE8, 0x50, 0x00, 0x83, 0x46, 0xFE, 0x03, 0x83, 0x7E, 0xFE, 0x08, 0x7C, 0xED, 0xC7, 0x46, 0xFC, 0x00, 0x00, 0xB8, 0x38, 0x00, 0x2B, 0x46, 0xFC, 0xE8, 0x38, 0x00, 0xC7, 0x46, 0xFC, 0x00, 0x00, 0xB8, 0x43, 0x00, 0x2B, 0x46, 0xFC, 0xE8, 0x2A, 0x00, 0xC7, 0x46, 0xF6, 0x33, 0x00, 0xB8, 0x44, 0x00, 0x2B, 0x46, 0xFC, 0xE8, 0x1C, 0x00, 0xE8, 0x08, 0x00, 0x5F, 0x5E, 0x5A, 0x59, 0x8B, 0xE5, 0x5D, 0xC3, 0xB8, 0x00, 0x4C, 0xCD, 0x21, 0xC3, 0xB4, 0x02, 0xB2, 0x0D, 0xCD, 0x21, 0xB2, 0x0A, 0xCD, 0x21, 0xC3, 0x8A, 0xD0, 0xB4, 0x02, 0xCD, 0x21, 0xC3, 0x00, 0x2B, 0x00, 0x23, 0x00, 0x4B, 0x00, 0x64, 0x00
        ];
    }
}