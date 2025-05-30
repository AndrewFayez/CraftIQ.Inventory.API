using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.API.Endpoints.Transactions.Read.ReadById
{
    public class ReadByIdRequest
    {
        [FromRoute]
        public Guid transactionId { get; set; }

    }
}
