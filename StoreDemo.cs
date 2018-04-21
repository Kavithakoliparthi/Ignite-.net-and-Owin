using Apache.Ignite.Core;
using Apache.Ignite.Core.Cache.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ignitedotnet
{
    class StoreDemo
    {
        private const string CacheName = "cache store";
        /// <summary>
        /// Main method is starting point of execution
        /// </summary>

        [STAThread]
        public static void Main()
        {
            Test();
            Console.ReadKey();
        }
        static void Test()
        {
            using (var ignite = Ignition.Start())   //FromApplicationConfiguration())
            {
                Console.WriteLine(">>>cache store example");
                var cache = ignite.GetOrCreateCache<int, Employee>(new CacheConfiguration
                {
                    Name = CacheName,
                    ReadThrough = true,
                    WriteThrough = true,
                    KeepBinaryInStore = false,
                    CacheStoreFactory = new EmployeeStoreFactory()
                });
                //clean up caches all nodes before run
                cache.Clear();
                Console.WriteLine(">> Cleared values from cache");
                Console.WriteLine(">> Current cache size :" + cache.GetSize());

                //load entries from store which pass provided filter
                cache.LoadCache(new EmployeeStorePredicate());

                Console.WriteLine(">>Load entry from store through ICache.LoadCache()");
                Console.WriteLine(">> Current  cache size:" + cache.GetSize());

                //Load entry from store calling ICache.Get()
                Employee emp = cache.Get(2);

                Console.WriteLine("Loaded entry from stor ethrough ICach.Get():" + emp);
                Console.WriteLine(">> Current cache size:" + cache.GetSize());

                //Put an entry to the cache
                cache.Put(3, new Employee(
                    "James Wilson",
                    12500,
                    new Address("1096 Eddy Street, San Francisco, CA", 94109),
                    new List<string> { "Human Resources", "Customer Service" }
                    ));

                Console.WriteLine();
                Console.WriteLine(">>> Put entry to cache. ");
                Console.WriteLine(">>> Current cache size: " + cache.GetSize());

                // Clear values again.
                cache.Clear();

                Console.WriteLine();
                Console.WriteLine(">>> Cleared values from cache again.");
                Console.WriteLine(">>> Current cache size: " + cache.GetSize());

                // Read values from cache after clear.
                Console.WriteLine();
                Console.WriteLine(">>> Read values after clear:");

                for (int i = 1; i <= 3; i++)
                    Console.WriteLine(">>>     Key=" + i + ", value=" + cache.Get(i));
            }

            Console.WriteLine();
            Console.WriteLine(">>> Example finished, press any key to exit ...");
            Console.ReadKey();
        }

    }
}

