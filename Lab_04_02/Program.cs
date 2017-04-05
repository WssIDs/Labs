using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab_04_02
{

    public struct MenuItem
    {
        public string MenuText;
        public int menuPosition;

        public MenuItem(string text, int position)
        {
            MenuText = text;
            menuPosition = position;
        }
    }

    class Menu
    {
        public List<MenuItem> menuItems = new List<MenuItem>();

        // Позиция курсора (private)
        private int position = 0;


        public bool bSubMenu = false;

        // Свойство для возвращения позиции курсора (readonly)
        public int Position
        {
            get { return position; }
        }


        // Обработка нажатия кнопки вверх
        public void UpPosition()
        {
            if (position > 0) position--;
            else position = menuItems.Count-1;
            Show(position,8);
        }

        // Обработка нажатия кнопки вниз
        public void DownPosition()
        {
            if (position < menuItems.Count-1) position++;
            else position = 0;
            Show(position,8);
        }


        // Вывод на экран результата действия
        public void ShowAction(int action, object data)
        {
            switch (action)
            {
                case 1:
                    string[] array1D = (string[])data;
                    Console.SetCursorPosition(30, 17);
                    int b = 17;
                    foreach (string x in array1D)
                    {
                        Console.SetCursorPosition(30, b++);
                        Console.WriteLine(x);
                    }
                    Console.SetCursorPosition(79, 24);
                    break;
                case 2:
                    int[][] array2D = (int[][])data;
                    Console.SetCursorPosition(30, 17);
                    for (int i = 0; i < array2D.Length; i++, Console.WriteLine(), Console.Write(new string(' ', 30)))
                        for (int j = 0; j < array2D[i].Length; j++)
                            Console.Write("{0,3}", array2D[i][j]);
                    Console.SetCursorPosition(79, 24);
                    break;
                case 3:
                    Console.SetCursorPosition(30, 18);
                    Console.Write("Одномерный массив создан");
                    Console.SetCursorPosition(79, 24);
                    break;
                case 4:
                    Console.SetCursorPosition(30, 18);
                    Console.Write("Двумерный массив создан");
                    Console.SetCursorPosition(79, 24);
                    break;
                case 5:
                    Console.SetCursorPosition(30, 18);
                    Console.Write(" Массив записан в файл ");
                    Console.SetCursorPosition(79, 24);
                    break;
            }
        }

        public void ShowCustomText(int leftPosition, int topPosition, string inText)
        {
            Show(position,8);
            Console.SetCursorPosition(leftPosition, topPosition);
            Console.Write(inText);
            Console.SetCursorPosition(79, 24);
        }

        // Отображение меню с учетом позиции курсора
        public void Show(int position,int startPos)
        {
            Console.Clear();
            Console.SetCursorPosition(31, startPos++);
            Console.WriteLine("ГЛАВНОЕ МЕНЮ");
            Console.SetCursorPosition(30, startPos++);
            Console.WriteLine(new string('-', 20));


            for (int i=0; i< menuItems.Count; ++i)
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

        // Конструктор класса меню
        public Menu(List<MenuItem> items)
        {
            menuItems = items;

            Console.Title = "Лабораторная №4";
            Console.SetWindowSize(80, 25);
            Console.SetBufferSize(80, 25); // width, height
            Console.Clear();
        }
    }


    /// <summary>
    /// Класс расширение для работы с датой
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Попадает ли дата в указанный диапазон дат
        /// </summary>
        /// <param name="inDate">входящая дата</param>
        /// <param name="startDate">начальная дата</param>
        /// <param name="endDate">конечная дата</param>
        public static bool IsInRange(DateTime inDate, DateTime startDate, DateTime endDate)
        {
            return (inDate >= startDate && inDate <= endDate);
        }
    }

    class Program
    {

        static System.Collections.Specialized.StringCollection log = new System.Collections.Specialized.StringCollection();

        /// <summary>
        /// список директорий 
        /// </summary>
        static List<FileSystemInfo> directories = new List<FileSystemInfo>();

        /// <summary>
        /// Поиск файлов и каталогов в заданной директории
        /// </summary>
        /// <param name="root">директория поиска</param>
        /// <param name="pattern">маска поиска</param>
        static void WalkDirectoryTree(DirectoryInfo root, string pattern)
        {
            FileInfo[] files = null;
            DirectoryInfo[] subDirs = null;

            try
            {
                files = root.GetFiles(pattern);
            }

            catch (UnauthorizedAccessException e)
            {
                log.Add(e.Message);
            }

            catch (System.IO.DirectoryNotFoundException e)
            {
                Trace.WriteLine(e.Message);
            }

            if (files != null)
            {
                foreach (System.IO.FileInfo fi in files)
                {
                    directories.Add(fi);
                }

                subDirs = root.GetDirectories();

                foreach (DirectoryInfo dirInfo in subDirs)
                {
                    directories.Add(dirInfo);

                    WalkDirectoryTree(dirInfo, pattern);
                }
            }
        }

        /// <summary>
        /// Удалить файл заданный файл/каталог (под определенным номером)
        /// </summary>
        /// <param name="number">номер файла/каталога</param>
        /// <param name="inDir">Коллекция типа FileSystemInfo (список файлов и каталогов)</param>
        /// <returns>Коллекция типа FileSystemInfo (список файлов и каталогов) за исключением удаленного элемента</returns>
        static List<FileSystemInfo> DeleteCustomFilesFolders(int number, List<FileSystemInfo> inDir)
        {
            if (number > 0)
            {
                if (File.Exists(inDir[number - 1].FullName))
                {
                    FileInfo file = inDir[number - 1] as FileInfo;

                    if (file.Attributes != FileAttributes.System && file.Attributes != FileAttributes.Hidden)
                    {
                        try
                        {
                            file.Delete();
                            Trace.WriteLine("Файл удален: " + file.FullName);

                            Console.SetCursorPosition(30, 17);
                            Console.WriteLine("Файл удален: " + file.FullName);
                            Console.SetCursorPosition(79, 24);

                            inDir.RemoveAt(number - 1);
                        }
                        catch (Exception ex)
                        {
                            Trace.WriteLine("Удаление невозможно: " + ex.Message);

                            Console.SetCursorPosition(30, 17);
                            Console.WriteLine("Удаление невозможно: " + ex.Message);
                            Console.SetCursorPosition(79, 24);
                        }
                    }
                    else
                    {
                        Trace.WriteLine("Файл является системным удаление невозможно: " + file.FullName);
                    }
                }
                else if (Directory.Exists(inDir[number - 1].FullName))
                {
                    DirectoryInfo dir = inDir[number - 1] as DirectoryInfo;

                    if (dir.Attributes == FileAttributes.Directory)
                    {
                        if (dir.Attributes != FileAttributes.System && dir.Attributes != FileAttributes.Hidden)
                        {
                            try
                            {
                                dir.Delete();
                                Trace.WriteLine("Директория удалена: " + dir.FullName);

                                Console.SetCursorPosition(30, 17);
                                Console.WriteLine("Директория удалена: " + dir.FullName);
                                Console.SetCursorPosition(79, 24);

                                inDir.RemoveAt(number - 1);
                            }
                            catch (Exception ex)
                            {
                                Trace.WriteLine("Удаление невозможно: " + ex.Message);

                                Console.SetCursorPosition(30, 17);
                                Console.WriteLine("Удаление невозможно: " + ex.Message);
                                Console.SetCursorPosition(79, 24);
                            }
                        }
                        else
                        {
                            Trace.WriteLine("Директория является системной удаление невозможно: " + dir.FullName);
                        }
                    }
                }
            }

            return inDir;
        }

        /// <summary>
        /// Удаление всех файлов и каталогов 
        /// </summary>
        /// <param name="inDirs">Коллекция типа FileSystemInfo (список файлов и каталогов)</param>
        static void DeleteFilesFolders(List<FileSystemInfo> inDirs)
        {
            for (int i = 0; i < inDirs.Count; i++)
            {
                if (File.Exists(inDirs[i].FullName))
                {
                    FileInfo file = inDirs[i] as FileInfo;

                    if (file.Attributes != FileAttributes.System && file.Attributes != FileAttributes.Hidden)
                    {
                        try
                        {
                            file.Delete();
                            Trace.WriteLine("Файл удален: " + file.FullName);
                            inDirs.RemoveAt(i);
                        }
                        catch (Exception ex)
                        {
                            Trace.WriteLine("Удаление невозможно: " + ex.Message);
                        }
                    }
                    else
                    {
                        Trace.WriteLine("Файл является системным удаление невозможно: " + file.FullName);
                        inDirs.RemoveAt(i);
                    }
                }
            }

            for (int i = 0; i < inDirs.Count; i++)
            {
                if (Directory.Exists(inDirs[i].FullName))
                {
                    DirectoryInfo dir = inDirs[i] as DirectoryInfo;

                    if (dir.Attributes == FileAttributes.Directory)
                    {
                        if (dir.Attributes != FileAttributes.System && dir.Attributes != FileAttributes.Hidden)
                        {
                            try
                            {
                                dir.Delete(true);
                                Trace.WriteLine("Директория удалена: " + dir.FullName);
                            }
                            catch (Exception ex)
                            {
                                Trace.WriteLine("Удаление невозможно: " + ex.Message);
                            }
                        }
                        else
                        {
                            Trace.WriteLine("Директория является системной удаление невозможно: " + dir.FullName);
                        }
                    }
                }
            }

        }

        static void Main(string[] args)
        {
            List<MenuItem> items = new List<MenuItem>();

            items.Add(new MenuItem("  Поиск в указанном каталоге файлов        ", 10));
            items.Add(new MenuItem("  Замена указанного текста в файлах        ", 11));
            items.Add(new MenuItem("  Поиск по всему диску файлов и каталогов  ", 12));
            items.Add(new MenuItem("  Выйти из приложения                      ", 13));

            Menu menu = new Menu(items);



            menu.Show(0,8);
            ConsoleKeyInfo cki;
            do
            {
                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.UpArrow) menu.UpPosition();
                else if (cki.Key == ConsoleKey.DownArrow) menu.DownPosition();
                else if (cki.Key == ConsoleKey.Enter)
                {
                    switch (menu.Position)
                    {
                        case 1:
                            {
                                //menu.Show(menu.Position);

                                Console.SetCursorPosition(25, 17);
                                Console.Write("Введите путь к директории:");
                                Console.SetCursorPosition(25, 18);
                                string currentDirectory = Console.ReadLine();
                                Console.SetCursorPosition(25, 19);
                                Console.Write("Введите маску поиска:");

                                string mask = Console.ReadLine();
                                Console.SetCursorPosition(25, 20);
                                Console.Write("Введите диапазон даты начало:");

                                DateTime startdate = DateTime.Parse(Console.ReadLine());

                                Console.SetCursorPosition(25, 21);
                                Console.Write("Введите диапазон даты конец:");

                                DateTime endDate = DateTime.Parse(Console.ReadLine());

                                Console.SetCursorPosition(79, 24);

                                DirectoryInfo di = new DirectoryInfo(currentDirectory);
                                directories.Clear();

                                menu.ShowCustomText(30, 17, "Выполнение...");

                                WalkDirectoryTree(di, mask);

                                using (FileStream fs = new FileStream("data_1.txt", FileMode.Create))
                                {
                                    TextWriter tw = new StreamWriter(fs);

                                    foreach (FileSystemInfo file in directories)
                                    {
                                        if (File.Exists(file.FullName))
                                        {
                                            if (DateTimeExtensions.IsInRange(file.LastWriteTime, startdate, endDate))
                                            {
                                                tw.WriteLine("Дата модификации " + file.LastWriteTime.Date + ":" + file.FullName);
                                            }
                                        }
                                    }

                                    tw.Flush();
                                    tw.Close();
                                }

                                menu.ShowCustomText(10, 18, "Поиск завершен. результаты записаны в текстовый файл data_1.txt");

                                break;
                            }
                        case 2:
                            {
                                //menu.Show(menu.Position);

                                Console.SetCursorPosition(30, 17);
                                Console.Write("Введите путь к директории:");
                                Console.SetCursorPosition(30, 18);
                                string currentDirectory = Console.ReadLine();
                                Console.SetCursorPosition(30, 19);
                                Console.Write("Введите маску поиска:");
                                string mask = Console.ReadLine();
                                Console.SetCursorPosition(30, 20);
                                Console.Write("Введите текст, для поиска:");
                                string currentText = Console.ReadLine();
                                Console.SetCursorPosition(30, 21);
                                Console.Write("Введите текст под замену:");
                                string replaceText = Console.ReadLine();

                                Console.SetCursorPosition(79, 24);

                                DirectoryInfo di = new DirectoryInfo(currentDirectory);
                                directories.Clear();

                                menu.ShowCustomText(30, 17, "Выполнение...");

                                WalkDirectoryTree(di, mask);


                                List<string> changedFiles = new List<string>();

                                foreach (FileSystemInfo file in directories)
                                {
                                    if (File.Exists(file.FullName))
                                    {

                                        if (file.Exists)
                                        {
                                            string str = string.Empty;

                                            using (FileStream fs = new FileStream(file.FullName, FileMode.Open))
                                            {
                                                TextReader tr = new StreamReader(fs);

                                                Trace.WriteLine("opened file " + file.FullName);

                                                str = tr.ReadToEnd();

                                                tr.Close();
                                            }

                                            if (Regex.IsMatch(str, currentText))
                                            {
                                                Trace.WriteLine("changed file " + file.FullName);

                                                using (FileStream fs = new FileStream(file.FullName, FileMode.Create))
                                                {
                                                    TextWriter tw = new StreamWriter(fs);

                                                    str = Regex.Replace(str, currentText, replaceText);

                                                    tw.Write(str);

                                                    tw.Flush();
                                                    tw.Close();

                                                    changedFiles.Add(file.FullName);
                                                }
                                            }
                                        }
                                    }
                                }

                                using (FileStream fs = new FileStream("data_2.txt", FileMode.Create))
                                {
                                    TextWriter tw = new StreamWriter(fs);

                                    foreach (string file in changedFiles)
                                    {
                                        tw.WriteLine(file);
                                        Trace.WriteLine(file);
                                    }

                                    Trace.WriteLine("Общее количество измененных файлов: " + changedFiles.Count);

                                    tw.WriteLine("Общее количество измененных файлов: {0}", changedFiles.Count);

                                    tw.Flush();
                                    tw.Close();
                                }

                                menu.ShowCustomText(10, 18, "Действие завершено. результаты записаны в текстовый файл data_2.txt");

                                break;
                            }
                        case 3:
                            {
                                break;
                            }

                        case 4:
                            Environment.Exit(0);
                            break;
                    }
                }
            } while (true);

        }
    }
}
