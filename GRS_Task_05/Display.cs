using System;

namespace GRS_Task_05 {
    class Display {
        public Display() {
        }

        public void Clear() {
            Console.Clear();
        }

        public void Show(ref Map map) { 
            for(uint h = 0; h < map.height; ++h) {
                for(uint w = 0; w < map.width; ++w) {
                    ShowMapSymbol(map.GetCell(w, h));
                }
                Console.WriteLine();
            }
        }

        public void Show(ref Player player) {
            Console.Write("HP: [");
            ShowStatusBar(player.HP);
            Console.WriteLine("] {0}%", player.HP);
        }

        private void ShowMapSymbol(char symbol) { 
            switch(symbol) { 
                case Constaints.WALL_BLOCK:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case Constaints.HEALTH_BLOCK:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case Constaints.ENEMY_BLOCK:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case Constaints.PLAYER_BLOCK:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
            }
            Console.Write(symbol);
            Console.ResetColor();
        }

        public void ShowStatusBar(int value, int min_value = 10, int max_value = 100) {
            if(value * 4 < max_value) {
                Console.ForegroundColor = ConsoleColor.Red;
            } else { 
                Console.ForegroundColor = ConsoleColor.Green;
            }

            for(int i = min_value; i <= value; i += 10) {
                Console.Write(Constaints.WALL_BLOCK);
            }
            Console.ResetColor();
            for(int i = value + 1; i < max_value; i += 10) {
                Console.Write(" ");
            }
        }
    }
}