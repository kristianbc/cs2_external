using System;
using System.Runtime.InteropServices;
using System.Threading;
using CSGO_Memory;
using CSGO_Offsets;

namespace triggerbot
{
    class Triggerbot
    {
        [DllImport("user32.dll")]
        static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);

        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(int vKey);

        // Constants
        const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        const uint MOUSEEVENTF_LEFTUP = 0x0004;

        // mouse5
        const int VK_XBUTTON2 = 0x05;
        public struct POINT
        {
            public int X;
            public int Y;
        }

        public static void TriggerModule()
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
                    Console.WriteLine("Failed to find local player.");
                    Thread.Sleep(100);
                    continue;
                }

                // read cross entity index
                int crosshairEnt = Memory.MemoryRead<int>(cs2ProcessHandle, localPlayer + Offsets.m_iIDEntIndex);
                if (crosshairEnt < 0)
                {
                    Console.WriteLine("No valid entity in crosshair.");
                    Thread.Sleep(100);
                    continue;
                }

                Console.WriteLine("Entity in crosshair detected: " + crosshairEnt);

                // check if mouse5 is active
                if (GetAsyncKeyState(VK_XBUTTON2) < 0)
                {
                    if (GetCursorPos(out POINT p))
                    {
                        mouse_event(MOUSEEVENTF_LEFTDOWN, (uint)p.X, (uint)p.Y, 0, UIntPtr.Zero);
                        mouse_event(MOUSEEVENTF_LEFTUP, (uint)p.X, (uint)p.Y, 0, UIntPtr.Zero);
                        Console.WriteLine("Mouse click simulated.");
                    }
                }

                Thread.Sleep(100);
            }
            Memory.CloseHandle(cs2ProcessHandle);
        }
    }
}
