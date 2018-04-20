using Apache.Ignite.Core.Cache.Store;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ignitedotnet
{
    class EmployeeStore : CacheStoreAdapter<int, Employee>
    {
        /// <summary>
        /// Dictionary representing the store.
        /// </summary>
        private readonly ConcurrentDictionary<int, Employee> db = new ConcurrentDictionary<int, Employee>(
            new List<KeyValuePair<int, Employee>>
            {
                new KeyValuePair<int, Employee>(1, new Employee(
                    "Allison Mathis",
                    25300,
                    new Address("2702 Freedom Lane, San Francisco, CA", 94109),
                    new List<string> {"Development"}
                    )),

                new KeyValuePair<int, Employee>(2, new Employee(
                    "Breana Robbin",
                    6500,
                    new Address("3960 Sundown Lane, Austin, TX", 78130),
                    new List<string> {"Sales"}
                    ))
            });

        /// <summary>
        /// Loads all values from underlying persistent storage.
        /// This method gets called as a result of <see cref="ICache{TK,TV}.LoadCache"/> call.
        /// </summary>
        /// <param name="act">Action that loads a cache entry.</param>
        /// <param name="args">Optional arguments.</param>
        public override void LoadCache(Action<int, Employee> act, params object[] args)
        {
            // Iterate over whole underlying store and call act on each entry to load it into the cache.
            foreach (var entry in db)
                act(entry.Key, entry.Value);
        }

        /// <summary>
        /// Loads multiple objects from the cache store.
        /// This method gets called as a result of <see cref="ICache{K,V}.GetAll"/> call.
        /// </summary>
        /// <param name="keys">Keys to load.</param>
        /// <returns>
        /// A map of key, values to be stored in the cache.
        /// </returns>
        public override IEnumerable<KeyValuePair<int, Employee>> LoadAll(IEnumerable<int> keys)
        {
            var result = new Dictionary<int, Employee>();

            foreach (var key in keys)
                result[key] = Load(key);

            return result;
        }

        /// <summary>
        /// Loads an object from the cache store.
        /// This method gets called as a result of <see cref="ICache{K,V}.Get"/> call.
        /// </summary>
        /// <param name="key">Key to load.</param>
        /// <returns>Loaded value</returns>
        public override Employee Load(int key)
        {
            Employee val;

            db.TryGetValue(key, out val);

            return val;
        }

        /// <summary>
        /// Write key-value pair to store.
        /// </summary>
        /// <param name="key">Key to write.</param>
        /// <param name="val">Value to write.</param>
        public override void Write(int key, Employee val)
        {
           db[key] = val;
        }

        /// <summary>
        /// Delete cache entry form store.
        /// </summary>
        /// <param name="key">Key to delete.</param>
        public override void Delete(int key)
        {
            Employee val;

            db.TryRemove(key, out val);
        }
    }
}
