using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

/**
    Володько В.И. 60331-1

        – в лабораторной работе требуется определить класс, содержащий типизированную
           коллекцию, который с помощью событий извещает об изменениях в коллекции.
        – коллекция состоит из объектов ссылочных типов. Коллекция изменяется при
           удалении/добавлении элементов или при изменении одной из входящих в коллекцию
           ссылок, например, когда одной из ссылок присваивается новое значение. В этом случае в
           соответствующих методах или свойствах класса бросаются события.
        – при изменении данных объектов, ссылки на которые входят в коллекцию, значения самих
           ссылок не изменяются. Этот тип изменений не порождает событий.
        – для событий, извещающих об изменениях в коллекции, определяется свой делегат.
           События регистрируются в специальных классах-слушателях.

 */


namespace Lab_06_01
{
    /// <summary>
    /// Элемент меню
    /// </summary>
    public struct MenuItem
    {
        /// <summary>
        /// Текст элемента меню
        /// </summary>
        public string MenuText;
        /// <summary>
        /// Позиция элемента меню
        /// </summary>
        public int menuPosition;

        public MenuItem(string text, int position)
        {
            MenuText = text;
            menuPosition = position;
        }
    }

    /// <summary>
    /// Класс Меню
    /// </summary>
    class Menu
    {
        /// <summary>
        /// Коллекция элементов меню
        /// </summary>
        public List<MenuItem> menuItems = new List<MenuItem>();

        /// <summary>
        /// Позиция курсора (private)
        /// </summary>
        private int position = 0;

        /// <summary>
        /// Свойство для возвращения позиции курсора (readonly)
        /// </summary>
        public int Position
        {
            get { return position; }
        }


        /// <summary>
        /// Обработка нажатия кнопки вверх
        /// </summary>
        public void UpPosition()
        {
            if (position > 0) position--;
            else position = menuItems.Count - 1;
            Show(position, 8);
        }

        /// <summary>
        /// Обработка нажатия кнопки вниз
        /// </summary>
        public void DownPosition()
        {
            if (position < menuItems.Count - 1) position++;
            else position = 0;
            Show(position, 8);
        }

        /// <summary>
        /// Вывод текста в меню
        /// </summary>
        /// <param name="leftPosition">левая позиция курсора</param>
        /// <param name="topPosition">верхняя позиция курсора</param>
        /// <param name="inText">Текст</param>
        public void ShowCustomText(int leftPosition, int topPosition, string inText)
        {
            Show(position, 8);
            Console.SetCursorPosition(leftPosition, topPosition);
            Console.Write(inText);
            Console.SetCursorPosition(79, 24);
        }

        /// <summary>
        /// Отображение меню с учетом позиции курсора
        /// </summary>
        /// <param name="position">позиция курсора</param>
        /// <param name="startPos">начальная позиция меню</param>
        public void Show(int position, int startPos)
        {
            Console.Clear();
            Console.SetCursorPosition(31, startPos++);
            Console.WriteLine("ГЛАВНОЕ МЕНЮ");
            Console.SetCursorPosition(30, startPos++);
            Console.WriteLine(new string('-', 20));


            for (int i = 0; i < menuItems.Count; ++i)
            {
                Console.SetCursorPosition(23, menuItems[i].menuPosition);
                Console.WriteLine(menuItems[i].MenuText);
            }

            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;


            Console.SetCursorPosition(23, menuItems[position].menuPosition);
            Console.WriteLine(menuItems[position].MenuText);

            Console.ResetColor();
            Console.SetCursorPosition(79, 24);
        }

        /// <summary>
        /// Конструктор класса меню
        /// </summary>
        /// <param name="items">Коллекция элементов меню</param>
        public Menu(List<MenuItem> items)
        {
            menuItems = items;

            Console.Title = "Лабораторная №6";
            Console.SetWindowSize(80, 25);
            Console.SetBufferSize(80, 25); // width, height
            Console.Clear();
        }
    }




