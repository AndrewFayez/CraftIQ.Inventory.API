using CraftIQ.Inventory.Services.Factories;
using CraftIQ.Inventory.Shared.Contracts.Inventories;
using huzcodes.Endpoints.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.API.Endpoints.Inventories.Create
{
    public class Inventories(InventoryFactory<InventoriesOperationsContract , InventoriesContract> factory) :EndpointsAsync.WithRequest<CreateInventoryRequest>.WithActionResult<CreateInventoryResponse>
    {

        private readonly InventoryFactory<InventoriesOperationsContract, InventoriesContract> _factory = factory;
        [HttpPost(Routes.InventoriesRoutes.BaseUrl)]
        public override async Task<ActionResult<CreateInventoryResponse>> HandleAsync(CreateInventoryRequest request, CancellationToken cancellationToken = default)
        {
            var services = _factory.Build(nameof(Inventory));
            var oData = new InventoriesOperationsContract(Guid.Empty,request.Name, request.Quantity, request.Reorderlevel, request.Location );
            var oCreateInventory = await services.Create(oData);

            var oResult = new CreateInventoryResponse(oCreateInventory._InventoryId);
            return Ok(oResult);
             
        }
    }
}
