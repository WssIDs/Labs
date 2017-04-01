using System;
using System.Collections.Generic;
using System.Text;

namespace Lab_01_06
{
    class Program
    {

        static int SymbolCountInText(char symbol, StringBuilder text)
        {
            int count = 0;

            for (int i = 0; i < text.Length; ++i)
            {
                if (text[i].Equals(symbol))
                {
                    count++;
                }
            }

            return count;
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Введите строку: ");
            StringBuilder a = new StringBuilder(Console.ReadLine());

            Console.WriteLine("Введите первый символ: ");
            char x = char.Parse(Console.ReadLine());
            Console.WriteLine("Введите второй символ: ");
            char y = char.Parse(Console.ReadLine());

            int countX = SymbolCountInText(x, a);
     
            int countY = SymbolCountInText(y, a);


            Console.WriteLine("Введенная строка:\n" + a);

            if (countX > countY)
            {
                Console.WriteLine("Символ - {0} в строке встречается чаще, чем символ - {1}", x, y);
            }

            else if (countX < countY)
            {
                Console.WriteLine("Символ - {0} в строке встречается чаще, чем символ - {1}", y, x);
            }

            else
            {
                Console.WriteLine("Количество символов - {0} в тексте равно количествоу символов {1}", x, y);
            }

            Console.WriteLine("Количество символов {0} в строке: {1}", x, countX);
            Console.WriteLine("Количество символов {0} в строке: {1}", y, countY);

            Console.ReadKey();
        }
    }
}
