using System;
using System.Collections.Generic;
using System.Text;

namespace Lab_01_05
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

        static int[] InputVector(int n)
        {

            Console.WriteLine("Размерность вектора X = {0}",n);

            int[] x = new int[n];
            for (int i = 0; i < n; ++i)
            {
                Console.Write("x[{0}]= ", i);
                x[i] = int.Parse(Console.ReadLine());
            }

            return x;
        }

        static void Print(int[,] a)
        {
            for (int i = 0; i < a.GetLength(0); ++i, Console.WriteLine())
                for (int j = 0; j < a.GetLength(1); ++j)
                    Console.Write("{0,5} ", a[i, j]);
        }

        static void Print(int[] a)
        {
            foreach (int item in a)
            {
                Console.Write("{0,5} ", item);
            }


            Console.WriteLine();
        }

        static void Change(int[,] a, int[] x)
        {
            for (int j = 0; j < a.GetLength(1); ++j)
            {
                if(j % 2 == 0)
                { 
                    for (int i = 0; i < a.GetLength(0); ++i)
                    {
                        a[i, j] = x[i];
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            int n, m;
            int[,] customArray = Input(out n, out m);
            Console.WriteLine("Исходный массив:");
            Print(customArray);

            int[] vectorX = InputVector(n);

            Console.WriteLine("Вектор X:");

            Print(vectorX);

            Change(customArray, vectorX);

            Print(customArray);
            
            Console.ReadKey();

        }
    }
}
