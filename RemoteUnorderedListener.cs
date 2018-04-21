using Apache.Ignite.Core;
using Apache.Ignite.Core.Messaging;
using Apache.Ignite.Core.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ignitedotnet
{
    class RemoteUnorderedListener : IMessageListener<int>
    {
        /** Injected Ignite instance. */
        [InstanceResource]
        private readonly IIgnite ignite;
        /// <summary>
        /// Receives a message and returns a value 
        /// indicating whether provided message and node id satisfy this predicate.
        /// Returning false will unsubscribe this listener from future notifications.
        /// </summary>
        public bool Invoke(Guid nodeId, int message)
        {
            Console.WriteLine("Received unordered message [msg={0}, fromNodeId={1}]", message, nodeId);

            ignite.GetCluster().ForNodeIds(nodeId).GetMessaging().Send(message, Topic.Unordered);

            return true;
        }
    }
}




