using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.API.Endpoints.Inventories.Delete
{
    public class DeleteRequest 
    {
        [FromRoute]
        public Guid InventoryId { get; set; }
    }
}
