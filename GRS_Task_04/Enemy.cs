using System;
using System.Collections.Generic;
using System.Text;

namespace GRS_Task_04 {
    public class EnemyAttack : AttackType {
        public const short ExhaustionManna = LAST_ATTACK + 1;
        public const short ExhaustionHP    = ExhaustionManna + 1;
    }

    class Enemy : Entity {
        public Enemy() : base() {}

        public override void ShowStatus() {
            Console.WriteLine("======= Враг =======");
            base.ShowStatus();
            Console.WriteLine();
        }

        public unsafe bool AttackPlayer(ref Player enemy, short attackType) {
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
            if(attackType < AttackType.LAST_ATTACK) {
                return base.Attack(ref entity, attackType);
            }
            Random rnd = new Random();

            switch(attackType) {
                case EnemyAttack.ExhaustionManna: {
                    int mannaStill = rnd.Next(MIN_MANNA, MAX_MANNA / 10);
                    if(entity.Manna - mannaStill < 0) {
                        mannaStill = entity.Manna;
                    }
                    entity.Manna -= mannaStill;
                    Manna += (Manna + mannaStill < MAX_MANNA) ? MAX_MANNA - Manna : mannaStill;
                    return true;
                }
                case EnemyAttack.ExhaustionHP: {
                    int HPStill = rnd.Next(MIN_HP, MAX_HP / 10);
                    entity.HP -= HPStill ;
                    HP += (HP + HPStill < MAX_HP) ? MAX_HP - HP : HPStill ;
                    return true;
                }

            }
            return false;
        }
    }
}
