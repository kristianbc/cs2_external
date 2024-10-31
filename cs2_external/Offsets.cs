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

        public static int dwLocalPlayerPawn = 0x1834B18;
        public static int m_iIDEntIndex = 0x1458;
        public const int dwLocalPlayerController = 0x1A1F8F0;
        public const int dwEntityList = 0x19CFC48;
        public const int m_iTeamNum = 0x3E3;
        public const int m_flFlashMaxAlpha = 0x1408;

        // fflags
        public const uint STANDING = 0;
        public const uint CROUCHING = 645456465; // random value
        public const uint PLUS_JUMP = 65537;
        public const uint MINUS_JUMP = 0;




    }
}
