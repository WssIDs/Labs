using System;
using System.Collections.Generic;
using System.Text;

namespace Lab_01
{
    class Program
    {
        static int[] Input()
        {
            Console.WriteLine("Ввод количества элементов массива n :");
            int n = 0;
            try
            {
                n = int.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Environment.Exit(0);
            }
            int[] a = new int[n];
            for (int i = 0; i < n; i++)
            {
                Console.Write("a[{0}]= ", i);
                try
                {
                    a[i] = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Environment.Exit(0);
                }
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
                try
                {
                    first = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Environment.Exit(0);
                }


                Console.Write("last= ");
                try
                {
                    last = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Environment.Exit(0);
                }

                if (first == last)
                {
                    Console.WriteLine("Начало и конец интервала не должны быть равны\n Введите интервал снова:");
                }
            }
        }

        static void Print(int[] a)
        {
            for (int i = 0; i < a.Length; ++i) Console.Write("{0} ", a[i]);
            Console.WriteLine();
        }

        static void Change(int[] a)
        {
            int first,last;

            InputInterval(out first, out last);

            for (int i = 0; i < a.Length; ++i)
            {
                if (a[i] >= Math.Min(first,last) && a[i] <= Math.Max(first,last))
                {
                    a[i] = 0;
                }
            }

        }


        static void Main(string[] args)
        {
            int[] customArray = Input();
            Console.WriteLine("Исходный массив:");
            Print(customArray);
            Change(customArray);
            Console.WriteLine("Измененный массив:");
            Print(customArray);
            Console.ReadKey();

        }

    }
}
