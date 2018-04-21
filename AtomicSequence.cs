using Apache.Ignite.Core;
using Apache.Ignite.Core.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ignitedotnet
{
    class AtomicSequence
    {
        static void Main()
        {
            using (var ignite = Ignition.Start())
            {
                Console.WriteLine("Atomic sequence example");
                IAtomicSequence atomicSequence = ignite.GetAtomicSequence(AtomicSequenceIncrementAction.AtomicSequenceName, 0,true);              
                Console.WriteLine(">>Atomic Sequence Initial value:" + atomicSequence.Read());
                // Broadcast an action that increments AtomicSequence a number of times.
                ignite.GetCompute().Broadcast(new AtomicSequenceIncrementAction());

                // Actual value will depend on number of participating nodes.
                Console.WriteLine("\n>>> Atomic sequence current value: " + atomicSequence.Read());
            }
            Console.WriteLine("\n>>> Check output on all nodes.");
            Console.WriteLine("\n>>> press any key to exit ...");
            Console.ReadKey();
        }
    }
}
