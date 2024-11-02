using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGO_Offsets
{
    public static class Offsets
    {
        // probably not actual

        // constants
        public const int SPACE_BAR = 0x20;

        public static int dwLocalPlayerPawn = 0x1836BB8;
        public static int m_iIDEntIndex = 0x1458;
        public const int dwLocalPlayerController = 0x1A219E0;
        public const int dwEntityList = 0x19D1A98;
        public const int m_iTeamNum = 0x3E3;
        public const int m_flFlashMaxAlpha = 0x1408;

        // fflags

        public const uint INACTION = 65537;
        public const uint NOTINACTION = 0;

    }
}
