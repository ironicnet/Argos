using ArgosCore;
using ArgosCore.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArgosConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            ResourceContainer container = new ResourceContainer("Server 1");
            container.SetProperty("SERVERNAME", "SRVR1");
            container.SetReadTrigger(new ArgosCore.Triggers.FrequencyTrigger(10000));
            Resource service = new Resource("Service1", "UNKNOWN");
            service.SetProperty("Status_URL", "http://www.google.com");
            service.SetProperty("Status_Method", "GET");
            service.SetReader(new HttpReader("$Status_URL$", "$Status_Method$"));
            service.SetReadTrigger(new ArgosCore.Triggers.FrequencyTrigger(5000));
            container.AddResource(service);

            container.StartTrigger();
            service.StartTrigger();

            Console.ReadLine();
            service.StopTrigger();
            container.StopTrigger();

            Console.ReadLine();

        }
    }
}
