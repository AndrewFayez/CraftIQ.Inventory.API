using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftIQ.Inventory.Shared.Contracts.OrderDetails
{
    public class OrderDetailsOperationContract
    {
        public Guid _OrderDetailId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }

        public OrderDetailsOperationContract(Guid orderDetailId,
                                              int quantity,
                                              decimal totalPrice,
                                              Guid orderId,
                                              Guid productId)
        {
            _OrderDetailId = orderDetailId;
            Quantity = quantity;
            TotalPrice = totalPrice;
            OrderId = orderId;
            ProductId = productId;
        }
    }
}
