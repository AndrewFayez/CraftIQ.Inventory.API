namespace CraftIQ.Inventory.API.Endpoints.Products.Create
{
    public class CreateProductsResponse
    {
        public Guid ProductId { get; set; }

        public CreateProductsResponse(Guid productId)
        {
            ProductId = productId;
        }
    }
}
