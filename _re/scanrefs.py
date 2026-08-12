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
    print(f"SECT {name} RVA=0x{vaddr:X} size=0x{vsize:X} file=0x{rawPtr:X} rawSize=0x{rawSize:X}")

def rva2file(rva):
    for name, vaddr, vsize, rawPtr, rawSize in sections:
        if vaddr <= rva < vaddr + min(vsize, rawSize):
            return rawPtr + (rva - vaddr)
    return None

md = Cs(CS_ARCH_X86, CS_MODE_64)
md.detail = True

# 目标常量 (double)
targets = {
    0x049E8568: "double 10000",
    0x049E86C8: "double 1e9",
    0x049E86E0: "double 2147483647",
    0x049E86E8: "double 2147483647.5",
    0x049E8700: "double 4294967295",
    0x049E8720: "double 1e10",
    0x049E8760: "double 1e12",
}
target_ranges = [(rva, rva+8, name) for rva, name in targets.items()]

# 扫描全部可执行 section
print("\n===== 精确 RIP 引用扫描 (capstone) =====")
hits = {name: [] for name in targets.values()}
for name, vaddr, vsize, rawPtr, rawSize in sections:
    if name not in (".text", "il2cpp"):
        continue
    # 逐函数边界扫描: 简单起见逐页反汇编
    size = min(vsize, rawSize)
    code = data[rawPtr:rawPtr+size]
    addr = vaddr
    # 跳过程序头, 直接从 start 反汇编(速度考虑, 只扫 .text + il2cpp)
    step = 0x10000
    for base in range(0, size, step):
        chunk = code[base:base+step]
        cur = addr + base
        for ins in md.disasm(chunk, cur):
            # 检查内存操作数中的 RIP 目标
            for op in ins.operands:
                if op.type == X86_OP_MEM and op.mem.base == X86_REG_RIP:
                    target = ins.address + ins.size + op.mem.disp
                    for (trva, tend, tname) in target_ranges:
                        if trva <= target < tend:
                            hits[tname].append(ins.address)

for name in targets.values():
    lst = hits[name]
    print(f"\n{name}: {len(lst)} 引用")
    for a in lst[:40]:
        print(f"  @RVA 0x{a:X}")
