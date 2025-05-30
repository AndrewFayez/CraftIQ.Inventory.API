using CraftIQ.Inventory.Core.Entities.Transactions;
using CraftIQ.Inventory.Services.Factories;
using huzcodes.Endpoints.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.API.Endpoints.Transactions.Delete
{
    public class Transactions (InventoryFactory<DeleteRequest,ActionResult> factory) : EndpointsAsync.WithRequest<DeleteRequest>.WithActionResult
    {
        private readonly InventoryFactory<DeleteRequest, ActionResult> _factory = factory;

        [HttpDelete(Routes.TransactionsRoutes.Delete)]
        public override async Task<ActionResult> HandleAsync(DeleteRequest request, CancellationToken cancellationToken = default)
        {
            var services = _factory.Build(nameof(Transaction));
            await services.Delete(request.transactionId);
            return Ok("Transaction Deleted Successfully");
        }
    }
}
