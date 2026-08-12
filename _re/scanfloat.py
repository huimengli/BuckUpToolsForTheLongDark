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

# 扫描 float 常量引用
FLOAT_TARGETS = {
    0x461C4000: "float 10000.0f",
    0x4E6E6B28: "float 1e9f",
    0x3B9ACA00: "float 0.001875f",
}
# 需要先知道这些 float 在 .rdata 的地址 - 直接扫字节值!
print("===== 扫描 float 常量字节 (0x461C4000=10000f, 0x4E6E6B28=1e9f) =====")
rdata_start = 0x4035000
rdata_size = 0xDBD504
rf = rva2file(rdata_start)
rdata = data[rf:rf+rdata_size]

float_addrs = {}
for fval, fname in FLOAT_TARGETS.items():
    pat = struct.pack("<f", fval)
    pos = 0
    while True:
        idx = rdata.find(pat, pos)
        if idx < 0:
            break
        rva = rdata_start + idx
        float_addrs.setdefault(fval, []).append(rva)
        pos = idx + 1
    print(f"{fname}: {len(float_addrs.get(fval, []))} 个位置")

# 对每个 float 位置扫 RIP 引用
all_float_refs = {}
for fval, addrs in float_addrs.items():
    for a in addrs:
        all_float_refs[a] = (fval, len(all_float_refs))

refs_found = {a: [] for a in all_float_refs}
for name, vaddr, vsize, rawPtr, rawSize in sections:
    if name not in (".text", "il2cpp"):
        continue
    size = min(vsize, rawSize)
    code = data[rawPtr:rawPtr+size]
    step = 0x10000
    for base in range(0, size, step):
        chunk = code[base:base+step]
        cur = vaddr + base
        for ins in md.disasm(chunk, cur):
            try:
                for op in ins.operands:
                    if op.type == X86_OP_MEM and op.mem.base == X86_REG_RIP:
                        t = ins.address + ins.size + op.mem.disp
                        if t in all_float_refs:
                            refs_found[t].append(ins.address)
            except CsError:
                pass

for a, lst in refs_found.items():
    fval, _ = all_float_refs[a]
    print(f"\nfloat {'10000.0f' if fval==0x461C4000 else '1e9f'} @ 0x{a:X}: {len(lst)} 引用")
    for r in lst[:30]:
        print(f"  @RVA 0x{r:X}")
