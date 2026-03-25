using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TodoList
{
    public static class FileManager
    {
        public static void EnsureDataDirectory(string dirPath)
        {
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
        }

        public static void SaveProfile(Profile profile, string filePath)
        {
            string content = $"{profile.FirstName};{profile.LastName};{profile.BirthYear}";
            File.WriteAllText(filePath, content);
        }

        public static Profile LoadProfile(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Profile file not found", filePath);
            string[] parts = File.ReadAllText(filePath).Split(';');
            if (parts.Length != 3)
                throw new InvalidDataException("Invalid profile file format");
            string firstName = parts[0];
            string lastName = parts[1];
            int birthYear = int.Parse(parts[2]);
            return new Profile(firstName, lastName, birthYear);
        }

        public static void SaveTodos(TodoList todos, string filePath)
        {
            var lines = new List<string>();
            int count = todos.Count;
            for (int i = 0; i < count; i++)
            {
                var item = todos.GetItem(i);
                string escapedText = EscapeCsv(item.Text);
                string line = $"{i};{escapedText};{item.IsDone};{item.LastUpdate:o}";
                lines.Add(line);
            }
            File.WriteAllLines(filePath, lines);
        }

        public static TodoList LoadTodos(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Todos file not found", filePath);
            var todos = new TodoList();
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                string[] parts = SplitCsvLine(line);
                if (parts.Length != 4)
                    continue;
                string text = UnescapeCsv(parts[1]);
                bool isDone = bool.Parse(parts[2]);
                DateTime lastUpdate = DateTime.Parse(parts[3]);
                var item = new TodoItem(text);
                item.LoadState(isDone, lastUpdate);
                todos.Add(item);
            }
            return todos;
        }

        private static string EscapeCsv(string text)
        {
            return "\"" + text.Replace("\"", "\"\"").Replace("\n", "\\n") + "\"";
        }

        private static string UnescapeCsv(string text)
        {
            if (text.StartsWith("\"") && text.EndsWith("\""))
                text = text.Substring(1, text.Length - 2);
            return text.Replace("\\n", "\n").Replace("\"\"", "\"");
        }

        private static string[] SplitCsvLine(string line)
        {
            var result = new List<string>();
            bool inQuotes = false;
            int start = 0;
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '"')
                    inQuotes = !inQuotes;
                else if (line[i] == ';' && !inQuotes)
                {
                    result.Add(line.Substring(start, i - start));
                    start = i + 1;
                }
            }
            result.Add(line.Substring(start));
            return result.ToArray();
        }
    }
}