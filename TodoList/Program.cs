namespace TodoList
{
    class Program
    {
        private static string firstName, lastName;
        private static int age;

        private static string[] todos = new string[2];
        private static bool[] statuses = new bool[2];
        private static DateTime[] dates = new DateTime[2];
        private static int index;

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
            firstName = Console.ReadLine() ?? "";
            Console.Write("Введите вашу фамилию: ");
            lastName = Console.ReadLine() ?? "";

            Console.Write("Введите ваш год рождения: ");
            if (!int.TryParse(Console.ReadLine(), out int year))
            {
                Console.WriteLine("Некорректный год рождения. Будет использован 2000 год.");
                year = 2000;
            }
            age = DateTime.Now.Year - year;

            Console.WriteLine($"Добавлен пользователь {firstName} {lastName}, возраст - {age}");
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
            Console.WriteLine($"{firstName} {lastName}, возраст - {age}");
        }

        private static void AddTodoCommand(string[] parts)
        {
            bool multiline = false;
            if (parts.Length > 1 && (parts[1] == "--multiline" || parts[1] == "-m"))
            {
                multiline = true;
            }

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
                AddTodo(task);
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
            AddTodo(fullText);
        }

        private static void AddTodo(string task)
        {
            if (string.IsNullOrWhiteSpace(task))
            {
                Console.WriteLine("Задача не может быть пустой.");
                return;
            }

            if (index == todos.Length)
                ExpandArrays();

            todos[index] = task;
            statuses[index] = false;
            dates[index] = DateTime.Now;

            Console.WriteLine($"Добавлена задача: {index}) {task}");
            index++;
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
            if (idx < 0 || idx >= index)
            {
                Console.WriteLine("Задачи с таким индексом не существует.");
                return;
            }
            statuses[idx] = true;
            dates[idx] = DateTime.Now;
            Console.WriteLine($"Задача \"{todos[idx]}\" отмечена выполненной.");
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
            if (idx < 0 || idx >= index)
            {
                Console.WriteLine("Задачи с таким индексом не существует.");
                return;
            }

            for (int i = idx; i < index - 1; i++)
            {
                todos[i] = todos[i + 1];
                statuses[i] = statuses[i + 1];
                dates[i] = dates[i + 1];
            }
            index--;
            if (index < todos.Length)
            {
                todos[index] = null!;
                statuses[index] = false;
                dates[index] = default;
            }
            Console.WriteLine($"Задача с индексом {idx} удалена.");
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
            if (idx < 0 || idx >= index)
            {
                Console.WriteLine("Задачи с таким индексом не существует.");
                return;
            }

            string newText = string.Join(" ", parts, 2, parts.Length - 2);
            if (string.IsNullOrWhiteSpace(newText))
            {
                Console.WriteLine("Текст задачи не может быть пустым.");
                return;
            }

            todos[idx] = newText;
            dates[idx] = DateTime.Now;
            Console.WriteLine($"Задача {idx} обновлена.");
        }

        private static void ViewTodoCommand(string[] parts)
        {
            bool showIndex = false;
            bool showStatus = false;
            bool showDate = false;

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

            if (!showIndex && !showStatus && !showDate)
            {
                for (int i = 0; i < index; i++)
                {
                    string task = todos[i];
                    string shortTask = task.Length > 30 ? task.Substring(0, 30) + "..." : task;
                    Console.WriteLine($"{shortTask}");
                }
                return;
            }

            var rows = new List<(string idx, string task, string status, string date)>();
            int maxIdxLen = 0, maxTaskLen = 0, maxStatusLen = 0, maxDateLen = 0;

            for (int i = 0; i < index; i++)
            {
                string idxStr = showIndex ? i.ToString() : "";
                string taskFull = todos[i];
                string taskShort = taskFull.Length > 30 ? taskFull.Substring(0, 30) + "..." : taskFull;
                string statusStr = showStatus ? (statuses[i] ? "Выполнена" : "Не выполнена") : "";
                string dateStr = showDate ? dates[i].ToString("dd.MM.yyyy HH:mm") : "";

                rows.Add((idxStr, taskShort, statusStr, dateStr));

                if (showIndex) maxIdxLen = Math.Max(maxIdxLen, idxStr.Length);
                maxTaskLen = Math.Max(maxTaskLen, taskShort.Length);
                if (showStatus) maxStatusLen = Math.Max(maxStatusLen, statusStr.Length);
                if (showDate) maxDateLen = Math.Max(maxDateLen, dateStr.Length);
            }

            string separator = "+";
            if (showIndex) separator += new string('-', maxIdxLen + 2) + "+";
            separator += new string('-', maxTaskLen + 2) + "+";
            if (showStatus) separator += new string('-', maxStatusLen + 2) + "+";
            if (showDate) separator += new string('-', maxDateLen + 2) + "+";

            Console.WriteLine(separator);
            string header = "|";
            if (showIndex) header += $" {"Индекс".PadRight(maxIdxLen)} |";
            header += $" {"Задача".PadRight(maxTaskLen)} |";
            if (showStatus) header += $" {"Статус".PadRight(maxStatusLen)} |";
            if (showDate) header += $" {"Дата изменения".PadRight(maxDateLen)} |";
            Console.WriteLine(header);
            Console.WriteLine(separator);

            foreach (var row in rows)
            {
                string line = "|";
                if (showIndex) line += $" {row.idx.PadRight(maxIdxLen)} |";
                line += $" {row.task.PadRight(maxTaskLen)} |";
                if (showStatus) line += $" {row.status.PadRight(maxStatusLen)} |";
                if (showDate) line += $" {row.date.PadRight(maxDateLen)} |";
                Console.WriteLine(line);
            }
            Console.WriteLine(separator);
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
            if (idx < 0 || idx >= index)
            {
                Console.WriteLine("Задачи с таким индексом не существует.");
                return;
            }

            Console.WriteLine($"--- Задача #{idx} ---");
            Console.WriteLine($"Текст:\n{todos[idx]}");
            Console.WriteLine($"Статус: {(statuses[idx] ? "Выполнена" : "Не выполнена")}");
            Console.WriteLine($"Последнее изменение: {dates[idx]:dd.MM.yyyy HH:mm}");
        }

        private static void ExpandArrays()
        {
            int newSize = todos.Length * 2;
            Array.Resize(ref todos, newSize);
            Array.Resize(ref statuses, newSize);
            Array.Resize(ref dates, newSize);
        }
    }
}