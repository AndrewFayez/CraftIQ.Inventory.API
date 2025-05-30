using CraftIQ.Inventory.Core.Entities;
using CraftIQ.Inventory.Services.Factories;
using huzcodes.Endpoints.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.API.Endpoints.Products.Categories.Update
{
    public class Products(InventoryFactory<UpdateProductCategoryRequest, ActionResult> factory) : EndpointsAsync.WithRequest<UpdateProductCategoryRequest>.WithActionResult
    {

        private readonly InventoryFactory<UpdateProductCategoryRequest, ActionResult> _factory = factory;



        [HttpPut(Routes.ProductsRoutes.UpdateProductCategoryId)]
        public override async Task<ActionResult> HandleAsync(UpdateProductCategoryRequest request, CancellationToken cancellationToken = default)
        {
            var services = _factory.Build(nameof(Product));
            await services.UpdateParentId(request.productId , request.Product.CategoryId);
            return Ok("Updated Successfully");
        }
    }
}
