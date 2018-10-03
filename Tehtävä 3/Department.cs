using System.Collections.Generic;
using System.Linq;

// Janne Rajuvaara
// NTK17SP

namespace CS2T3
{
    public class Department
    {
        public int Id;
        public string Name;
        public List<Employee> Employees;
        public int EmployeeCount => Employees.Count(); 

        public Department()
        {
            Employees = new List<Employee>();
        }
        public Department(int id, string name)
            : this()
        {
            Id = id;
            Name = name;
        }

        public override string ToString() => $"{Name} ({EmployeeCount})";
    }
}