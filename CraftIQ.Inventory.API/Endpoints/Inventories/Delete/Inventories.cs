using CraftIQ.Inventory.API.Endpoints.Inventories.Create;
using CraftIQ.Inventory.Core.Entities.Categories;
using CraftIQ.Inventory.Services.Factories;
using CraftIQ.Inventory.Shared.Contracts.Inventories;
using huzcodes.Endpoints.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.API.Endpoints.Inventories.Delete
{
    public class Inventories(InventoryFactory<DeleteRequest, ActionResult> factory) : EndpointsAsync.WithRequest<DeleteRequest>.WithoutResult 
    {

        private readonly InventoryFactory<DeleteRequest, ActionResult> _factory = factory;

        [HttpDelete(Routes.InventoriesRoutes.Delete)]
        public async override Task<ActionResult> HandleAsync(DeleteRequest request, CancellationToken cancellationToken = default)
        {
            var services = _factory.Build(nameof(Inventory));
            await services.Delete(request.InventoryId);
            return Ok("Category Deleted Successfully");
        }
    }
}
