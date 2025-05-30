using CraftIQ.Inventory.Core.Entities.Orders;
using CraftIQ.Inventory.Core.Entities.Orders.Specifications;
using CraftIQ.Inventory.Core.Interfaces;
using CraftIQ.Inventory.Shared.Contracts.Orders;
using huzcodes.Extensions.Exceptions;
using huzcodes.Persistence.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CraftIQ.Inventory.Services.OrdersImplementations
{
    public class OrdersServices<TRequest, TResponse>(IRepository<Order> repository) : IGenericServices<TRequest, TResponse>
    {
        private readonly IRepository<Order> _repository = repository;
        public async ValueTask<TRequest> Create(TRequest contract)
        {
            var oContract = contract as OrdersOperationContract;

            var oData = new Order(oContract!.SupplierId,
                                   oContract.TotalAmount,
                                   oContract.Status,
                                   oContract.ExpectedDeliveryDate,
                                   oContract.OrderType);

            var oResult = await _repository.AddAsync(oData);
            if (oResult == null)
                return default!;

            oContract._OrderId = oResult._OrderId;
            return oContract as dynamic;
        }

        public async ValueTask Delete(Guid contractId)
        {
            var oReadByIdSpec = new ReadOrdersByIdSpecification(contractId);
            var oResult = await _repository.FirstOrDefaultAsync(oReadByIdSpec);
            if(oResult != null)
            {
                await _repository.DeleteAsync(oResult);
            }
            else
            {
                throw new ResultException("Order is not found.",(int)HttpStatusCode.Forbidden);
            }
        }

        public async ValueTask<List<TResponse>> Read()
        {
            var oReadByIdSpec = new ReadOrdersSpecification();
            var oData = await _repository.ListAsync(oReadByIdSpec);
            if (oData != null || oData.Count > 0)
            {
                var oResult = oData.Select(o => new OrdersContract(o._OrderId,
                                                                o.SupplierId,
                                                                o.OrderDate,
                                                                o.TotalAmount,
                                                                o.Status,
                                                                o.ExpectedDeliveryDate,
                                                                o.OrderType,
                                                                o.ReceivedDate,
                                                                o.CreatedBy,
                                                                o.ModifiedBy,
                                                                o.CreatedOn,
                                                                o.ModifiedOn)).ToList();
                return oResult as dynamic;
            }
           else return new List<OrdersContract>() as dynamic; 
        }

        public async ValueTask<TResponse> ReadById(Guid contractId)
        {
            var oReadByIdSpec = new ReadOrdersByIdSpecification(contractId);
            var oResult = await _repository.FirstOrDefaultAsync(oReadByIdSpec);
            if (oResult != null)
            {
                var oContract = new OrdersContract(oResult._OrderId,
                                                   oResult.SupplierId,
                                                   oResult.OrderDate,
                                                   oResult.TotalAmount,
                                                   oResult.Status,
                                                   oResult.ExpectedDeliveryDate,
                                                   oResult.OrderType,
                                                   oResult.ReceivedDate,
                                                   oResult.CreatedBy,
                                                   oResult.ModifiedBy,
                                                   oResult.CreatedOn,
                                                   oResult.ModifiedOn);
                return oContract as dynamic;
            }
            else
            {
                throw new ResultException("Order is not found.", (int)HttpStatusCode.NotFound);
            }
        }

        public async ValueTask Update(Guid contractId, TRequest contract)
        {
            var oContract = contract as OrdersOperationContract;
            var oReadByIdSpec = new ReadOrdersByIdSpecification(contractId);
            var oData = await _repository.FirstOrDefaultAsync(oReadByIdSpec);

            if (oData != null)
            {
                // Functional update in Entities
                oData.UpdateOrder(oContract!);
                await _repository.UpdateAsync(oData);
            }
            else
            {
                throw new ResultException("Order is not found.", (int)HttpStatusCode.NotFound);
            }
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
