using System;
using static System.Console;

// Janne Rajuvaara
// NTK17SP 2018

//Luokka Operation sisältää automaattiset ominaisuudet Id, TotalTimeInSeconds, SpendTimeInSeconds, 
//Breaks(kokonaislukuja), Started ja Ended(DateTime).

namespace CS2T4
{
    class Operation
    {
        public int Id;
        public int TotalTimeInSeconds;
        public int SpendTimeInSeconds;
        public int Breaks;
        public DateTime Started;
        public DateTime? Ended; // tein tästä nullablen, niin PrintOperations() ei sotke pakkaa
        private int top;
        private int left;

        public Operation()
        {
            Started = DateTime.Now;
        }


        public void Print()
        {
            top = CursorTop;
            left = CursorLeft;

            SetCursorPosition(0, Id);
            // Kun epäröit, lisää sulkeita.
            Write($"{Id} {Started.ToLongTimeString()} {(int)((1.0 * SpendTimeInSeconds / TotalTimeInSeconds) * 100)}%");

            SetCursorPosition(left, top);
        }
        public void PrintEnded()
        {
            top = CursorTop;
            left = CursorLeft;

            SetCursorPosition(0, Id);
            Write($"{Id} {Started.ToLongTimeString()} - {Ended?.ToLongTimeString()} = duration {(Ended - Started)?.Seconds} seconds");

            SetCursorPosition(left, top);
        }
    }
}
