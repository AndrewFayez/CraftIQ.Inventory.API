using CraftIQ.Inventory.Core.Entities;
using CraftIQ.Inventory.Services.Factories;
using CraftIQ.Inventory.Shared.Contracts.Products;
using huzcodes.Endpoints.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace CraftIQ.Inventory.API.Endpoints.Products.Read.ReadById
{
    public class Products(InventoryFactory<ProductsOperationsContract, ProductsContract> factory) : EndpointsAsync.WithRequest<ReadProductsByIdRequest>.WithActionResult<ReadProductsResponse>
    {
        private readonly InventoryFactory<ProductsOperationsContract, ProductsContract> _factory = factory;

        [HttpGet(Routes.ProductsRoutes.ReadById)]
        public override async Task<ActionResult<ReadProductsResponse>> HandleAsync(ReadProductsByIdRequest request, CancellationToken cancellationToken = default)
        {
            var services = _factory.Build(nameof(Product));
            var oData = await services.ReadById(request.productId);

            if (oData is null)
                return NotFound();

            var oResult = new ReadProductsResponse(oData);
            return Ok(oResult);
        }
    }
}
