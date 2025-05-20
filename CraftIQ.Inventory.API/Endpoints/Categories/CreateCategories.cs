using CraftIQ.Inventory.Core.Entities;
using huzcodes.Endpoints.Abstractions;
using huzcodes.Extensions.Exceptions;
using huzcodes.Persistence.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.API.Endpoints.Categories
{
    public class CreateCategories(IRepository<Category> repository) : EndpointsAsync.WithRequest<CreateCategoriesRequest>
                                                  .WithActionResult<CreateCategoriesResponse>
    {

        readonly IRepository<Category> _repository = repository;    

        [HttpPost(Routes.CategoriesRoutes.CreateCategory)]
        public override async Task<ActionResult<CreateCategoriesResponse>> HandleAsync
                        (CreateCategoriesRequest request, CancellationToken cancellationToken = default)
        {
           if (request == null)
                throw new ResultException("Request cannot be null", StatusCodes.Status400BadRequest);

           var oData = new Category
           {
               Name = request.Name,
               Description = request.Description
           };
            var oResult = await _repository.AddAsync(oData);

            return Ok(new CreateCategoriesResponse(oResult.Name , oResult.Description));
        }
    }
}
