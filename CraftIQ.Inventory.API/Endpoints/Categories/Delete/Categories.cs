using CraftIQ.Inventory.Core.Entities.Categories;
using CraftIQ.Inventory.Core.Interfaces;
using CraftIQ.Inventory.Services.Factories;
using CraftIQ.Inventory.Shared.Contracts.Categories;
using huzcodes.Endpoints.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.API.Endpoints.Categories.Delete
{
    public class Categories(InventoryFactory<CategoriesOperationsContract, CategoriesContract> factory) : EndpointsAsync.WithRequest<DeleteRequest>
                                            .WithActionResult
    {
        private readonly InventoryFactory<CategoriesOperationsContract, CategoriesContract> _factory = factory;

        [HttpDelete(Routes.CategoriesRoutes.Delete)]
        public override async Task<ActionResult> HandleAsync(DeleteRequest request, CancellationToken cancellationToken = default)
        {
            var services = _factory.Build(nameof(Category));    
            await services.Delete(request.categoryId);
            return Ok("Category Deleted Successfully");

        }
    }
}
