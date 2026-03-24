namespace TodoList
{
	class Program
	{
		public static void Main()
		{
			Console.WriteLine("Работу выполнили Сироткин и Галои 3834");
			Console.Write("Введите ваше имя: ");
			string firstName = Console.ReadLine();
			Console.Write("Введите вашу фамилию: ");
			string lastName = Console.ReadLine();

			Console.Write("Введите ваш год рождения: ");
			int year = int.Parse(Console.ReadLine());
			int age = DateTime.Now.Year - year;
			Console.WriteLine($"Добавлен пользователь {firstName} {lastName}, возраст - {age}");

			string[] todos = new string[2] { "test", "test" };
			int index = 0;

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
	}
}