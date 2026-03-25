using System;

namespace TodoList
{
    public class TodoItem
    {
        public string Text { get; private set; }
        public bool IsDone { get; private set; }
        public DateTime LastUpdate { get; private set; }

        public TodoItem(string text)
        {
            Text = text;
            IsDone = false;
            LastUpdate = DateTime.Now;
        }

        public void MarkDone()
        {
            IsDone = true;
            LastUpdate = DateTime.Now;
        }

        public void UpdateText(string newText)
        {
            Text = newText;
            LastUpdate = DateTime.Now;
        }

        public void LoadState(bool isDone, DateTime lastUpdate)
        {
            IsDone = isDone;
            LastUpdate = lastUpdate;
        }

        public string GetShortInfo()
        {
            string shortText = Text.Length > 30 ? Text.Substring(0, 30) + "..." : Text;
            string status = IsDone ? "Выполнена" : "Не выполнена";
            return $"{shortText} | {status} | {LastUpdate:dd.MM.yyyy HH:mm}";
        }

        public string GetFullInfo()
        {
            return $"Текст:\n{Text}\nСтатус: {(IsDone ? "Выполнена" : "Не выполнена")}\nПоследнее изменение: {LastUpdate:dd.MM.yyyy HH:mm}";
        }
    }
}