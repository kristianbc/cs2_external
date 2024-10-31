using CSGO_Memory;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using CSGO_Offsets;
namespace bunnyhop
{
    class Bunnyhop
    {
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(int vKey);

        public static void BunnyHopModule()
        {
            // get cs2 processid
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

            // get module base address
            IntPtr cs2ModuleClient = Memory.GetModuleBaseAddress(cs2ProcessId, "client.dll");
            if (cs2ModuleClient == IntPtr.Zero)
            {
                Console.WriteLine("Failed to get client.dll module base.");
                return;
            }

            // idk
            IntPtr forceJumpAddr = cs2ModuleClient + 0x17BA530;
            IntPtr playerStateAddr = cs2ModuleClient + 0x17BA530;

            while (true)
            {
                if (GetAsyncKeyState(Offsets.SPACE_BAR) < 0)
                {
                    uint fFlag = Memory.ReadUInt(cs2ProcessHandle, playerStateAddr);

                    Console.WriteLine($"Player state (fFlag): {fFlag}");

                    if (fFlag == Offsets.STANDING || fFlag == Offsets.CROUCHING)
                    {
                        Thread.Sleep(1);
                        Memory.WriteUInt(cs2ProcessHandle, forceJumpAddr, Offsets.PLUS_JUMP);
                        Console.WriteLine("Jump executed.");
                    }
                    else
                    {
                        Memory.WriteUInt(cs2ProcessHandle, forceJumpAddr, Offsets.MINUS_JUMP);
                        Console.WriteLine("Stop jump.");
                    }
                }

                Thread.Sleep(10);
            }
        }
    }
}
