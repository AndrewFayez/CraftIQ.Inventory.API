using CraftIQ.Inventory.Core.Entities.Transactions;
using CraftIQ.Inventory.Services.Factories;
using CraftIQ.Inventory.Shared.Contracts.Transactions;
using huzcodes.Endpoints.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.API.Endpoints.Transactions.Update
{
    public class Transactions(InventoryFactory<TransactionsOperationsContract, dynamic> factory) : EndpointsAsync.WithRequest<UpdateRequest>.WithActionResult
    {
        private readonly InventoryFactory<TransactionsOperationsContract, dynamic> _factory = factory;
        [HttpPut(Routes.TransactionsRoutes.Update)]
        public override async Task<ActionResult> HandleAsync(UpdateRequest request, CancellationToken cancellationToken = default)
        {
            var services = _factory.Build(nameof(Transaction));
           var oData = new TransactionsOperationsContract( request.transactionId,
                                                           request.Transaction.EmployeeId,
                                                           request.Transaction.TransactionDate,
                                                           request.Transaction.Quantity,
                                                           request.Transaction.TransactionType,
                                                           request.Transaction.Notes );
            await services.Update(request.transactionId, oData);

            return Ok("Transaction updated successfully.");
        }
    }
}
