using CraftIQ.Inventory.Core.Entities;
using CraftIQ.Inventory.Services.Factories;
using CraftIQ.Inventory.Shared.Contracts.Products;
using huzcodes.Endpoints.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.API.Endpoints.Products.Delete
{
    public class Products(InventoryFactory<DeleteRequest, ActionResult> factory )  : EndpointsAsync.WithRequest<DeleteRequest>
        .WithActionResult
    {

        private readonly InventoryFactory<DeleteRequest, ActionResult> _factory = factory;

        [HttpDelete(Routes.ProductsRoutes.Delete)]
        public override async Task<ActionResult> HandleAsync(DeleteRequest request, CancellationToken cancellationToken = default)
        {
          var services = _factory.Build(nameof(Product));  
            await services.Delete(request.productId);
            return Ok("Product Deleted Successfully");
        }
    }
}
