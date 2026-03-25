using System;

namespace TodoList
{
    public class DeleteCommand : ICommand
    {
        private readonly TodoList _todoList;
        private readonly int _index;
        private readonly string _todoFilePath;

        public DeleteCommand(TodoList todoList, int index, string todoFilePath)
        {
            _todoList = todoList;
            _index = index;
            _todoFilePath = todoFilePath;
        }

        public void Execute()
        {
            try
            {
                _todoList.Delete(_index);
                Console.WriteLine($"Задача с индексом {_index} удалена.");
                FileManager.SaveTodos(_todoList, _todoFilePath);
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Задачи с таким индексом не существует.");
            }
        }
    }
}