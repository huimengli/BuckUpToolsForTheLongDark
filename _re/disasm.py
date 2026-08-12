import struct, sys
from capstone import *

DLL = r"J:\SteamLibrary\steamapps\common\TheLongDark\GameAssembly.dll"
data = open(DLL, "rb").read()

peOff = struct.unpack_from("<I", data, 0x3C)[0]
numSections = struct.unpack_from("<H", data, peOff + 6)[0]
optSize = struct.unpack_from("<H", data, peOff + 20)[0]
sectStart = peOff + 24 + optSize

sections = []
for i in range(numSections):
    p = sectStart + i * 40
    name = data[p:p+8].rstrip(b"\0").decode()
    vsize, vaddr, rawSize, rawPtr = struct.unpack_from("<IIII", data, p + 8)
    sections.append((name, vaddr, vsize, rawPtr, rawSize))
    print(f"SECT {name} RVA=0x{vaddr:X} size=0x{vsize:X} file=0x{rawPtr:X} rawSize=0x{rawSize:X}")

def rva2file(rva):
    for name, vaddr, vsize, rawPtr, rawSize in sections:
        if vaddr <= rva < vaddr + min(vsize, rawSize):
            return rawPtr + (rva - vaddr)
    return None

md = Cs(CS_ARCH_X86, CS_MODE_64)
md.detail = True

def disasm_range(start_rva, end_rva, label=""):
    print(f"\n===== {label} RVA 0x{start_rva:X} - 0x{end_rva:X} =====")
    f = rva2file(start_rva)
    if f is None:
        print("  (不在 section 内)")
        return
    # 一次性读整个范围(可能跨 section, 但目标都在同一 section 内)
    size = end_rva - start_rva
    code = data[f:f+size]
    for ins in md.disasm(code, start_rva):
        print(f"  0x{ins.address:X}: {ins.mnemonic:8s} {ins.op_str}")

if __name__ == "__main__":
    # 默认 dump 关键区域
    ranges = [
        (0x003C0C00, 0x003C1200, "IntBackedUnit 换算 (1e9 refs @ 0x003C0D4F/0x003C0F98)"),
        (0x00730800, 0x00730A00, "显示格式化 (10000 ref @ 0x007308C4)"),
        (0x02C0AB00, 0x02C0B100, "conv.i4 溢出保护 (int.MaxValue refs @ 0x02C0ACDB+)"),
        (0x02CA0000, 0x02CA1000, "imul 1e9 密集区头部"),
    ]
    if len(sys.argv) >= 3:
        ranges = [(int(sys.argv[1], 16), int(sys.argv[2], 16), "自定义")]
    for s, e, label in ranges:
        disasm_range(s, e, label)
