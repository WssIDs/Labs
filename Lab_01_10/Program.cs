using System;

/*
    Разработать рекурсивный метод для вывода на экран всех возможных разложений натурального числа n на множители (без повторений). Например, для n = 12 на экран должно
    быть выведено:

    2*2*3=12
    2*6=12
    3*4=12
    
*/

namespace Lab_01_10
{
    class Program
    {

        /// <summary>
        /// Массив множителей
        /// </summary>
        static int[] mass = new int[100];
        static int number = 0;

        /// <summary>
        /// Поиск всех делителей числа (без повторений) 
        /// </summary>
        /// <param name="divider">делитель</param>
        /// <param name="n">число</param>
        /// <param name="index">индекс</param>
        static void FindDividers(int divider, int n, int index)
        {
            //если n = 1 , вывод на экран
            if (n == 1)
            {
                for (int i = 0; i < index - 1; i++)
                {
                    Console.Write(mass[i] + "*");
                }

                if (index > 0 && mass[index - 1] != number)
                {
                    Console.WriteLine(mass[index - 1] + "=" + number);
                    return;
                }

            }

            // Поиск множителей
            for (mass[index] = divider; mass[index] <= n; mass[index]++)
            {
                if ((n % mass[index]) == 0)
                {
                    FindDividers(mass[index], n / mass[index], index + 1);
                }
            }
        }

        static void Main()
        {
            Console.Write("Введите число: ");

            int n = int.Parse(Console.ReadLine());
            number = n;

            FindDividers(2, n, 0);

            Console.ReadKey();
        }

    }
}
