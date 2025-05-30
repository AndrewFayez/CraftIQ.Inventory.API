using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftIQ.Inventory.Core.Entities.Products.Specifications
{
    public class ReadProductsSpecification : Specification<Product>
    {
        public ReadProductsSpecification()
        {
            Query.Where(p => p.Id != 0) 
                .Include(i=>i.Inventory)
                .Include(t=>t.Transaction);
        }
    }
   
}
