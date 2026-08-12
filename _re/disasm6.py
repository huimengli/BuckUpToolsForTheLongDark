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

# 1) dump float 常量表 0x49E7E50 - 0x49E8300
print("===== float/double 常量表 0x49E7E50 - 0x49E8300 =====")
f = rva2file(0x49E7E50)
chunk = data[f:f+(0x49E8300-0x49E7E50)]
for off in range(0, len(chunk), 16):
    rva = 0x49E7E50 + off
    b = chunk[off:off+16]
    f1, f2 = struct.unpack_from("<ff", b, 0)
    d = struct.unpack_from("<d", b, 0)[0]
    u32a, u32b, u32c, u32d = struct.unpack_from("<IIII", b, 0)
    print(f"  0x{rva:X}: f1={f1:12.6g} f2={f2:12.6g} | d={d:18.8g} | u32: {u32a:08X} {u32b:08X} {u32c:08X} {u32d:08X}")

# 2) dump 引用点 0x31173E1 / 0x3117406
md = Cs(CS_ARCH_X86, CS_MODE_64)
md.detail = True
md.skipdata = True

def disasm_around(center_rva, before=0x80, after=0x90, label=""):
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

disasm_around(0x31173E1, label="int.MaxValue B")
disasm_around(0x3117406, label="uint.MaxValue")
