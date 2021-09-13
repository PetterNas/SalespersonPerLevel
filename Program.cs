using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L002B_Inl2_PetterNaslund
{
    class Program
    {
        public class SalesPerson
        {
            public SalesPerson(string name, int personalNum, string disctrict, int amountSold) //Level is set by the constructor.
            {
                Name = name;
                this.PersonalNum = personalNum;
                this.Disctrict = disctrict;
                this.AmountSold = amountSold;

                if (amountSold < 50)
                    this.Level = 1;

                else if (amountSold <= 99)
                    this.Level = 2;

                else if (amountSold <= 199)
                    this.Level = 3;

                else if (amountSold > 200)
                    this.Level = 4;
            }

            public string Name { get; set; }
            public int PersonalNum { get; set; }
            public string Disctrict { get; set; }
            public int AmountSold { get; set; }
            public int Level { get; set; }
        }
        public static Dictionary<int, List<SalesPerson>> EnterSalesPersons() //Takes user input and populates a dictionary.
        {
            Dictionary<int, List<SalesPerson>> allSalesWithLevel = new Dictionary<int, List<SalesPerson>>();//Key is level, value is list of salesperson.

            Console.Write("How many persons do you want to register? ");
            int amountOfSalesPersons = Int32.Parse(Console.ReadLine());

            int i = 0;
            while (i < amountOfSalesPersons)
            {
                Console.Write(Environment.NewLine + "Enter name: ");
                var name = Console.ReadLine();

                Console.Write("Enter personal number: ");
                var persNum = Console.ReadLine();

                Console.Write("Enter disctrict: ");
                var disctrict = Console.ReadLine();

                Console.Write("Amount sold: ");
                var amountSold = Int32.Parse(Console.ReadLine());

                var person = new SalesPerson(name, Int32.Parse(persNum), disctrict, amountSold);

                if (allSalesWithLevel.ContainsKey(person.Level)) //If the newly created person belongs to a level already seen in the loop, add person to corresponding list.
                    allSalesWithLevel[person.Level].Add(person);

                else
                    allSalesWithLevel.Add(person.Level, new List<SalesPerson> { person }); //If a level that is not in my dictionary, insert level and person.

                i++;
            }

            return allSalesWithLevel;
        }

        public static void PrintSalesmen(List<SalesPerson> salesPerLevel)
        {
            foreach (var salesman in salesPerLevel.OrderBy(x => x.AmountSold))
            {
                var saleInfo = $"{salesman.Name,-15}{salesman.PersonalNum,-15}{salesman.Disctrict,-15}{salesman.AmountSold,-15}";
                Console.WriteLine(saleInfo);
            }
        }
        public static string LevelDetails(int level)
        {
            switch (level)
            {
                case 1:
                    return " salesmen has reached level 1: Less then 50 articles sold.";

                case 2:
                    return " salesmen has reached level 2: 50-99 articles sold.";

                case 3:
                    return " salesmen has reached level 3: 100-199 articles sold";

                default:
                    return " salesmen has reached level 4: over 199 articles sold";
            }
        }

        static void Main(string[] args)
        {
            Dictionary<int, List<SalesPerson>> salesPerLevel = EnterSalesPersons();

            string reportHeader = String.Format("{0}{1,15}{2,15}{3,15}", "Name", "PersonalNum", "Disctrict", "Amount");
            Console.WriteLine(Environment.NewLine+ reportHeader);

            foreach (var level in salesPerLevel.OrderBy(x => x.Key)) //Loops the keys to check ecah level.
            {
                PrintSalesmen(salesPerLevel[level.Key]); //Prints information about each salesperson in the list-value.
                var summary = salesPerLevel[level.Key].Count() + LevelDetails(level.Key) + Environment.NewLine;
                Console.WriteLine(summary);
            }
        }
    }
}

