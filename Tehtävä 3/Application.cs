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
            MenuItem m1 = new MenuItem() { Id = 1, Name = "50-vuotiaat työntekijät"};
            MenuItem m2 = new MenuItem() { Id = 2, Name = "Osastot yli 50 henkilöä" };
            MenuItem m3 = new MenuItem() { Id = 3, Name = "Sukunimen työntekijät" };
            MenuItem m4 = new MenuItem() { Id = 4, Name = "Osastojen isoimmat palkat" };
            MenuItem m5 = new MenuItem() { Id = 5, Name = "Viisi yleisintä sukunimeä" };
            MenuItem m6 = new MenuItem() { Id = 6, Name = "Osastojen ikäjakaumat" };

            Menu = new List<MenuItem>()
            {
                m1,m2,m3,m4,m5,m6
            };

        }

        static void m1_selected(object sender, MenuItemEventArgs e)
        {
            throw new System.NotImplementedException();
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
            return Syote.KokonaislukuPakoittaen("Valitse (0 = lopetus):", 0, Menu.Max(x => x.Id));
        }

        public static void Run()
        {
            Initialize();
            int selection = -1;
            do
            {
                selection = ReadFromMenu();
                Menu.Select(x => x.Id = selection);
            } while (selection != 0);
        }
    }
}
