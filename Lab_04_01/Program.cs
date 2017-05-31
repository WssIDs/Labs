using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab_04_01
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

        //////////////  Действия Главного меню //////////////////////////////////

        /// <summary>
        /// Поиск файлов и каталогов в указанной директории в необходимом диапазоне
        /// </summary>
        /// <param name="menu">Меню</param>
        static void SearchFilesInDirectory(Menu menu)
        {
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
        }

        /// <summary>
        /// Поиск и замена текста в найденных файлах
        /// </summary>
        /// <param name="menu">Меню</param>
        static void ReplaceTextInFiles(Menu menu)
        {
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
        }

        /// <summary>
        /// Поиск файлов и каталогов на указанном локальном диске
        /// </summary>
        /// <param name="menu">Меню</param>
        /// <param name="countFiles">количество найденных файлов</param>
        /// <param name="countDirs">количество найденных директорий</param>
        static void SearchAllFilesDirectoryDrive(Menu menu, out int countFiles, out int countDirs)
        {
            Console.SetCursorPosition(25, 17);
            Console.Write("Введите имя диска:");
            Console.SetCursorPosition(25, 18);
            string driveName = Console.ReadLine();
            Console.SetCursorPosition(25, 19);
            Console.Write("Введите маску поиска:");
            string mask = Console.ReadLine();

            DriveInfo drInfo = new DriveInfo(driveName);

            menu.ShowCustomText(30, 17, "Выполнение...");

            WalkDirectoryTree(drInfo.RootDirectory, mask);

            Trace.WriteLine("Files with restricted access:");
            foreach (string s in log)
            {
                Trace.WriteLine(s);
            }

            foreach (FileSystemInfo item in directories)
            {
                if (File.Exists(item.FullName))
                {
                    Trace.WriteLine("Файл" + item.FullName);
                }
                else if (Directory.Exists(item.FullName))
                {
                    Trace.WriteLine("Директория" + item.FullName);
                }
            }

            countFiles = 0;
            countDirs = 0;

            using (FileStream fs = new FileStream("data_3.txt", FileMode.Create))
            {
                TextWriter tw = new StreamWriter(fs);

                int countItems = 1;

                foreach (FileSystemInfo item in directories)
                {
                    char[] sep = { '\\' };

                    string[] names = item.FullName.Split(sep);

                    if (File.Exists(item.FullName))
                    {
                        if (names.Length > 3)
                        {
                            string temp = "...";

                            for (int i = names.Length - 2; i < names.Length; i++)
                            {
                                temp += "\\" + names[i];
                            }

                            tw.WriteLine(countItems + ": Файл: " + temp);
                        }

                        else
                        {
                            tw.WriteLine(countItems + ": Файл: " + item.FullName);
                        }

                        Trace.WriteLine(countItems + ": Файл: " + item.FullName);
                        countFiles++;
                        countItems++;
                    }
                    else if (Directory.Exists(item.FullName))
                    {
                        if (names.Length > 3)
                        {
                            string temp = "...";

                            for (int i = names.Length - 2; i < names.Length; i++)
                            {
                                temp += "\\" + names[i];
                            }

                            tw.WriteLine(countItems + ": Директория: " + temp);
                        }

                        else
                        {
                            tw.WriteLine(countItems + ": Директория: " + item.FullName);
                        }

                        Trace.WriteLine(countItems + ": Директория: " + item.FullName);
                        countDirs++;
                        countItems++;
                    }
                }

                Trace.WriteLine("Общее количество директорий: " + countDirs);
                Trace.WriteLine("Общее количество файлов: " + countFiles);

                tw.WriteLine("Общее количество директорий: {0}", countDirs);
                tw.WriteLine("Общее количество файлов: {0}", countFiles);

                tw.WriteLine();

                tw.Flush();
                tw.Close();
            }
        }

        //////////////  Действия Подменю //////////////////////////////////

        /// <summary>
        /// Удаление всех файлов и директорий на диске
        /// </summary>
        /// <param name="subMenu">Подменю</param>
        static void DeleteAllFilesFoldersDrive(Menu subMenu)
        {
            subMenu.ShowCustomText(30, 17, "Выполнение");
            DeleteFilesFolders(directories);

            subMenu.ShowCustomText(30, 17, "Операция завершена");
        }


        /// <summary>
        /// Удаление файлов и директорий под определенным номером
        /// </summary>
        /// <param name="subMenu">Подменю</param>
        static void DeleteFilesFoldersByNumber(Menu subMenu)
        {
            Console.SetCursorPosition(25, 17);
            Console.Write("Введите номер файла/каталога для удаления:");
            Console.SetCursorPosition(25, 18);
            int fileNumber = int.Parse(Console.ReadLine());
            Console.SetCursorPosition(25, 19);

            subMenu.ShowCustomText(30, 17, "Выполнение");

            directories = DeleteCustomFilesFolders(fileNumber, directories);
        }

        /// <summary>
        /// Удалить файлы и директории в заданном диапазоне
        /// </summary>
        /// <param name="subMenu">Подменю</param>
        static void DeleteRangeFilesFolders(Menu subMenu)
        {
            Console.SetCursorPosition(25, 17);
            Console.Write("Введите диапазон для удаления:");
            Console.SetCursorPosition(25, 18);
            Console.Write("начало:");
            int first = int.Parse(Console.ReadLine());
            Console.SetCursorPosition(25, 19);
            Console.Write("конец:");
            int last = int.Parse(Console.ReadLine());
            Console.SetCursorPosition(25, 20);

            subMenu.ShowCustomText(30, 17, "Выполнение");

            List<FileSystemInfo> newFiles = new List<FileSystemInfo>();

            for (int i = first - 1; i < last; i++)
            {
                newFiles.Add(directories[i]);
            }

            DeleteFilesFolders(newFiles);

            subMenu.ShowCustomText(30, 17, "Операция завершена");
        }

        static void Main(string[] args)
        {
            List<MenuItem> items = new List<MenuItem>();

            items.Add(new MenuItem("  Поиск в указанном каталоге файлов        ", 10));
            items.Add(new MenuItem("  Замена указанного текста в файлах        ", 11));
            items.Add(new MenuItem("  Поиск по всему диску файлов и каталогов  ", 12));
            items.Add(new MenuItem("  Выйти из приложения                      ", 13));

            Menu menu = new Menu(items);

            menu.Show(0, 8);
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
                        case 0:
                            {
                                SearchFilesInDirectory(menu);
                                break;
                            }
                        case 1:
                            {
                                ReplaceTextInFiles(menu);
                                break;
                            }
                        case 2:
                            {
                                int countFiles;
                                int countDirs;

                                SearchAllFilesDirectoryDrive(menu, out countFiles, out countDirs);

                                List<MenuItem> subItems = new List<MenuItem>();

                                subItems.Add(new MenuItem("  Удалить все найденное                    ", 10));
                                subItems.Add(new MenuItem("  Удалить заданный файл или каталог        ", 11));
                                subItems.Add(new MenuItem("  Удалить диапазон файлов или каталогов    ", 12));
                                subItems.Add(new MenuItem("  Вернуться в предыдущее меню              ", 13));

                                Menu subMenu = new Menu(subItems);

                                subMenu.Show(0, 8);

                                subMenu.ShowCustomText(25, 17, "Найдено директорий: " + countDirs + "|  файлов: " + countFiles);
                                Console.SetCursorPosition(20, 18);
                                Console.Write("Результаты записаны в текстовый файл data_3.txt");
                                Console.SetCursorPosition(79, 24);

                                bool isSubMenu = true;

                                do
                                {
                                    cki = Console.ReadKey();
                                    if (cki.Key == ConsoleKey.UpArrow) subMenu.UpPosition();
                                    else if (cki.Key == ConsoleKey.DownArrow) subMenu.DownPosition();
                                    else if (cki.Key == ConsoleKey.Enter)
                                    {
                                        switch (subMenu.Position)
                                        {
                                            case 0:
                                                {
                                                    DeleteAllFilesFoldersDrive(subMenu);
                                                    break;
                                                }
                                            case 1:
                                                {
                                                    DeleteFilesFoldersByNumber(subMenu);
                                                    break;
                                                }
                                            case 2:
                                                {
                                                    DeleteRangeFilesFolders(subMenu);
                                                    break;
                                                }

                                            case 3:
                                                {
                                                    isSubMenu = false;
                                                    break;
                                                }
                                        }
                                    }
                                } while (isSubMenu);

                                menu.Show(menu.Position, 8);

                                break;
                            }

                        case 3:
                            Environment.Exit(0);
                            break;
                    }
                }
            } while (true);

        }
    }
}