    /// <summary>
    /// Структура для зачетов и экзаменов
    /// </summary>
    public struct ZachetItem
    {
        /// <summary>
        /// Наименование предмета
        /// </summary>
        public string Name;

        /// <summary>
        /// Зачтено
        /// </summary>
        public bool Mark;

        public ZachetItem(string name, bool mark) : this()
        {
            this.Name = name;
            this.Mark = mark;
        }
    }

    /// <summary>
    /// Структура для экзаменов
    /// </summary>
    public struct ExamItem
    {
        /// <summary>
        /// Наименование предмета
        /// </summary>
        public string Name;

        /// <summary>
        /// Оценка по предмету
        /// </summary>
        public int Mark;

        public ExamItem(string name, int mark) : this()
        {
            this.Name = name;
            this.Mark = mark;
        }
    }

    /// <summary>
    /// Класс Студент
    /// </summary>
    public class Student
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Форма обучения
        /// </summary>
        public string FormOfTraining { get; private set; }

        /// <summary>
        /// Номер группы
        /// </summary>
        public int Group { get; private set; }

        /// <summary>
        /// Список зачетов
        /// </summary>
        public ArrayList ListZachet { get; private set; }

        /// <summary>
        /// Список экзаменов
        /// </summary>
        public ArrayList ListExams { get; private set; }

