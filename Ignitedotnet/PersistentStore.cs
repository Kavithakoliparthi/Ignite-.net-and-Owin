using Apache.Ignite.Core;
using Apache.Ignite.Core.PersistentStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ignitedotnet
{
    class PersistentStore
    {
        static void Main(string[] args)
        {
            Sample();
            Console.ReadKey();
        }
        static void Sample()
        {
            var cfg = new IgniteConfiguration
            {
                PersistentStoreConfiguration =new PersistentStoreConfiguration()
            };
            using (var ignite=Ignition.Start())
            {
                    //Required with enabled persistence
                ignite.SetActive(true);
                var cache = ignite.GetOrCreateCache<int,string>("myCache");
                cache.Put(1, "Ignite");
                cache.Put(2, ".net");
                Console.WriteLine(cache.Get(1) + " " + cache.Get(2));
                Console.WriteLine("Cache size is:" + cache.GetSize());
            }
        }
    }
}
