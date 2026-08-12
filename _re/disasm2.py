import struct
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

def rva2file(rva):
    for name, vaddr, vsize, rawPtr, rawSize in sections:
        if vaddr <= rva < vaddr + min(vsize, rawSize):
            return rawPtr + (rva - vaddr)
    return None

# 1) dump 常量表 0x049E8500 - 0x049E8790, 解释 float/double
print("===== 常量表 RVA 0x049E8500 - 0x049E8790 =====")
f0 = rva2file(0x049E8500)
for off in range(0, 0x290, 8):
    rva = 0x049E8500 + off
    d = struct.unpack_from("<d", data, f0 + off)[0]
    fl = struct.unpack_from("<f", data, f0 + off)[0]
    fl2 = struct.unpack_from("<f", data, f0 + off + 4)[0]
    print(f"  0x{rva:X}: double={d!r:30s}  f1={fl!r:15s}  f2={fl2!r}")

md = Cs(CS_ARCH_X86, CS_MODE_64)
md.detail = True

def disasm_range(start_rva, end_rva, label=""):
    print(f"\n===== {label} 0x{start_rva:X} - 0x{end_rva:X} =====")
    f = rva2file(start_rva)
    if f is None:
        print("  (越界)")
        return
    code = data[f:f+(end_rva-start_rva)]
    for ins in md.disasm(code, start_rva):
        print(f"  0x{ins.address:X}: {ins.mnemonic:8s} {ins.op_str}")

# 2) 显示格式化函数 (10000 ref @ 0x007308C4, float 版本)
disasm_range(0x00730800, 0x00730C00, "显示格式化 (10000 float refs @ 0x007308C4)")
