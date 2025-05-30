using CraftIQ.Inventory.Core.Entities.Transactions;
using CraftIQ.Inventory.Services.Factories;
using CraftIQ.Inventory.Shared.Contracts.Transactions;
using huzcodes.Endpoints.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CraftIQ.Inventory.API.Endpoints.Transactions.Create
{
    public class Transactions(InventoryFactory<TransactionsOperationsContract, TransactionsContract> factory) :
                                            EndpointsAsync.WithRequest<CreateTransactionsRequest>.WithActionResult<CreateTransactionsResponse>
    {
        private readonly InventoryFactory<TransactionsOperationsContract, TransactionsContract> _factory = factory;


        [HttpPost(Routes.TransactionsRoutes.BaseUrl)]
        public override async Task<ActionResult<CreateTransactionsResponse>> HandleAsync(CreateTransactionsRequest request, CancellationToken cancellationToken = default)
        {
            var service = _factory.Build(nameof(Transaction));

            var oData = new TransactionsOperationsContract(Guid.Empty,
                                                           request.EmployeeId,
                                                           request.TransactionDate,
                                                           request.Quantity,
                                                           request.TransactionType,
                                                           request.Notes);
            var oTransactions = await service.Create(oData);

            var oResult = new CreateTransactionsResponse(oTransactions._TransactionId);
            return Ok(oResult);
        }
    }
}
