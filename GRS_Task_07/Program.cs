using System;

namespace GRS_Task_07 {
    class Program {
        static void Main(string[] args) {
            int size = 0;
            do {
                Console.Write("Введите размер массива: ");
                string line = Console.ReadLine();
                if(!int.TryParse(line, out size)) {
                    Console.WriteLine("Ошибка, попробуйте снова!");
                } else if(size <= 0) {
                    Console.WriteLine("Размер не может быть отрицательным! Попробуйте снова.");
                } else {
                    break;
                }
            } while (true);

            int[] array = GetRandomArray(size);
            Console.Write("Ваш массив: ");
            Show(array);
            Console.WriteLine("Перемешиваю...");
            Shuffle(ref array);
            Console.Write("Результат: ");
            Show(array);
        }

        static void Shuffle(ref int[] array) { 
            Random rnd = new Random((int)System.DateTime.Now.Ticks);
            for(int i = 0; i < array.Length; ++i) {
                int new_index = rnd.Next(0, array.Length);
                if(new_index == i) {
                    continue;
                }
                int buff = array[i];
                array[i] = array[new_index];
                array[new_index] = buff;
            }
        }

        static int[] GetRandomArray(int size = 10) {
            Random rnd = new Random((int)System.DateTime.Now.Ticks);
            int[] arr = new int[size];

            for(int i = 0; i < size; ++i) {
                arr[i] = rnd.Next(-100, 100);
            }
            return arr;
        }

        static void Show(int[] arr) {
            Console.Write("[");
            for(int i = 0; i < arr.Length; ++i) {
                string value = arr[i].ToString();
                Console.Write(value + ((i + 1 != arr.Length) ? ", " : ""));
            }
            Console.WriteLine("];");
        }
    }
}
