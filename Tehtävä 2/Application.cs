using System;
using System.Collections.Generic;

// Janne Rajuvaara 
// NTK17SP 2018

namespace CS2T2
{
    static class Application
    {
        static private ConsoleControl JobMenu;
        static private ConsoleControl JobDetails;
        static private ConsoleControl JobEmployees;

        static private void BindMenuData(List<Job> jobs)
        {
            if (JobMenu.Items == null)
            {
                JobMenu.Items = new List<string>();
            }
            else
            {
                JobMenu.Items.Clear();
            }
            foreach (Job j in jobs)
            {
                JobMenu.Items.Add($"{j.Id} {j.Title}");
            }
        }

        static private void BindDetailsData(Job job)
        {
            if (JobDetails.Items == null)
            {
                JobDetails.Items = new List<string>();
            }
            else
            {
                JobDetails.Items.Clear();
            }
            JobDetails.Items.Add("TYÖN TIEDOT");
            JobDetails.Items.Add($"Id: {job.Id}");
            JobDetails.Items.Add($"Nimi: {job.Title}");
            JobDetails.Items.Add($"Alkaa: {job.StartDate.ToShortDateString()}");
            JobDetails.Items.Add($"Loppuu: {job.EndDate.ToShortDateString()}");
        }

        static private void BindEmployeesData(Job job)
        {
            if (JobEmployees.Items == null)
            {
                JobEmployees.Items = new List<string>();
            }
            else
            {
                JobEmployees.Items.Clear();
            }

            foreach (Employee e in Data.employees)
            {
                //if (e.Jobs.Contains(job))
                //{
                //    JobEmployees.Items.Add(e.Name);
                //}

                foreach (Job j in e.Jobs)
                {
                    if (j.Id == job.Id)
                    {
                        JobEmployees.Items.Add(e.Name);
                    }
                }
            }
        }

        private static void Initialize()
        {
            // kevyt kenttäautismi
            JobMenu = new ConsoleControl(1, 2, (Console.WindowWidth/2) - 4, Data.jobs.Count) { BackColor = ConsoleColor.Gray, TextColor = ConsoleColor.DarkBlue };
            JobDetails = new ConsoleControl((Console.WindowWidth / 2) - 1, 2, Console.WindowWidth/2 - 1, 5) { BackColor = ConsoleColor.Gray, TextColor = ConsoleColor.DarkGreen };
            JobEmployees = new ConsoleControl(JobDetails.Column, JobDetails.Height + 3, JobDetails.Width, Console.WindowHeight - JobDetails.Height - 1) { BackColor = ConsoleColor.Gray, TextColor = ConsoleColor.DarkRed };
            //JobEmployeesin korkeus on kyllä kieltämättä vähän kamalasti toteutettu

            BindMenuData(Data.jobs);

            /*  asetetaan Mediator‐olion JobChanged‐tapahtumalle tapahtumakäsittelijä, jossa kutsutaan 
             *  metodit BindDetailsData ja BindEmployeesData antaen molemmissa argumentiksi 
             *  tapahtumakäsittelijän saaman JobEventArgs‐olion Job‐ominaisuuden arvo.  */

            // Mitä riivattua minä juuri luin?
            Mediator.Instance.JobChanged += OnJobChanged;
        }

        private static void OnJobChanged(object sender, JobChangedEventArgs args)
        {
            BindDetailsData(args.Job);
            BindEmployeesData(args.Job);
        }

        private static void MenuSelectionChanged(int id)
        {
            foreach (Job j in Data.jobs)
            {
                if (j.Id == id)
                {
                    Mediator.Instance.OnJobChanged(JobMenu, j);
                    JobDetails.Draw();
                    JobEmployees.Draw();
                }
            }
        }

        private static void CleanInputField()
        {
            //Pyyhitään ylin rivi, jotta syötekenttään ei jää kummittelemaan vanhoja töherryksiä.
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < Console.WindowWidth; i++) { Console.Write(" "); }
            JobMenu.Draw(); //Piirtämällä menu uudelleen vältetään tilanne jossa rakas käyttäjä on antanut niin pitkän syötteen, että se on mennyt menun päälle.
        }

        public static void Run()
        {
            Initialize();
            JobMenu.Draw();

            int syote = -1;

            Console.SetCursorPosition(0, 0);

            do
            {
                try
                {
                    CleanInputField();

                    //Aloitetaan puhtaalta pöydältä
                    Console.SetCursorPosition(0, 0);
                    syote = Syote.Kokonaisluku("Valitse työn id (nolla lopettaa): ", 0, JobMenu.Items.Count);
                }
                catch
                {
                    CleanInputField();

                    Console.SetCursorPosition(0, 0);
                    Console.Write("Virheellinen syöte. Paina enter."); 
                    Console.ReadLine(); //Oikeesti tässä voi kirjottaa vaikka romaanin konsoliin ja sitten vasta painaa enter.
                }
                Console.SetCursorPosition(0, 0);
                MenuSelectionChanged(syote);
            } while (syote != 0);
        }
    }
}
