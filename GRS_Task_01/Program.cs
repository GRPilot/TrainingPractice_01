using System;

namespace GRS_Task_01 {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Магазин \"У холма\"");
            const int crystalPrice = 5;

            Console.WriteLine("Текущий курс золота к кристаллу: 1 кристалл за {0} золота", crystalPrice);
            Console.Write("Введите количество золота: ");
            int userAmount = int.Parse(Console.ReadLine());
            
            if(userAmount <= 0) {
                Console.WriteLine("Зачем приходить в магазин с долгами?");
                return;
            }
            int availableCrystals = userAmount / crystalPrice;

            if(availableCrystals <= 0) {
                Console.WriteLine("У вас недостаточно средств для покупки кристалла.");
                return;
            }

            Console.WriteLine("Вы можете преобрести {0} кристаллов.", availableCrystals);
            Console.Write("Введите количество кристаллов, которое вы желаете преобрести: ");
            int crystalCount = int.Parse(Console.ReadLine());
            if(crystalCount <= 0) {
                Console.WriteLine("Вы не можете преобрести отрицательное количество кристаллов");
                return;
            }

            if(crystalCount > availableCrystals) {
                Console.WriteLine("У вас недостаточно средств для покупки {0} кристаллов.", crystalCount);
                return;
            }

            userAmount -= crystalPrice * crystalCount;
            Console.WriteLine("Вы преобрели {0} кристаллов", crystalCount);
            Console.WriteLine("Ваш баланс: {0}", userAmount);
        }
    }
}
