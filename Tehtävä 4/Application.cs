using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// Janne Rajuvaara
// NTK17SP 2018
// Epäinhimillinen versio

namespace CS2T4
{
    static class Application
    {
        static int LastOperationId;
        static List<Operation> Operations;
        static Random r;
        static int MaxBreaks;
        static int MinTimeInSeconds;
        static int MaxTimeInSeconds;

        static Application()
        {
            LastOperationId = 0;
            Operations = new List<Operation>();
            r = new Random();
            MaxBreaks = 10;
            MinTimeInSeconds = 5;
            MaxTimeInSeconds = 10;
        }

        static void PrintOperations(List<Operation> Ops)
            // Modasin tehtävänannon metodia niin, että ohjelma ei heitä häränpyllyä, 
            // jos käyttäjä antaa lopetus-käskyn ennen kaikkien tehtävien valmistumista.
        {
            bool allEnded = false;

            while (!allEnded)
            {
                allEnded = true;
                foreach (Operation o in Ops)
                {
                    if (o.Ended.HasValue)
                    {
                        o.PrintEnded();
                    }
                    else
                    {
                        allEnded = false;
                    }
                } 
                System.Threading.Thread.Sleep(1000);
            }
        }

        async static Task KaynnistaOperaatioAsync(Operation o)
        {
            await Task.Run(() =>
            {
                for (int i = 1; i <= o.Breaks; i++)
                {
                    System.Threading.Thread.Sleep((int)((o.TotalTimeInSeconds * 1.0 / o.Breaks) * 1000));
                    o.SpendTimeInSeconds = (int)(o.TotalTimeInSeconds * i * 1.0 / o.Breaks);
                    o.Print();
                }
                o.Ended = DateTime.Now;
            });
        }

        static void ClearLine()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, 0);
        }

        public static void Run()
        {
            bool jatka = true;
            while (jatka == true)
            {
                char merkki;
                while (true)
                {
                    try
                    {
                        ClearLine();
                        merkki = Syote.Merkki("Käynnistä uusi operaatio = K, Lopeta ohjelma = L: ");
                        break;
                    }
                    catch { /* MOI MAAILMA */ } 
                }
                
                if (merkki == 'l' || merkki == 'L')
                {
                    ClearLine();
                    Console.SetCursorPosition(0, 0);
                    Console.Write("Paina Enter, kun kaikki operaatiot on suoritettu");
                    PrintOperations(Operations);
                    ConsoleKeyInfo vahvistus = Console.ReadKey();
                    if (vahvistus.Key == ConsoleKey.Enter)
                    {
                        jatka = false;
                    }
                    break;
                }
                else if (merkki == 'k' || merkki == 'K')
                {
                    LastOperationId++;
                    Operation o = new Operation() { Id = LastOperationId, Breaks = r.Next(1, MaxBreaks), TotalTimeInSeconds = r.Next(MinTimeInSeconds, MaxTimeInSeconds) };
                    o.Print(); // lisäsin tänne printtikomennon, jotta operaatioiden lisäyksellä ei ole viivettä ruudulla. huom. Task.Run sisäisen loopin alku int i = 1.
                    Operations.Add(o);
                    Task temp = KaynnistaOperaatioAsync(o);
                }
            }
        }
    }
}
