using Apache.Ignite.Core;
using Apache.Ignite.Core.Compute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ignitedotnet
{
    class Program
    {
       static void Main(String[] args)
        {
            Ignition.Start();
            Console.WriteLine("Ignite .net Program");
            Console.ReadLine();
        }
    }
}
