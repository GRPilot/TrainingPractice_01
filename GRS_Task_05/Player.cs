using System;
using System.Collections.Generic;
using System.Text;

namespace GRS_Task_05 {
    public struct Point{
        public uint x;
        public uint y;
    }
    class Player {
        private Point lastPosition;
        private Point position;
        public bool onExit;

        public Int16 HP { get; set; }

        public Player() {
            HP = 100;
            position.x = 1u;
            position.y = 1u;
            onExit = false;
        }

        public bool IsAlive() {
            return HP > 0;
        }

        public void SetRandomPosition(char[,] map, uint width, uint height) {
            Random rnd = new Random(Constaints.Seed());
            do {
                position.x = (uint)rnd.Next(1, (int)(width - 2));
                position.y = (uint)rnd.Next(1, (int)(height - 2));

            } while (map[position.y, position.x] == Constaints.WALL_BLOCK);
        }

        public void Move(ref Map map) {
            uint x = position.x;
            uint y = position.y;
            switch(Console.ReadKey(true).Key) {
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    x--;
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    x++;
                    break;

                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    y--;
                    break;

                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    y++;
                    break;
            }
            lastPosition = position;
            if(map.GetCell(x, y) != Constaints.WALL_BLOCK) {
                position.x = x;
                position.y = y;
            }
            switch(map.GetCell(x, y)) {
                case Constaints.ENEMY_BLOCK:
                    HP = (short)((HP - 20 < 0) ? 0 : HP - 20);
                    break;
                case Constaints.HEALTH_BLOCK:
                    HP = (short)((HP + 10 > 100) ? 100 : HP + 10);
                    break;
                case Constaints.EXIT_BLOCK:
                    onExit = true;
                    break;
            }
        }

        public uint X() => position.x;
        public uint Y() => position.y;

        public uint LastX() => lastPosition.x;
        public uint LastY() => lastPosition.y;
    }
}
