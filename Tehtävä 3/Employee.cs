// Janne Rajuvaara
// NTK17SP

using System;

namespace CS2T3
{
    public class Employee
    {
        private double _salary;

        public int Id { get; }
        public string FirstName;
        public string LastName;
        public Department Department;
        public DateTime StartDate;
        public DateTime? EndDate; // nullable DateTime
        public DateTime? DateOfBirth;

        public string Name => $"{LastName} {FirstName}";

        public int Age => (DateOfBirth == null) ? 0 : (int)((DateTime.Now - DateOfBirth)?.TotalDays / 365.2421875);

        public double Salary
        {
            get => Math.Round(_salary, 2);
            
            set
            {
                try
                {
                    if (value >= 0)
                    {
                        _salary = value;
                    }
                    else
                    {
                        throw new ApplicationException("Negatiivinen palkka");
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public Employee(int id, string first, string last, DateTime dob, double salary)
        {
            Id = id;
            FirstName = first;
            LastName = last;
            DateOfBirth = dob;
            Salary = salary;

            StartDate = DateTime.Today;
            EndDate = null;
        }

        public override string ToString() => $"{Id} {FirstName} {LastName}";
    }
}
