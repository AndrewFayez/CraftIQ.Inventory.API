using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftIQ.Inventory.Core.Entities.Transactions.Specifications
{
    public class ReadTransactionsSpecification : Specification<Transaction>
    {
        public ReadTransactionsSpecification() 
        {
            Query.Where(o => o.Id != 0);
        }
    }
}
