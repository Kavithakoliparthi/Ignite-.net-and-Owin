using System;
using System.Threading;
using Apache.Ignite.Core.Messaging;

namespace Ignitedotnet
{
    public class LocalListner : IMessageListener<int>
    {
        /** Countdown event. */
        private readonly CountdownEvent count;
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalListener"/> class.
        /// </summary>
        public LocalListner(CountdownEvent countdown)
        {
            if (countdown == null)
                throw new ArgumentNullException("countdown");

            count = countdown;
        }
        /// <summary>
        /// Receives a message and returns a value 
        /// indicating whether provided message and node id satisfy this predicate.
        /// Returning false will unsubscribe this listener from future notifications.
        /// </summary>
        public bool Invoke(Guid nodeId, int message)
        {
            count.Signal();
            return !count.IsSet;
        }
    }
}
