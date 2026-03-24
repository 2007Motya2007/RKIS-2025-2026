using System;

namespace TodoList
{
    public class TodoList
    {
        private TodoItem[] _items;
        private int _count;

        public TodoList(int initialCapacity = 2)
        {
            _items = new TodoItem[initialCapacity];
            _count = 0;
        }

        public void Add(TodoItem item)
        {
            if (_count == _items.Length)
                IncreaseArray();
            _items[_count++] = item;
        }

        public void Delete(int index)
        {
            if (index < 0 || index >= _count)
                throw new IndexOutOfRangeException("Неверный индекс");

            for (int i = index; i < _count - 1; i++)
                _items[i] = _items[i + 1];
            _count--;
            if (_count < _items.Length)
                _items[_count] = null;
        }

        public TodoItem GetItem(int index)
        {
            if (index < 0 || index >= _count)
                throw new IndexOutOfRangeException("Неверный индекс");
            return _items[index];
        }

        public void View(bool showIndex, bool showStatus, bool showDate)
        {
            if (_count == 0)
            {
                Console.WriteLine("Нет задач.");
                return;
            }

            if (!showIndex && !showStatus && !showDate)
            {
                for (int i = 0; i < _count; i++)
                {
                    string shortText = _items[i].Text.Length > 30 ? _items[i].Text.Substring(0, 30) + "..." : _items[i].Text;
                    Console.WriteLine(shortText);
                }
                return;
            }

            var rows = new (string idx, string text, string status, string date)[_count];
            int maxIdxLen = 0, maxTextLen = 0, maxStatusLen = 0, maxDateLen = 0;

            for (int i = 0; i < _count; i++)
            {
                string idxStr = showIndex ? i.ToString() : "";
                string text = _items[i].Text;
                string shortText = text.Length > 30 ? text.Substring(0, 30) + "..." : text;
                string status = showStatus ? (_items[i].IsDone ? "Выполнена" : "Не выполнена") : "";
                string date = showDate ? _items[i].LastUpdate.ToString("dd.MM.yyyy HH:mm") : "";

                rows[i] = (idxStr, shortText, status, date);

                if (showIndex) maxIdxLen = Math.Max(maxIdxLen, idxStr.Length);
                maxTextLen = Math.Max(maxTextLen, shortText.Length);
                if (showStatus) maxStatusLen = Math.Max(maxStatusLen, status.Length);
                if (showDate) maxDateLen = Math.Max(maxDateLen, date.Length);
            }

            string separator = "+";
            if (showIndex) separator += new string('-', maxIdxLen + 2) + "+";
            separator += new string('-', maxTextLen + 2) + "+";
            if (showStatus) separator += new string('-', maxStatusLen + 2) + "+";
            if (showDate) separator += new string('-', maxDateLen + 2) + "+";

            Console.WriteLine(separator);
            string header = "|";
            if (showIndex) header += $" {"Индекс".PadRight(maxIdxLen)} |";
            header += $" {"Задача".PadRight(maxTextLen)} |";
            if (showStatus) header += $" {"Статус".PadRight(maxStatusLen)} |";
            if (showDate) header += $" {"Дата изменения".PadRight(maxDateLen)} |";
            Console.WriteLine(header);
            Console.WriteLine(separator);

            foreach (var row in rows)
            {
                string line = "|";
                if (showIndex) line += $" {row.idx.PadRight(maxIdxLen)} |";
                line += $" {row.text.PadRight(maxTextLen)} |";
                if (showStatus) line += $" {row.status.PadRight(maxStatusLen)} |";
                if (showDate) line += $" {row.date.PadRight(maxDateLen)} |";
                Console.WriteLine(line);
            }
            Console.WriteLine(separator);
        }

        private void IncreaseArray()
        {
            int newSize = _items.Length * 2;
            Array.Resize(ref _items, newSize);
        }
    }
}