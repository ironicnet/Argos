using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArgosCore
{
    public interface IGuardian
    {
        List<Resource> Resources { get; set; }
    }
}
