using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CraftIQ.Inventory.Core.Entities.OrderDetails.Specifications
{
    public class ReadOrderDetailsByIdSpecification : SingleResultSpecification<OrderDetail>
    {
        public ReadOrderDetailsByIdSpecification(Guid orderDetailId)
        {
            Query.Where(o => o._OrderDetailId == orderDetailId)
                 .Include(o => o.Product)
                 .Include(o => o.Order);
        }
    }
}
