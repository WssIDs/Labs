using System;
using System.Collections.Generic;
using System.Text;

namespace Lab_01_09
{
    class Program
    {
        /* Ввод двумерного массива */
        static int[][] Input(out int n, out int m)
        {
            Console.WriteLine("Ввод размерности массива : ");
            Console.Write("n = ");
            n = int.Parse(Console.ReadLine());
            Console.Write("m = ");
            m = int.Parse(Console.ReadLine());
            int[][] a = new int[n][];
            for (int i = 0; i < n; ++i)
            {
                a[i] = new int[m];

                for (int j = 0; j < m; ++j)
                {
                    Console.Write("a[{0},{1}]= ", i, j);
                    a[i][j] = int.Parse(Console.ReadLine());
                }
            }

            return a;
        }

        /* Ввод одномерного массива */
        static int[] Input(int n)
        {
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

        /* Вывод на экран двумерного массива */
        static void Print(int[][] a)
        {
            for (int i = 0; i < a.Length; ++i, Console.WriteLine())
                for (int j = 0; j < a[i].Length; ++j)
                    Console.Write("{0,5} ", a[i][j]);
        }

        /* Вставить строку после строки в которой встречается первый минимальный элемент */
        static int[][] InsertRowAfterFirstMin(int[][] inArray)
        {
            int[] newMass = Input(inArray[0].Length);

            return InsertRowAfterIndex(inArray, newMass, GetIndexFirstMinElement(inArray));
        }

        /* Вставить столбец перед столбцом, в котором встречается заданное число */
        static int[][] InsertColumnBeforeNumber(int[][] inArray)
        {
            int[] mass = Input(inArray.Length);

            Console.Write("Введите число = ");
            int number = int.Parse(Console.ReadLine());

            for (int i = 0; i < RowsToColsArray(inArray).Length; i++)
            {
                if(IsContainsNumber(RowsToColsArray(inArray)[i],number))
                {
                    inArray = InsertColumnBeforeIndex(inArray, mass, i);
                    i++;
                }
            }

            return inArray;
        }

        /* Удаление всех строк, в которой отстутствуют четные элементы */
        static int[][] DeleteNotEvenRows(int[][] inArray)
        {
            for (int i = 0; i < inArray.Length; ++i)
            {
                if (IsContainsEvenNumber(inArray[i]) == false)
                {
                    inArray = RemoveRow(inArray,i);

                    i--;
                }
            }

            return inArray;
        }

        /* Удаление всех столбцов, в которой все элементы положительны */
        static int[][] DeletePositiveCols(int[][] inArray)
        {
            for (int i = 0; i < RowsToColsArray(inArray).Length; ++i)
            {
                if (IsContainsAllPositiveNumbers(RowsToColsArray(inArray)[i]) == true)
                {
                    inArray = RemoveCol(inArray, i);

                    i--;
                }
            }

            return inArray;
        }

        /* Удалить из массива k-столбец и i-строку, если значения их равны */
        static int[][] DeleteEqualsRowCol(int[][] inArray, int row, int col)
        {

            if (IsEquals(inArray[row],RowsToColsArray(inArray)[col]))
            {
                //Console.WriteLine("Равны");

                inArray = RemoveRow(inArray, row);
                inArray = RemoveCol(inArray, col);

            }

            return inArray;
        }

        /* Удалить столбцы и строки, которые содержат одно и тоже число */
        static int[][] DeleteRowsColsContainsSameValue(int[][] inArray, int inNumber)
        {
            for (int i = 0; i < inArray.Length; ++i)
            {
                if (IsAllElementsContainNumber(inArray[i],inNumber) == true)
                {
                    inArray = RemoveRow(inArray, i);

                    i--;
                }
            }

            for (int i = 0; i < RowsToColsArray(inArray).Length; ++i)
            {
                if (IsAllElementsContainNumber(RowsToColsArray(inArray)[i],inNumber) == true)
                {
                    inArray = RemoveCol(inArray, i);

                    i--;
                }
            }

            return inArray;
        }

        static int[][] InsertColumnBeforeIndex(int[][] inArray, int[] source, int index)
        {
            int[][] tempArray = RowsToColsArray(inArray);

            inArray = InsertRowBeforeIndex(tempArray, source, index);

            return RowsToColsArray(inArray);
        }

        /* Вставка строки после указаного индекса строки */
        static int[][] InsertRowAfterIndex(int[][] inArray, int[] source, int index)
        {

            return InsertRowBeforeIndex(inArray, source, index + 1);
        }

        /* Вставка строки перед указанным индексом строки */
        static int[][] InsertRowBeforeIndex(int[][] inArray, int[] source, int index)
        {
            int[][] dest = new int[inArray.Length + 1][];

            int n = 0;

            // если индекс меньше нуля, присваиваем ему 0
            if (index < 0)
            {
                index = 0;
            }

            // если индекс больше длины массива, присваиваем ему длину массива
            if (index > inArray.Length)
            {
                index = inArray.Length;
            }

            for (int i = 0; i < inArray.Length; i++)
            {
                if (index == inArray.Length)
                {
                    dest[n] = new int[source.Length];
                    dest[n] = inArray[i];

                    if (i == index - 1)
                    {
                        n++;
                        dest[n] = new int[source.Length];
                        dest[n] = source;
                    }
                }

                else
                {
                    if (i == index)
                    {
                        dest[n] = new int[source.Length];
                        dest[n] = source;
                        n++;
                    }

                    dest[n] = new int[source.Length];
                    dest[n] = inArray[i];
                }

                n++;
            }

            return dest;
        }



        /* Удалить строку под заданым индексом */
        static int[][] RemoveRow(int[][] source, int index)
        {
            var dest = new int[source.Length - 1][];
            if (index > 0)
                Array.Copy(source, 0, dest, 0, index);

            if (index < source.Length - 1)
                Array.Copy(source, index + 1, dest, index, source.Length - index - 1);

            return dest;
        }

        /* Удалить столбец под заданным индексом */
        static int[][] RemoveCol(int[][] source, int index)
        {
            int[][] tempArray = RowsToColsArray(source);

            tempArray = RemoveRow(tempArray, index);

            return RowsToColsArray(tempArray);
        }

        /* Получить индекс строки, в котором встречается первый раз минимальный элемент */
        static int GetIndexFirstMinElement(int[][] inArray)
        {
            int min = inArray[0][0];
            int index = 0;

            for (int i = 0; i < inArray.Length; i++)
            {
                for (int j = 0; j < inArray[i].Length; j++)
                {
                    if (min != Math.Min(min, inArray[i][j]))
                    {
                        min = Math.Min(min, inArray[i][j]);
                        index = i;
                    }
                }
            }

            return index;
        }

        /* содержит ли массив заданное число inNumber - возвращает */
        static bool IsContainsNumber(int[] inArray, int inNumber)
        {
            foreach (int num in inArray)
            {
                if (num == inNumber)
                {
                    return true;
                }
            }

            return false;
        }

        /* все ли значения в массиве равны заданному числу inNumber */
        static bool IsAllElementsContainNumber(int[] inArray, int inNumber)
        {
            foreach (int num in inArray)
            {
                if (num != inNumber)
                {
                    return false;
                }
            }

            return true;
        }


        /* содержит ли массив четное число - возвращает true/false */
        static bool IsContainsEvenNumber(int[] inArray)
        {
            foreach (int num in inArray)
            {
                if (num % 2 == 0)
                {
                    return true;
                }
            }

            return false;
        }

        /* содержит ли массив все положительные элементы */
        static bool IsContainsAllPositiveNumbers(int[] inArray)
        {
            foreach (int num in inArray)
            {
                if (num < 0)
                {
                    return false;
                }
            }

            return true;
        }


        /* Если значения массивов равны */
        static bool IsEquals(int[] inArrayA,int[] inArrayB)
        {
            for (int i = 0; i < inArrayA.Length; i++)
            {
                if(inArrayA[i] != inArrayB[i])
                {
                    return false;
                }
            }

            return true;
        }

        /* Поменять местами строки и столбцы */
        static int[][] RowsToColsArray(int[][] inArray)
        {
            int[][] tempArray = new int[inArray[0].Length][];

            for (int i = 0; i < inArray[0].Length; i++)
            {
                tempArray[i] = new int[inArray.Length];

                for (int j = 0; j < inArray.Length; j++)
                {
                    tempArray[i][j] = inArray[j][i];
                }
            }

            return tempArray;
        }

        static void Main(string[] args)
        {
            int[][] customArray = new int[0][];

            int n, m;

            while (true)
            {
                customArray = Input(out n, out m);
                Console.WriteLine("Исходный массив:");
                Print(customArray);

                int inputMenu = 0;

                Console.WriteLine();
                Console.WriteLine("МЕНЮ");
                Console.WriteLine("1 - Вставить новую строку после строки, в которой находится первый встреченный минимальный элемент");
                Console.WriteLine("2 - Вставить новый столбец перед всеми столбцами, в которых встречается заданное число");
                Console.WriteLine("3 - Удалить все строки, в которых нет ни одного четного элемента");
                Console.WriteLine("4 - Удалить все столбцы, в которых все элементы положительны");
                Console.WriteLine("5 - Удалить из массива k-тую строку и j-тый столбец, если их значения совпадают");
                Console.WriteLine("6 - Уплотнить массив, удалив из него все нулевые строки и столбцы");
                Console.WriteLine("0 - Выход");
                Console.Write("Выберите пункт меню: ");

                inputMenu = int.Parse(Console.ReadLine());

                if (inputMenu == 1)
                {
                    Print(InsertRowAfterFirstMin(customArray));
                }

                else if (inputMenu == 2)
                {
                    Print(InsertColumnBeforeNumber(customArray));
                }

                else if (inputMenu == 3)
                {
                    Print(DeleteNotEvenRows(customArray));
                }

                else if (inputMenu == 4)
                {
                    Print(DeletePositiveCols(customArray));
                }

                else if (inputMenu == 5)
                {
                    int k, j;

                    Console.WriteLine("Введите индекс строки и столбца : ");
                    Console.Write("k = ");
                    k = int.Parse(Console.ReadLine());
                    Console.Write("j = ");
                    j = int.Parse(Console.ReadLine());

                    Print(DeleteEqualsRowCol(customArray, k, j));
                }

                else if (inputMenu == 6)
                {
                    Print(DeleteRowsColsContainsSameValue(customArray, 0));
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
