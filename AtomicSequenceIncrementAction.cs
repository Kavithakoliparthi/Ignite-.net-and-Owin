using Apache.Ignite.Core;
using Apache.Ignite.Core.Compute;
using Apache.Ignite.Core.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ignitedotnet
{
    public class AtomicSequenceIncrementAction : IComputeAction
    {
        public const string AtomicSequenceName = "Atomic sequence increment action";
        private readonly IIgnite ignite;
        /// <summary>
        /// Invoke method
        /// </summary>
        public void Invoke()
        {
            IAtomicSequence atomicSequence = ignite.GetAtomicSequence(AtomicSequenceName,0,true);
            for (int i = 0; i < 10; i++)
                Console.WriteLine(">>Atomic sequence value has been incremneted"+atomicSequence.Increment());
        }
    }
}
