using Ardalis.Specification;
using CraftIQ.Inventory.Core.Entities.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftIQ.Inventory.Core.Entities.Products.Specifications
{
    public class ReadSingleProductByCategoryIdSpecification : SingleResultSpecification<Category>
    {
        public ReadSingleProductByCategoryIdSpecification(Guid productId, Guid categoryId)
        {
            Query.Where(o => o._CategoryId == categoryId)
                .Include(p => p.Products.Where(x => x.Category._CategoryId == categoryId && x._ProductId == productId));
        }
    }
}
