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

				if (command == "profile")
				{
					Console.WriteLine(firstName + " " + lastName + ", - " + age);
				}
			}
		}
	}
}