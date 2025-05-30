using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CraftIQ.Inventory.Core.Entities.Inventories.Specifications
{
    public class ReadInventoriesSpecification : Specification<Inventory>
    {
        public ReadInventoriesSpecification()
        {
            Query.Where(x=>x.Id != 0);
        }
    }
}
