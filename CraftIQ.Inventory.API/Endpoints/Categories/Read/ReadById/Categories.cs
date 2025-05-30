using CraftIQ.Inventory.Core.Entities.Categories;
using CraftIQ.Inventory.Core.Interfaces;
using CraftIQ.Inventory.Services.Factories;
using CraftIQ.Inventory.Shared.Contracts.Categories;
using huzcodes.Endpoints.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.API.Endpoints.Categories.Read.ReadById
{
    public class Categories(InventoryFactory<CategoriesOperationsContract, CategoriesContract> factory) : EndpointsAsync.WithRequest<ReadCategoriesByIdRequest>
        .WithActionResult<ReadCategoriesByIdResponse>
    {
       private readonly  InventoryFactory<CategoriesOperationsContract, CategoriesContract> _factory = factory;

          [HttpGet(Routes.CategoriesRoutes.ReadById)]
        public override async Task<ActionResult<ReadCategoriesByIdResponse>> HandleAsync(
            ReadCategoriesByIdRequest request,
            CancellationToken cancellationToken = default)
        {
            var services = _factory.Build(nameof(Category));
            var oData = await services.ReadById(request.categoryId);
            if (oData is null)
                return NotFound();
            var oResult = new ReadCategoriesByIdResponse(oData);
            return Ok(oResult);
        }
    }
    
}
