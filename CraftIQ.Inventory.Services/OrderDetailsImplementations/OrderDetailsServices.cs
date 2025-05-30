using CraftIQ.Inventory.Core.Entities;
using CraftIQ.Inventory.Core.Entities.OrderDetails;
using CraftIQ.Inventory.Core.Entities.OrderDetails.Specifications;
using CraftIQ.Inventory.Core.Entities.Orders;
using CraftIQ.Inventory.Core.Entities.Orders.Specifications;
using CraftIQ.Inventory.Core.Entities.Products.Specifications;
using CraftIQ.Inventory.Core.Interfaces;
using CraftIQ.Inventory.Shared.Contracts.OrderDetails;
using huzcodes.Extensions.Exceptions;
using huzcodes.Persistence.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CraftIQ.Inventory.Services.OrderDetailsImplementations
{
    public class OrderDetailsServices<TRequest, TResponse>(IRepository<OrderDetail> orderDetailRepository,
                                                            IRepository<Product> productRepository,
                                                            IRepository<Order> orderRepository)
                                                          : IGenericServices<TRequest, TResponse>
    {
        private readonly IRepository<Order> _orderRepository = orderRepository;
        private readonly IRepository<Product> _productRepository = productRepository;
        private readonly IRepository<OrderDetail> _orderDetailRepository = orderDetailRepository;

        public async ValueTask<TRequest> Create(TRequest contract)
        {
           var oContract = contract as OrderDetailsOperationContract;

            // Check if the order exists
            var oReadorderByIdSpec = new ReadOrdersByIdSpecification(oContract!.OrderId);
            var oOrderResult = await _orderRepository.FirstOrDefaultAsync(oReadorderByIdSpec);
            if (oOrderResult == null) 
                throw new ResultException("Order not found.", (int)HttpStatusCode.NotFound);

            // Check if the product exists
            var oReadProductByIdSpec = new ReadProductsByIdSpecification(oContract.ProductId);
            var oProductResult = await _productRepository.FirstOrDefaultAsync(oReadProductByIdSpec);
            if (oProductResult == null)
                throw new ResultException("Product not found.", (int)HttpStatusCode.NotFound);

            var oData = new OrderDetail(oContract.Quantity, oContract.TotalPrice);

            oData.SetOrder(oOrderResult);
            oData.SetProduct(oProductResult);

            var oResult = await _orderDetailRepository.AddAsync(oData);

            if (oResult == null)
                return default!;

            oContract._OrderDetailId = oResult._OrderDetailId;
            return oContract as dynamic;

        }

        public async ValueTask Delete(Guid contractId)
        {
            var oReadByIdSpec = new ReadOrderDetailsByIdSpecification(contractId);
            var oResult = await _orderDetailRepository.FirstOrDefaultAsync(oReadByIdSpec);
            if (oResult != null)
                await _orderDetailRepository.DeleteAsync(oResult);
            else throw new ResultException("You can't delete an object that does not exist.", (int)HttpStatusCode.Forbidden);
        }

        public async ValueTask<List<TResponse>> Read()
        {
            var oReadSpec = new ReadOrderDetailsSpecification();
            var oData = await _orderDetailRepository.ListAsync(oReadSpec);
            if (oData != null && oData.Count > 0)
            {
                var oResult = oData.Select(o => new OrderDetailsContract(o._OrderDetailId,
                                                                         o.Quantity,
                                                                         o.TotalPrice,
                                                                         o.Order._OrderId,
                                                                         o.Product._ProductId,
                                                                         o.CreatedBy,
                                                                         o.ModifiedBy,
                                                                         o.CreatedOn,
                                                                         o.ModifiedOn)).ToList();
                return oResult as dynamic;
            }
            else return new List<OrderDetailsContract>() as dynamic;
        }

        public async ValueTask<TResponse> ReadById(Guid contractId)
        {
            var oReadByIdSpec = new ReadOrderDetailsByIdSpecification(contractId);
            var oResult = await _orderDetailRepository.FirstOrDefaultAsync(oReadByIdSpec);
            if (oResult != null)
                return new OrderDetailsContract(oResult._OrderDetailId,
                                                oResult.Quantity,
                                                oResult.TotalPrice,
                                                oResult.Order._OrderId,
                                                oResult.Product._ProductId,
                                                oResult.CreatedBy,
                                                oResult.ModifiedBy,
                                                oResult.CreatedOn,
                                                oResult.ModifiedOn) as dynamic;

            else throw new ResultException("This object does not exist", (int)HttpStatusCode.NotFound);
        }

        public async ValueTask Update(Guid contractId, TRequest contract)
        {
            var oContract = contract as OrderDetailsOperationContract;
            var oReadByIdSpec = new ReadOrderDetailsByIdSpecification(contractId);
            var oResult = await _orderDetailRepository.FirstOrDefaultAsync(oReadByIdSpec);
            if (oResult != null)
            {
                oResult.UpdateOrderDetail(oContract!);
                await _orderDetailRepository.UpdateAsync(oResult);
            }
            else throw new ResultException("This object does not exist", (int)HttpStatusCode.NotFound);
        }








        public ValueTask<List<TResponse>> ReadByParentId(Guid parentContractId)
        {
            throw new NotImplementedException();
        }

        public ValueTask<TResponse> ReadSingleByParentId(Guid contractId, Guid parentContractId)
        {
            throw new NotImplementedException();
        }


        public ValueTask UpdateParentId(Guid contractId, Guid parentContractId)
        {
            throw new NotImplementedException();
        }
    }
}
