using CraftIQ.Inventory.Core.Entities.Transactions;
using CraftIQ.Inventory.Services.Factories;
using CraftIQ.Inventory.Shared.Contracts.Transactions;
using huzcodes.Endpoints.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.API.Endpoints.Transactions.Read.ReadById
{
    public class Transactions(InventoryFactory<TransactionsOperationsContract, TransactionsContract> factory) : EndpointsAsync.WithRequest<ReadByIdRequest>
                                                                                                                .WithActionResult<ReadTransactionResponse>
    {
        private readonly InventoryFactory<TransactionsOperationsContract, TransactionsContract> _factory = factory;
        [HttpGet(Routes.TransactionsRoutes.ReadById)]
        public override async Task<ActionResult<ReadTransactionResponse>> HandleAsync(ReadByIdRequest request, CancellationToken cancellationToken = default)
        {
           var service = _factory.Build(nameof(Transaction));
            var oData = await service.ReadById(request.transactionId);
            var oResult = new ReadTransactionResponse(oData);
            return Ok(oResult);
        }
    }
}
