using System;
using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Activity workflow1 = new Activity1();
            
            WorkflowInvoker.Invoke(workflow1);
        }
    }
}
