using System;
using System.Collections.Generic;
using System.Text;

namespace Lab_01_07
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите строку:");
            string s = Console.ReadLine();
            char[] sep = { ' ', '.', ',', ':', ';', '?', '!' };
            string[] words = s.Split(sep,StringSplitOptions.RemoveEmptyEntries);

            Console.WriteLine("Слова начинающиеся с прописной буквы :");

            foreach (string word in words)
            {
                if (word[0] == word.ToLower()[0])
                {
                    Console.WriteLine(word);
                }
            }

            Console.ReadKey();

        }
    }
}
