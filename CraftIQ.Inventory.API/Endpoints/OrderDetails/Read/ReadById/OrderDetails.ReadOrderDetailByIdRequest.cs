using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.API.Endpoints.OrderDetails.Read.ReadById
{
    public class ReadOrderDetailByIdRequest
    {
        [FromRoute]
        public Guid orderDetailId { get; set; }
    }
}
