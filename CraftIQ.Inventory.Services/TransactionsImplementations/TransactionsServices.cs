using CraftIQ.Inventory.Core.Entities.Transactions;
using CraftIQ.Inventory.Core.Entities.Transactions.Specifications;
using CraftIQ.Inventory.Core.Interfaces;
using CraftIQ.Inventory.Shared.Contracts.Transactions;
using huzcodes.Extensions.Exceptions;
using huzcodes.Persistence.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CraftIQ.Inventory.Services.TransactionsImplementations
{
    public class TransactionsServices<TRequest, TResponse> (IRepository<Transaction> repositories) : IGenericServices<TRequest, TResponse>
    {
          readonly IRepository<Transaction> _repositories = repositories;

        public async ValueTask<TRequest> Create(TRequest contract)
        {
            var oContract = contract as TransactionsOperationsContract;

            var oData = new Transaction(oContract.EmployeeId,
                                        oContract.TransactionDate,
                                        oContract.TransactionType,
                                        oContract.Quantity,
                                        oContract.Notes);

            var oResult = await _repositories.AddAsync(oData);

            if (oResult == null)
                return default!;

            oContract._TransactionId = oResult._TransactionId;
            return oContract as dynamic;
        }

        public async ValueTask Delete(Guid contractId)
        {
            var oReadByIdSpec = new ReadTransactionsByIdSpecification(contractId);
            var oResult = await _repositories.FirstOrDefaultAsync(oReadByIdSpec);
            if (oResult != null)
                await _repositories.DeleteAsync(oResult);
            else
                throw new ResultException("You can't delete Transaction that is not exit.", (int)HttpStatusCode.Forbidden);

        }

        public async ValueTask<List<TResponse>> Read()
        {
            var oReadByIdSpec = new ReadTransactionsSpecification();
            var oResult = await _repositories.ListAsync(oReadByIdSpec);
            if (oResult != null && oResult.Count() > 0)
            {
                var oData = oResult.Select(o => new TransactionsContract(o._TransactionId,
                                                                         o.EmployeeId,
                                                                         o.TransactionDate,
                                                                         o.TransactionType,
                                                                         o.Quantity,
                                                                         o.Notes,
                                                                         o.CreatedBy,
                                                                         o.ModifiedBy,
                                                                         o.CreatedOn,
                                                                         o.ModifiedOn)).ToList();

                return oData as dynamic;
            }

            else return new List<TransactionsContract>() as dynamic;
        }

        public async ValueTask<TResponse> ReadById(Guid contractId)
        {
            var oReadByIdSpec = new ReadTransactionsByIdSpecification(contractId);
            var oResult = await _repositories.FirstOrDefaultAsync(oReadByIdSpec);
            if (oResult != null)
               {
                    var oData = new TransactionsContract(oResult._TransactionId,
                                                         oResult.EmployeeId,
                                                         oResult.TransactionDate,
                                                         oResult.TransactionType,
                                                         oResult.Quantity,
                                                         oResult.Notes,
                                                         oResult.CreatedBy,
                                                         oResult.ModifiedBy,
                                                         oResult.CreatedOn,
                                                         oResult.ModifiedOn);
                    return oData as dynamic;
                }
            throw new ResultException("This object does not exist", (int)HttpStatusCode.NotFound);
        }

        public async ValueTask Update(Guid contractId, TRequest contract)
        {
            var oContract = contract as TransactionsOperationsContract;
            var oReadByIdSpec = new ReadTransactionsByIdSpecification(contractId);
            var oResult = await _repositories.FirstOrDefaultAsync(oReadByIdSpec);
            if (oResult != null)
            {
                oResult.UpdateTransaction(oContract!);
                await _repositories.UpdateAsync(oResult);
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
