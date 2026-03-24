using System;

namespace TodoList
{
    public class ViewCommand : ICommand
    {
        private readonly TodoList _todoList;
        private readonly bool _showIndex;
        private readonly bool _showStatus;
        private readonly bool _showDate;

        public ViewCommand(TodoList todoList, bool showIndex, bool showStatus, bool showDate)
        {
            _todoList = todoList;
            _showIndex = showIndex;
            _showStatus = showStatus;
            _showDate = showDate;
        }

        public void Execute()
        {
            _todoList.View(_showIndex, _showStatus, _showDate);
        }
    }
}