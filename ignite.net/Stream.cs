using Apache.Ignite.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ignitedotnet
{
    class Stream
    {
        private const int EntryCount = 500000;
        private const string CacheName = "dotnet-cache-datastreamer";
        static void Main()
        {
            using (var ignite = Ignition.Start())
            {
                Console.WriteLine(">> cache data streamer example");
                ignite.GetOrCreateCache<int, Account>(CacheName).Clear();
                Stopwatch timer = new Stopwatch();
                timer.Start();
                using (var v = ignite.GetDataStreamer<int, Account>(CacheName))
                {
                    v.PerNodeBufferSize = 1024;
                    for(int i=0;i<EntryCount;i++)
                    {
                        v.AddData(i, new Account(i, i));

                        if (i > 0 && i % 10000 == 0)
                            Console.WriteLine("Loaded" + i + "accounts");
                    }
                }
                timer.Stop();
                long dur = timer.ElapsedMilliseconds;
                Console.WriteLine(">> Loaded" +EntryCount + "accounts in"+dur+"ms");
            }
            Console.WriteLine("press any key to exit");
            Console.ReadKey();
        }
    }
}
