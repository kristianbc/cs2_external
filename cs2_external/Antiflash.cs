using System;
using System.Threading;
using CSGO_Memory;
using CSGO_Offsets;

namespace CSGO_Antiflash
{
    public class Antiflash
    {
        public static void AntiFlashModule()
        {
            // get processid
            int cs2ProcessId = Memory.GetProcessIdByName("cs2");
            if (cs2ProcessId == 0)
            {
                Console.WriteLine("Could not find cs2.exe process.");
                return;
            }

            IntPtr cs2ProcessHandle = Memory.OpenProcess(Memory.PROCESS_ALL_ACCESS, false, cs2ProcessId);
            if (cs2ProcessHandle == IntPtr.Zero)
            {
                Console.WriteLine("Failed to open CS2 process.");
                return;
            }

            // get the base address
            IntPtr cs2ModuleClient = Memory.GetModuleBaseAddress(cs2ProcessId, "client.dll");
            if (cs2ModuleClient == IntPtr.Zero)
            {
                Console.WriteLine("Failed to get client.dll module base.");
                Memory.CloseHandle(cs2ProcessHandle);
                return;
            }

            while (true) // main loop
            {
                // read local player pointer
                IntPtr localPlayer = Memory.MemoryRead<IntPtr>(cs2ProcessHandle, cs2ModuleClient + Offsets.dwLocalPlayerPawn);
                if (localPlayer == IntPtr.Zero)
                {
                    Console.WriteLine("Failed to find local player.(antiflashmodule)");
                    Thread.Sleep(100);
                    continue;
                }

                int flashmaxalpha = Memory.MemoryRead<int>(cs2ProcessHandle, localPlayer + Offsets.m_flFlashMaxAlpha);

                if (flashmaxalpha >= 0)
                {
                    Memory.MemoryWrite<int>(cs2ProcessHandle, localPlayer + Offsets.m_flFlashMaxAlpha, 0);
                }

                Thread.Sleep(100);
            }
            Memory.CloseHandle(cs2ProcessHandle);
        }
    }
}
