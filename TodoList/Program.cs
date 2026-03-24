using System.Text;

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
                string? command = Console.ReadLine();
                if (command == null) break;

                command = command.Trim();
                if (string.IsNullOrEmpty(command)) continue;

                string[] parts = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string cmd = parts[0].ToLower();

                switch (cmd)
                {
                    case "help":
                        HelpCommand();
                        break;
                    case "profile":
                        ShowProfile();
                        break;
                    case "exit":
                        return;
                    case "add":
                        AddTodoCommand(parts);
                        break;
                    case "done":
                        DoneTodoCommand(parts);
                        break;
                    case "delete":
                        DeleteTodoCommand(parts);
                        break;
                    case "update":
                        UpdateTodoCommand(parts);
                        break;
                    case "view":
                        ViewTodoCommand(parts);
                        break;
                    case "read":
                        ReadTodoCommand(parts);
                        break;
                    default:
                        Console.WriteLine("Неизвестная команда. Введите 'help' для списка команд.");
                        break;
                }
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

        private static void HelpCommand()
        {
            Console.WriteLine("Команды:");
            Console.WriteLine("help — выводит этот список");
            Console.WriteLine("profile — выводит данные пользователя");
            Console.WriteLine("add [--multiline|-m] — добавить задачу (однострочно или многострочно)");
            Console.WriteLine("done <индекс> — отметить задачу выполненной");
            Console.WriteLine("delete <индекс> — удалить задачу");
            Console.WriteLine("update <индекс> <новый текст> — обновить текст задачи");
            Console.WriteLine("view [флаги] — вывести список задач. Флаги:");
            Console.WriteLine("  -i, --index         показывать индекс");
            Console.WriteLine("  -s, --status        показывать статус");
            Console.WriteLine("  -d, --update-date   показывать дату последнего изменения");
            Console.WriteLine("  -a, --all           показать всё");
            Console.WriteLine("  Без флагов выводится только текст задачи (обрезанный до 30 символов)");
            Console.WriteLine("read <индекс> — показать полный текст задачи, статус и дату");
            Console.WriteLine("exit — выход из программы");
        }

        private static void ShowProfile()
        {
            Console.WriteLine(profile.GetInfo());
        }

        private static void AddTodoCommand(string[] parts)
        {
            bool multiline = false;
            if (parts.Length > 1 && (parts[1] == "--multiline" || parts[1] == "-m"))
                multiline = true;

            if (multiline)
            {
                AddMultilineTodo();
            }
            else
            {
                if (parts.Length < 2)
                {
                    Console.WriteLine("Использование: add \"текст задачи\"");
                    return;
                }
                string task = string.Join(" ", parts, 1, parts.Length - 1);
                todoList.Add(new TodoItem(task));
                Console.WriteLine($"Добавлена задача: {task}");
            }
        }

        private static void AddMultilineTodo()
        {
            Console.WriteLine("Введите строки задачи. Для завершения введите !end (каждая строка начинается с '> '):");
            var lines = new List<string>();
            while (true)
            {
                Console.Write("> ");
                string? line = Console.ReadLine();
                if (line == null) break;
                if (line == "!end") break;
                lines.Add(line);
            }
            string fullText = string.Join("\n", lines);
            if (!string.IsNullOrWhiteSpace(fullText))
            {
                todoList.Add(new TodoItem(fullText));
                Console.WriteLine("Добавлена многострочная задача.");
            }
            else
            {
                Console.WriteLine("Задача не может быть пустой.");
            }
        }

        private static void DoneTodoCommand(string[] parts)
        {
            if (parts.Length < 2)
            {
                Console.WriteLine("Использование: done <индекс>");
                return;
            }
            if (!int.TryParse(parts[1], out int idx))
            {
                Console.WriteLine("Индекс должен быть числом.");
                return;
            }
            try
            {
                var item = todoList.GetItem(idx);
                item.MarkDone();
                Console.WriteLine($"Задача \"{item.Text}\" отмечена выполненной.");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Задачи с таким индексом не существует.");
            }
        }

        private static void DeleteTodoCommand(string[] parts)
        {
            if (parts.Length < 2)
            {
                Console.WriteLine("Использование: delete <индекс>");
                return;
            }
            if (!int.TryParse(parts[1], out int idx))
            {
                Console.WriteLine("Индекс должен быть числом.");
                return;
            }
            try
            {
                todoList.Delete(idx);
                Console.WriteLine($"Задача с индексом {idx} удалена.");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Задачи с таким индексом не существует.");
            }
        }

        private static void UpdateTodoCommand(string[] parts)
        {
            if (parts.Length < 3)
            {
                Console.WriteLine("Использование: update <индекс> <новый текст>");
                return;
            }
            if (!int.TryParse(parts[1], out int idx))
            {
                Console.WriteLine("Индекс должен быть числом.");
                return;
            }
            string newText = string.Join(" ", parts, 2, parts.Length - 2);
            if (string.IsNullOrWhiteSpace(newText))
            {
                Console.WriteLine("Текст задачи не может быть пустым.");
                return;
            }
            try
            {
                var item = todoList.GetItem(idx);
                item.UpdateText(newText);
                Console.WriteLine($"Задача {idx} обновлена.");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Задачи с таким индексом не существует.");
            }
        }

        private static void ViewTodoCommand(string[] parts)
        {
            bool showIndex = false, showStatus = false, showDate = false;

            for (int i = 1; i < parts.Length; i++)
            {
                string flag = parts[i];
                if (flag == "--index" || flag == "-i")
                    showIndex = true;
                else if (flag == "--status" || flag == "-s")
                    showStatus = true;
                else if (flag == "--update-date" || flag == "-d")
                    showDate = true;
                else if (flag == "--all" || flag == "-a")
                {
                    showIndex = true;
                    showStatus = true;
                    showDate = true;
                }
                else if (flag.StartsWith("-") && flag.Length > 1 && !flag.StartsWith("--"))
                {
                    foreach (char ch in flag.Substring(1))
                    {
                        switch (ch)
                        {
                            case 'i': showIndex = true; break;
                            case 's': showStatus = true; break;
                            case 'd': showDate = true; break;
                            case 'a':
                                showIndex = true;
                                showStatus = true;
                                showDate = true;
                                break;
                            default:
                                Console.WriteLine($"Неизвестный флаг: -{ch}");
                                break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Неизвестный флаг: {flag}");
                }
            }

            todoList.View(showIndex, showStatus, showDate);
        }

        private static void ReadTodoCommand(string[] parts)
        {
            if (parts.Length < 2)
            {
                Console.WriteLine("Использование: read <индекс>");
                return;
            }
            if (!int.TryParse(parts[1], out int idx))
            {
                Console.WriteLine("Индекс должен быть числом.");
                return;
            }
            try
            {
                var item = todoList.GetItem(idx);
                Console.WriteLine(item.GetFullInfo());
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Задачи с таким индексом не существует.");
            }
        }
    }
}