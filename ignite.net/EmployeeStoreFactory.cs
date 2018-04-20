using Apache.Ignite.Core.Cache.Store;
using Apache.Ignite.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ignitedotnet
{
    class EmployeeStoreFactory : IFactory<ICacheStore>
    {
        /// <summary>
        /// Creates an instance of the cache store.
        /// </summary>
        public ICacheStore CreateInstance()
        {
            return new EmployeeStore();
        }
    }
}
