using System;
using System.Collections.Generic;
using System.Text;

namespace GRS_Task_05 {
    public class Constaints {
        public const uint MIN_WIDTH = 8;
        public const uint MIN_HEIGHT = 8;
        public const char WALL_BLOCK = '█';
        public const char EXIT_BLOCK = '@';
        public const char ENEMY_BLOCK = '☼';
        public const char HEALTH_BLOCK = '♥';
        public const char PLAYER_BLOCK = '☺';
        static public int Seed() => (int)System.DateTime.Now.Ticks;
    }
}
