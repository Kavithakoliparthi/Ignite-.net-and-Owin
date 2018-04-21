using System;
using System;
//using System.Transactions;
using Apache.Ignite.Core;
using Apache.Ignite.Core.Cache;
using Apache.Ignite.Core.Cache.Configuration;
using Apache.Ignite.Core.Transactions;

namespace Ignitedotnet
{
    class TransactionDemo
    {
        private const string CacheName = "Transaction example";
        [STAThread]
        static void Main()
        {
            using (var ignite = Ignition.Start())
            {
                Console.WriteLine(">> Transaction example started");
                var cache = ignite.GetOrCreateCache<int, Account>(new CacheConfiguration
                {
                    Name = CacheName,
                    AtomicityMode=CacheAtomicityMode.Transactional
                });
                InitAccounts(cache);
                Console.WriteLine(">> Transaferring with ignite transaction API");
                        // Transfer money between accounts in a single transaction.
                using (var tx = cache.Ignite.GetTransactions()
                    .TxStart(Apache.Ignite.Core.Transactions.TransactionConcurrency.Pessimistic, Apache.Ignite.Core.Transactions.TransactionIsolation.RepeatableRead))
                {
                    Account acc1 = cache.Get(1);
                    Account acc2 = cache.Get(2);
                    acc1.Balance += 100;
                    acc2.Balance -= 100;
                    cache.Put(1, acc1);
                    cache.Put(2, acc2);
                    tx.Commit();
                }
                DisplayAccounts(cache);
                InitAccounts(cache);
                Console.WriteLine("Transferring with transactionScope API"); ;
                //Do the same transaction with TransactionScope API

                //using (var ts = new TransactionScope())
                //{
                //    Account acc1 = cache.Get(1);
                //    Account acc2 = cache.Get(2);

                //    acc1.Balance += 100;
                //    acc2.Balance -= 100;

                //    cache.Put(1, acc1);
                //    cache.Put(2, acc2);

                //    ts.Complete();
                //}

                DisplayAccounts(cache);
            }
            Console.WriteLine(">> Press any  key to continue");
            Console.ReadKey();
        }
        /// <summary>
        /// DisplayAccounts method
        /// </summary>
        /// <param name="cache"></param>
        private static void DisplayAccounts(ICache<int, Account> cache)
        {
            Console.WriteLine("Transfer finished");
            Console.WriteLine(">>Accounts after transfer");
            Console.WriteLine(">> " + cache.Get(1));
            Console.WriteLine(">>" + cache.Get(2));
        }
        /// <summary>
        /// Initializes account balance
        /// </summary>
        /// <param name="cache"></param>
        private static void InitAccounts(ICache <int, Account> cache)
        {
                //Clean up caches on all nodes before run
            cache.Clear();
                //Initialize
            cache.Put(1, new Account(1, 100));
            cache.Put(2, new Account(2, 200));
            Console.WriteLine(">>> Accounts before transfer: ");
            Console.WriteLine(">>>     " + cache.Get(1));
            Console.WriteLine(">>>     " + cache.Get(2));
        }
    }
}
