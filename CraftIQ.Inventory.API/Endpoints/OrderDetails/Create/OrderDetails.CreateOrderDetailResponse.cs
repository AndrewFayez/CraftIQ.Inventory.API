namespace CraftIQ.Inventory.API.Endpoints.OrderDetails.Create
{
    public class CreateOrderDetailResponse
    {
        public Guid OrderDetailId { get; set; }

        public CreateOrderDetailResponse(Guid orderDetailId)
        {
            OrderDetailId = orderDetailId;
        }
    }
}
