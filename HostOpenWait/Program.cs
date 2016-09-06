using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace HostOpenWait
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(OpenWaitService.ServiceOpenWait)))
            {
                host.Open();

                Console.WriteLine("Start service ...");
                Console.ReadLine();
            }
        }
    }
}
