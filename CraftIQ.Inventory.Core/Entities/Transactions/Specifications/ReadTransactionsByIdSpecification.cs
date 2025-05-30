using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftIQ.Inventory.Core.Entities.Transactions.Specifications
{
    public class ReadTransactionsByIdSpecification :SingleResultSpecification<Transaction>
    {
        public ReadTransactionsByIdSpecification(Guid transactionId)
        {
            Query.Where(o => o._TransactionId == transactionId);
        }
    }
}
