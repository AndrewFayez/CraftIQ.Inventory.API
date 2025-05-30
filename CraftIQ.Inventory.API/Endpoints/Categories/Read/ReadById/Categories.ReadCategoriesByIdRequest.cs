using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.API.Endpoints.Categories.Read.ReadById
{
    public class ReadCategoriesByIdRequest
    {
        [FromRoute]
        public Guid categoryId { get; set; }
    }
}
