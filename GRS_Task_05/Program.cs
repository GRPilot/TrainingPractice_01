using System;

namespace GRS_Task_05 {
    class Program {
        static void Main(string[] args) {
            do {
                Console.Clear();
                Engine engine = new Engine();
                engine.StartGame();
            } while(DoYouWannaTryAgain());
        }

        static bool DoYouWannaTryAgain() {
            string answer;
            do {
                Console.WriteLine("Не желаете попробовать снова?");
                Console.Write(">> ");
                answer = Console.ReadLine();

                answer = answer.ToLower();

                if(answer.Contains("yes") || answer.Contains("да")) {
                    return true;
                }
                if(answer.Contains("no") || answer.Contains("нет")) { 
                    return false;
                }
                if(answer.Contains("idk") || answer.Contains("хз")) {
                    return (new Random(Constaints.Seed())).Next(1, 101) > 50;
                }

            } while(true);
        }
    }
}
