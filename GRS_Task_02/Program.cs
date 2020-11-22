using System;

namespace GRS_Task_02 {
    class Program {
        static void Main(string[] args) {
            string cmd = "";
            do {
                Console.Write("Введите команду: ");
                cmd = Console.ReadLine();
            } while(cmd.ToLower() != "exit");

            /* Or we can do:
             while(true) {
                Console.Write("Введите команду: ");
                cmd = Console.ReadLine();

                if(cmd.ToLower() == "exit") {
                    break;
                }
             }

             But this method is disgust. Usually when we wanna exit from a loop,
             we need make the correct logic for our program and correct condition
             */
        }
    }
}
