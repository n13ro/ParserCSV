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
            using (var reader = new StreamReader(file))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<User>().ToList();
                foreach(var r in records)
                {
                    Console.WriteLine($"{r.Name}, {r.Age}, {r.City}");
                }

                List<User> users = new List<User>();

                var A = records.Where(w => w.Name.Contains('A'));
                Console.Write($"\nИмена на A: ");

                foreach (var thisUser in A)
                {
                    users.Add(thisUser);
                    Console.Write(thisUser.Name + " ");
                }

                double avgAge = records.Average(s => s.Age);
                Console.WriteLine($"\nСредний возраст: {avgAge:F2}");

            }

        }
    }

    class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }

    }
}
