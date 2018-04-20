using Apache.Ignite.Core;
using Apache.Ignite.Core.Lifecycle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ignitedotnet
{
    class LifecycleHandler : ILifecycleHandler
    {
        public void OnLifecycleEvent(LifecycleEventType evt)
        {
            if (evt == LifecycleEventType.AfterNodeStart)
                Started = true;

            else if (evt == LifecycleEventType.AfterNodeStop)
                Started = false;
        }
        public bool Started { get; set; }
    }
    class Test
    {
        static void Main(string[] args)
        {
            IgniteLifeCycle();
            Console.ReadKey();
        }
        static void IgniteLifeCycle()
        {
            var cfg = new IgniteConfiguration
            {
                LifecycleHandlers = new[] { new LifecycleHandler() }
            };

            using (var ignite = Ignition.Start(cfg))
            {
                LifecycleHandler l = new LifecycleHandler();
                if (l.Started)
                    Console.WriteLine("After node start");
                else
                    Console.WriteLine("After node stop");
            }
        }
    }
}