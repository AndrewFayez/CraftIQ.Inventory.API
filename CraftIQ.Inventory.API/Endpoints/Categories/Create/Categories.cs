using CraftIQ.Inventory.Core.Entities.Categories;
using CraftIQ.Inventory.Core.Interfaces;
using CraftIQ.Inventory.Services.Factories;
using CraftIQ.Inventory.Shared.Contracts.Categories;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using huzcodes.Persistence.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.API.Endpoints.Categories.Create
{
    public class Categories(InventoryFactory<CategoriesOperationsContract , CategoriesContract> factory) : EndpointsAsync.WithRequest<CreateCategoriesRequest>
                                                  .WithActionResult<CreateCategoriesResponse>
    {

        private readonly InventoryFactory<CategoriesOperationsContract, CategoriesContract> _factory = factory;

        [HttpPost(Routes.CategoriesRoutes.BaseUrl)]
        public override async Task<ActionResult<CreateCategoriesResponse>> HandleAsync
                        (CreateCategoriesRequest request, CancellationToken cancellationToken = default)
        {
           if (request == null)
                throw new ResultException("Request cannot be null", StatusCodes.Status400BadRequest);

           var services = _factory.Build(nameof(Category));

            var oData = new CategoriesOperationsContract(request.Name, request.Description);

            var oResult = await services.Create(oData);

            return Ok(new CreateCategoriesResponse(oResult.Name , oResult.Description));
        }
    }
}
