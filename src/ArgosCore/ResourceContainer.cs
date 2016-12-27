using System;
using System.Linq;
using System.Collections.Generic;

namespace ArgosCore
{
    public class ResourceContainer : Resource
    {
        protected List<Resource> Resources { get; set; }
        public ResourceContainer(string name):base(name, "UNKNOWN")
        {
            Resources = new List<Resource>();
        }
        public override string GetStatus()
        {
            return string.Join(",", Resources.Select(r => r.GetStatus()));
        }

        public void AddResource(Resource resource)
        {
            Resources.Add(resource);
        }
    }
}