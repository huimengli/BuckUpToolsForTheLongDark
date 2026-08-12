import struct
from capstone import *
from capstone.x86 import *

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

md = Cs(CS_ARCH_X86, CS_MODE_64)
md.detail = True
md.skipdata = True

def disasm_range(start_rva, end_rva, label=""):
    print(f"\n===== {label} 0x{start_rva:X} - 0x{end_rva:X} =====")
    f = rva2file(start_rva)
    if f is None:
        print("  (越界)")
        return
    code = data[f:f+(end_rva-start_rva)]
    for ins in md.disasm(code, start_rva):
        extra = ""
        try:
            for op in ins.operands:
                if op.type == X86_OP_MEM and op.mem.base == X86_REG_RIP:
                    t = ins.address + ins.size + op.mem.disp
                    extra = f"  ; -> 0x{t:X}"
        except CsError:
            pass
        print(f"  0x{ins.address:X}: {ins.mnemonic:8s} {ins.op_str}{extra}")

# 1) 唯一 10000 引用 @ 0x322DCF3 - 前后文
disasm_range(0x322D000, 0x322E200, "double 10000 唯一引用 @ 0x322DCF3")

# 2) 1e9 引用 @ 0x3C11FD / 0x3C1370 / 0x3C151A / 0x3C1A79
disasm_range(0x3C1100, 0x3C1600, "1e9 refs 区域 A")
