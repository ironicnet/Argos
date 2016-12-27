using System;
using System.Timers;

namespace ArgosCore.Triggers
{
    public class FrequencyTrigger : IReadTrigger
    {

        private Timer Timer;
        private System.Collections.Generic.List<Resource> Listeners;

        public FrequencyTrigger(int milliseconds)
        {
            Listeners = new System.Collections.Generic.List<Resource>();
            Timer = new Timer(milliseconds);
            Timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("FrequencyTrigger triggered");
            for (int i = 0; i < Listeners.Count; i++)
            {
                Listeners[i].Read();
            }
        }

        public void Stop()
        {
            System.Diagnostics.Debug.WriteLine("FrequencyTrigger stopped");
            Timer.Stop();
        }

        public void Start()
        {
            System.Diagnostics.Debug.WriteLine("FrequencyTrigger started");
            Timer.Start();
        }

        public void AddListener(Resource resource)
        {
            if (!Listeners.Contains(resource))
                Listeners.Add(resource);
        }
    }
}