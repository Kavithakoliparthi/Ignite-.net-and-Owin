using Apache.Ignite.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ignitedotnet
{
    class Message
    {
        /// <summary>
        /// Runs the example.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            using (var ignite = Ignition.Start())
            {
                var remotes = ignite.GetCluster().ForRemotes();

                if (remotes.GetNodes().Count == 0)
                {
                    Console.WriteLine(">>> This example requires remote nodes to be started.");
                    Console.WriteLine(">>> Please start at least 1 remote node.");
                    Console.WriteLine(">>> Refer to example's documentation for details on configuration.");
                }
                else
                {
                    Console.WriteLine(">>> Messaging example started.");
                    Console.WriteLine();

                    // Set up local listeners
                    var localMessaging = ignite.GetCluster().ForLocal().GetMessaging();

                    var msgCount = remotes.GetNodes().Count * 10;

                    var orderedCounter = new CountdownEvent(msgCount);
                    var unorderedCounter = new CountdownEvent(msgCount);

                    localMessaging.LocalListen(new LocalListner(unorderedCounter), Topic.Unordered);

                    localMessaging.LocalListen(new LocalListner(orderedCounter), Topic.Ordered);

                    // Set up remote listeners
                    var remoteMessaging = remotes.GetMessaging();

                    var idUnordered = remoteMessaging.RemoteListen(new RemoteUnorderedListener(), Topic.Unordered);
                    var idOrdered = remoteMessaging.RemoteListen(new RemoteOrderedListener(), Topic.Ordered);

                    // Send unordered
                    Console.WriteLine(">>> Sending unordered messages...");

                    for (var i = 0; i < 10; i++)
                        remoteMessaging.Send(i, Topic.Unordered);

                    Console.WriteLine(">>> Finished sending unordered messages.");

                    // Send ordered
                    Console.WriteLine(">>> Sending ordered messages...");

                    for (var i = 0; i < 10; i++)
                        remoteMessaging.SendOrdered(i, Topic.Ordered);

                    Console.WriteLine(">>> Finished sending ordered messages.");

                    Console.WriteLine(">>> Check output on all nodes for message printouts.");
                    Console.WriteLine(">>> Waiting for messages acknowledgements from all remote nodes...");

                    unorderedCounter.Wait();
                    orderedCounter.Wait();

                    // Unsubscribe
                    remoteMessaging.StopRemoteListen(idUnordered);
                    remoteMessaging.StopRemoteListen(idOrdered);
                }
            }

            Console.WriteLine();
            Console.WriteLine(">>> Example finished, press any key to exit ...");
            Console.ReadKey();
        }
    }
}
