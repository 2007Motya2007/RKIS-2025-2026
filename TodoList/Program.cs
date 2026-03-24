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

			while (true)
			{
				Console.Write("Введите команду: ");
				string command = Console.ReadLine();

				if (command == "help")
				{
					Console.WriteLine("Команды:");
					Console.WriteLine("help — выводит список всех доступных команд с кратким описанием");
					Console.WriteLine("profile — выводит данные пользователя");
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
				else
				{
					Console.WriteLine("Неизвестная команда.");
				}
			}
		}
	}
}