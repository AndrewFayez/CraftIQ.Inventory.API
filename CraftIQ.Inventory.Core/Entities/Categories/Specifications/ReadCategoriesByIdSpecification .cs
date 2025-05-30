using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftIQ.Inventory.Core.Entities.Categories.Specifications
{
    public class ReadCategoriesByIdSpecification : SingleResultSpecification<Category>
    {
        public ReadCategoriesByIdSpecification(Guid categoryId)
        {
            Query.Where(c => c._CategoryId == categoryId);
        }
                

    }
}
