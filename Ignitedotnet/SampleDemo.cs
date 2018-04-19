using Apache.Ignite.Core;
using Apache.Ignite.Core.Compute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ignitedotnet
{
    class SampleDemo
    {
        static void Main(string[] args)
        {
            compute();
            Console.ReadKey();
        }
        static void compute()
        {
            using (var ignite = Ignition.Start())
            {
                var v = "count total number of characters using callable".Split(' ')
                     .Select(word => new ComputeFunc { Word = word });
                ICollection<int> i = ignite.GetCompute().Call(v);
                var sum = i.Sum();
                Console.WriteLine(">> Total number of characters in the phrase is '{0}'.", sum);
            }
        }
        class ComputeFunc : IComputeFunc<int>
        {
            public string Word { get; set; }
            public int Invoke()
            {
                return Word.Length;
            }
        }
    }
}
