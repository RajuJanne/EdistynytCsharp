using System;
using System.Collections.Generic;
using static System.Console;

// Janne Rajuvaara
// NTK17SP 2018

// Tehtävänannossa ei erikseen käsketty tekemään täysin virheenkestävää, joten muutamissa paikoissa on laiskuuden kunniaksi jätetty try-catch pois

namespace H1T1
{
    class Program
    {
        static void Main(string[] args)
        {
            var Laatikot = new List<Laatikko>();
            WriteLine("Tehdään laatikot");

            try
            {
                do
                {

                    Write("Anna laatikon nimi (tyhjä lopettaa):");
                    string syote = ReadLine();

                    if (syote == "") { break; } // tyhjä string lopettaa

                    else
                    {

                        Laatikko laatikko = new Laatikko() { Nimi = syote };

                        WriteLine("Lisätään laatikkoon esineitä");

                        do
                        {

                            Write("Anna esineen nimi (tyhjä lopettaa): ");
                            string syote2 = ReadLine();

                            if (syote2 == "") { break; } // tyhjä string lopettaa

                            else
                            {

                                Esine esine = new Esine() { Nimi = syote2 };

                                try
                                {
                                    // huom. itsellä käytössä desimaalierottimena piste!
                                    Write("Anna esineen paino (kg): ");
                                    string paino = ReadLine();
                                    double dpaino = double.Parse(paino);
                                    esine.Paino = dpaino;
                                }
                                catch
                                {
                                    WriteLine("Painon oltava positiivinen luku");
                                }

                                Write("Lisättävä määrä: ");
                                string syote3 = ReadLine();
                                int isyote3 = int.Parse(syote3);
                                int virhe = 0;

                                try
                                {

                                    for (int i = 0; i < isyote3; i++)
                                    {
                                        virhe = i; // tällä seurataan maksimipainon ylittyessä miten moni esine lisättiin laatikkoon.
                                        laatikko.LisaaEsine(esine);
                                    }
                                    WriteLine($"Esinettä {esine.Nimi} lisättiin {isyote3} määrä");

                                }
                                catch (Exception e)
                                {
                                    WriteLine(e.Message);

                                    WriteLine($"Esinettä {esine.Nimi} lisättiin {virhe} määrä");

                                }
                            }
                        } while (true);

                        Laatikot.Add(laatikko);

                    }

                } while (true);
            }
            catch (Exception e)
            {

                WriteLine(e.Message);
            }

            WriteLine(); // muotoilun vuoksi tyhjä rivi tulostuksen eteen

            foreach (Laatikko la in Laatikot)
            {
                WriteLine($"Laatikko {la.Nimi} {la.LaatikonPaino()} kg");
                WriteLine($"Esineitä {la.Esineet.Count} kpl.");
                WriteLine($"Painavin esine {la.Painavin()}");
            }

            ReadLine();
        }
    }
}