using System;
using System.Collections.Generic;
using System.Text;

namespace GRS_Task_05 {
    class MazeMapBuilder {
        public float wallChance;
        public float enemyChance;
        public float healthChance;
        char[,] maze;

        public MazeMapBuilder() {
            wallChance = .1f;
            enemyChance = .1f;
            healthChance = .1f;
        }

        public Map FromDimensions(uint width, uint height) { 
            if(width < Constaints.MIN_WIDTH) {
                width = Constaints.MIN_WIDTH;
            }
            if(height < Constaints.MIN_HEIGHT) {
                height = Constaints.MIN_HEIGHT;
            }
            Map map = new Map(width, height);

            maze = new char[height, width];

            int rMax = maze.GetUpperBound(0);
            int cMax = maze.GetUpperBound(1);
            Random rnd = new Random();

            for(int i = 0; i <= rMax; ++i) {
                for(int j = 0; j <= cMax; ++j) {
                    if(i == 0 || j == 0 || i == rMax || j == cMax) {
                        maze[i, j] = Constaints.WALL_BLOCK;
                        continue;
                    }

                    if((i % 2 == 0 && j % 2 == 0) && (rand(ref rnd) > wallChance)) {
                        maze[i, j] = Constaints.WALL_BLOCK;

                        int a = rand(ref rnd) < .5 ? 0 : (rand(ref rnd) < .5 ? -1 : 1);
                        int b = a != 0 ? 0 : (rand(ref rnd) < .5 ? -1 : 1);
                        maze[i + a, j + b] = Constaints.WALL_BLOCK;
                        continue;
                    }

                    if(rand(ref rnd) < enemyChance) {
                        maze[i, j] = Constaints.ENEMY_BLOCK;
                    } else if (rand(ref rnd) < healthChance) {
                        maze[i, j] = Constaints.HEALTH_BLOCK;
                    } else {
                        maze[i, j] = ' ';
                    }
                }
            }

            map.SetMap(maze, height, width);

            return map;
        }

        public char[,] LastMazeArray() {
            return maze;
        }

        private float rand(ref Random random) {
            return (float)random.NextDouble();
        }
    }
}
