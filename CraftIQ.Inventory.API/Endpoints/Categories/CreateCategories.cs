using huzcodes.Endpoints.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.API.Endpoints.Categories
{
    public class CreateCategories : EndpointsAsync.WithRequest<CreateCategoriesRequest>
                                                  .WithActionResult<CreateCategoriesResponse>
    {
        [HttpPost(Routes.CategoriesRoutes.CreateCategory)]
        public override Task<ActionResult<CreateCategoriesResponse>> HandleAsync
                        (CreateCategoriesRequest request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
