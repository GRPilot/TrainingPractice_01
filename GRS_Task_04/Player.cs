using System;
using System.Collections.Generic;
using System.Text;

namespace GRS_Task_04 {
    public class PlayerAttack : AttackType {
        public const short DoubleAttack = LAST_ATTACK + 1;   // 130 manna
        public const short DeamonSword  = DoubleAttack + 1;
    }
    class Player : Entity {
        public const int MIN_DEAMON_PWR = 0;
        public const int MAX_DEAMON_PWR = 200;
        public Player() : base() {
        }


        public override void ShowStatus() {
            Console.WriteLine("======= Игрок =======");
            base.ShowStatus();
            Console.WriteLine();
        }

        public unsafe bool AttackEnemy(ref Enemy enemy, short attackType) {
            Entity e = new Entity {
                HP = enemy.HP,
                Manna = enemy.Manna,
                Power = enemy.Power,
                ProtectionCount = enemy.ProtectionCount
            };
            bool status = Attack(ref e, attackType);
            enemy.HP = e.HP;
            enemy.Manna = e.Manna;
            enemy.Power = e.Power;
            enemy.ProtectionCount = e.ProtectionCount;
            return status;
        }

        public override unsafe bool Attack(ref Entity entity, short attackType) {
            if(DeamonSwordCount > 0) {
                --DeamonSwordCount;
                HP -= DeamonSwordPower;
            } else if(DeamonSwordPower > 0) {
                Power -= DeamonSwordPower;
                DeamonSwordPower = 0;
            }

            if(attackType < AttackType.LAST_ATTACK) {
                return base.Attack(ref entity, attackType);
            }

            switch(attackType) {
                case PlayerAttack.DoubleAttack: {
                    if(Manna - 130 < 0) {
                        return false;
                    }
                    Manna -= 130;
                    entity.HP -= (Power) * 2;
                    return true;
                }
                case PlayerAttack.DeamonSword: {
                    if(Manna - 300 < 0) {
                        return false;
                    }
                    Manna -= 300;
                    Random rnd = new Random();
                    DeamonSwordPower += rnd.Next(50, MAX_DEAMON_PWR);
                    DeamonSwordCount += rnd.Next(1, 5);
                    Power += DeamonSwordPower;
                    return true;
                }
            }
            return true;
        }

        private int DeamonSwordPower;
        private int DeamonSwordCount;
    }
}
