namespace TodoList
{
	class Program
	{
		private static string firstName, lastName;
		private static int age;

		private static string[] todos = new string[2];
		private static int index;

		public static void Main()
		{
			Console.WriteLine("Работу выполнили Сироткин и Галои 3834");
			AddUser();

			while (true)
			{
				Console.Write("Введите команду: ");
				string command = Console.ReadLine();

				if (command == "help") HelpCommand();
				else if (command == "profile") ShowProfile();
				else if (command == "exit") break;
				else if (command.StartsWith("add ")) AddTodo(command);
				else if (command == "view") ViewTodo();
				else Console.WriteLine("Неизвестная команда.");
			}
		}
		private static void AddUser()
		{
			Console.Write("Введите ваше имя: ");
			firstName = Console.ReadLine();
			Console.Write("Введите вашу фамилию: ");
			lastName = Console.ReadLine();

			Console.Write("Введите ваш год рождения: ");
			var year = int.Parse(Console.ReadLine());
			age = DateTime.Now.Year - year;

			Console.WriteLine($"Добавлен пользователь {firstName} {lastName}, возраст - {age}");
		}

		private static void HelpCommand()
		{
			Console.WriteLine("Команды:");
			Console.WriteLine("help — выводит список всех доступных команд с кратким описанием");
			Console.WriteLine("profile — выводит данные пользователя");
			Console.WriteLine("add \"текст задачи\" — добавляет новую задачу");
			Console.WriteLine("view — выводит все задачи");
			Console.WriteLine("exit — выход из программы");
		}

		private static void ShowProfile()
		{
			Console.WriteLine($"{firstName} {lastName}, возраст - {age}");
		}

		private static void AddTodo(string command)
		{
			var task = command.Split("add ", 2)[1];
			if (index == todos.Length)
			{
				string[] newTodos = new string[todos.Length * 2];
				for (int i = 0; i < todos.Length; i++)
				{
					newTodos[i] = todos[i];
				}
			}

			todos[index] = task;

			Console.WriteLine($"Добавлена задача: {index}) {task}");
			index++;
		}

		private static void ViewTodo()
		{
			Console.WriteLine("Задачи:");
			for (var i = 0; i < todos.Length; i++)
			{
				var todo = todos[i];

				if (!string.IsNullOrEmpty(todo))
					Console.WriteLine($"{i}) {todo}");
			}
		}

	}
}