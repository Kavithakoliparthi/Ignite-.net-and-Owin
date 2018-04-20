using Apache.Ignite.Core;
using Apache.Ignite.Core.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ignitedotnet
{
    class AtomicLong
    {
        static void Main()
        {
            Test();
            Console.ReadKey();
        }
        static void Test()
        {
            using (var ignite = Ignition.Start())
            {
                Console.WriteLine(">>Atomic Example");
                IAtomicLong atomiclong = ignite.GetAtomicLong(AtomicLongIncrementAction.AtomicLongName, 0, true);
                Console.WriteLine("Actomic Long Initial Value:" + atomiclong);
                    //Broadcast an action that increments AtomicLong a number of times
                ignite.GetCompute().Broadcast(new AtomicLongIncrementAction());
                //Actual value will depend on a number of participating nodes
                Console.WriteLine(">> Atomic long current value:"+atomiclong.Read());
            }
            Console.WriteLine();
        }
    }
}
