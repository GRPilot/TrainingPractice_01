using System;
using System.Collections.Generic;
using System.Text;

namespace GRS_Task_05 {
    public class Engine {
        uint width = 30;
        uint height = 30;

        public Random rnd;
        Display display;
        Map map;
        Player player;

        public Engine() {
            EnterMapSize();
            InitializeDependencies();
        }

        public void StartGame() {
            while(player.IsAlive() && !player.onExit) {
                Show();

                player.Move(ref map);
                map.Update(ref player);

                display.Clear();
            }
            Show();

            if(player.IsAlive()) {
                // lose
            } else {
                // win
            }
        }

        private void Show() { 
            display.Show(ref map);
            display.Show(ref player);
        }

        private void EnterMapSize() {
            string input;
            do { 
                Console.Write("Введите размеры лабиринта (Высота/Ширина): ");
                input = Console.ReadLine();
                if(input.ToLower() == "exit") {
                    Environment.Exit(0);
                }
                Console.Clear();
            } while(!IsInputCorrect(input));

            ParseInput(input);
        }

        private void InitializeDependencies() {
            display = new Display();
            rnd = new Random(Constaints.Seed());
            player = new Player();
            MazeMapBuilder builder = new MazeMapBuilder();
            map = builder.FromDimensions(width, height);
            map.SetRandomExit(builder.LastMazeArray(), width, height);
            player.SetRandomPosition(builder.LastMazeArray(), width, height);
            map.Update(ref player);
        }

        // ========= other ========= //
        private bool IsInputCorrect(string input) {
            bool hasSlash = input.Contains('/');
            if(!hasSlash) {
                return false;
            }
            string first_num = input.Substring(0, input.IndexOf('/'));
            string second_num = input.Substring(input.IndexOf('/') + 1);
            try {
                uint.Parse(first_num);
                uint.Parse(second_num);
            } catch {
                return false;
            }
            return true;
        }

        private void ParseInput(string input) {
            string first_num = input.Substring(0, input.IndexOf('/'));
            string second_num = input.Substring(input.IndexOf('/') + 1);
            height = uint.Parse(first_num);
            width = uint.Parse(second_num);
        }
    }
}
