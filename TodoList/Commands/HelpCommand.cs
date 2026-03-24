using System;

namespace TodoList
{
    public class HelpCommand : ICommand
    {
        public void Execute()
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
    }
}