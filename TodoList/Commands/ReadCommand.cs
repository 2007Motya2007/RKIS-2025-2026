using System;

namespace TodoList
{
    public class ReadCommand : ICommand
    {
        private readonly TodoList _todoList;
        private readonly int _index;

        public ReadCommand(TodoList todoList, int index)
        {
            _todoList = todoList;
            _index = index;
        }

        public void Execute()
        {
            try
            {
                var item = _todoList.GetItem(_index);
                Console.WriteLine(item.GetFullInfo());
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Задачи с таким индексом не существует.");
            }
        }
    }
}