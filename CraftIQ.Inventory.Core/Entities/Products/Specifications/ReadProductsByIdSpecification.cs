using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftIQ.Inventory.Core.Entities.Products.Specifications
{
    public class ReadProductsByIdSpecification :SingleResultSpecification<Product>
    {
        public ReadProductsByIdSpecification(Guid productId)
        {
            Query.Where(p => p._ProductId == productId)
                .Include(i => i.Inventory)
                .Include(t => t.Transaction);
        }
    }
     
}
