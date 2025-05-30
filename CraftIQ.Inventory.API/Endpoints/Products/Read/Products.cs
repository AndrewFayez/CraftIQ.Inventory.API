using CraftIQ.Inventory.Core.Entities;
using CraftIQ.Inventory.Services.Factories;
using CraftIQ.Inventory.Shared.Contracts.Products;
using huzcodes.Endpoints.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.API.Endpoints.Products.Read
{
    public class Products(InventoryFactory<dynamic, ProductsContract> factory) : EndpointsAsync.WithoutRequest.WithActionResult<ReadProductsResponse>
    {
        private readonly InventoryFactory<dynamic, ProductsContract> _factory = factory;

        [HttpGet(Routes.ProductsRoutes.BaseUrl)]
        public override async Task<ActionResult<ReadProductsResponse>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var services = _factory.Build(nameof(Product)); 

            var oData = await services.Read();
            
            var oResult = oData.Select(o => new ReadProductsResponse(o)).ToList();
            return Ok(oResult);

        }
    }
}
