using System;
using System.IO;

namespace TodoList
{
    class Program
    {
        private static readonly string dataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");
        private static readonly string profilePath = Path.Combine(dataDir, "profile.txt");
        private static readonly string todoPath = Path.Combine(dataDir, "todo.csv");

        public static void Main()
        {
            Console.WriteLine("Работу выполнили Сироткин и Галои 3834");

            FileManager.EnsureDataDirectory(dataDir);

            Profile profile;
            if (File.Exists(profilePath))
            {
                try
                {
                    profile = FileManager.LoadProfile(profilePath);
                    Console.WriteLine("Профиль загружен: " + profile.GetInfo());
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка загрузки профиля: {ex.Message}. Будет создан новый.");
                    profile = CreateProfileFromUser();
                    FileManager.SaveProfile(profile, profilePath);
                }
            }
            else
            {
                profile = CreateProfileFromUser();
                FileManager.SaveProfile(profile, profilePath);
            }

            TodoList todoList;
            if (File.Exists(todoPath))
            {
                try
                {
                    todoList = FileManager.LoadTodos(todoPath);
                    Console.WriteLine("Задачи загружены.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка загрузки задач: {ex.Message}. Начинаем с пустого списка.");
                    todoList = new TodoList();
                }
            }
            else
            {
                todoList = new TodoList();
            }

            while (true)
            {
                Console.Write("Введите команду: ");
                string? input = Console.ReadLine();
                if (input == null) break;

                ICommand? command = CommandParser.Parse(input, todoList, profile, todoPath);
                if (command == null)
                {
                    Console.WriteLine("Неизвестная команда. Введите 'help' для списка команд.");
                    continue;
                }
                command.Execute();
            }
        }

        private static Profile CreateProfileFromUser()
        {
            Console.Write("Введите ваше имя: ");
            string firstName = Console.ReadLine() ?? "";
            Console.Write("Введите вашу фамилию: ");
            string lastName = Console.ReadLine() ?? "";

            Console.Write("Введите ваш год рождения: ");
            if (!int.TryParse(Console.ReadLine(), out int year))
            {
                Console.WriteLine("Некорректный год рождения. Будет использован 2000 год.");
                year = 2000;
            }

            var profile = new Profile(firstName, lastName, year);
            Console.WriteLine(profile.GetInfo());
            return profile;
        }
    }
}