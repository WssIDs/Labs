using System;
//using System.Collections.Generic;
//using System.Text;

namespace Lab_01_08
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

        static void Print(int[] inArray)
        {
            if (inArray.Length > 0)
            {
                foreach (int a in inArray)
                {
                    Console.Write("{0,5}", a);
                }
            }

            else
            {
                Console.WriteLine("Массив пуст");
            }

            Console.WriteLine();
        }

        /* Удалить все четные числа из массива */
        static int[] RemoveEvenNumbers(ref int[] inArray)
        {
            int n = 0;

            foreach (int a in inArray)
            {
                if (a % 2 != 0)
                {
                    inArray[n] = a;
                    n++;
                }
            }

            Array.Resize(ref inArray,n);

            return inArray;
        }

        /* Вставить новый элемент после всех элементов, которые заканчиваются на данную
        цифру*/
        static int[] InsertElements(int[] inArray,int inNumber)
        {
            int[] newArray = new int[inArray.Length*2];

            Random rand = new Random();

            int n = 0;

            for (int i = 0; i < inArray.Length; ++i)
            {
                newArray[n] = inArray[i];

                n++;

                if ((inArray[i].ToString().Substring(inArray[i].ToString().Length-1,1).Equals(inNumber.ToString())))
                {
                    newArray[n] = rand.Next(-10,-5);

                    n++;
                }
            }

            Array.Resize(ref newArray, n);

            return newArray;
        }


        static int[] DeleteRepeated(int[] inArray)
        {
            for (int i = 0; i < inArray.Length; ++i)
            {
                inArray = SortArray(inArray, inArray[i]);
            }

            return inArray;
        }

        /* Удалить из массива повторяющиеся элементы, оставив только их первые вхождения*/
        static int[] SortArray(int[] inArray, int inNumber)
        {
            int[] temp = new int[inArray.Length];

            bool hasUnique = true;

            int n = 0;

            for (int i = 0; i < inArray.Length; ++i)
            {
                if (inArray[i] == inNumber && hasUnique == true)
                {
                    hasUnique = false;
                    temp[n] = inArray[i];
                    n++;
                }

                if (inArray[i] != inNumber)
                {
                    temp[n] = inArray[i];
                    n++;
                }
            }

            Array.Resize(ref temp, n);

            return temp;
        }

        /* Вставить новый элемент между всеми парами элементов, имеющими разные знаки */
        static int[] addElementBetween(int[] inArray)
        {
            Random rand = new Random();

            for (int i = 0; i < inArray.Length-1; ++i)
            {
                if ((inArray[i] > 0 && inArray[i + 1] < 0) || (inArray[i] < 0 && inArray[i + 1] > 0))
                {
                    Array.Resize(ref inArray, inArray.Length + 1);
                    for (int j = inArray.Length - 1; j >= i + 1; j--)
                    {
                        inArray[j] = inArray[j - 1];
                    }
                    inArray[i + 1] = rand.Next(30,50);
                }
            }

            return inArray;
        }

        /* Удалить из массива все заданные значения */
        static int[] RemoveZero(int[] inArray,int inNumber)
        {
            int n = 0;

            foreach (int a in inArray)
            {
                if (a != inNumber)
                {
                    inArray[n] = a;
                    n++;
                }
            }

            Array.Resize(ref inArray, n);

            return inArray;
        }

        static void Main(string[] args)
        {
            int[] customArray = new int[0];

            while (true)
            {
                customArray = Input();
                Console.WriteLine("Исходный массив:");
                Print(customArray);

                int inputMenu = 0;

                Console.WriteLine();
                Console.WriteLine("МЕНЮ");
                Console.WriteLine("1 - Удалить из массива все четные числа");
                Console.WriteLine("2 - Вставить новый элемент после всех элементов, которые заканчиваются на данную цифру");
                Console.WriteLine("3 - Удалить из массива повторяющиеся элементы, оставив только их первые вхождения");
                Console.WriteLine("4 - Вставить новый элемент между всеми парами элементов, имеющими разные знаки");
                Console.WriteLine("5 - Уплотнить массив, удалив из него все нулевые значения");
                Console.WriteLine("0 - Выход");
                Console.Write("Выберите пункт меню: ");

                inputMenu = int.Parse(Console.ReadLine());

                if (inputMenu == 1)
                {
                    Print(RemoveEvenNumbers(ref customArray));
                }

                else if (inputMenu == 2)
                {
                    Console.WriteLine("Введите цифру, после которой необходимо вставить элемент :");

                    int n = 0;

                    n = int.Parse(Console.ReadLine());

                    Print(InsertElements(customArray, n));

                }

                else if (inputMenu == 3)
                {
                    Print(DeleteRepeated(customArray));
                }

                else if (inputMenu == 4)
                {
                    Print(addElementBetween(customArray));
                }

                else if (inputMenu == 5)
                {
                    Print(RemoveZero(customArray,0));
                }

                else if (inputMenu == 0)
                {
                    break;
                }

                else
                {
                    Console.WriteLine("Введите правильный пункт меню");
                }
            }

            Console.ReadKey();

        }
    }
}
