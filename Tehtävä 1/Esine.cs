using System;

// Janne Rajuvaara
// NTK17SP 2018

namespace H1T1
{
    class Esine : INimi, IComparable<Esine>
    {
        private double _paino;

        public double Paino
        {
            get
            {
                return _paino;
            }
            set
            {
                _paino = (value > 0) ? Math.Round(value, 3) : throw new ApplicationException("Painon oltava suurempi kuin nolla.");
            }
        }

        public string Nimi { get; set; }

        public int PainoG => (int)(Paino * 1000);

        public int CompareTo(Esine esine) => _paino.CompareTo(esine.Paino);

        public override string ToString()
        {
            return $"{Nimi} {Paino} kg";
        }
    }
}