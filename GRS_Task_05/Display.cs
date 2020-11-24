using System;

namespace GRS_Task_05 {
    class Display {
        private Map.Cell[] mapbuffer;
        public Map map;
        public Player player;

        public Display(ref Map _map, ref Player _player) {
            map = _map;
            mapbuffer = new Map.Cell[map.height];
            for(uint i = 0; i < map.height; ++i) {
                mapbuffer[i].content = new char[map.width];
            }
            player = _player;
        }

        public void Update(ref Player _player) {
            player = _player;
            map.Update(ref player);
        }

        public void Clear() {
            Console.Clear();
        }

        public void ShowMap() { 
            for(uint h = 0; h < map.height; ++h) {
                for(uint w = 0; w < map.width; ++w) {
                    ShowMapSymbol(map.GetCell(w, h), h, w);
                }
            }
        }

        public void ShowPlayerStatus() {
            Console.SetCursorPosition(0, (int)(map.height + 1));
            Console.Write("HP: [");
            ShowStatusBar(player.HP);
            Console.WriteLine("] {0}%", player.HP);
        }

        public void ShowGameStatus(ref Player player) { 
            if(!player.IsAlive()) {
                Console.WriteLine("Вы проиграли!");
            } else { 
                Console.WriteLine("Победа на вашей стороне!");
            }
        }

        private void ShowMapSymbol(char symbol, uint h, uint w) { 
            if(mapbuffer[h].content[w] == symbol) {
                return;
            }

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

            mapbuffer[h].content[w] = symbol;
            Console.SetCursorPosition((int)w, (int)h);
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