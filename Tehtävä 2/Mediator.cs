using System;

// Janne Rajuvaara
// NTK17SP 2018

namespace CS2T2
{
    sealed class Mediator
    {
        private static readonly Mediator instance = new Mediator();
        
        public static Mediator Instance
        {
            get
            {
                return instance;
            }
        }

        // hiipivä välittäjä, kätketty rakentaja
        private Mediator() { } 

        // Tästä eteenpäin tehtävänanto aiheuttaa kriittistä kaipuuta kuolemaan

        public event EventHandler<JobChangedEventArgs> JobChanged;


        //public void OnJobChanged(object sender, Job job)
        //{
        //    var jobChangeDelegate = JobChanged as EventHandler<JobChangedEventArgs>;
        //    if (jobChangeDelegate != null)
        //    {
        //        jobChangeDelegate(sender, new JobChangedEventArgs() { Job = job });
        //    }
        //}

        public void OnJobChanged(object sender, Job job)
        {
            (JobChanged as EventHandler<JobChangedEventArgs>)?.Invoke(sender, new JobChangedEventArgs() { Job = job });
        }
    }
}
