using System;

namespace GRS_Task_3 {
    class Program {
        static void Main(string[] args) {
            const string password = "123456";
            const int MAX_TRIES = 3;

            string userPass;
            bool isPassCorrect = false;
            for(int cur_try = MAX_TRIES - 1; cur_try >= 0; --cur_try) {
                Console.Write("Введите пароль: ");
                userPass = Console.ReadLine();

                if(userPass == password) {
                    isPassCorrect = true;
                    break;
                }
                Console.WriteLine("Неверно введен пароль! Осталось попыток: {0}", cur_try);
            }

            if(isPassCorrect) {
                Console.WriteLine("Секретное сообщение: \"Пароль был {0}\"", password);
            }
        }
    }
}
