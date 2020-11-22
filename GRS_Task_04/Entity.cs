using System;
using System.Collections.Generic;
using System.Text;

namespace GRS_Task_04 {
    public class AttackType {
        public const short Default     = 0;    // just kick
        public const short Protection  = 1;    // Protect entity for a two steps
        public const short Heal        = 2;
        public const short AddManna    = 3;
        public const short LAST_ATTACK = 4;
    }

    public class Entity {
        public const int MIN_HP = 50;
        public const int MAX_HP = 1000;
        public const int MIN_PWR_ATTK = 5;
        public const int MAX_PWR_ATTK = 200;
        public const int MAX_MANNA = 1000;
        public const int MIN_MANNA = 50;

        public Entity() {
            Random rnd = new Random();
            HP = rnd.Next(MIN_HP, MAX_HP);
            Power = rnd.Next(MIN_PWR_ATTK, MAX_PWR_ATTK);
            Manna = MAX_MANNA;
            ProtectionCount = 0;
        }

        public bool isAlive() {
            return HP > 0;
        }

        public virtual void ShowStatus() {
            Console.WriteLine("Жизни:\t{0}\tHP", HP);
            Console.WriteLine("Мана:\t{0}\tMP", Manna);
            Console.WriteLine("Сила: \t{0}\tPP", Power);
            if (ProtectionCount != 0) {
                Console.WriteLine("Защита:\t{0}", ProtectionCount);
            }
        }

        public virtual unsafe bool Attack(ref Entity entity, short attackType) {
            
            switch (attackType) {
                case AttackType.Default: {
                    entity.HP -= Power;
                    return true;
                }
                case AttackType.Protection: {
                    ProtectionCount = 2;
                    return true;
                }
                case AttackType.Heal: {
                    Random rnd = new Random();
                    int HPCount = rnd.Next(MIN_HP, MAX_HP / 5);
                    int mannaCount = AttackType.Heal * MIN_HP + HPCount;
                    if (Manna - mannaCount < 0) {
                        return false;
                    }
                    HP += HPCount;
                    Manna -= mannaCount;
                    return true;
                }
                case AttackType.AddManna: {
                    Random rnd = new Random();
                    Manna += rnd.Next(MIN_MANNA, MAX_MANNA / 5);
                    return true;
                }
            }
            return false;
        }

        public int HP { get; set; }
        public int Power { get; set; }
        public int Manna { get; set; }
        public int ProtectionCount { get; set; }
    }
}
