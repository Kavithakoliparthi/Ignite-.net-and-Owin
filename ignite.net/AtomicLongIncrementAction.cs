using Apache.Ignite.Core;
using Apache.Ignite.Core.Compute;
using Apache.Ignite.Core.DataStructures;
using Apache.Ignite.Core.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ignitedotnet
{
    class AtomicLongIncrementAction : IComputeAction
    {
        public const string AtomicLongName = "dotnet-atomic-long";

        [InstanceResource] private readonly IIgnite ignite;
        public void Invoke()
        {
            IAtomicLong atomicLong = ignite.GetAtomicLong(AtomicLongName, 0, true);
            for (int i = 0; i < 20; i++)
                Console.WriteLine(">> AtomicLong value has been incremented:"+atomicLong.Increment());
        }
    }
}