        /// <summary>
        /// Конструктор класса Student
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="formoftraining">Вид обучения</param>
        /// <param name="group">Номер группы</param>
        /// <param name="zachet">Список зачетов</param>
        /// <param name="exam">Список экзаменов</param>
        public Student(string name, string formoftraining, int group, ArrayList zachet, ArrayList exam)
        {
            Name = name;
            FormOfTraining = formoftraining;
            Group = group;
            ListZachet = zachet;
            ListExams = exam;
        }

    }

    public class StudentListHandlerEventArgs : System.EventArgs
    {
        // Поля
        //private string m_Name = string.Empty;
        //private string m_TypeChange = string.Empty;
        //private Student m_Student = null;

        // Конструктор
        public StudentListHandlerEventArgs(string name, string typechange, Student student)
        {
            Name = name;
            TypeChange = typechange;
            Student = student;
        }
        

        public string Name { get; private set; }

        public string TypeChange { get; private set; }

        public Student Student { get; private set; }

        public override string ToString()
        {
            string out_txt = string.Empty;

            out_txt += string.Format("{0}|{1}|{2}|\n", Student.Name, Student.Group, Student.FormOfTraining);

            out_txt += "Зачеты\n";

            foreach (ZachetItem zi in Student.ListZachet)
            {
                out_txt += zi.Name + ":" + zi.Mark + "\n";
            }

            out_txt += "Экзамены\n";

            foreach (ExamItem ei in Student.ListExams)
            {
                out_txt += ei.Name + ":" + ei.Mark + "\n";
            }

            out_txt += "\n";

            return out_txt;
        }
    }

    public delegate void StudentListHandler(object source, StudentListHandlerEventArgs args);

    public class StudentCollection
    {
        public StudentCollection(string name)
        {
            this.NameCollection = name;
        }

        public event StudentListHandler StudentsCountChanged;

        public event StudentListHandler StudentReferenceChanged;

        private List<Student> students = new List<Student>();

        private string NameCollection;

        /// <summary>
        /// Добавить по умолчанию студентов в коллекцию
        /// </summary>
        public void AddDefaults()
        {
            Student student1 = new Student("Володько", "Заочное", 60331, new ArrayList { new ZachetItem("ООП", true), new ZachetItem("Менеджмент в ИТ", true) }, new ArrayList { new ExamItem("ТКОПИС", 6), new ExamItem("ОПБД", 8), new ExamItem("ОАПЯВУ", 9) });

            students.Add(student1);

                if (StudentsCountChanged != null)
                    StudentsCountChanged(this, new StudentListHandlerEventArgs(NameCollection, "Добавление", student1));

                if (StudentReferenceChanged != null)
                    StudentReferenceChanged(this, new StudentListHandlerEventArgs(NameCollection, "Изменение значение ссылки", student1));

            Student student2 = new Student("Петров", "Дневное", 50922, new ArrayList { new ZachetItem("ООП", false), new ZachetItem("Менеджмент в ИТ", true) }, new ArrayList { new ExamItem("ТКОПИС", 6) });

            students.Add(student2);

            if (StudentsCountChanged != null)
                StudentsCountChanged(this, new StudentListHandlerEventArgs(NameCollection, "Добавление", student2));

            if (StudentReferenceChanged != null)
                StudentReferenceChanged(this, new StudentListHandlerEventArgs(NameCollection, "Изменение значение ссылки", student2));

        }

        /// <summary>
        /// Добавить в коллекцию студнетов
        /// </summary>
        /// <param name="args">массив класса Student</param>
        public void AddStudents(params Student[] args)
        {
                foreach (Student s in args)
                {
                    students.Add(s);

                    if (StudentsCountChanged != null)
                        StudentsCountChanged(this, new StudentListHandlerEventArgs(NameCollection, "Добавление", s));

                    if (StudentReferenceChanged != null)
                        StudentReferenceChanged(this, new StudentListHandlerEventArgs(NameCollection, "Изменение значение ссылки", s));
            }
        }

        /// <summary>
        /// Вывод всех записей коллекции включая экзамены и зачеты
        /// </summary>
        /// <returns>Данные коллекции students</returns>
        public override string ToString()
        {
            string out_txt = string.Empty;

            foreach (Student st in students)
            {
                out_txt += string.Format("{0}|{1}|{2}|",st.Name,st.Group,st.FormOfTraining);

                foreach (ZachetItem zi in st.ListZachet)
                {
                    out_txt += zi.Name + ":" + zi.Mark + " | ";
                }

                foreach (ExamItem ei in st.ListExams)
                {
                    out_txt += ei.Name + ":" + ei.Mark + " | ";
                }

                out_txt += "\n";
            }

            return out_txt;
        }

        /// <summary>
        /// Вывод всех записей коллекции students (cредний бал, количество зачетов и экзаменов)
        /// </summary>
        /// <returns>Данные коллекции students</returns>
        public string ToShortString()
        {
            string out_txt = string.Empty;

            foreach (Student st in students)
            {
                int summ = 0;

                foreach(ExamItem ei in st.ListExams)
                {
                    summ += ei.Mark;
                }

                double average = summ / st.ListExams.Count;
 
                out_txt += st.Name + " | " + st.Group + " | " + st.FormOfTraining + " | Количество зачетов: " + st.ListZachet.Count + " | Количество экзаменов: " + st.ListExams.Count + " | Количество зачетов: " + st.ListZachet.Count + " | Средний балл: " + average + " |\n";            }

            return out_txt;
        }

        /// <summary>
        /// Удаление из коллекции
        /// </summary>
        /// <param name="j">индекс</param>
        /// <returns>true - если успешное удаление, false - если ошибка удаления</returns>
        public bool Remove(int j)
        {
            if (students.Count > 0)
            {
                Student stud = students[j];

                students.RemoveAt(j);

                if (StudentsCountChanged != null)
                    StudentsCountChanged(this, new StudentListHandlerEventArgs(NameCollection, "Удаление", stud));

                if (StudentReferenceChanged != null)
                    StudentReferenceChanged(this, new StudentListHandlerEventArgs(NameCollection, "Изменение значение ссылки", stud));
            }

            else
            {
                return false;
            }

            return true;
        }

        // Индексатор
        public Student this[int index]
        {
            set
            {
                students[index] = value;

                if (StudentReferenceChanged != null)
                    StudentReferenceChanged(this, new StudentListHandlerEventArgs(NameCollection, "Изменение значения ссылки", students[index]));
            }

            get
            {
                return students[index];
            }
            
        }
    }

    public class JournalEntry
    {
        // Конструктор
        public JournalEntry(string name, string typechange, string student)
        {
            Name = name;
            TypeChange = typechange;
            Student = student;
        }

        public string Name { get; private set; }

        public string TypeChange { get; private set; }

        public string Student { get; private set; }

        public override string ToString()
        {
            return Name+"\n"+TypeChange+"\n"+Student;
        }

    }

    /// <summary>
    /// Класс слушатель
    /// </summary>
    public class Journal
    {
        private List<JournalEntry> journalEntries = new List<JournalEntry>();

        private string Name;

        public Journal(string name)
        {
            this.Name = name;
        }

        public void OnStudentsCountChanged(object source, StudentListHandlerEventArgs args)
        {
            Console.WriteLine("Событие: "+Name);
            Console.WriteLine("{0}\n{1}\n{2}", args.Name, args.TypeChange, args.ToString());

            journalEntries.Add(new JournalEntry(args.Name,args.TypeChange,args.ToString()));
        }

        public void OnStudentReferenceChanged(object source, StudentListHandlerEventArgs args)
        {

            //Student st = source as Student;

            Console.WriteLine("Событие: " + Name);
            Console.WriteLine("{0}\n{1}\n{2}", args.Name, args.TypeChange, args.ToString());

            journalEntries.Add(new JournalEntry(args.Name, args.TypeChange, args.ToString()));
        }

        /// <summary>
        /// Вывод всех записей journalEntries
        /// </summary>
        /// <returns>Данные коллекции journalEntries</returns>
        public override string ToString()
        {
            string out_txt = string.Empty;

            foreach (JournalEntry je in journalEntries)
            {
                out_txt += je.ToString();
            }

            return out_txt;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            //StudentCollection studcol1 = new StudentCollection("Коллекция 1");
            //StudentCollection studcol2 = new StudentCollection("Коллекция 2");

            //Journal journal1 = new Journal("Журнал 1");
            //Journal journal2 = new Journal("Журнал 2");

            //studcol1.StudentsCountChanged += journal1.OnStudentReferenceChanged;
            //studcol1.StudentReferenceChanged += journal1.OnStudentReferenceChanged;

            //studcol1.StudentReferenceChanged += journal2.OnStudentReferenceChanged;
            //studcol2.StudentReferenceChanged += journal2.OnStudentReferenceChanged;

            ///// Добавление в коллекцию значений по умолчанию

            //studcol1.AddDefaults();

            //Student student1 = new Student("Иванов", "Дневное", 53422, new ArrayList { new ZachetItem("ООП", true) }, new ArrayList { new ExamItem("ТКОПИС", 8) });
            //Student student2 = new Student("Васильев", "Заочное", 43522, new ArrayList { new ZachetItem("Менеджмент ИТ", false) }, new ArrayList { new ExamItem("ОПБД", 5) });

            ///// Добавление в коллекцию через параметры

            //studcol2.AddStudents(student1,student2);

            //Student student3 = new Student("Каржак", "Дистанционное", 40034, new ArrayList { new ZachetItem("Менеджмент ИТ", true) }, new ArrayList { new ExamItem("ОПБД", 9) });

            ///// Изменение значения ссылок через индексатор

            //studcol2[1] = student3;
            //studcol1[1] = student3;

            ///// Удаление из коллекции

            //studcol1.Remove(0);
            //studcol2.Remove(1);



            //Console.WriteLine("Вывод журнала 1");
            //Console.WriteLine("_______________");
            //Console.WriteLine(journal1.ToString());

            //Console.WriteLine("Вывод журнала 2");
            //Console.WriteLine("_______________");
            //Console.WriteLine(journal2.ToString());

            //Console.ReadKey();
        }
    }
}
