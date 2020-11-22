using System;

namespace GRS_Task_04 {
    class Program {
        enum Msg : short { 
            ERROR_MSG = -1,
            EXIT_MSG = -100,
        }
        static void Main(string[] args) {
            Player player = new Player();
            Enemy enemy = new Enemy();

            Random random = new Random();
            ulong cur_step = 1;
            while(player.isAlive() && enemy.isAlive()) {
                Console.WriteLine("Текущий ход: {0}", cur_step);
                enemy.ShowStatus();
                player.ShowStatus();

                ShowPlayerSkills(ref player);
                Console.Write("Введите атаку: ");
                string skill = Console.ReadLine();
                short selectedSkill = TranslateMessage(skill);
                if(selectedSkill < 0) {
                    if(selectedSkill == (short)Msg.EXIT_MSG) {
                        return;
                    }
                    Console.Clear();
                    continue;
                }
                // ООП в C# просто ужасен. Такая простая механника требует так много костылей
                bool success = player.AttackEnemy(ref enemy, selectedSkill);
                if(!success) {
                    Console.WriteLine("Недостаточно манны!");
                    System.Threading.Thread.Sleep(3000);
                    Console.Clear();
                }

                do {
                    short attack = (short)random.Next(EnemyAttack.Default, EnemyAttack.ExhaustionHP);
                    Console.WriteLine("Босс атакует {0}", attack);
                    System.Threading.Thread.Sleep(3000);
                    success = enemy.AttackPlayer(ref player, attack);
                } while(!success);

                ++cur_step;

                Console.Clear();
            }

            string winnerMessage;
            if(player.isAlive()) { 
                winnerMessage = "Вы выйграли!";
            } else {
                winnerMessage = "Вы проиграли :c";
            }
            Console.WriteLine(winnerMessage);
        }

        static short TranslateMessage(string msg) {
            msg = msg.ToLower();
            if(msg == "exit" || msg == "выйти") {
                return (short)Msg.EXIT_MSG;
            }

            if(msg == "kick" || msg == "удар") { 
                return AttackType.Default;
            }
            if(msg == "protection" || msg == "защита") { 
                return AttackType.Protection;
            }
            if(msg == "heal" || msg == "лечение") { 
                return AttackType.Heal;
            }
            if(msg == "add manna" || msg == "добавить манны") { 
                return AttackType.AddManna;
            }
            if(msg == "double kick" || msg == "двойной удар") { 
                return PlayerAttack.DoubleAttack;
            }
            if(msg == "deamon sword" || msg == "демонический меч") { 
                return PlayerAttack.DeamonSword;
            }
            return (short)Msg.ERROR_MSG;
        }

        static void ShowPlayerSkills(ref Player player) {
            Console.WriteLine("Атаки игрока:");
            Console.WriteLine("  Kick (Удар)                     - Наносит {0} ед. урона врагу", player.Power);
            Console.WriteLine("  Protection (Защита)             - Защищает игрока на текущий и следующий хода");
            Console.WriteLine("  Heal (Лечение)                  - Добавляет {0} + рандомное число к жизням пользователя,");
            Console.WriteLine("                                    отнимая {0} + рандомное число манны пользователя.",
                              AttackType.Heal * Entity.MIN_HP);
            Console.WriteLine("  Add manna (Добавить манны)      - Добавляет рандомное количество манны");
            Console.WriteLine("  Double kick (Двойной удар)      - Дважды ударяет противника, забирает у игрока 130 манны");
            Console.WriteLine("  Deamon sword (Демонический меч) - Призывает демонический меч, который добавляет");
            Console.WriteLine("                                    рандомное кол-во урона к ударам, отнимая");
            Console.WriteLine("                                    каждый ход жизни. Кол-во ходов от 1 до 5");
        }
    }
}
