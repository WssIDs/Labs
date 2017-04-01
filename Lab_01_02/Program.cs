using System;
using System.Collections.Generic;
using System.Text;

namespace Lab_01_02
{
    class Program
    {
        static int[,] Input(out int n, out int m)
        {
            Console.WriteLine("Ввод размерности массива : ");
            Console.Write("n = ");
            n = int.Parse(Console.ReadLine());
            Console.Write("m = ");
            m = int.Parse(Console.ReadLine());
            int[,] a = new int[n, m];
            for (int i = 0; i < n; ++i)
                for (int j = 0; j < m; ++j)
                {
                    Console.Write("a[{0},{1}]= ", i, j);
                    a[i, j] = int.Parse(Console.ReadLine());
                }
            return a;
        }

        static void InputInterval(out int first, out int last)
        {
            first = 0;
            last = 0;

            Console.WriteLine("Ввод интервала :");

            while (first == last)
            {
                Console.Write("first= ");
                first = int.Parse(Console.ReadLine());

                Console.Write("last= ");

                last = int.Parse(Console.ReadLine());

                if (first == last)
                {
                    Console.WriteLine("Начало и конец интервала не должны быть равны\n Введите интервал снова:");
                }
            }
        }

        static void Print(int[,] a)
        {
            for (int i = 0; i < a.GetLength(0); ++i, Console.WriteLine())
                for (int j = 0; j < a.GetLength(1); ++j)
                    Console.Write("{0,5} ", a[i, j]);
        }
        static void Change(int[,] a)
        {
            int first, last;

            InputInterval(out first, out last);

            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    if (a[i, j] >= Math.Min(first, last) && a[i, j] <= Math.Max(first, last))
                    {
                        a[i, j] = 0;
                    }
                }
        }

        static void Main()
        {
            int n, m;
            int[,] customArray = Input(out n, out m);
            Console.WriteLine("Исходный массив:");
            Print(customArray);
            Change(customArray);
            Console.WriteLine("Измененный массив:");
            Print(customArray);

            Console.ReadKey();
        }

    }
}
