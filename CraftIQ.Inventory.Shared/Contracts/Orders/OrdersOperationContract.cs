using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftIQ.Inventory.Shared.Contracts.Orders
{
    public class OrdersOperationContract
    {
        public Guid _OrderId { get; set; }
        public Guid SupplierId { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public int TotalAmount { get; set; }
        public int Status { get; set; }
        public DateTimeOffset ExpectedDeliveryDate { get; set; }
        public int OrderType { get; set; }
        public DateTimeOffset ReceivedDate { get; set; }

        public OrdersOperationContract(Guid orderId,
                                        Guid supplierId,
                                        DateTimeOffset orderDate,
                                        int totalAmount,
                                        int status,
                                        DateTimeOffset expectedDeliveryDate,
                                        int orderType,
                                        DateTimeOffset receivedDate)
        {
            _OrderId = orderId;
            SupplierId = supplierId;
            OrderDate = orderDate;
            TotalAmount = totalAmount;
            Status = status;
            ExpectedDeliveryDate = expectedDeliveryDate;
            OrderType = orderType;
            ReceivedDate = receivedDate;
        }
    }
}
