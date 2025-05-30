using CraftIQ.Inventory.Services.Factories;
using CraftIQ.Inventory.Shared.Contracts.Inventories;
using huzcodes.Endpoints.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.API.Endpoints.Inventories.Read.ReadById
{
    public class Inventories(InventoryFactory<ReadInventoryByIdRequest, InventoriesContract> factory) : EndpointsAsync.WithRequest<ReadInventoryByIdRequest>
                                                                                                     .WithActionResult<ReadInventoriesResponse>
    {
        private readonly InventoryFactory<ReadInventoryByIdRequest, InventoriesContract> _factory = factory;

        [HttpGet(Routes.InventoriesRoutes.ReadById)]
        public async override Task<ActionResult<ReadInventoriesResponse>> HandleAsync(ReadInventoryByIdRequest request, CancellationToken cancellationToken = default)
        {
            var services = _factory.Build(nameof(Inventory));
            var oData = await services.ReadById(request.inventoryId);
            var oResult = new ReadInventoriesResponse(oData);
            return Ok(oResult);
        }
    }
}
