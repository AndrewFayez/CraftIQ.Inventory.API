using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftIQ.Inventory.Core.Entities.Inventories.Specifications
{
    public class ReadInventoriesByIdSpecification : SingleResultSpecification<Inventory>
    {
        public ReadInventoriesByIdSpecification(Guid inventoryId)
        {
            Query.Where(x => x._InventoryId == inventoryId);
        }
    }
}
