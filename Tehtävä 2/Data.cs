using System;
using System.Collections.Generic;

// Janne Rajuvaara
// NTK17SP 2018

namespace CS2T2
{
    class Data
    {
        public static List<Job> jobs = new List<Job> {
            new Job { Id = 1, Title = "Siiven C luokkien ylläpito", StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(1) },
            new Job { Id = 2, Title = "Parkkialueiden maalaus", StartDate = DateTime.Today.AddDays(-5), EndDate = DateTime.Today.AddDays(5) },
            new Job { Id = 3, Title = "Puiden istutus", StartDate = DateTime.Today.AddDays(4), EndDate = DateTime.Today.AddDays(10) },
            new Job { Id = 4, Title = "Väliseinän rakentaminen", StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(15) }
        };
        public static List<Employee> employees = new List<Employee> {
            new Employee { Id = 1, Name = "Matti Mainio", Jobs = new List<Job> {jobs[0],jobs[1],jobs[2],jobs[3]}},
            new Employee { Id = 2, Name = "Jussi Juonio", Jobs = new List<Job> {jobs[2],jobs[3]}},
            new Employee { Id = 3, Name = "Maija‐Liisa Lahtinen", Jobs = new List<Job> {jobs[1],jobs[3]}},
            new Employee { Id = 4, Name = "Anneli Aalto", Jobs = new List<Job> {jobs[0],jobs[1],jobs[3]}},
            new Employee { Id = 5, Name = "Pentti Penttilä", Jobs = new List<Job> {jobs[2]}},
            new Employee { Id = 6, Name = "Sakari Ruoho", Jobs = new List<Job> {jobs[0],jobs[3]}}
        };
    }
}
