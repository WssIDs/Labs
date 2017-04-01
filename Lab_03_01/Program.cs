using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/* Вариант 2

    1. Создать абстрактный класс Stud с методами, позволяющим вывести на экран информацию
        о персоне, а также определить ее возраст (на момент текущей даты).
    2. Создать производные классы: Абитуриент (фамилия, дата рождения, факультет), Студент
        (фамилия, дата рождения, факультет, курс), Преподавать (фамилия, дата рождения, факультет,
        должность, стаж), со своими методами вывода информации на экран, и определения возраста. 
    3. Создать базу (массив) из n персон, вывести полную информацию из базы на экран, а также
        организовать поиск персон, чей возраст попадает в заданный диапазон.
*/



namespace Lab_03_01
{

    /// <summary>
    /// Абстрактный класс Stud
    /// </summary>
    [Serializable]
    abstract class Stud
    {
        public Stud()
        {
            SetIndex(0);
        }
        public Stud(string lastName, DateTime birthDay)
        {
            this.lastName = lastName;
            this.birthDay = birthDay;
            SetIndex(0);
        }

        /// <summary>
        /// сериализация класса
        /// </summary>
        public void Serialize(FileStream fs)
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, this);
            fs.Flush();
        }

        /// <summary>
        /// десериализация класса
        /// </summary>
        abstract public void Deserialize(FileStream fs);

        /// <summary>
        /// Фамилия
        /// </summary>
        public string lastName;

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime birthDay;

        /// <summary>
        /// Имя факультета
        /// </summary>
        public string faculty;

        /// <summary>
        /// Номер курса
        /// </summary>
        public int course;

        /// <summary>
        /// Должность
        /// </summary>
        public string position;

        /// <summary>
        /// Стаж
        /// </summary>
        public int experience;

        /// <summary>
        /// Индекс класса
        /// </summary>
        private int index;

        /// <summary>
        /// показать все данные
        /// </summary>
        abstract public void Show();


        /// <summary>
        /// получить все данные класса
        /// </summary>
        /// <returns>данные класса в строку</returns>
        abstract public string GetDataForSave();

        /// <summary>
        /// получить текущий индекс
        /// </summary>
        virtual public int GetIndex()
        {
            return index;
        }

        /// <summary>
        /// Установить новый индекс
        /// </summary>
        /// <param name="idx">индекс</param>
        virtual public void SetIndex(int idx)
        {
            index = idx;
        }

        /// <summary>
        /// Опредеделить возраст
        /// </summary>
        /// <returns>значение возраста</returns>
        public int GetNumberOfYears()
        {
            DateTime dateNow = DateTime.Today;

            int year = dateNow.Year - birthDay.Year;
            if (dateNow.Month<birthDay.Month || (dateNow.Month == birthDay.Month && dateNow.Day<birthDay.Day)) year--;

            return year;
        }

        /// <summary>
        /// Получить дату рождения со временем
        /// </summary>
        /// <returns>дата рождения</returns>
        virtual public DateTime GetBirthDay()
        {
            return birthDay;
        }

        /// <summary>
        /// Находится ли возраст в диапазоне
        /// </summary>
        /// <param name="first">начало диапазона</param>
        /// <param name="last">конец диапазона</param>
        public bool IsInRange(int first, int last)
        {
            return (GetNumberOfYears() >= first && GetNumberOfYears() <= last) ? true : false;
        }

    }

    /// <summary>
    /// Класс абитуриент
    /// </summary>
    [Serializable]
    class Abiturient : Stud
    {
        public Abiturient()
        {
            SetIndex(1);
        }

        public Abiturient(string lastName, DateTime birthDay, string faculty): base (lastName,birthDay)
        {
            this.faculty = faculty;
            SetIndex(1);

        }


        /// <summary>
        /// Десериализация класса
        /// </summary>
        public override void Deserialize(FileStream fs)
        {
            BinaryFormatter bf = new BinaryFormatter();
            Abiturient deserialized = (Abiturient)bf.Deserialize(fs);
            lastName = deserialized.lastName;
            birthDay = deserialized.birthDay;
            faculty = deserialized.faculty;
        }



        /// <summary>
        /// Показать все данные
        /// </summary>
        public override void Show()
        {
            Console.WriteLine("Абитуриент:\nФамилия: {0}\nДата рождения: {1}\nФакультет: {2}", lastName, birthDay.ToShortDateString(), faculty);
        }

        /// <summary>
        /// Получить все данные класса
        /// </summary>
        /// <returns>данные класса в строку</returns>
        public override string GetDataForSave()
        {
            string delim = "|"; 
            return lastName + delim + birthDay.ToShortDateString() + delim + faculty;
        }
    }

    /// <summary>
    /// Класс Студент
    /// </summary>
    [Serializable]
    class Student : Stud
    {
        public Student()
        {
            SetIndex(2);
        }

        public Student(string lastName, DateTime birthDay, string faculty, int course) : base(lastName, birthDay)
        {
            this.faculty = faculty;
            this.course = course;
            SetIndex(2);
        }


        /// <summary>
        /// Десериализация класса
        /// </summary>
        public override void Deserialize(FileStream fs)
        {
            BinaryFormatter bf = new BinaryFormatter();
            Student deserialized = (Student)bf.Deserialize(fs);
            lastName = deserialized.lastName;
            birthDay = deserialized.birthDay;
            faculty = deserialized.faculty;
            course = deserialized.course;
        }

        /// <summary>
        /// Показать все данные
        /// </summary>
        public override void Show()
        {
            Console.WriteLine("Студент:\nФамилия: {0}\nДата рождения: {1}\nФакультет: {2}\nКурс: {3}", lastName, birthDay.ToShortDateString(), faculty, course);
        }

        /// <summary>
        /// Получить дату рождения
        /// </summary>
        /// <returns>дата рождения</returns>
        public override DateTime GetBirthDay()
        {
            return birthDay.Date;
        }

        /// <summary>
        /// Получить все данные класса
        /// </summary>
        /// <returns>данные класса в строку</returns>
        public override string GetDataForSave()
        {
            string delim = "|";
            return lastName + delim + birthDay.ToShortDateString() + delim + faculty + delim + course.ToString();
        }

    }

    /// <summary>
    /// Класс преподаватель
    /// </summary>
    [Serializable]
    class Teacher : Stud
    {
        public Teacher()
        {
            SetIndex(3);
        }

        public Teacher(string lastName, DateTime birthDay, string faculty, string position, int experience) : base(lastName,birthDay)
        {
            this.faculty = faculty;
            this.position = position;
            this.experience = experience;
            SetIndex(3);
        }

        /// <summary>
        /// Десериализация класса
        /// </summary>
        public override void Deserialize(FileStream fs)
        {
            BinaryFormatter bf = new BinaryFormatter();
            Teacher deserialized = (Teacher)bf.Deserialize(fs);
            lastName = deserialized.lastName;
            birthDay = deserialized.birthDay;
            faculty = deserialized.faculty;
            position = deserialized.position;
            experience = deserialized.experience;
        }

        /// <summary>
        /// Показать все данные
        /// </summary>
        public override void Show()
        {
            Console.WriteLine("Преподаватель :\nФамилия: {0}\nДата рождения: {1}\nФакультет: {2}\nДолжность: {3}\nСтаж: {4}", lastName, birthDay.ToShortDateString(), faculty, position, experience);
        }

        /// <summary>
        /// Получить все данные класса
        /// </summary>
        /// <returns>данные класса в строку</returns>
        public override string GetDataForSave()
        {
            string delim = "|";
            return lastName + delim + birthDay.ToShortDateString() + delim + faculty + delim + position+ delim + experience;
        }

    }


    class Program
    {
        /// <summary>
        /// Сериализация данных
        /// </summary>
        static void SaveData()
        {
            Stud[] studOb = AddData(); 

            using (FileStream fs = new FileStream("Serialize.bin", FileMode.OpenOrCreate, FileAccess.Write))
            {
                foreach (Stud ob in studOb)
                {
                    ob.Serialize(fs);
                }
            }
        }

        /// <summary>
        /// Десериализация данных
        /// </summary>
        /// <returns>массив объектов Stud</returns>
        static Stud[] LoadData()
        {
            Stud dab = new Abiturient();
            Stud dst = new Student();
            Stud dteach = new Teacher();
            Stud dst2 = new Student();

            using (FileStream fs1 = new FileStream("Serialize.bin", FileMode.OpenOrCreate, FileAccess.Read))
            {             
                dab.Deserialize(fs1);
                dst.Deserialize(fs1);
                dteach.Deserialize(fs1);
                dst2.Deserialize(fs1);
            }

            Stud[] studOb = new Stud[4];

            studOb[0] = dab;
            studOb[1] = dst;
            studOb[2] = dteach;
            studOb[3] = dst2;

            return studOb;
        }


        /// <summary>
        /// Сохранение данных класса в текстовый файл
        /// </summary>
        /// <param name="studOb">массив объектов класса Stud</param>
        static void Save(Stud[] studOb)
        {
            if (!File.Exists("data.txt"))
            {
                try
                {

                    FileStream fs = new FileStream("data.txt", FileMode.OpenOrCreate);
                    StreamWriter writer = new StreamWriter(fs);

                    foreach (Stud ob in studOb)
                    {
                        writer.WriteLine(ob.GetIndex() +"|"+ ob.GetDataForSave());
                    }

                    writer.Flush();
                    writer.Dispose();
                    fs.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            else
            {
                foreach (Stud ob in studOb)
                {
                   try
                    {
                        FileStream fs = new FileStream("data.txt", FileMode.Append);
                        StreamWriter writer = new StreamWriter(fs);
                        writer.WriteLine(ob.GetIndex()+"|"+ob.GetDataForSave());
                        writer.Flush();
                        writer.Dispose();
                        fs.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// Загрузить данные из текстового файла
        /// </summary>
        /// <returns>массив объектов Stud</returns>
        static Stud[] Load()
        {
            if (!File.Exists("data.txt"))
            {
                Console.WriteLine("Файл не существует");

                return null;
            }

            else
            {
                try
                {
                    string[] mass = File.ReadAllLines("data.txt");

                    Stud[] studOb = new Stud[mass.Length];

                    int n = 0;

                    foreach (string m in mass)
                    {
                        char[] sep = {'|'};

                        string[] mass_s = m.Split(sep, StringSplitOptions.RemoveEmptyEntries);

                        try
                        {
                            if (Convert.ToInt32(mass_s[0]) == 1)
                            {
                                //Console.WriteLine("Абитуриент");
                                try
                                {
                                    studOb[n] = new Abiturient(mass_s[1], Convert.ToDateTime(mass_s[2]), mass_s[3]);
                                    n++;
                                }
                                catch (FormatException ex)
                                {
                                    Console.WriteLine("Строка:{0} - {1}",n+1,ex.Message);
                                    //return null;
                                }
                            }
                            else if (Convert.ToInt32(mass_s[0]) == 2)
                            {
                                //Console.WriteLine("Студент");

                                try
                                {
                                    studOb[n] = new Student(mass_s[1], Convert.ToDateTime(mass_s[2]), mass_s[3], Convert.ToInt32(mass_s[4]));
                                    n++;
                                }
                                catch (FormatException ex)
                                {
                                    Console.WriteLine("Строка:{0} - {1}", n + 1, ex.Message);
                                    //return null;
                                }
                            }
                            else if (Convert.ToInt32(mass_s[0]) == 3)
                            {
                                //Console.WriteLine("Учитель");

                                try
                                {
                                    studOb[n] = new Teacher(mass_s[1], Convert.ToDateTime(mass_s[2]), mass_s[3], mass_s[4], Convert.ToInt32(mass_s[5]));
                                    n++;
                                }
                                catch (FormatException ex)
                                {
                                    Console.WriteLine("Строка:{0} - {1}", n + 1, ex.Message);
                                    //return null;
                                }
                            }

                            else
                            {
                                Console.WriteLine("Строка {0}: неверное значение \"{1}\". Значение первого символа должно быть от 1 до 3",n + 1,mass_s[0]);
                            }
                        }
                        catch(FormatException ex)
                        {
                            Console.WriteLine("Строка {0}: значение \"{1}\" должно быть быть целым числом - {2}",n + 1,mass_s[0], ex.Message);
                            //return null;
                        }
                    }

                    return studOb;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    return null;
                }
            }
        }


        /// <summary>
        /// Добавить данные (для тестов)
        /// </summary>
        /// <returns>массив объектов Stud</returns>
        static Stud[] AddData()
        {
            Abiturient ab = new Abiturient();

            ab.birthDay = new DateTime(1990, 12, 29);

            ab.lastName = "Володько";

            ab.faculty = "Инженерный";

            ///////////////////////////////

            Student st = new Student();

            st.birthDay = new DateTime(1989, 10, 01);

            st.lastName = "Иванов";

            st.faculty = "Инженерный";

            st.course = 2;

            //////////////////////////////

            Teacher teach = new Teacher();

            teach.lastName = "Васильев";

            teach.birthDay = new DateTime(1980, 09, 05);

            teach.faculty = "Иностранных языков";

            teach.position = "Заведующий кафедры";

            teach.experience = 10;

            /////////////////////////////////////////

            Student st2 = new Student();

            st2.birthDay = new DateTime(1996, 06, 11);

            st2.lastName = "Петров";

            st2.faculty = "Инженерный";

            st2.course = 4;

            Stud[] studOb = new Stud[4];

            studOb[0] = ab;
            studOb[1] = st;
            studOb[2] = teach;
            studOb[3] = st2;

            return studOb;
        }

        static void Main(string[] args)
        {
            while (true)
            {
                int inputMenu = 0;

                Console.WriteLine();
                Console.WriteLine("МЕНЮ");
                Console.WriteLine("1 - Сохранить данные данные");
                Console.WriteLine("2 - Считать данные из текстового файла данные и вывести на экран");
                Console.WriteLine("3 - Поиск по заданному диапазону");
                Console.WriteLine("________________________________");
                Console.WriteLine("4 - Сериализация данных");
                Console.WriteLine("5 - Десериализация данных");
                Console.WriteLine("0 - Выход");
                Console.Write("Выберите пункт меню: ");

                try
                {
                    inputMenu = int.Parse(Console.ReadLine());

                    if (inputMenu == 1)
                    {
                        Stud[] studOb = AddData();

                        Save(studOb);
                    }

                    else if (inputMenu == 2)
                    {
                        Stud[] studOb = Load();

                        if (studOb != null)
                        {
                            foreach (Stud ab in studOb)
                            {
                                if (ab != null)
                                {
                                    ab.Show();
                                    Console.WriteLine("Количество лет: {0}\n", ab.GetNumberOfYears());
                                }
                            }
                        }
                    }

                    else if (inputMenu == 3)
                    {

                        Stud[] studOb = Load();

                        Console.WriteLine("Введите диапазон возраста для поиска:");
                        Console.Write("a:");
                        int a = int.Parse(Console.ReadLine());
                        Console.Write("b:");
                        int b = int.Parse(Console.ReadLine());

                        Console.WriteLine();
                        Console.WriteLine("Записи, которые попадают в заданный диапазон возраста:");
                        Console.WriteLine("______________________________________________________");

                        if (studOb != null)
                        {
                            foreach (Stud ob in studOb)
                            {
                                if (ob.IsInRange(a, b))
                                {
                                    ob.Show();
                                    Console.WriteLine("Количество лет: {0}\n", ob.GetNumberOfYears());
                                }
                            }
                        }
                    }

                    else if (inputMenu == 4)
                    {
                        SaveData();
                    }

                    else if (inputMenu == 5)
                    {
                        Stud[] studOb = LoadData();

                        if (studOb != null)
                        {
                            foreach (Stud ab in studOb)
                            {
                                if (ab != null)
                                {
                                    ab.Show();
                                    Console.WriteLine("Количество лет: {0}\n", ab.GetNumberOfYears());
                                }
                            }
                        }
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
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

            Console.ReadKey();
        }
    }
}
