using System.Collections.Generic;
using System.Linq;
using static System.Console;
using System.Reflection;

// Janne Rajuvaara & Hans Nieminen (WriteResult algoritmi)
// NTK17SP / SAMK

namespace CS2T3
{
    public class Application
    {
        static List<MenuItem> Menu;

        static void WriteResult<T>(int itemid, List<T> result)
        {
            string row;
            //otsikkorivit
            WriteLine();
            WriteLine(Menu.Where(mi => mi.Id == itemid).First().Name.ToUpper());
            WriteLine("‐".PadRight(18 * result[0].GetType().GetProperties().Length + 2, '‐'));
            //sarakeotsikkorivit
            if (result.Count > 0)
            {
                row = "";
                foreach (PropertyInfo property in result[0].GetType().GetProperties())
                {
                    row += $"{property.Name}".PadRight(16) + " | ";
                }
                WriteLine(row);
            }
            WriteLine("‐".PadRight(18 * result[0].GetType().GetProperties().Length + 2, '‐'));
            //datarivit
            foreach (object item in result)
            {
                row = "";
                foreach (PropertyInfo property in item.GetType().GetProperties())
                {
                    row += $"{property.GetValue(item, null).ToString()}".PadRight(16) + " | ";
                }
                WriteLine(row);
            }
            WriteLine("‐".PadRight(18 * result[0].GetType().GetProperties().Length + 2, '‐'));
            WriteLine();
            Write("Paina Enter jatkaaksesi.");
            ReadLine();
        }

        static void InitializeMenu()
        {

            Menu = new List<MenuItem>()
            {
                new MenuItem() { Id = 1, Name = "50-vuotiaat työntekijät"},
                new MenuItem() { Id = 2, Name = "Osastot yli 50 henkilöä" },
                new MenuItem() { Id = 3, Name = "Sukunimen työntekijät" },
                new MenuItem() { Id = 4, Name = "Osastojen isoimmat palkat" },
                new MenuItem() { Id = 5, Name = "Viisi yleisintä sukunimeä" },
                new MenuItem() { Id = 6, Name = "Osastojen ikäjakaumat" },
                new MenuItem() { Id = 7, Name = "Kymmenen kovapalkkaisinta alle 30-vuotiasta!"}
            };

            Menu[0].ItemSelected += (obj, a) =>
            {
                var result = Data.Employees
                .Where(e => e.Age == 50)
                .Select(e => e).ToList();
                WriteResult(a.ItemId, result);
            };

            Menu[1].ItemSelected += (obj, a) =>
            {
                var result = Data.Departments
                .Where(d => d.EmployeeCount > 50)
                .Select(d => new {
                    Id = d.Id,
                    Nimi = d.Name,
                    Vahvuus = d.EmployeeCount
                }).ToList();
                WriteResult(a.ItemId, result);
            };
            Menu[2].ItemSelected += (obj, a) =>
            {
                string ss = Syote.Merkkijono("Anna sukunimi: ");
                var result = Data.Employees
                .Where(e => e.LastName == ss)
                .Select(e => new
                {
                    Id = e.Id,
                    Nimi = e.Name
                }).ToList();
                WriteResult(a.ItemId, result);
            };
            Menu[3].ItemSelected += (obj, a) =>
            {
                // isoimmat palkat
                var result = Data.Departments.SelectMany(d => d.Employees,
                (d, e) => new
                {
                    Osasto = d.Name,
                    Palkka = e.Salary
                })
                .GroupBy(x => x.Osasto)
                .Select(y => new
                {
                    Osasto = y.Key,
                    Palkka = y.Max(x => x.Palkka)
                }
                ).ToList();
                WriteResult(a.ItemId, result);
            };
            Menu[4].ItemSelected += (obj, a) =>
            {
                //5 yleisintä sukunimeä
                var result = Data.Employees
                .GroupBy(e => e.LastName)
                .Select(e => new
                {
                    Sukunimi = e.Key,
                    Lkm = e.Count()
                })
                .OrderByDescending(e => e.Lkm)
                .Take(5)
                .ToList();

                WriteResult(a.ItemId, result);
            };
            Menu[5].ItemSelected += (obj, a) =>
            {
                // osastojen ikäjakauma alle 30, 30 - 50, yli 50
                var result = Data.Departments
                .Select(d
                => new
                {
                    Osasto = d.Name,
                    Alle30v = d.Employees.Where(e => e.Age < 30).Count(),
                    Välillä30_50v = d.Employees.Where(e => 30 >= e.Age && e.Age <= 50).Count(),
                    Yli50v = d.Employees.Where(e => e.Age > 50).Count()
                })
                .ToList();
                WriteResult(a.ItemId, result);
            };
            Menu[6].ItemSelected += (obj, a) =>
            {
                // 10 suurinta palkkaa alle 30-v
                var result = Data.Employees
                .Select(e =>
                new
                {
                    Nimi = e.Name,
                    Ikä = e.Age,
                    Palkka = e.Salary,
                    Osasto = e.Department
                })
                .Where(e => e.Ikä < 30)
                .OrderByDescending(e => e.Palkka)
                .Take(10)
                .ToList();

                WriteResult(a.ItemId, result);
            };
        }

        static void Initialize()
        {
            Data.GenerateData();
            InitializeMenu();
        }

        static int ReadFromMenu()
        {
            Clear();
            WriteLine("Vaihtoehdot");
            foreach (MenuItem m in Menu)
            {
                WriteLine(m.ToString());
            }
            // FUTUR PRUF eli listan Menu sisältämien olioiden korkein Id arvo
            return Syote.KokonaislukuPakoittaen("Valitse (0 = lopetus): ", 0, Menu.Max(x => x.Id));
        }

        public static void Run()
        {
            Initialize();
            int selection = -1;
            while (selection != 0)
            {
                selection = ReadFromMenu();
                // huomioi kynkäältä tarttuneet pahat tavat
                if (selection != 0) Menu[selection - 1].Select();
                else break;
            }
        }
    }
}