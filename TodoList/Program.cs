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

				if (command == "help")
				{
					Console.WriteLine("Команды:");
					Console.WriteLine("help — выводит список всех доступных команд с кратким описанием");
					Console.WriteLine("profile — выводит данные пользователя");
					Console.WriteLine("view — выводит все задачи");
					Console.WriteLine("exit — выход из программы");
				}
				else if (command == "profile")
				{
					Console.WriteLine(firstName + " " + lastName + ", - " + age);
				}

				else if (command == "exit")
				{
					Console.WriteLine("Выход из программы.");
					break;
				}
				else if (command.StartsWith("add "))
				{
					string task = command.Split("add ")[1];
					if (index == todos.Length)
					{
						string[] newTodos = new string[todos.Length * 2];
						for (int i = 0; i < todos.Length; i++)
						{
							newTodos[i] = todos[i];
						}

						todos = newTodos;
					}

					todos[index] = task;
					index++;

					Console.WriteLine("Добавлена задача: " + task);
				}
				else if (command == "view")
				{
					Console.WriteLine("Задачи:");
					foreach (string todo in todos)
					{
						if (!string.IsNullOrEmpty(todo))
						{
							Console.WriteLine(todo);
						}
					}
				}
				else
				{
					Console.WriteLine("Неизвестная команда.");
				}
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

			Console.WriteLine("Добавлен пользователь " + firstName + " " + lastName + ", возраст - " + age);
		}
	}
}