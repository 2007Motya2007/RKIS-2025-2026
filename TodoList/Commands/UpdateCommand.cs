using System;

namespace TodoList
{
    public class UpdateCommand : ICommand
    {
        private readonly TodoList _todoList;
        private readonly int _index;
        private readonly string _newText;

        public UpdateCommand(TodoList todoList, int index, string newText)
        {
            _todoList = todoList;
            _index = index;
            _newText = newText;
        }

        public void Execute()
        {
            if (string.IsNullOrWhiteSpace(_newText))
            {
                Console.WriteLine("Текст задачи не может быть пустым.");
                return;
            }
            try
            {
                var item = _todoList.GetItem(_index);
                item.UpdateText(_newText);
                Console.WriteLine($"Задача {_index} обновлена.");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Задачи с таким индексом не существует.");
            }
        }
    }
}