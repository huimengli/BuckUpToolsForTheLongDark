using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THInjector
{
    DWORD WINAPI LoadDll(PVOID param)
    {
        PMANUAL_INJECT ManualInject = (PMANUAL_INJECT)param;

        HMODULE hModule;
        DWORD i, count;
        DWORD_PTR delta, Function;

        PDWORD_PTR ptr;
        PWORD list;

        PIMAGE_BASE_RELOCATION pIBR;
        PIMAGE_IMPORT_DESCRIPTOR pIID;
        PIMAGE_IMPORT_BY_NAME pIBN;
        PIMAGE_THUNK_DATA FirstThunk, OrigFirstThunk;

        PDLL_MAIN EntryPoint;

        pIBR = ManualInject->BaseRelocation;
        delta = (DWORD_PTR)((LPBYTE)ManualInject->ImageBase - ManualInject->NtHeaders->OptionalHeader.ImageBase); // Calculate the delta

        // Relocate the image

        while (pIBR->VirtualAddress)
        {
            if (pIBR->SizeOfBlock >= sizeof(IMAGE_BASE_RELOCATION))
            {
                count = (pIBR->SizeOfBlock - sizeof(IMAGE_BASE_RELOCATION)) / sizeof(WORD);
                list = (PWORD)(pIBR + 1);

                for (i = 0; i < count; i++)
                {
                    if (list[i])
                    {
                        ptr = (PDWORD_PTR)((LPBYTE)ManualInject->ImageBase + (pIBR->VirtualAddress + (list[i] & 0xFFF)));
                        *ptr += delta;
                    }
                }
            }

            pIBR = (PIMAGE_BASE_RELOCATION)((LPBYTE)pIBR + pIBR->SizeOfBlock);
        }

        pIID = ManualInject->ImportDirectory;

        // Resolve DLL imports

        while (pIID->Characteristics)
        {
            OrigFirstThunk = (PIMAGE_THUNK_DATA)((LPBYTE)ManualInject->ImageBase + pIID->OriginalFirstThunk);
            FirstThunk = (PIMAGE_THUNK_DATA)((LPBYTE)ManualInject->ImageBase + pIID->FirstThunk);

            hModule = ManualInject->fnLoadLibraryA((LPCSTR)ManualInject->ImageBase + pIID->Name);

            if (!hModule)
            {
                return FALSE;
            }

            while (OrigFirstThunk->u1.AddressOfData)
            {
                if (OrigFirstThunk->u1.Ordinal & IMAGE_ORDINAL_FLAG)
                {
                    // Import by ordinal

                    Function = (DWORD_PTR)ManualInject->fnGetProcAddress(hModule, (LPCSTR)(OrigFirstThunk->u1.Ordinal & 0xFFFF));

                    if (!Function)
                    {
                        return FALSE;
                    }

                    FirstThunk->u1.Function = Function;
                }

                else
                {
                    // Import by name

                    pIBN = (PIMAGE_IMPORT_BY_NAME)((LPBYTE)ManualInject->ImageBase + OrigFirstThunk->u1.AddressOfData);
                    Function = (DWORD_PTR)ManualInject->fnGetProcAddress(hModule, (LPCSTR)pIBN->Name);

                    if (!Function)
                    {
                        return FALSE;
                    }

                    FirstThunk->u1.Function = Function;
                }

                OrigFirstThunk++;
                FirstThunk++;
            }

            pIID++;
        }

        if (ManualInject->NtHeaders->OptionalHeader.AddressOfEntryPoint)
        {
            EntryPoint = (PDLL_MAIN)((LPBYTE)ManualInject->ImageBase + ManualInject->NtHeaders->OptionalHeader.AddressOfEntryPoint);
            return EntryPoint((HMODULE)ManualInject->ImageBase, DLL_PROCESS_ATTACH, NULL); // Call the entry point
        }

        return TRUE;
    }

    int InjectDLL(PVOID buffer, DWORD size, LPCWSTR process)
    {
        PIMAGE_DOS_HEADER pIDH;
        PIMAGE_NT_HEADERS pINH;
        PIMAGE_SECTION_HEADER pISH;

        HANDLE hProcess, hThread, hToken, hSnap;
        PVOID image, mem, mem2, mem3;
        DWORD i, ProcessId, threadId = -1;

        TOKEN_PRIVILEGES tp;
        MANUAL_INJECT ManualInject;

        THREADENTRY32 te32;
        CONTEXT ctx;

        if (OpenProcessToken((HANDLE) - 1, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, &hToken))
        {
            tp.PrivilegeCount = 1;
            tp.Privileges[0].Attributes = SE_PRIVILEGE_ENABLED;

            tp.Privileges[0].Luid.LowPart = 20;
            tp.Privileges[0].Luid.HighPart = 0;

            AdjustTokenPrivileges(hToken, FALSE, &tp, 0, NULL, NULL);
            CloseHandle(hToken);
        }

        pIDH = (PIMAGE_DOS_HEADER)buffer;

        if (pIDH->e_magic != IMAGE_DOS_SIGNATURE)
        {
            return 0;
        }

        pINH = (PIMAGE_NT_HEADERS)((LPBYTE)buffer + pIDH->e_lfanew);

        if (pINH->Signature != IMAGE_NT_SIGNATURE)
        {
            return 0;
        }

        if (!(pINH->FileHeader.Characteristics & IMAGE_FILE_DLL))
        {
            return 0;
        }

        hProcess = Process::GetProcessHandle(process);
        ProcessId = Process::GetProcessW(process).th32ProcessID;

        image = VirtualAllocEx(hProcess, NULL, pINH->OptionalHeader.SizeOfImage, MEM_COMMIT | MEM_RESERVE, PAGE_EXECUTE_READWRITE); // Allocate memory for the DLL

        if (!image)
        {
            CloseHandle(hProcess);
            return 0;
        }

        // Copy the header to target process

        if (!WriteProcessMemory(hProcess, image, buffer, pINH->OptionalHeader.SizeOfHeaders, NULL))
        {
            VirtualFreeEx(hProcess, image, 0, MEM_RELEASE);
            CloseHandle(hProcess);
            return 0;
        }

        pISH = (PIMAGE_SECTION_HEADER)(pINH + 1);

        // Copy the DLL to target process

        for (i = 0; i < pINH->FileHeader.NumberOfSections; i++)
        {
            WriteProcessMemory(hProcess, (PVOID)((LPBYTE)image + pISH[i].VirtualAddress), (PVOID)((LPBYTE)buffer + pISH[i].PointerToRawData), pISH[i].SizeOfRawData, NULL);
        }

        mem = VirtualAllocEx(hProcess, NULL, 4096, MEM_COMMIT | MEM_RESERVE, PAGE_EXECUTE_READWRITE); // Allocate memory for the loader code


        if (!mem)
        {
            VirtualFreeEx(hProcess, image, 0, MEM_RELEASE);
            CloseHandle(hProcess);
            return 0;
        }

        mem2 = VirtualAllocEx(hProcess, NULL, 4096, MEM_COMMIT | MEM_RESERVE, PAGE_EXECUTE_READWRITE); // Allocate memory for the shellcode

        if (!mem2)
        {
            VirtualFreeEx(hProcess, image, 0, MEM_RELEASE);
            VirtualFreeEx(hProcess, mem, 0, MEM_RELEASE);
            CloseHandle(hProcess);
            return 0;
        }

        mem3 = VirtualAllocEx(hProcess, NULL, sizeof(int), MEM_COMMIT | MEM_RESERVE, PAGE_EXECUTE_READWRITE); // Allocate memory for the shellcode

        if (!mem3)
        {
            VirtualFreeEx(hProcess, image, 0, MEM_RELEASE);
            VirtualFreeEx(hProcess, mem, 0, MEM_RELEASE);
            VirtualFreeEx(hProcess, mem2, 0, MEM_RELEASE);
            CloseHandle(hProcess);
            return 0;
        }
        int value = 0;
        WriteProcessMemory(hProcess, mem3, &value, sizeof(int), NULL);

        memset(&ManualInject, 0, sizeof(MANUAL_INJECT));

        ManualInject.ImageBase = image;
        ManualInject.NtHeaders = (PIMAGE_NT_HEADERS)((LPBYTE)image + pIDH->e_lfanew);
        ManualInject.BaseRelocation = (PIMAGE_BASE_RELOCATION)((LPBYTE)image + pINH->OptionalHeader.DataDirectory[IMAGE_DIRECTORY_ENTRY_BASERELOC].VirtualAddress);
        ManualInject.ImportDirectory = (PIMAGE_IMPORT_DESCRIPTOR)((LPBYTE)image + pINH->OptionalHeader.DataDirectory[IMAGE_DIRECTORY_ENTRY_IMPORT].VirtualAddress);
        ManualInject.fnLoadLibraryA = LoadLibraryA;
        ManualInject.fnGetProcAddress = GetProcAddress;

        SIZE_T number = 0;

        WriteProcessMemory(hProcess, mem, &ManualInject, sizeof(MANUAL_INJECT), NULL); // Write the loader information to target process
        WriteProcessMemory(hProcess, (PMANUAL_INJECT)mem + 1, LoadDll, 0x1000, &number); // Write the loader code to target process

        // WRITING SHELLCODE

        unsigned char shellcode[]{
            0x9C, 0x50, 0x53, 0x51, 0x52, 0x41, 0x50, 0x41, 0x51, 0x41, 0x52, 0x41, 0x53, // push     REGISTERS
                                   0x48, 0x83, 0xEC, 0x30,                                                       // sub rsp, 0x30
                                   0x48, 0xB9,       0x00, 0x00, 0x00, 0x00 ,0x00 ,0x00 ,0x00 ,0x00,             // movabs rcx, 0x0000000000000000
                                   0x36, 0x80, 0x39, 0x00,                                                       // cmp byte ptr ss : [rcx], 0
                                   0x74, 0x02,                                                                   // je 0x02
                                   0xEB, 0x1A,                                                                   // jmp short 0x1A
                                   0x36, 0xC6, 0x01, 0x01,                                                       // mov byte ptr ss:[rcx], 1
                                   0x48, 0xB9,       0x00, 0x00, 0x00, 0x00 ,0x00 ,0x00 ,0x00 ,0x00,             // movabs rcx, 0x0000000000000000
                                   0x48, 0xB8,       0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,             // mobabs rax, 0x0000000000000000
                                   0xFF, 0xD0,                                                                   // call rax
                                   0x48, 0x83, 0xC4, 0x30,                                                       // add      RSP, 0x30
                                   0x41, 0x5B, 0x41, 0x5A, 0x41, 0x59, 0x41, 0x58, 0x5A, 0x59, 0x5B, 0x58, 0x9D, // pop      REGISTER
                                   0xC3                                                                          // ret
                                  };

        // Replacing param
        *(DWORD_PTR*)(shellcode + 19) = (DWORD_PTR)mem3;
        *(DWORD_PTR*)(shellcode + 41) = (DWORD_PTR)mem;
        *(DWORD_PTR*)(shellcode + 51) = (DWORD_PTR)((PMANUAL_INJECT)mem + 1);

        WriteProcessMemory(hProcess, mem2, shellcode, sizeof(shellcode), &number);

        // END WRITING SHELLCODE

        // Find a thread to hijhack
        HANDLE h = CreateToolhelp32Snapshot(TH32CS_SNAPTHREAD, 0);
        if (h != INVALID_HANDLE_VALUE)
        {
            THREADENTRY32 te;
            te.dwSize = sizeof(te);
            if (Thread32First(h, &te))
            {
                do
                {
                    if (te.th32OwnerProcessID == ProcessId)
                    {
                        threadId = te.th32ThreadID;
                        break;
                    }
                    te.dwSize = sizeof(te);
                } while (Thread32Next(h, &te));
            }
        }
        CloseHandle(h);

        if (threadId == -1)
        {
            VirtualFreeEx(hProcess, image, 0, MEM_RELEASE);
            VirtualFreeEx(hProcess, mem, 0, MEM_RELEASE);
            CloseHandle(hProcess);
            return 0;
        }

        HMODULE mod = GetModuleHandleA("ntdll.dll");
        HHOOK injectHook = SetWindowsHookExA(WH_GETMESSAGE, (HOOKPROC)mem2, mod, threadId);

        while (1)
        {
            int var = -1;
            ReadProcessMemory(hProcess, mem3, &var, sizeof(int), NULL);

            if (var == 1)
            {
                UnhookWindowsHookEx(injectHook);
                break;
            }

            Sleep(1);
        }

        Sleep(1000);

        VirtualFreeEx(hProcess, mem, 0, MEM_RELEASE);
        VirtualFreeEx(hProcess, mem2, 0, MEM_RELEASE);
        VirtualFreeEx(hProcess, mem3, 0, MEM_RELEASE);
        CloseHandle(hProcess);

        return 1;
    }
}