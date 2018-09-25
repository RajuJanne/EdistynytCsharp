using System;
using System.Collections.Generic;

// Janne Rajuvaara
// NTK17SP 2018

namespace H1T1
{
    class Laatikko : INimi
    {
        public string Nimi { get; set; }
        public List<Esine> Esineet { get; set; }
        public int MaxPaino { get; set; }

        public Laatikko(int maxPaino = 100)
        {
            MaxPaino = maxPaino;
            Esineet = new List<Esine>();
        }

        //ominaisuutena toteutettuna laatikon paino.
        //public double LaatikonPaino
        //{
        //    get 
        //    {
        //        double sum = 0;
        //        foreach (Esine e in Esineet)
        //        {
        //            sum += e.Paino;
        //        }
        //        return sum;
        //    }
        //}

        //ominaisuutena toteutettu painavin
        //public Esine Painavin
        //{
        //    get
        //    {
        //        Esineet.Sort();
        //        return Esineet[Esineet.Count - 1];
        //    }
        //}

        public double LaatikonPaino()
        {
            double SummaPaino = 0;
            foreach (Esine e in Esineet)
            {
                SummaPaino += e.Paino;
            }
            return SummaPaino;
        }

        public Esine Painavin()
        {
            Esineet.Sort(); // järjestää pienimmästä suurimpaan, jolloin indeksillä 0 on kevyin, ja Esineet.Count - 1 (eli listan viimeinen otus) on raskain
            return Esineet[Esineet.Count - 1];
        }

        public void LisaaEsine(Esine esine)
        {
            if (LaatikonPaino() + esine.Paino <= MaxPaino)
            {
                Esineet.Add(esine);
            }
            else
            {
                throw new ApplicationException($"Virhe: Laatikkoon {Nimi} ei voitu lisätä, koska maksimipaino olisi ylittynyt.");
            }
        }
    }
}