using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using triggerbot;
using bunnyhop;
using CSGO_Antiflash;

namespace testingonkey
{
    class Program
    {
        static void Main()
        {
            Thread bunnyhopThread = new Thread(Bunnyhop.BunnyHopModule);
            Thread triggerThread = new Thread(Triggerbot.TriggerModule);
            Thread antiflashThread = new Thread(Antiflash.AntiFlashModule);

            bunnyhopThread.Start();
            triggerThread.Start();
            antiflashThread.Start();

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine(); // wait for user input to end

            bunnyhopThread.Join();
            triggerThread.Join();
            antiflashThread.Join();

            Console.WriteLine("Exiting application...");
        }
    }
}

