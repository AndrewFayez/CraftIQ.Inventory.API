using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.API.Endpoints.Transactions.Delete
{
    public class DeleteRequest 
    {
        [FromRoute]
        public Guid transactionId { get; set; }
    }
}
