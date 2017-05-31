using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

/*
    Вариант 2.
    
    На основе данных входного файла составить список студентов группы, включив следующие данные:
    ФИО, номер группы, результаты сдачи трех экзаменов.
    Вывести в новый файл информацию о студентах, успешно сдавших сессию, отсортировав по номеру группы. 

    + во всех вариантах подразумевается, что исходная информация хранится в текстовом файле input.txt, каждая строка которого содержит полную информацию о некотором объекте, результирующая информация должна быть записана в файл output.txt;
    + для хранения данных внутри программы организовать массив структур;
    + в типе структура реализуется метод CompareTo() интерфейса IComparable, перегружается метод ToString() базового класса object и необходимые операции отношения, поля данных и дополнительные методы продумайте самостоятельно
*/


namespace Lab_05_01
{
    /// <summary>
    /// Структура студент
    /// </summary>
    public struct Student : IComparable
    {
        public Student(string name, int group, string address, int subject1, int subject2, int subject3)
            : this()
        {
            Name = name;
            Group = group;
            Address = address;
            Subject1 = subject1;
            Subject2 = subject2;
            Subject3 = subject3;
        }

        /// <summary>
        /// ФИО
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; private set; }

        /// <summary>
        /// Номер группы
        /// </summary>
        public int Group { get; private set; }
        
        /// <summary>
        /// Оценка по предмету 1
        /// </summary>
        public int Subject1 { get; private set; }
        
        /// <summary>
        /// Оценка по предмету 2
        /// </summary>
        public int Subject2 { get; private set; }
        
        /// <summary>
        /// Оценка по предмету 3
        /// </summary>
        public int Subject3 { get; private set; }


        public int CompareTo(object obj)
        {
            return Group.CompareTo(((Student)obj).Group);
        }

        /// <summary>
        /// Расчет среднего балла по экзаменам
        /// </summary>
        /// <returns>средний балл</returns>
        public double Average()
        {
            return (Subject1 + Subject2 + Subject3) / 3.0f;
        }

        public override string ToString()
        {
            return string.Format("{0,35} | {1,1} | {2,1} | {3,1} | {4,1} | {5,1} | {6,4:0.##}",
                                 Name, Group, Address, Subject1, Subject2, Subject3,Average());
        }
    }


    class Program
    {
        private static void Main()
        {
            int min = 4;

            string[] lines = File.ReadAllLines("input.txt");
            
            List<Student> students = new List<Student>();

            for (int index = 0; index < lines.Length; index++)
            {
                string line = lines[index];
                string[] fields = line.Split(';');
                try
                {
                    Student student = new Student(fields[0], Convert.ToInt32(fields[1]), fields[2], Convert.ToInt32(fields[3]), Convert.ToInt32(fields[4]), Convert.ToInt32(fields[5]));
                    students.Add(student);
                }
                catch(FormatException ex)
                {
                    Console.WriteLine("Строка {0} - {1}: проверьте корректность ввода данных в файле input.txt",index+1,ex.Message);
                }
            }

            Console.WriteLine("Данные до сортировки");

            // Выводим данные до сортировки
            foreach (Student student in students)
            {
                Console.WriteLine(student);
            }

            Console.WriteLine();
            Console.WriteLine();

            students.Sort();

            List<string> linesToSave = new List<string>();

            Console.WriteLine("Данные после сортировки");

            // Выводим данные
            foreach (Student student in students)
            {
                if (student.Subject1 >= min && student.Subject2 >= min && student.Subject3 >= min)
                {
                    Console.WriteLine(student);
                    linesToSave.Add(student.ToString());
                }
            }

            // Сохраняем в файл
            File.WriteAllLines("output.txt", linesToSave.ToArray());
            Console.ReadKey();
        }
    }
}
