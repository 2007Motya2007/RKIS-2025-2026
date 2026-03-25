using System;
using System.Collections.Generic;

namespace TodoList
{
    public class AddCommand : ICommand
    {
        private readonly TodoList _todoList;
        private readonly bool _multiline;
        private readonly string _text;
        private readonly string _todoFilePath;

        public AddCommand(TodoList todoList, string text, bool multiline, string todoFilePath)
        {
            _todoList = todoList;
            _text = text;
            _multiline = multiline;
            _todoFilePath = todoFilePath;
        }

        public void Execute()
        {
            if (_multiline)
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
                    _todoList.Add(new TodoItem(fullText));
                    Console.WriteLine("Добавлена многострочная задача.");
                    FileManager.SaveTodos(_todoList, _todoFilePath);
                }
                else
                {
                    Console.WriteLine("Задача не может быть пустой.");
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(_text))
                {
                    Console.WriteLine("Задача не может быть пустой.");
                    return;
                }
                _todoList.Add(new TodoItem(_text));
                Console.WriteLine($"Добавлена задача: {_text}");
                FileManager.SaveTodos(_todoList, _todoFilePath);
            }
        }
    }
}