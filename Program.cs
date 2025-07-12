using CsvHelper;
using System.Globalization;
using System.Linq;


namespace ParserCSV
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string file = @"../../../table.csv";
            List<User> users = new List<User>();

            if (File.Exists(file))
            {
                #region Чтение всех в файле
                using (var reader = new StreamReader(file))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<User>().ToList();
                    foreach (var r in records)
                    {
                        Console.WriteLine($"{r.Name}, {r.Age}, {r.City}");
                    }

                }
                #endregion

                #region Ввод данных
                Console.WriteLine("Введите имя: ");
                string name = Console.ReadLine();

                Console.WriteLine("Введите возраст: ");
                int age = int.Parse(Console.ReadLine());

                Console.Write("Введите город: ");
                string city = Console.ReadLine();
                #endregion

                User newUser = new User { Name = name, Age = age, City = city };
                users.Add(newUser);

                #region Есть ли такой файл?
                bool fileExists = File.Exists(file);
                bool writeHeader = !fileExists || new FileInfo("table.csv").Length == 0;
                #endregion

                #region Запись(Добавление) информации
                using (var stream = new FileStream(file, FileMode.Append, FileAccess.Write))
                using (var writer = new StreamWriter(stream))
                using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    if (writeHeader)
                    {
                         csvWriter.WriteHeader<User>();
                         csvWriter.NextRecord();
                    }
                    csvWriter.WriteRecord(newUser);
                    csvWriter.NextRecord();

                }
                Console.WriteLine("Данные записаны в table.csv");
                #endregion
            }
        }

        class User
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public string City { get; set; }

        }
    }
}
