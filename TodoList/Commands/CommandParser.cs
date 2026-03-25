using System;
using System.Linq;

namespace TodoList
{
    public static class CommandParser
    {
        public static ICommand? Parse(string input, TodoList todoList, Profile profile, string todoFilePath)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;

            var parts = input.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string cmd = parts[0].ToLower();

            switch (cmd)
            {
                case "help":
                    return new HelpCommand();

                case "profile":
                    return new ProfileCommand(profile);

                case "exit":
                    return new ExitCommand();

                case "add":
                    {
                        bool multiline = false;
                        string text = "";
                        int i = 1;
                        while (i < parts.Length)
                        {
                            if (parts[i] == "--multiline" || parts[i] == "-m")
                            {
                                multiline = true;
                                i++;
                            }
                            else
                            {
                                text = string.Join(" ", parts.Skip(i));
                                break;
                            }
                        }
                        if (!multiline && string.IsNullOrWhiteSpace(text))
                        {
                            Console.WriteLine("Использование: add \"текст задачи\"");
                            return null;
                        }
                        return new AddCommand(todoList, text, multiline, todoFilePath);
                    }

                case "done":
                    if (parts.Length < 2 || !int.TryParse(parts[1], out int doneIndex))
                    {
                        Console.WriteLine("Использование: done <индекс>");
                        return null;
                    }
                    return new DoneCommand(todoList, doneIndex, todoFilePath);

                case "delete":
                    if (parts.Length < 2 || !int.TryParse(parts[1], out int deleteIndex))
                    {
                        Console.WriteLine("Использование: delete <индекс>");
                        return null;
                    }
                    return new DeleteCommand(todoList, deleteIndex, todoFilePath);

                case "update":
                    if (parts.Length < 3 || !int.TryParse(parts[1], out int updateIndex))
                    {
                        Console.WriteLine("Использование: update <индекс> <новый текст>");
                        return null;
                    }
                    string newText = string.Join(" ", parts.Skip(2));
                    return new UpdateCommand(todoList, updateIndex, newText, todoFilePath);

                case "view":
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
                        return new ViewCommand(todoList, showIndex, showStatus, showDate);
                    }

                case "read":
                    if (parts.Length < 2 || !int.TryParse(parts[1], out int readIndex))
                    {
                        Console.WriteLine("Использование: read <индекс>");
                        return null;
                    }
                    return new ReadCommand(todoList, readIndex);

                default:
                    return null;
            }
        }
    }
}