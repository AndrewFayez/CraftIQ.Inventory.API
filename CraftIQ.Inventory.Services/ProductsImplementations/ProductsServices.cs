using CraftIQ.Inventory.Core.Entities;
using CraftIQ.Inventory.Core.Entities.Categories;
using CraftIQ.Inventory.Core.Entities.Categories.Specifications;
using CraftIQ.Inventory.Core.Entities.Inventories.Specifications;
using CraftIQ.Inventory.Core.Entities.Products.Specifications;
using CraftIQ.Inventory.Core.Entities.Transactions;
using CraftIQ.Inventory.Core.Entities.Transactions.Specifications;
using CraftIQ.Inventory.Core.Interfaces;
using CraftIQ.Inventory.Shared.Contracts.Categories;
using CraftIQ.Inventory.Shared.Contracts.Inventories;
using CraftIQ.Inventory.Shared.Contracts.Products;
using huzcodes.Extensions.Exceptions;
using huzcodes.Persistence.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CraftIQ.Inventory.Services.ProductsImplementations
{
    public class ProductsServices<TRequest, TResponse>
                                 (IRepository<Product> productRepository,
                                  IRepository<Category> categoryRepository,
                                  IRepository<Core.Entities.Inventories.Inventory> inventoryRepository,
                                  IRepository<Transaction> transactionRepository
                                    ) : IGenericServices<TRequest, TResponse>
    {

        private readonly IRepository<Product> _productRepository = productRepository;
        private readonly IRepository<Category> _categoryRepository = categoryRepository;
        private readonly IRepository<Core.Entities.Inventories.Inventory> _inventoryRepository = inventoryRepository; 
        private readonly IRepository<Transaction> _transactionRepository = transactionRepository;
        public async ValueTask<TRequest> Create(TRequest contract)
        {
            var oContract = contract as ProductsOperationsContract;

            // Categort
            var oCategoryReaByIdSpec = new ReadCategoriesByIdSpecification(oContract!._CategoryId);
            var oCategory = await _categoryRepository.FirstOrDefaultAsync(oCategoryReaByIdSpec);
            if (oCategory == null)
                throw new ResultException("Category not found", (int)HttpStatusCode.NotFound);

            /// Inventory
            var oInventoryReaByIdSpec = new ReadInventoriesByIdSpecification(oContract!._InventoryId);
            var oInventory = await _inventoryRepository.FirstOrDefaultAsync(oInventoryReaByIdSpec);
            if (oInventory == null)
                throw new ResultException("Inventory not found you can't add product", (int)HttpStatusCode.NotFound);

            /// Transaction
            var oTransactionReaByIdSpec = new ReadTransactionsByIdSpecification(oContract!._TransactionId);
            var oTransaction = await _transactionRepository.FirstOrDefaultAsync(oTransactionReaByIdSpec);
            if (oTransaction == null)
                throw new ResultException("you must start a transaction before you add product!", (int)HttpStatusCode.NotFound);




            var oProducts = new Product(oContract.Name,
                                          oContract.Description,
                                          oContract.UnitPrice,
                                          oContract.Weight,
                                          oContract.Length,
                                          oContract.Width,
                                          oContract.Height,
                                          oContract.TaxCost,
                                          oContract.ProfitPerUnit,
                                          oContract.ProductionCost);

            oProducts.SetCategory(oCategory);

            oProducts.SetTransaction(oTransaction);

            oProducts.SetInventory(oInventory);

            var oProductResult = await _productRepository.AddAsync(oProducts);

            if (oProductResult == null)
                return default!;

            oContract._ProductId = oProductResult._ProductId;

            return oContract as dynamic;


        }

        public async ValueTask Delete(Guid contractId)
        {
            var oReadByIdSpec = new ReadProductsByIdSpecification(contractId);
            var oResult = await _productRepository.FirstOrDefaultAsync(oReadByIdSpec);
            if (oResult != null)
                await _productRepository.DeleteAsync(oResult);

            else
                throw new ResultException("Product not found", (int)HttpStatusCode.Forbidden);
        }

        public async ValueTask<List<TResponse>> Read()
        {
            var oReadAllSpec = new ReadProductsSpecification();
            var oData = await _productRepository.ListAsync(oReadAllSpec);

            if (oData != null && oData.Count > 0)
            {
                var oResult = oData.Select(o => new ProductsContract(o._ProductId,
                                                                     o.Category._CategoryId,
                                                                     o.Inventory._InventoryId,
                                                                     o.Transaction._TransactionId,
                                                                     o.Name,
                                                                     o.Description,
                                                                     o.UnitPrice,
                                                                     o.Weight,
                                                                     o.Length,
                                                                     o.Width,
                                                                     o.Height,
                                                                     o.TaxCost,
                                                                     o.ProfitPerUnit,
                                                                     o.ProductionCost,
                                                                     o.CreatedBy,
                                                                     o.ModifiedBy,
                                                                     o.CreatedOn,
                                                                     o.ModifiedOn,
                                                                     o.Inventory.Quantity,
                                                                     o.Inventory.Reorderlevel)).ToList();
                return oResult as dynamic;
            }
            else
                return new List<CategoriesContract>() as dynamic;

        }


        public async ValueTask<TResponse> ReadById(Guid contractId)
        {
            var oReadByIdSpec = new ReadProductsByIdSpecification(contractId);
            var oResult = await _productRepository.FirstOrDefaultAsync(oReadByIdSpec);
            if (oResult != null)
                return new ProductsContract(oResult._ProductId,
                                            oResult.Category._CategoryId,
                                            oResult.Inventory._InventoryId,
                                            oResult.Transaction._TransactionId,
                                            oResult.Name,
                                            oResult.Description,
                                            oResult.UnitPrice,
                                            oResult.Weight,
                                            oResult.Length,
                                            oResult.Width,
                                            oResult.Height,
                                            oResult.TaxCost,
                                            oResult.ProfitPerUnit,
                                            oResult.ProductionCost,
                                            oResult.CreatedBy,
                                            oResult.ModifiedBy,
                                            oResult.CreatedOn,
                                            oResult.ModifiedOn,
                                            oResult.Inventory.Quantity,
                                            oResult.Inventory.Reorderlevel
                                             ) as dynamic;

            else
                throw new ResultException("Category not found", (int)HttpStatusCode.NotFound);
        }



        public async ValueTask Update(Guid contractId, TRequest contract)
        {
            var oContract = contract as ProductsOperationsContract;
            var oReadByIdSpec = new ReadProductsByIdSpecification(contractId);
            var oResult = await _productRepository.FirstOrDefaultAsync(oReadByIdSpec);
            if (oResult != null)
            {
                oResult.UpdateProduct(oContract);

                await _productRepository.UpdateAsync(oResult);
            }
            else
                throw new ResultException("Product not found", (int)HttpStatusCode.NotFound);
        }


        public async ValueTask<List<TResponse>> ReadByParentId(Guid parentContractId)
        {
            var oReadCategoriesByIdSpecification = new ReadProductsByCategoryIdSpecification(parentContractId);
            var oCategoryResult = await _categoryRepository.FirstOrDefaultAsync(oReadCategoriesByIdSpecification);
            if (oCategoryResult != null)
            {
                var oProduct = oCategoryResult.Products;
                var oResult = oProduct.Select(o => new ProductsContract(o._ProductId,
                                                                        oCategoryResult._CategoryId,
                                                                        Guid.Empty,
                                                                        Guid.Empty,
                                                                        o.Name,
                                                                        o.Description,
                                                                        o.UnitPrice,
                                                                        o.Weight,
                                                                        o.Length,
                                                                        o.Width,
                                                                        o.Height,
                                                                        o.TaxCost,
                                                                        o.ProfitPerUnit,
                                                                        o.ProductionCost
                                                                        )).ToList();
                return oResult as dynamic;
            }

            return new List<ProductsContract>() as dynamic;
        }


        public async ValueTask<TResponse> ReadSingleByParentId(Guid contractId, Guid parentContractId)
        {
            var oReadSingleProductByCategoryIdSpecification = new ReadSingleProductByCategoryIdSpecification(contractId, parentContractId);
            var oCategoryResult = await _categoryRepository.FirstOrDefaultAsync(oReadSingleProductByCategoryIdSpecification);
            if (oCategoryResult != null)
            {
                if (oCategoryResult.Products == null || oCategoryResult.Products.Count == 0)
                    throw new ResultException("This object is not exit", (int)HttpStatusCode.NotFound);


                var oProduct = oCategoryResult.Products.FirstOrDefault();
                var oResult =   new ProductsContract(oProduct._ProductId,
                                                    oCategoryResult._CategoryId,
                                                    Guid.Empty,
                                                    Guid.Empty,
                                                    oProduct.Name,
                                                    oProduct.Description,
                                                    oProduct.UnitPrice,
                                                    oProduct.Weight,
                                                    oProduct.Length,
                                                    oProduct.Width,
                                                    oProduct.Height,
                                                    oProduct.TaxCost,
                                                    oProduct.ProfitPerUnit,
                                                    oProduct.ProductionCost
                                                        ) ;
                return oResult as dynamic;
            }

            return default!;
        }


        public async ValueTask UpdateParentId(Guid contractId, Guid parentContractId)
        {
            var oReadByIdSpec = new ReadProductsByIdSpecification(contractId);
            var oResult = await _productRepository.FirstOrDefaultAsync(oReadByIdSpec);
            if (oResult == null)
                throw new ResultException("This product object is not exit", (int)HttpStatusCode.NotFound);

            var oReadParentByIdSpec = new ReadCategoriesByIdSpecification(parentContractId);
            var oParentResult = await _categoryRepository.FirstOrDefaultAsync(oReadParentByIdSpec);
            if (oParentResult == null)
                throw new ResultException("This category object is not exit", (int)HttpStatusCode.NotFound);

            oResult.SetCategory(oParentResult);
            await _productRepository.UpdateAsync(oResult);
        }
    }
}
