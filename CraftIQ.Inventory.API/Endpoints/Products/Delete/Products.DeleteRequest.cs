using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.API.Endpoints.Products.Delete
{
    public class DeleteRequest 
    {
        [FromRoute]
        public Guid productId { get; set; }
    }
}
