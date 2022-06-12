//03.07.2019
using System.Globalization;

namespace PU2019
{
    class Client
    {
        private string _name;
        private int _purchases;

        public string Name
        {
            get => _name;
            private set
            {
                if (value.Length <= 40 && !String.IsNullOrEmpty(value))
                {
                    _name = value;
                }
                else
                {
                    throw new Exception("Name was too long or empty.");
                }
            }
        }
        public DateOnly Date { get; private set; }
        public int Purchases
        {
            get => _purchases;
            private set
            {
                if (value > 0 && value < 10_000)
                {
                    _purchases = value;
                }
                else
                {
                    throw new Exception("Invalid number of purchases.");
                }
            }
        }
        public decimal MoneySpent { get; private set; }
        public int Rating => Purchases switch
        {
            < 100 => 1,
            >= 100 and < 300 => 2,
            >= 300 and < 500 => 3,
            >= 500 and < 1000 => 4,
            >= 1000 and < 10_000 => 5,
        };

        public Client(string name, DateOnly date, int purchases, decimal moneySpent)
        {
            Name = name;
            Date = date;
            Purchases = purchases;
            MoneySpent = moneySpent;
        }

        public override string ToString()
        {
            return $"{Name}, {Purchases}, {MoneySpent:f2}, {Date.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture)}, {new string('*', Rating)}";
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int n = 0;
            do
            {
                n = int.Parse(Console.ReadLine());

            } while (n<1 && n>5000);

            List<Client> clients = new List<Client>();

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("Enter name:");
                string name = Console.ReadLine();
                Console.WriteLine("Enter date:");
                DateOnly date = DateOnly.ParseExact(Console.ReadLine(),"dd/MM/yyyy");
                Console.WriteLine("Enter number of purchases:");
                int purchases = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter the money spent:");
                decimal moneySpent = decimal.Parse(Console.ReadLine());
                var curr = new Client(name, date, purchases, moneySpent);
                clients.Add(curr);
            }
            Console.WriteLine($"\r\n{new string('-',20)}(2){new string('-', 20)}\r\n");
            var ordered = clients.OrderBy(c => c.Name).ToList();
            Console.WriteLine(String.Join(Environment.NewLine,ordered.Select(c=>c.ToString())));

            Console.WriteLine($"\r\n{new string('-',20)}(3){new string('-',20)}\r\n");
            var ordered2 = clients.Where(x => x.Rating == 2).OrderByDescending(x => x.MoneySpent).ThenBy(x=>x.Name).ToList();
            Console.WriteLine(String.Join(Environment.NewLine, ordered2.Select(c => c.ToString())));


            Console.WriteLine($"\r\n{new string('-', 20)}(4){new string('-', 20)} \r\nEnter rating:");
            
            int rating = int.Parse(Console.ReadLine());
            var ordered3 = clients.Where(x=>x.Rating==rating).GroupBy(x => x.Date.Year);
            if (!ordered3.Any())
            {
                Console.WriteLine($"There are no clients rated with {rating} stars.");
            }
            foreach (var year in ordered3)
            {
                Console.WriteLine($"{year.Key} - {year.Count()}");
            }


        }
    }
}