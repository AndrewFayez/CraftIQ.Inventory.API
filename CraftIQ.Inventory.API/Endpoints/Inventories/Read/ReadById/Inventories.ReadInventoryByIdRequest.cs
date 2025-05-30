using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.API.Endpoints.Inventories.Read.ReadById
{
    public class ReadInventoryByIdRequest
    {
        [FromRoute]
        public Guid inventoryId { get; set; }
    }
}
