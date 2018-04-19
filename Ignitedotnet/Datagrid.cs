using Apache.Ignite.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ignitedotnet
{
    class Datagrid
    {
        static void Main(string[] args)
        {
            test();
            Console.ReadKey();
        }
        static void test()
        {
            using (var ignite=Ignition.Start())
            {
                var cache=ignite.GetOrCreateCache<int,string>("mycache");
                for(int i=0;i<10;i++)
                {
                    cache.Put(i, i.ToString());
                }
                for(int i=0;i<10;i++)
                {
                    Console.WriteLine("[key={0}, val={1}]", i,cache.Get(i));
                }
            }
        }
    }
}
