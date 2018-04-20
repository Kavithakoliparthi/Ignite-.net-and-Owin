using Apache.Ignite.Core.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ignitedotnet
{
    class EmployeeStorePredicate : ICacheEntryFilter<int, Employee>
    {
        /// <summary>
        /// Returns a value indicating whether provided cache entry satisfies this predicate.
        /// </summary>
        /// <param name="entry">Cache entry.</param>
        /// <returns>Value indicating whether provided cache entry satisfies this predicate.</returns>
        public bool Invoke(ICacheEntry<int, Employee> entry)
        {
            return entry.Key == 1;
        }
    }
}
