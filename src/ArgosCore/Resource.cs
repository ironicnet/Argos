using System;
using System.Collections.Generic;

namespace ArgosCore
{
    public class Resource
    {
        public virtual string Name { get; set; }
        public virtual PropertyCollection Properties { get; set; }

        protected virtual IResourceReader Reader { get; set; }
        protected virtual Triggers.IReadTrigger Trigger { get; set; }
        private string Status;

        public Resource(string name, string initialStatus, PropertyCollection properties)
        {
            Name = name;
            Status = initialStatus;
            Properties = properties;
        }
        public Resource(string name, string initialStatus) : this(name, initialStatus, new PropertyCollection())
        {
        }


        public virtual void Read()
        {
            System.Diagnostics.Debug.WriteLine(this.Name + " Reading");
            if (Reader != null)
            {
                System.Diagnostics.Debug.WriteLine(this.Name + " No reader assigned");
                Reader.Read(this);
            }
        }

        public virtual string GetStatus()
        {
            return Status;
        }

        public void SetReader(IResourceReader reader)
        {
            Reader = reader;
        }
        public void SetReadTrigger(Triggers.IReadTrigger trigger)
        {
            Trigger = trigger;
            Trigger.AddListener(this);
        }

        public virtual void SetStatus(string newStatus)
        {
            System.Diagnostics.Debug.WriteLine(this.Name + " Status changing from " + Status  + " to " + newStatus);
            Status = newStatus;
        }
        public void SetProperty(string name, string value)
        {
            Properties.Add(name, value);
        }

        public void StartTrigger()
        {
            Trigger.Start();
        }

        public void StopTrigger()
        {
            Trigger.Stop();
        }
    }
    
}