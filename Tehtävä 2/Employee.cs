using System;
using System.Collections.Generic;

// Janne Rajuvaara
// NTK17SP 2018

namespace CS2T2
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Job> Jobs { get; set; }
    }
}
