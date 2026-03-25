using System;

namespace TodoList
{
    public class DoneCommand : ICommand
    {
        private readonly TodoList _todoList;
        private readonly int _index;
        private readonly string _todoFilePath;

        public DoneCommand(TodoList todoList, int index, string todoFilePath)
        {
            _todoList = todoList;
            _index = index;
            _todoFilePath = todoFilePath;
        }

        public void Execute()
        {
            try
            {
                var item = _todoList.GetItem(_index);
                item.MarkDone();
                Console.WriteLine($"Задача \"{item.Text}\" отмечена выполненной.");
                FileManager.SaveTodos(_todoList, _todoFilePath);
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Задачи с таким индексом не существует.");
            }
        }
    }
}