using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CraftIQ.Inventory.Core.Entities.Orders.Specifications
{
    public class ReadOrdersSpecification : Specification<Order>
    {
        public ReadOrdersSpecification()
        {
            Query.Where(o => o.Id != 0);
        }
    }
}
