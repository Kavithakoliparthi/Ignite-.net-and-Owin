using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ignitedotnet
{
    class Account
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="balance"></param>
        public Account(int id,decimal balance)
        {
            Id = id;
            Balance = balance;
        }
        /// <summary>
        /// Account ID.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Account balance
        /// </summary>
        public decimal Balance { get; set; }
        public override string ToString()
        {
            return string.Format("{0}[id={1},balance={2}]",typeof(Account).Name,Id,Balance);
        }
    }
}
