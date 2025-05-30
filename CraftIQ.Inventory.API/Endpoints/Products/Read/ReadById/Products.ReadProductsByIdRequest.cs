using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.API.Endpoints.Products.Read.ReadById
{
    public class ReadProductsByIdRequest
    {
        [FromRoute]                                       
        public Guid productId { get; set; }
    }
}
