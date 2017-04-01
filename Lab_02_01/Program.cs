using System;
using System.Collections.Generic;
using System.Text;


/* ВАРИАНТ 2
    Создать класс Triangle, разработав следующие элементы класса: 
    Поля: 
		– int a, b, c; 
    Конструктор, позволяющий создать экземпляр класса с заданными длинами сторон. 
    Методы, позволяющие: 
		– вывести длины сторон треугольника на экран; 
		– рассчитать периметр треугольника; 
		– рассчитать площадь треугольника. 
    Свойства: 
		– получить-установить длины сторон треугольника (доступное для чтения и записи); 
        – позволяющее установить, существует ли треугольник с данными длинами сторон
               (доступное только для чтения). 
    Индексатор, позволяющий по индексам обращаться к полям класса. 
    Перегрузку: 
		– операции ++ (--): одновременно увеличивает (уменьшает) значение полей на 1; 
		– констант true и false: обращение к экземпляру класса дает значение true, если
                           треугольник с заданными длинами сторон существует, иначе false; 
		– операции *: одновременно домножает поля a, b и c на скаляр; 
		– преобразования типа Triangle в string (и наоборот). */

namespace Lab_02_01
{

    class Triangle
    {
        public Triangle(int a, int b, int c)
        {

            IsCorrect(a, b, c);

            A = a;
            B = b;
            C = c;
        }

        int a;
        int b;
        int c;
            
        // Вывести длины сторон треугольника на экран
        public void PrintSidesTriangle()
        {
            Console.WriteLine("Стороны треугольника равны : A = {0}, B = {1}, C = {2}", A, B, C);
        }

        // Периметр треугольника
        public double Perimeter()
        {
            return a + b + c;
        }

        // Площадь треугольника
        public double Area()
        {
            double p = Perimeter() / 2;

            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }

        // Корректность ввода данных
        static void IsCorrect(int a, int b, int c)
        {
            if (a <= 0 || b <= 0 || c <= 0)
                throw new Exception("Стороны треугольника должны быть больше нуля");
        }

        //свойство для обращения к полю a
        public int A
        {   
            get { return a; }
            set { a = value; }
        }

        //свойство для обращения к полю b
        public int B
        {   
            get { return b; }
            set { b = value; }
        }

        //свойство для обращения к полю c
        public int C
        {   
            get { return c; }
            set { c = value; }
        }

        //Свойство проверки треугольника
        public bool IsTriangle
        {
            get
            {
                //Если подходит под условие существования треугольника
                //возвращаем true, иначе false
                if (a + b > c && a + c > b && b + c > a)
                {
                    return true;
                }

                return false;
            }

        }

        //Индексатор index
        public int this[int index]
        {
            get
            {
                if (index == 0)
                    return a;
                else if (index == 1)
                    return b;
                else if (index == 2)
                    return c;
                else
                    throw new Exception("индекс может принимать значения от 0 до 2");
            }
            set
            {
                if (index == 0)
                    a = value;
                else if (index == 1)
                    b = value;
                else if (index == 2)
                    c = value;
                else
                    throw new Exception("индекс может принимать значения от 0 до 2");
            }
        }

        // перегрузка оператора ++ одновременно увеличивает а,b,с на 1
        public static Triangle operator ++(Triangle tr)
        {
            ++tr.a;
            ++tr.b;
            ++tr.c;

            return tr;
        }

        // перегрузка оператора ++ одновременно уменьшает а,b,с на 1
        public static Triangle operator --(Triangle tr)
        {
            --tr.a;
            --tr.b;
            --tr.c;

            return tr;
        }

        // перегрузка оператора * одновременно домножает а,b,с на множитель
        public static Triangle operator *(Triangle tr, int multiplier)
        {
            tr.a *= multiplier;
            tr.b *= multiplier;
            tr.c *= multiplier;

            return tr;
        }

        // перегрузка константы true
        public static bool operator true(Triangle tr)
        {
            //Console.WriteLine("существует ли треугольник: {0}", tr.IsTriangle);

            return tr.IsTriangle;
        }

        // перегрузка константы false
        public static bool operator false(Triangle tr)
        {
            return tr.IsTriangle;

        }

        // преобразования типа string[] в Triangle
        public static implicit operator Triangle(string[] a)
        {
            return new Triangle(Convert.ToInt32(a[0]), Convert.ToInt32(a[1]), Convert.ToInt32(a[2]));
        }

        // преобразования типа Triangle в string[]
        public static implicit operator string[] (Triangle a)
        {
            string[] temp = new string[3];
            for (int i = 0; i < temp.Length; ++i)
                temp[i] = a[i].ToString();
            return temp;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            int a, b, c;

            while (true)
            {

                Console.WriteLine("Ввод сторон треугольника : ");
                Console.Write("a = ");
                a = int.Parse(Console.ReadLine());
                Console.Write("b = ");
                b = int.Parse(Console.ReadLine());
                Console.Write("c = ");
                c = int.Parse(Console.ReadLine());

                Triangle triangle1 = new Triangle(a, b, c);

                triangle1.PrintSidesTriangle();

                if (triangle1.IsTriangle) // Существует ли треугольник
                {
                    Console.WriteLine("Периметр = {0}", triangle1.Perimeter());

                    Console.WriteLine("Площадь треугольника = {0:0.##}", triangle1.Area());

                    Console.WriteLine("Новые значения треугольника через индексы");

                    triangle1[0] = 5;
                    triangle1[1] = 6;
                    triangle1[2] = 4;

                    triangle1.PrintSidesTriangle();

                    Console.WriteLine("уменьшение сторон на 1");

                    triangle1--;

                    triangle1.PrintSidesTriangle();

                    Console.WriteLine("увеличение сторон на 1");

                    triangle1++;

                    triangle1.PrintSidesTriangle();

                    Console.WriteLine("умножение сторон на 3");

                    triangle1 *= 3;

                    triangle1.PrintSidesTriangle();

                    Console.WriteLine("Преобраование в string[] из Triangle");

                    string[] str1 = new string[3];

                    str1 = triangle1;

                    foreach(string s in str1)
                    {
                        Console.WriteLine("a = {0}", s);
                    }

                    Console.WriteLine("Преобраование в Triangle из string[]");

                    triangle1 = str1;

                    triangle1.PrintSidesTriangle();
                }

                Triangle triangle2 = new Triangle(10, 42, 8);

                if (triangle1)
                {
                    Console.WriteLine("треугольник 1 с заданными длинами сторон существует");
                }
                else
                    Console.WriteLine("треугольник 1 с заданными длинами сторон не существует");

                if (triangle2)
                {
                    Console.WriteLine("треугольник 2 с заданными длинами сторон существует");
                }
                else
                    Console.WriteLine("треугольник 2 с заданными длинами сторон не существует");



                Console.WriteLine("Нажмите любую клавишу, чтобы продолжить. Для выхода нажмите N : ");

                if (Console.ReadKey().Key == ConsoleKey.N)
                {
                    break;
                }

            }


            Console.ReadKey();
        }
    }
}
