using System;

/*
    Разработать рекурсивный метод для вывода на экран всех возможных разложений натурального числа n на слагаемые (без повторений). Например, для n = 5 на экран должно 
    быть выведено:
    1+1+1+1+1=5
    1+1+1+2=5
    1+1+3=5
    1+4=5
    2+1+2=5
    2+3=5

*/



namespace Lab_01_11
{
    class Program
    {
        static int first = 1; // первый элемент слагаемого
        static int last = 1;  // последний элемент слагаемого

        static void rec(int n,int k)
        {
            if(k == 0)
            {
                k = n;
            }

            for (int i = 1; i < n; i++)
            {
                string s="";
           
                s += first;

                for (int j = n - i; j > 1; j--)
                {
                    s += " + 1";
                }

                Console.WriteLine(s +" + " + last + " = " + k);

                last++;
            }

            last = first + 1;

            first++;

            if (n > 1)
            {
                rec(n - 2,k);
            }
        }

        static void Main(string[] args)
        {
            while (true)
            {
                int n = 0;

                Console.WriteLine("Введите целое число");

                try
                {
                    n = int.Parse(Console.ReadLine());
                    rec(n,0);

                    first = 1;
                    last = 1;
                }

                catch (FormatException ex)
                {
                    Console.WriteLine("Ошибка. необходимо ввести целое число - {0}", ex.Message);
                }
            }

            Console.ReadKey();
        }
    }
}
