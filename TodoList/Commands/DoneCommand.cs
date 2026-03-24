using System;

namespace TodoList
{
    public class DoneCommand : ICommand
    {
        private readonly TodoList _todoList;
        private readonly int _index;

        public DoneCommand(TodoList todoList, int index)
        {
            _todoList = todoList;
            _index = index;
        }

        public void Execute()
        {
            try
            {
                var item = _todoList.GetItem(_index);
                item.MarkDone();
                Console.WriteLine($"Задача \"{item.Text}\" отмечена выполненной.");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Задачи с таким индексом не существует.");
            }
        }
    }
}