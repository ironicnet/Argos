using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArgosCore
{
    public class Guardian : IGuardian
    {
        public List<Resource> Resources { get; set; }
        public Guardian()
        {
            Resources = new List<Resource>();
        }
    }
}
