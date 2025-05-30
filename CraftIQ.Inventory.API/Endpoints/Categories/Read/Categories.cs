using CraftIQ.Inventory.Core.Entities.Categories;
using CraftIQ.Inventory.Core.Interfaces;
using CraftIQ.Inventory.Services.Factories;
using CraftIQ.Inventory.Shared.Contracts.Categories;
using huzcodes.Endpoints.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.API.Endpoints.Categories.Read
{
    public class Categories(InventoryFactory<CategoriesOperationsContract, CategoriesContract> factory) : EndpointsAsync.WithoutRequest
        .WithActionResult<ReadCategoriesResponse>
    {

        private readonly InventoryFactory<CategoriesOperationsContract, CategoriesContract> _factory = factory;


        [HttpGet(Routes.CategoriesRoutes.BaseUrl)]
        public override async Task<ActionResult<ReadCategoriesResponse>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var services = _factory.Build(nameof(Category));
            var oData = await services.Read();

            var oResult = oData.Select(o=> new ReadCategoriesResponse(o)).ToList();

            return Ok(oResult);
        }
    }
     
}
