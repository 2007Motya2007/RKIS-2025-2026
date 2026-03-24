using System;

namespace TodoList
{
    public class DeleteCommand : ICommand
    {
        private readonly TodoList _todoList;
        private readonly int _index;

        public DeleteCommand(TodoList todoList, int index)
        {
            _todoList = todoList;
            _index = index;
        }

        public void Execute()
        {
            try
            {
                _todoList.Delete(_index);
                Console.WriteLine($"Задача с индексом {_index} удалена.");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Задачи с таким индексом не существует.");
            }
        }
    }
}