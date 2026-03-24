using System;

namespace TodoList
{
    class Program
    {
        private static TodoList todoList = new TodoList();
        private static Profile profile;

        public static void Main()
        {
            Console.WriteLine("Работу выполнили Сироткин и Галои 3834");
            AddUser();

            while (true)
            {
                Console.Write("Введите команду: ");
                string? input = Console.ReadLine();
                if (input == null) break;

                ICommand? command = CommandParser.Parse(input, todoList, profile);
                if (command == null)
                {
                    Console.WriteLine("Неизвестная команда. Введите 'help' для списка команд.");
                    continue;
                }
                command.Execute();
            }
        }

        private static void AddUser()
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

            profile = new Profile(firstName, lastName, year);
            Console.WriteLine(profile.GetInfo());
        }
    }
}