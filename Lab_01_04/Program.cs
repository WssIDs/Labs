using System;
using System.Collections.Generic;
using System.Text;

namespace Lab_01_04
{
    class Program
    {
        static int[,] Input(out int n)
        {
            Console.WriteLine("Ввод размерности матрицы : ");
            Console.Write("n = ");
            n = int.Parse(Console.ReadLine());

            int[,] a = new int[n, n];
            for (int i = 0; i < n; ++i)
                for (int j = 0; j < n; ++j)
                {
                    Console.Write("a[{0},{1}]= ", i, j);
                    a[i, j] = int.Parse(Console.ReadLine());
                }
            return a;
        }

        static void Print(int[,] a)
        {
            for (int i = 0; i < a.GetLength(0); ++i, Console.WriteLine())
                for (int j = 0; j < a.GetLength(1); ++j)
                    Console.Write("{0,5} ", a[i, j]);
        }

        static int Change(int[,] a)
        {
            int sum = 0;


            for (int i = 0; i < a.GetLength(0); ++i, Console.WriteLine())
            {
                sum += a[i, a.GetLength(1) - 1 - i];
            }

            return sum;
        }

        static void Main(string[] args)
        {
            int n;
            int[,] customArray = Input(out n);
            Console.WriteLine("Исходный массив:");
            Print(customArray);

            Console.WriteLine("Сумма элементов главной диагонали = {0}", Change(customArray));

            Console.ReadKey();
        }
    }
}
