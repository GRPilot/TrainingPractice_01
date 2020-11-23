using System;
using System.Collections.Generic;
using System.Text;

namespace GRS_Task_05 {
    class Map {
        private delegate bool ConditionFunction(char[,] m, uint i);

        private Random rnd = new Random(Constaints.Seed());
        public uint width;
        public uint height;

        public struct Cell {
            public char[] content;
        }
        public Cell[] map;

        public Map(uint w, uint h) {
            w *= 2;
            width = (w < Constaints.MIN_WIDTH) ? Constaints.MIN_WIDTH : w;
            height = (h < Constaints.MIN_HEIGHT) ? Constaints.MIN_HEIGHT : h;
            InitMap();
        }

        public void SetRandomExit(char[,] m, uint mWidth, uint mHeight) {
            int side = rnd.Next(0, 4);
            while(!SetRandExit(m, side)) side = rnd.Next(0, 4);
        }

        public void Update(ref Player p) {
            if(p.Y() == p.LastY() && p.X() == p.LastX()) {
                return;
            }

            map[p.LastY()].content[p.LastX()] = ' ';
            map[p.Y()].content[p.X()] = Constaints.PLAYER_BLOCK;
        }

        public void SetMap(char[,] m, uint mHeight, uint mWidth) {
            if(mHeight > height) {
                mHeight = height;
            }
            if(mWidth > width / 2) {
                mWidth = width / 2;
            }

            for (uint h = 0; h < mHeight; ++h) {
                for (uint w = 0; w < mWidth; ++w) {
                    SetBlock(m[h, w], w, h);
                }
            }
        }

        public char GetCell(uint x, uint y) { 
            if(x >= width || y >= height) {
                return Constaints.WALL_BLOCK;
            }

            return map[y].content[x];
        }

        private void InitMap() {
            map = new Cell[height];
            for(uint i = 0; i < height; ++i) {
                map[i].content = new char[width];
            }
        }

        private void SetBlock(char block, uint x, uint y) {
            x *= 2;
            if(x == 0) {
                x = 1;
            }

            switch(block) {
                case Constaints.WALL_BLOCK:
                    map[y].content[x] = block;
                    map[y].content[x - 1] = block;
                    break;
                case Constaints.HEALTH_BLOCK:
                case Constaints.ENEMY_BLOCK: { 
                    bool inLeft = (rnd.NextDouble() < .5f);
                    if(inLeft) {
                        map[y].content[x] = ' ';
                        map[y].content[x - 1] = block;
                    } else {
                        map[y].content[x] = block;
                        map[y].content[x - 1] = ' ';
                    }
                    break;
                }
                default:
                    map[y].content[x] = block;
                    map[y].content[x - 1] = block;
                    break;
            }
        }

        private bool SetRandExit(char[,] m, int side) {
            uint sideSize = 0;
            ConditionFunction cond;
            switch(side) {
                case 0: // left
                    cond = (m, i) => m[i, 1] == Constaints.WALL_BLOCK;
                    sideSize = height;
                    break;
                case 1: // right
                    cond = (m, i) => m[i, width / 2 - 2] == Constaints.WALL_BLOCK;
                    sideSize = height;
                    break;
                case 2: // top
                    cond = (m, i) => m[1, i] == Constaints.WALL_BLOCK;
                    sideSize = width / 2;
                    break;
                case 3: // bottom
                    cond = (m, i) => m[height - 2, i] == Constaints.WALL_BLOCK;
                    sideSize = width / 2;
                    break;
                default:
                    cond = (m, i) => false;
                    sideSize = 2;
                    break;
            }

            uint position = FindAvailableSpace(m, sideSize, cond);
            if(position == 0) {
                return false;
            }

             switch(side) {
                case 0: // left
                    SetBlock(Constaints.EXIT_BLOCK, 0, position);
                    break;
                case 1: // right
                    SetBlock(Constaints.EXIT_BLOCK, width / 2 - 1, position);
                    break;
                case 2: // top
                    SetBlock(Constaints.EXIT_BLOCK, position, 0);
                    break;
                case 3: // bottom
                    SetBlock(Constaints.EXIT_BLOCK, position, height - 1);
                    break;
                default:
                    return false;
            }
            return true;
        }

        private uint FindAvailableSpace(char[,] m, uint to, ConditionFunction cond) { 
            for(uint i = 1; i < to - 1; ++i) { 
                if(cond(m, i)) {
                    continue;
                }

                return i;
            }
            return 0;
        }
    }
}
