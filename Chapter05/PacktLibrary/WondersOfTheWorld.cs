using System;
using System.Collections.Generic;
using System.Text;

namespace Packt.Shared
{
    public class WondersOfTheWorld
    {
        [System.Flags]
        public enum WondersOfTheAncientWorld : byte
        {
            None = 0b_0000_0000, // то есть 0
            GreatPyramidOfGiza = 0b_0000_0001, // то есть 1
            HangingGardensOfBabylon = 0b_0000_0010, // то есть 2
            //StatueOfZeusAtOlympia = 0b_0000_0100, // то есть 4
            //TempleOfArtemisAtEphesus = 0b_0000_1000, // то есть 8
            //MausoleumAtHalicarnassus = 0b_0001_0000, // то есть 16
            //ColossusOfRhodes = 0b_0010_0000, // то есть 32  
            //LighthouseOfAlexandria = 0b_0100_0000 // то есть 64
        }
    }
}
