using CraftIQ.Inventory.Core.Entities.Categories;
using CraftIQ.Inventory.Core.Interfaces;
using CraftIQ.Inventory.Services.Factories;
using CraftIQ.Inventory.Shared.Contracts.Categories;
using huzcodes.Endpoints.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.API.Endpoints.Categories.Update
{
    public class Categories(InventoryFactory<CategoriesOperationsContract, CategoriesContract> factory) : EndpointsAsync.WithRequest<UpdateCategoriesRequest>
                                            .WithActionResult
    {
        private readonly InventoryFactory<CategoriesOperationsContract, CategoriesContract> _factory = factory;


        [HttpPut(Routes.CategoriesRoutes.Update)]
        public override async Task<ActionResult> HandleAsync(UpdateCategoriesRequest request, CancellationToken cancellationToken = default)
        {
            var services = _factory.Build(nameof(Category));
            var oData = new CategoriesOperationsContract(request.Category.Name , request.Category.Description);
              await services.Update(request.categoryId, oData);
            return Ok("Category Updated Successfully");
        }
    }
    
}
