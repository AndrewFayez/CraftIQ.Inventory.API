using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CraftIQ.Inventory.Core.Entities.OrderDetails.Specifications
{
    public class ReadOrderDetailsSpecification : Specification<OrderDetail>
    {
        public ReadOrderDetailsSpecification()
        {
            Query.Where(o => o.Id != 0);
        }
    }
}
