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

def disasm_around(center_rva, before=0x60, after=0x80, label=""):
    start = center_rva - before
    print(f"\n===== {label} 引用点 0x{center_rva:X} (0x{start:X} - 0x{center_rva+after:X}) =====")
    f = rva2file(start)
    if f is None:
        print("  (越界)")
        return
    code = data[f:f+(before+after)]
    for ins in md.disasm(code, start):
        extra = ""
        try:
            for op in ins.operands:
                if op.type == X86_OP_MEM and op.mem.base == X86_REG_RIP:
                    t = ins.address + ins.size + op.mem.disp
                    mark = " <<<" if abs(t - center_rva) <= 8 else ""
                    extra = f"  ; -> 0x{t:X}{mark}"
        except CsError:
            pass
        print(f"  0x{ins.address:X}: {ins.mnemonic:8s} {ins.op_str}{extra}")

for rva in (0x3C11FD, 0x3C1370, 0x3C151A, 0x3C1A79):
    disasm_around(rva, label="1e9")

# 2147483647 引用点
disasm_around(0x2C3C92A, label="int.MaxValue")
disasm_around(0x31173E1, label="int.MaxValue B")
disasm_around(0x3117406, label="uint.MaxValue")
