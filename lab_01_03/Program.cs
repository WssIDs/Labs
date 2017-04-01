using System;
using System.Collections.Generic;
using System.Text;

namespace Lab_01_03
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

        static void Print(int[] a)
        {
            //for (int i = 0; i < a.Length; ++i) Console.Write("{0} ", a[i]);

            foreach (int item in a)
            {
                Console.Write("{0} ", item);
            }


            Console.WriteLine();
        }

        static void Change(int[] a)
        {

            int max = a[0];

            for (int i = 0; i < a.Length; ++i)
            {
                max = Math.Max(max, a[i]);
            }

            for (int i = 0; i < a.Length; ++i)
            {
                if(a[i] == max)
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
