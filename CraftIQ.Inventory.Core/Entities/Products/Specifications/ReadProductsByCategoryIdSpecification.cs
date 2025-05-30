using Ardalis.Specification;
using CraftIQ.Inventory.Core.Entities.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftIQ.Inventory.Core.Entities.Products.Specifications
{
    public class ReadProductsByCategoryIdSpecification :Specification<Category>
    {
        public ReadProductsByCategoryIdSpecification(Guid categoryId)
        {
            Query.Where(c => c._CategoryId == categoryId)
                .Include(o=>o.Products.Where(p=>p.Category._CategoryId == categoryId));
        }
    }
}
