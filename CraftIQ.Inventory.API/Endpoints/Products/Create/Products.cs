using CraftIQ.Inventory.Core.Entities;
using CraftIQ.Inventory.Services.Factories;
using CraftIQ.Inventory.Shared.Contracts.Products;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CraftIQ.Inventory.API.Endpoints.Products.Create
{
    public class Products(InventoryFactory<ProductsOperationsContract, ProductsContract> factory) : EndpointsAsync.WithRequest<CreateProductsRequest>
                                        .WithActionResult<CreateProductsResponse>
    {
        private readonly InventoryFactory<ProductsOperationsContract, ProductsContract> _factory = factory;

        [HttpPost(Routes.ProductsRoutes.BaseUrl)]
        public override async Task<ActionResult<CreateProductsResponse>> HandleAsync([FromBody] CreateProductsRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
                throw new ResultException("Request cannot be null", StatusCodes.Status400BadRequest);

            var services = _factory.Build(nameof(Product));

            var oData = new ProductsOperationsContract(Guid.Empty,
                                                       request.CategoryId,
                                                       request.InventoryId,
                                                       request.TransactionId,
                                                       request.Name,
                                                       request.Description,
                                                       request.UnitPrice,
                                                       request.Weight,
                                                       request.Length,
                                                       request.Width,
                                                       request.Height,
                                                       request.TaxCost,
                                                       request.ProfitPerUnit,
                                                       request.ProductionCost);

            var oCreateProduct = await services.Create(oData);

            var oResult = new CreateProductsResponse(oCreateProduct._ProductId);
            return Ok(oResult);
        }
    }
}
