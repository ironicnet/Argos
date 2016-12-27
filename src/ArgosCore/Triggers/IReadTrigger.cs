using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArgosCore.Triggers
{
    public interface IReadTrigger
    {
        void Stop();
        void Start();
        void AddListener(Resource resource);
    }
}
